using cs490_scm_API.Models;
using cs490_scm_API.Providers;
using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace cs490_scm_API.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: Sales
        [System.Web.Http.Route("api/orders/status/{type}")]
        public string Get(string type)
        {
            DataTable dt = new DataTable();

            if (type == "customer")
            {
                dt = ProductOrders.getCustomerOrders();
            }
            else if (type == "retailer")
            {
                dt = ProductOrders.getRetailerOrders();
            }
            else
            {
                string msg = "Missing or Invalid Order Type";
                string reason = "Invalid Order Type";
                throwError(msg, reason);
            }

            string JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        [System.Web.Http.Route("api/orders/status/{type}/{id}")]
        public string Get(string type, int id)
        {
            DataTable dt = new DataTable();

            if (type == "customer")
            {
                dt = ProductOrders.getCustomerOrderById(id);
            }
            else if (type == "retailer")
            {
                dt = ProductOrders.getRetailerOrderById(id);
            }
            else
            {
                string msg = "Missing or Invalid Order Type.";
                string reason = "Invalid Order Type";
                throwError(msg, reason);
            }

            if (dt.Rows.Count == 0)
            {
                string msg = "No " + type + " order found for ID: " + id;
                string reason = "Invalid ID";
                throwError(msg, reason);
            }

            string JSONresult = JsonConvert.SerializeObject(dt.Rows[0].Table);
            return JSONresult;
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
    }
}