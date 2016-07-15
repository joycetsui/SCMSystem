using DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cs490_scm_API.Controllers
{
    public class InventoryController : ApiController
    {
        // GET: Inventory
        
        [System.Web.Http.Route("api/inventory/raw_materials")]
        public string Get()
        {
            DataTable dt = new DataTable();
            dt = Inventory.getRawMaterials();

            string JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        [System.Web.Http.Route("api/inventory/raw_materials/{id}")]
        public string Get(int id)
        {
            DataTable dt = new DataTable();
            dt = Inventory.getRawMaterialById(id);

            if (dt.Rows.Count == 0)
            {
                string msg = "No Raw Material Found for ID: " + id;
                string reason = "Invalid ID";
                throwError(msg, reason);
            }

            string JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        [System.Web.Http.Route("api/inventory/update_raw_materials/{id}/{siteId}/{amountUsed}")]
        public HttpResponseMessage Put(int id, int siteId, int amountUsed)
        {
            int newAmount = calculateNewRawMaterialAmountForId(id, siteId, amountUsed);
            Inventory.updateRawMaterialAmountForId(id, siteId, newAmount);

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
                Inventory.updateRawMaterialAmountForId(id, siteId, newAmount);
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
                try {
                    orderId = Procurement.addNewProcurementOrder(supplierId, destinationSiteId, rawMaterialId, amount);
                }
                catch (Exception e)
                {
                    throwError(e.Message, e.Source);
                }
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
        private static void throwError(string message, string reason)
        {
            var error = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(string.Format(message)),
                ReasonPhrase = reason,
            };

            throw new HttpResponseException(error);
        }

        public static int calculateNewRawMaterialAmountForId(int id, int siteId, int amountUsedUp)
        {
            int currentAmount = 0;

            DataTable dt = Inventory.getRawMaterialByIdAndSiteId(id, siteId);

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
    }
}