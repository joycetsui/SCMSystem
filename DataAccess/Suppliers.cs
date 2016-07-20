using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Suppliers
    {
        public static DataTable getSupplier()
        {
            string query = "SELECT *  " +
                            "FROM [Suppliers];";

            return Database.executeSelectQuery(query);
        }

        public static void updateSupplier(int id, String companyName, String location, String paymentDetails)
        {
            string query = "update [Suppliers] " +
                           "set [Company Name] = @companyName, " +
                               "[Location] = @location, " +
                               "[Payment Details] = @paymentDetails " +
                           "where [Supplier ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("companyName", companyName));
            pars.Add(new SqlParameter("location", location));
            pars.Add(new SqlParameter("paymentDetails", paymentDetails));
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static void addSupplier(String companyName, String location, String paymentDetails)
        {
            String query = "insert into [Suppliers]([Company Name], [Location],[Payment Details]) " +
                            "values( @companyName, @location, @paymentDetails);";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("companyName", companyName));
            pars.Add(new SqlParameter("location", location));
            pars.Add(new SqlParameter("paymentDetails", paymentDetails));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static void deleteSupplier(int id)
        {
            string query = "delete from [Suppliers] where [Supplier ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }
    }
}
