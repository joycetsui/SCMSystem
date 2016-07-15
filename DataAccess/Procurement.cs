using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public static class Procurement
    {
        public static int addNewProcurementOrder(int supplierId, int destinationSiteId, int rawMaterialId, int quantity)
        {
            string query = "insert into [Procurement Orders]([Supplier ID],[Destination Site ID], [Raw Material ID],[Quantity]) " +
                            "values( @supplierId, @destinationSiteId, @rawMaterialId, @quantity) " +
                            "SELECT scope_identity();";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("supplierId", supplierId));
            pars.Add(new SqlParameter("destinationSiteId", destinationSiteId));
            pars.Add(new SqlParameter("rawMaterialId", rawMaterialId));
            pars.Add(new SqlParameter("quantity", quantity));

            int id = Database.executeInsertQueryAndReturnId(query, pars);

            return id;
        }

        public static double getProcurementCost()
        {
            string procurementOrderQuery = "SELECT Sum([Order Cost]) as [Total Cost] FROM " +
                                            "(SELECT p.[Procurement Order ID], p.[Raw Material ID], r.[Type], r.[Unit Cost], p.[Quantity], p.[Quantity] * r.[Unit Cost] AS [Order Cost] " +
                                            "FROM [Procurement Orders] as p INNER JOIN [Raw Materials] as r " +
                                            "ON r.[Raw Material ID] = p.[Raw Material ID]) subq;";

            DataTable dt = Database.executeSelectQuery(procurementOrderQuery);
            double procurementCost = 0;
            if (dt.Rows.Count != 0 && dt.Rows[0]["Total Cost"].ToString() != "")
            {
                Double.TryParse(dt.Rows[0]["Total Cost"].ToString(), out procurementCost);
            }

            return procurementCost;
        }
    }
}
