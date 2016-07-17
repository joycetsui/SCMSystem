using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public static class Procurement
    {
        public static DataTable getProcurementOrders()
        {
            string query = "SELECT po.[Procurement Order ID] as [Order ID],  r.[Type] as [Raw Material], s.[Company Name] as [Supplier], w.[Name] as [Site], po.[Quantity] as [Quantity], po.[Actual Arrival Date] as [Actual Arrival Date], po.[Expected Arrival Date] as [Expected Arrival Date], (r.[Unit Cost] * po.[Quantity]) as [Total Cost] " +
                            "FROM ((([Procurement Orders] as po " +
                            "INNER JOIN [Raw Materials] as r ON po.[Raw Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN [Warehouse] as w ON po.[Destination Site ID] = w.[Site ID]) " +
                            "INNER JOIN [Suppliers] as s ON po.[Supplier ID] = s.[Supplier ID]);";

            return Database.executeSelectQuery(query);
        }

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

        public static void updateProcurementOrder(int orderId, int supplierId, int destinationId, int rawMaterialId, int quantity)
        {
            string query = "update [Procurement Orders] set [Supplier ID] = @supplierId, [Destination Site ID] = @destinationId, [Raw Material ID] = @rawMaterialId, [Quantity] = @quantity " +
                            "where [Procurement Order ID]= @orderId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("supplierId", supplierId));
            pars.Add(new SqlParameter("destinationId", destinationId));
            pars.Add(new SqlParameter("rawMaterialId", rawMaterialId));
            pars.Add(new SqlParameter("quantity", quantity));
            pars.Add(new SqlParameter("orderId", orderId));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static void deleteProcurementOrder(int id)
        {
            string query = "delete from [Procurement Orders] where [Procurement Order ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
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

        public static DataTable getSuppliersNames()
        {
            string sql = "select [Company Name] from [Suppliers]";
            return Database.executeSelectQuery(sql);
        }

        public static int getSupplierIdByName(string name)
        {
            string query = "select [Supplier ID] from [Suppliers] where [Company Name]= @name;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("name", name));

            DataTable dt = Database.executeSelectQuery(query, pars);

            return int.Parse(dt.Rows[0][0].ToString());
        }
    }
}
