using cs490_scm_API.Models;
using cs490_scm_API.Providers;
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
            string query = "";

            if (type == "customer")
            {
                query = "SELECT [Customer Order ID], [Tracking Number], [Status] " +
                        "FROM[Customer Shipping];";
            }
            else if (type == "retailer")
            {
                query = "SELECT [Product Order ID], [Stock Transfer ID], [Status] " +
                        "FROM[Distributor Shipping];";
            }
            else
            {
                string msg = "Missing or Invalid Order Type";
                string reason = "Invalid Order Type";
                throwError(msg, reason);
            }

            DataTable dt = ExternalService.executeSelectQuery(query);

            string JSONresult = JsonConvert.SerializeObject(dt);
            return JSONresult;
        }

        [System.Web.Http.Route("api/orders/status/{type}/{id}")]
        public string Get(string type, int id)
        {
            string query = "";

            if (type == "customer")
            {
                query = "SELECT [Customer Order ID], [Tracking Number], [Status] " +
                        "FROM[Customer Shipping] " +
                        "WHERE[Customer Order ID] = " + id + ";";
            }
            else if (type == "retailer")
            {
                query = "SELECT [Product Order ID], [Stock Transfer ID], [Status] " +
                        "FROM[Distributor Shipping] " +
                        "WHERE[Product Order ID] = " + id + ";";
            }
            else
            {
                string msg = "Missing or Invalid Order Type.";
                string reason = "Invalid Order Type";
                throwError(msg, reason);
            }

            DataTable dt = ExternalService.executeSelectQuery(query);

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