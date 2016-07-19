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

        public static DataTable getRetailerOrders()
        {
            string query = "SELECT [Product Order ID], [Stock Transfer ID], [Status] " +
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
    }
}
