using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
    class Shipping
    {
        public static DataTable getShippingCompanies()
        {
            string query = "SELECT [Company Name], [Contact Info], [Standard Shipping Rate] " +
                            "FROM [Shipping Company];";
            
            return Database.executeSelectQuery(query);
        }

        public static void updateShippingCompany(int id, String companyName, String contactInfo, double rate)
        {
            string query = "update [Shipping Company] " +
                           "set [Company Name] = @companyName, " +
                               "[Contact Info] = @contactInfo, " +
                               "[Standard Shipping Rate] = @rate " +
                           "where [Shipping Company ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("companyName", companyName));
            pars.Add(new SqlParameter("contactInfo", contactInfo));
            pars.Add(new SqlParameter("rate", rate));
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static void addShippingCompany(int id, String companyName, String contactInfo, double rate)
        {
            String query = "insert into [Shipping Company]([Shipping Company ID],[Company Name], [Contact Info],[Standard Shipping Rate]) " +
                            "values( @id, @companyName, @contactInfo, @rate) " +
                            "SELECT scope_identity();";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("companyName", companyName));
            pars.Add(new SqlParameter("contactInfo", contactInfo));
            pars.Add(new SqlParameter("rate", rate));
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static void deleteShippingCompany(int id)
        {
            string query = "delete from [Shipping Company] where [Shipping Company ID]= @id";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            Database.executeInsertUpdateQuery(query, pars);
        }

    }
}
