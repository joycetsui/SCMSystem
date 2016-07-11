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
    public class ExpensesController : ApiController
    {
        Expense[] expenses = External.getExpenses();

        // GET: Expenses
        public Expense[] Get()
        {
            return expenses;
        }

        public Expense Get(string id)
        {
            Expense expense;

            if (id == "procurement_cost")
            {
                expense = External.getProcurementCost();
            }
            else if (id == "transportation_cost")
            {
                expense = External.getTransportationCost();
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No HTTP resource was found that matches the request URI: " + id)),
                };

                throw new HttpResponseException(resp);
            }

            return expense;
        }
    }
}