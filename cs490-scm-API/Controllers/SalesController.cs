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
    public class OrdersController : ApiController
    {
        // GET: Sales
        [System.Web.Http.Route("api/orders/status/{type}")]
        public Object[] Get(string type)
        {
            if (type == "customer")
            {
                return Database.CustomerShipping.ToArray();
            }
            else if (type == "retailer")
            {
                return Database.DistributorShipping.ToArray();
            }
            else
            {
                var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Missing or Invalid Order Type.")),
                    ReasonPhrase = "Invalid Order Type",
                };

                throw new HttpResponseException(error);
            }
        }

        [System.Web.Http.Route("api/orders/status/{type}/{id}")]
        public External.OrderStatusResp Get(string type, int id)
        {
            External.OrderStatusResp orderStatusResp;

            if (type == "customer")
            {
                orderStatusResp = External.getCustomerOrderStatus(id);
            }

            else if (type == "retailer")
            {
                orderStatusResp = External.getRetailerOrderStatus(id);
            }
            else
            {
                var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Missing or Invalid Order Type.")),
                    ReasonPhrase = "Invalid Order Type",
                };

                throw new HttpResponseException(error);
            }

            if (orderStatusResp.Status == "Invalid")
            {
                var error = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No " + type + " order found for ID: " + id)),
                    ReasonPhrase = "Invalid ID",
                };

                throw new HttpResponseException(error);
            }

            return orderStatusResp;
        }
    }
}