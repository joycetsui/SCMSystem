using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            query = "SELECT po.[Procurement Order ID] as [Order ID],  r.[Type] as [Raw Material], s.[Company Name] as [Supplier], s.[Location] as [Address], s.[Payment Details], po.[Quantity] as [Quantity], (r.[Unit Cost] * po.[Quantity]) as [Total Cost] " +
                    "FROM[Procurement Orders] as po " +
                    "INNER JOIN[Raw Materials] as r ON po.[Raw Material ID] = r.[Raw Material ID] " +
                    "INNER JOIN[Suppliers] as s ON po.[Supplier ID] = s.[Supplier ID] " +
                    "WHERE po.[Procurement Order ID] = @id; ";

            List<SqlParameter> pars2 = new List<SqlParameter>();
            pars2.Add(new SqlParameter("id", id));

            DataTable dt = Database.executeSelectQuery(query, pars2);

            sendPurchaseOrderToAIS(dt.Rows[0]["Raw Material"].ToString(), dt.Rows[0]["Supplier"].ToString(), dt.Rows[0]["Address"].ToString(), dt.Rows[0]["Payment Details"].ToString(), int.Parse(dt.Rows[0]["Quantity"].ToString()), Double.Parse(dt.Rows[0]["Total Cost"].ToString()));

            return id;
        }

        private static void sendPurchaseOrderToAIS(string rawMaterial, string supplierName, string supplierAddress, string supplierPaymentDetails, int quantity, double totalCost)
        {
            Payee_details details = new Payee_details { name = supplierName, address = supplierAddress, paymentDetails = supplierPaymentDetails };

            AISRequestObject obj = new AISRequestObject();
            obj.team_name = "scm";
            obj.items_bought = new List<KeyValuePair<string, int>>
            {
                new KeyValuePair<string, int>(rawMaterial, quantity),
            };
            obj.payee_details = details;
            obj.value = totalCost;

            string json = JsonConvert.SerializeObject(obj);
            Console.WriteLine(json);

            string url = "http://cs490ais.herokuapp.com/api/purchase";
            string result = "";

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                try
                {
                    result = client.UploadString(url, "POST", json);
                }
                catch { }
            }

            Console.WriteLine(result);
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

        public static DataTable getProcurementForecasts()
        {
            string query = "SELECT pf.[Forecast Year] as [Year], pf.[Week Number], r.[Type] as [Raw Material], pf.[Quantity] " +
                            "FROM [Procurement Forecast] as pf " +
                            "INNER JOIN [Raw Materials] as r ON pf.[Raw Material ID] = r.[Raw Material ID] " +
                            "ORDER BY pf.[Forecast Year], pf.[Week Number]";

            return Database.executeSelectQuery(query);
        }

        public static DataTable getProcurementForecastsForWeekRange(int startWeek, int endWeek)
        {
            string query = "SELECT * " +
                            "FROM [Procurement Forecast]" +
                            "WHERE [Week Number] >= @startWeek AND [Week Number] <= @endWeek";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("startWeek", startWeek));
            pars.Add(new SqlParameter("endWeek", endWeek));

            return Database.executeSelectQuery(query, pars);
        }

        public static void deleteProcurementForecastsForWeekRange(int startWeek, int endWeek)
        {
            string query = "Delete FROM [Procurement Forecast]" +
                            "WHERE [Week Number] >= @startWeek AND [Week Number] <= @endWeek";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("startWeek", startWeek));
            pars.Add(new SqlParameter("endWeek", endWeek));

            Database.executeInsertUpdateQuery(query, pars);
        }

        static public ProcurementForecastItem[] getNewForecasts()
        {
            ProcurementForecastItem[] ProcurementForecasts = new[]
            {
                new ProcurementForecastItem { year = 2016, week = 29, rawMaterialId = 3, quantity = 30},
                new ProcurementForecastItem { year = 2016, week = 30, rawMaterialId = 1, quantity = 100},
                new ProcurementForecastItem { year = 2016, week = 31, rawMaterialId = 2, quantity = 42},
                new ProcurementForecastItem { year = 2016, week = 32, rawMaterialId = 3, quantity = 13},
                new ProcurementForecastItem { year = 2016, week = 33, rawMaterialId = 1, quantity = 44},
                new ProcurementForecastItem { year = 2016, week = 34, rawMaterialId = 1, quantity = 445},
            };

            return ProcurementForecasts;
        }

        public static void addNewProcurementForecast(int year, int week, int rawMaterialId, int quantity)
        {
            string query = "insert into [Procurement Forecast]([Forecast Year],[Week Number], [Raw Material ID],[Quantity]) " +
                            "values( @year, @week, @rawMaterialId, @quantity);";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("year", year));
            pars.Add(new SqlParameter("week", week));
            pars.Add(new SqlParameter("rawMaterialId", rawMaterialId));
            pars.Add(new SqlParameter("quantity", quantity));

            Database.executeInsertUpdateQuery(query, pars);
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

        public static int getSupplierIdByRawMaterialId(int rawMaterialId)
        {
            string query = "select [Supplier ID] from [Raw Materials] where [Raw Material ID]= @rawMaterialId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("rawMaterialId", rawMaterialId));

            DataTable dt = Database.executeSelectQuery(query, pars);

            return int.Parse(dt.Rows[0][0].ToString());
        }
    }
}
