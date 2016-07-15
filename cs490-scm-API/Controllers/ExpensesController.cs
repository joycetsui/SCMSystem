using DataAccess;
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
    public class ExpensesController : ApiController
    {

        // GET: Expenses
        [System.Web.Http.Route("api/expenses")]
        public Expense[] Get()
        {
            return getExpenses();
        }

        [System.Web.Http.Route("api/expenses/transportation_cost")]
        public Expense GetTransportationCost()
        {
            return getTransportationExpense();
        }

        [System.Web.Http.Route("api/expenses/procurement_cost")]
        public Expense GetProcurementCost()
        {
            return getProcurementExpense();
        }

        // Helper methods
        static private Expense[] getExpenses()
        {
            Expense procurement = getProcurementExpense();
            Expense transportation = getTransportationExpense();
            Expense rent = new Expense { Name = "Warehouse Rent Cost", amount = Inventory.getWarehouseRent() };

            Expense[] expenses = new[] { procurement, transportation, rent};

            return expenses;
        }
        public static Expense getProcurementExpense()
        {
            return new Expense { Name = "Procurement Cost", amount = Procurement.getProcurementCost() };
        }

        public static Expense getTransportationExpense()
        {
            return new Expense { Name = "Transportation Cost", amount = Transportation.getTransportationCost() };
        }
    }
}