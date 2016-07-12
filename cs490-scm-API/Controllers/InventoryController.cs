using Newtonsoft.Json.Linq;
using SCM_Desktop_Application;
using System;
using System.Collections.Generic;
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
        public InventoryItem[] Get()
        {
            return Database.RawMaterialsInventory.ToArray();
        }

        [System.Web.Http.Route("api/inventory/raw_materials/{id}")]
        public External.InventoryResp Get(int id)
        {
            External.InventoryResp inventoryResp = External.getRawMaterialsOnHandForId(id);
            if (inventoryResp.ItemName == "Invalid")
            {
                var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Raw Material Found for ID: " + id)),
                    ReasonPhrase = "Invalid ID",
                };

                throw new HttpResponseException(error);
            }

            return inventoryResp;
        }

        [System.Web.Http.Route("api/inventory/update_raw_materials/{id}/{amountUsed}")]
        public HttpResponseMessage Put(int id, int amountUsed)
        {
            int newAmount = calculateNewRawMaterialAmountForId(id, amountUsed);
            External.updateRawMaterialsOnHandForId(id, newAmount);

            External.InventoryResp newRawMaterialItem = Get(id);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Format("Raw Material Amount was updated for ID: " + id + ". New amount is " + newAmount)),
                ReasonPhrase = "Success",
            };

            return response;
        }

        [System.Web.Http.Route("api/inventory/update_raw_materials/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] JObject data)
        {
            int newAmount = 0;
            dynamic json = data;

            int amountUsedUp = json.amount_used_up;
            int rawMaterialId = json.raw_material_id;

            if (rawMaterialId == id)
            {
                newAmount = calculateNewRawMaterialAmountForId(id, amountUsedUp);
                External.updateRawMaterialsOnHandForId(id, newAmount);

                External.InventoryResp newRawMaterialItem = Get(id);
            }
            else
            {
                var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Raw Material ID: " + id + " given in the URI does not match Raw Material ID: " + rawMaterialId + " given in the body request.")),
                    ReasonPhrase = "Invalid ID",
                };

                throw new HttpResponseException(error);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Format("Raw Material Amount was updated for ID: " + id + ". New amount is " + newAmount)),
                ReasonPhrase = "Success",
            };

            return response;
        }

        private int calculateNewRawMaterialAmountForId(int id, int amountUsedUp)
        {
            int currentAmount = 0;

            if (id < Database.RawMaterialsInventory.Count)
            {
                currentAmount = Database.RawMaterialsInventory[id].unitsOnHand;
            }
            else
            {
                var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Raw Material Found for ID: " + id)),
                    ReasonPhrase = "Invalid ID",
                };

                throw new HttpResponseException(error);
            }

            int newAmount = currentAmount - amountUsedUp;

            if (newAmount < 0)
            {
                newAmount = 0;
            }

            return newAmount;
        }

        // POST api/inventory
        [System.Web.Http.Route("api/inventory/new_procurement_order")]
        public HttpResponseMessage Post([FromBody] JObject data)
        {
            dynamic json = data;

            int supplierId = 0;
            int destinationSiteId = json.destination_site_id ?? -1;
            int rawMaterialId = json.raw_material_id ?? -1;
            int amount = json.buy_amount ?? -1;
            int orderId = -1;

            if (supplierId != -1 && destinationSiteId != -1 && rawMaterialId != -1 && amount > 0)
            {
                orderId = Database.ProcurementOrders.Count;
                External.addNewProcurementOrder(supplierId, destinationSiteId, rawMaterialId, amount);
            }
            else
            {
                var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Missing or Invalid Order Parameters")),
                    ReasonPhrase = "Invalid Order",
                };

                throw new HttpResponseException(error);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(string.Format("New procurement order was created: [OrderID: " + orderId + ", Raw Material ID: " + rawMaterialId + ", Amount: " + amount + "]")),
                ReasonPhrase = "Success",
            };

            return response;
        }
    }
}