using cs490_scm_API.Models;
using cs490_scm_API.Providers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace cs490_scm_API.Controllers
{
    public class InventoryController : ApiController
    {
        // GET: Inventory
        [System.Web.Http.Route("api/inventory/raw_materials")]
        public DataTable Get()
        {
            string query = "SELECT inv.[Raw Material ID] as [Raw Material ID],  r.[Type] as [Type], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM (([Raw Material Inventory] as inv " +
                            "INNER JOIN [Raw Materials] as r ON inv.[Raw Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID]);";
            DataTable dt = ExternalService.executeSelectQuery(query);
            return dt;
        }

        [System.Web.Http.Route("api/inventory/raw_materials/{id}")]
        public DataTable Get(int id)
        {
            string query = "SELECT inv.[Raw Material ID] as [Raw Material ID],  r.[Type] as [Type], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM(([Raw Material Inventory]  inv " +
                            "INNER JOIN[Raw Materials]  r ON inv.[Raw Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN[Warehouse]  w ON inv.[Site ID] = w.[Site ID]) " +
                            "WHERE inv.[Raw Material ID] = " + id + ";";

            DataTable dt = ExternalService.executeSelectQuery(query);

            if (dt.Rows.Count == 0)
            {
                string msg = "No Raw Material Found for ID: " + id;
                string reason = "Invalid ID";
                throwError(msg, reason);
            }

            return dt;
        }

        /// <summary>
        /// Reduces the current amount for the given raw material id by the amount that was used up.
        /// </summary>
        /// <param name="id">The ID of the raw material.</param>
        /// <param name="amountUsed">The amount that will be deducted from the current amount.</param>
        /// 
        [System.Web.Http.Route("api/inventory/update_raw_materials/{id}/{siteId}/{amountUsed}")]
        public HttpResponseMessage Put(int id, int siteId, int amountUsed)
        {
            int newAmount = calculateNewRawMaterialAmountForId(id, siteId, amountUsed);
 
            string query = "update [Raw Material Inventory] set [Units] ='" + newAmount + "' where [Raw Material ID]=" + id + " AND [Site ID] = " + siteId + ";";

            ExternalService.executeInsertUpdateQuery(query);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Format("Raw Material Amount was updated for [ID: " + id + "]. New amount is " + newAmount)),
                ReasonPhrase = "Success",
            };

            return response;
        }

        [System.Web.Http.Route("api/inventory/update_raw_materials/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] JObject data)
        {
            int newAmount = 0;
            dynamic json = data;

            int amountUsedUp = json.amount_used_up ?? -1;
            int rawMaterialId = json.raw_material_id ?? -1;
            int siteId = json.site_id ?? -1;

            if (rawMaterialId == id && amountUsedUp != -1 && rawMaterialId != -1 && siteId != -1)
            {
                newAmount = calculateNewRawMaterialAmountForId(id, siteId, amountUsedUp);

                string query = "update [Raw Material Inventory] set [Units] ='" + newAmount + "' where [Raw Material ID]=" + id + " AND [Site ID] = " + siteId + ";";
                ExternalService.executeInsertUpdateQuery(query);
            }
            else if (rawMaterialId == id)
            {
                string msg = "Missing parameters in the body request.";
                string reason = "Missing or Invalid Body Params";
                throwError(msg, reason);
            }
            else
            {
                string msg = "Raw Material ID: " + id + " given in the URI does not match Raw Material ID: " + rawMaterialId + " given in the body request.";
                string reason = "Invalid ID";
                throwError(msg, reason);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Format("Raw Material Amount was updated for [ID: " + id + "]. New amount is " + newAmount)),
                ReasonPhrase = "Success",
            };

            return response;
        }

        // POST api/inventory
        [System.Web.Http.Route("api/inventory/new_procurement_order")]
        public HttpResponseMessage Post([FromBody] JObject data)
        {
            dynamic json = data;

            int supplierId = 1;
            int destinationSiteId = json.destination_site_id ?? -1;
            int rawMaterialId = json.raw_material_id ?? -1;
            int amount = json.buy_amount ?? -1;
            int orderId = -1;

            if (supplierId != -1 && destinationSiteId != -1 && rawMaterialId != -1 && amount > 0)
            {
                addNewProcurementOrder(supplierId, destinationSiteId, rawMaterialId, amount);
            }
            else
            {
                string msg = "Missing or Invalid Order Parameters";
                string reason = "Invalid Order";
                throwError(msg, reason);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Format("New procurement order was created: [OrderID: " + orderId + ", Raw Material ID: " + rawMaterialId + ", Amount: " + amount + "]")),
                ReasonPhrase = "Success",
            };

            return response;
        }

        // Helper Methods
        private void throwError(string message, string reason)
        {
            var error = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(string.Format(message)),
                ReasonPhrase = reason,
            };

            throw new HttpResponseException(error);
        }

        private int calculateNewRawMaterialAmountForId(int id, int siteId, int amountUsedUp)
        {
            int currentAmount = 0;

            string query = "SELECT [Units] FROM [Raw Material Inventory] " +
                           "WHERE [Raw Material ID] = " + id + " AND [Site ID] = " + siteId + ";";

            DataTable dt = ExternalService.executeSelectQuery(query);
            if (dt.Rows.Count == 0)
            {
                string msg = "No Raw Material Found for ID: " + id + " at site ID: " + siteId;
                string reason = "Invalid ID";
                throwError(msg, reason);
            }

            currentAmount = int.Parse(dt.Rows[0]["Units"].ToString());
            int newAmount = currentAmount - amountUsedUp;
            if (newAmount < 0)
            {
                newAmount = 0;
            }

            return newAmount;
        }

        public void addNewProcurementOrder(int supplierId, int destinationSiteId, int rawMaterialId, int quantity)
        {
            string query = "insert into [Procurement Orders]([Supplier ID],[Destination Site ID], [Raw Material ID],[Quantity]) values('" + supplierId + "','" + destinationSiteId + "','" + rawMaterialId + "','" + quantity + "')";
            try
            {
                ExternalService.executeInsertUpdateQuery(query);
            }
            catch (Exception e)
            {
                throwError(e.Message, e.Source);
            }
        }
    }
}