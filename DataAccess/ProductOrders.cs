 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ProductOrders
    {
        public static DataTable getCustomerOrders()
        {
            string query = "SELECT [Customer Order ID], [Tracking Number], [Status] " +
                            "FROM [Customer Shipping];";

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

        public static void updateCustomerOrder(int id, int tracking, int ship, DateTime date, string status)
        {
            string query = "update [Customer Shipping] " +
                           "set [Tracking Number] = @tracking, " +
                               "[Shipping Company ID] = @ship, " +
                               "[Date Shipped] = @date, " +
                               "[Status] = @status " +
                           "where [Customer Order ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("tracking", tracking));
            pars.Add(new SqlParameter("ship", ship));
            pars.Add(new SqlParameter("date", date));
            pars.Add(new SqlParameter("status", status));
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static DataTable getRetailerOrders()
        {
            string query = "SELECT [Distributor Order ID], [Product Order ID], [Stock Transfer ID], [Status] " +
                            "FROM[Distributor Shipping];";

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

        public static void updateRetailerOrder(int id, int stock, string status)
        {
            string query = "update [Distributor Shipping] " +
                           "set [Stock Transfer ID] = @stock, " +
                               "[Status] = @status " +
                           "where [Distributor Order ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("stock", stock));
            pars.Add(new SqlParameter("status", status));
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }
    }
}
