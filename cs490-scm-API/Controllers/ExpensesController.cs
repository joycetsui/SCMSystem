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
        [System.Web.Http.Route("api/expenses")]
        public Expense[] Get()
        {
            return expenses;
        }

        [System.Web.Http.Route("api/expenses/transportation_cost")]
        public Expense GetTransportationCost()
        {
            return External.getTransportationCost();
        }

        [System.Web.Http.Route("api/expenses/procurement_cost")]
        public Expense GetProcurementCost()
        {
            return External.getProcurementCost();
        }
    }
}