using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Analytics
    {
        public static DataTable getReports()
        {
            string query = "SELECT FORMAT([Date],'D') as [Date], * FROM [Analytics];";
            return Database.executeSelectQuery(query);
        }

        public static void createNewReport()
        {
            int supplierResponseTime = 3;
            int productionTime = 2;
            int orderFullfillmentTime = 6;
            int SCMCost = 2000;

            string query = "insert into [Analytics] ([Date],[Supplier Response Time], [Production Time],[Order Fullfillment Time], [Supply Chain Cost]) " +
                            "values( GETDATE(), @supplierResponseTime, @productionTime, @orderFullfillmentTime, @SCMCost);";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("supplierResponseTime", supplierResponseTime));
            pars.Add(new SqlParameter("productionTime", productionTime));
            pars.Add(new SqlParameter("orderFullfillmentTime", orderFullfillmentTime));
            pars.Add(new SqlParameter("SCMCost", SCMCost));

            Database.executeInsertUpdateQuery(query, pars);
        }
    }
}
