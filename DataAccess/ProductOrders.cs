using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ProductOrders
    {
        //public static SalesCustomer getCustomerInfo()
        //{
        //    string result = "";

        //    using (var client = new WebClient())
        //    {
        //        string url = "http://afternoon-basin-47187.herokuapp.com/sales/customers?salesperson=aaron";

        //        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        //        try
        //        {
        //            result = client.DownloadString(url);
        //        }
        //        catch { }
        //    }

        //    Console.WriteLine(result);

        //    SalesCustomer customerInfo = JsonConvert.DeserializeObject<SalesCustomer>(result);

        //    return customerInfo;
        //}

        public static DataTable getCustomerOrders()
        {
            string query = "SELECT [Customer Order ID], [Tracking Number], cs.[Product Order ID], po.[Customer ID], po.[Type], [Status], sc.[Company Name] as [Shipping Company], po.[Destination], [Date Shipped] " +
                            "FROM [Customer Shipping] as cs " +
                            "INNER JOIN [Shipping Company] as sc ON cs.[Shipping Company ID] = sc.[Shipping Company ID] " +
                            "INNER JOIN [Product Orders] as po ON cs.[Product Order ID] = po.[Product Order ID];";

            return Database.executeSelectQuery(query);
        }

        public static DataTable getCustomerOrderById(int id)
        {
            string query = "SELECT [Customer Order ID], [Tracking Number], [Status] " +
                            "FROM[Customer Shipping] " +
                            "WHERE[Customer Order ID] = @id;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            return Database.executeSelectQuery(query, pars);
        }

        public static void deleteCustomerOrder(int id)
        {
            string query = "delete from [Customer Shipping] where [Customer Order ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static void updateCustomerOrder(int id, int tracking, int shippingCompanyId, string date, string status)
        {
            string query = "update [Customer Shipping] " +
                           "set [Tracking Number] = @tracking, " +
                               "[Shipping Company ID] = @shippingCompanyId, " +
                               "[Date Shipped] = CASE @date WHEN '' THEN NULL ELSE @date END, " +
                               "[Status] = @status " +
                           "where [Customer Order ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("tracking", tracking));
            pars.Add(new SqlParameter("shippingCompanyId", shippingCompanyId));
            pars.Add(new SqlParameter("date", date));
            pars.Add(new SqlParameter("status", status));
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static DataTable getRetailerOrders()
        {
            string query = "SELECT [Distributor Order ID], [Stock Transfer ID], ds.[Product Order ID], po.[Customer ID], po.[Type], [Status], it.[Method], po.[Destination], [Date Shipped] " +
                            "FROM [Distributor Shipping] as ds " +
                            "INNER JOIN [Internal Transportation] as it ON ds.[Stock Transfer ID] = it.[id] " +
                            "INNER JOIN [Product Orders] as po ON ds.[Product Order ID] = po.[Product Order ID];";

            return Database.executeSelectQuery(query);
        }

        public static DataTable getRetailerOrderById(int id)
        {
            string query = "SELECT [Product Order ID], [Stock Transfer ID], [Status] " +
                            "FROM[Distributor Shipping] " +
                            "WHERE[Product Order ID] = @id;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            return Database.executeSelectQuery(query, pars);
        }

        public static void updateRetailerOrder(int id, int stock, string status, string date)
        {
            string query = "update [Distributor Shipping] " +
                           "set [Stock Transfer ID] = @stock, " +
                               "[Status] = @status, " +
                               "[Date Shipped] = CASE @date WHEN '' THEN NULL ELSE @date END " +
                           "where [Distributor Order ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("stock", stock));
            pars.Add(new SqlParameter("status", status));
            pars.Add(new SqlParameter("date", date));
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static void deleteDistributorOrder(int id)
        {
            string query = "delete from [Distributor Shipping] where [Distributor Order ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }
    }
}
