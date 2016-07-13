using cs490_scm_API.Models;
using cs490_scm_API.Providers;
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
        Expense[] expenses = getExpenses();

        // GET: Expenses
        [System.Web.Http.Route("api/expenses")]
        public Expense[] Get()
        {
            return expenses;
        }

        [System.Web.Http.Route("api/expenses/transportation_cost")]
        public Expense GetTransportationCost()
        {
            return getTransportationCost();
        }

        [System.Web.Http.Route("api/expenses/procurement_cost")]
        public Expense GetProcurementCost()
        {
            return getProcurementCost();
        }

        // Helper methods
        static private Expense[] getExpenses()
        {
            string procurementOrderQuery = "SELECT Sum([Order Cost]) as [Total Cost] FROM " +
                                            "(SELECT p.[Procurement Order ID], p.[Raw Material ID], r.[Type], r.[Unit Cost], p.[Quantity], p.[Quantity] * r.[Unit Cost] AS[Order Cost] " +
                                            "FROM[Procurement Orders] as p INNER JOIN[Raw Materials] as r " +
                                            "ON r.[Raw Material ID] = p.[Raw Material ID]);";

            DataTable dt = ExternalService.executeSelectQuery(procurementOrderQuery);
            int procurementCost = int.Parse(dt.Rows[0][0].ToString());

            string warehouseRentQuery = "SELECT Sum([Rent Cost]) as [Total Cost] " +
                                        "FROM[Warehouse];";
            dt = ExternalService.executeSelectQuery(warehouseRentQuery);
            int rentCost = int.Parse(dt.Rows[0][0].ToString());

            string transportationCostQuery = "SELECT Sum(it.[Cost]) +  Sum(cs.[Shipping Cost]) as [Total Cost] " +
                                        "FROM[Internal Transfers] as it, [Customer Shipping] as cs;";
            dt = ExternalService.executeSelectQuery(transportationCostQuery);
            int transportationCost = 0;
            if (dt.Rows[0][0].ToString() != "")
            {
                transportationCost = int.Parse(dt.Rows[0][0].ToString());
            }

            Expense[] expenses = new[]
            {
                new Expense { Name = "Procurement Cost", amount = procurementCost },
                new Expense {Name = "Transportation Cost", amount = transportationCost },
                new Expense {Name = "Warehouse Rent Cost", amount = rentCost },
            };

            return expenses;
        }
        public Expense getProcurementCost()
        {
            for (int i = 0; i < expenses.Length; i++)
            {
                if (expenses[i].Name == "Procurement Cost")
                {
                    return expenses[i];
                }
            }

            return new Expense { Name = "Invalid", amount = 0 };
        }

        public Expense getTransportationCost()
        {
            for (int i = 0; i < expenses.Length; i++)
            {
                if (expenses[i].Name == "Transportation Cost")
                {
                    return expenses[i];
                }
            }

            return new Expense { Name = "Invalid", amount = 0 };
        }
    }
}