using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
    public static class Shipping
    {
        public static DataTable getShippingCompanies()
        {
            string query = "SELECT * " +
                            "FROM [Shipping Company] Where [Shipping Company ID] <> 1;";
            
            return Database.executeSelectQuery(query);
        }

        public static DataTable getShippingCompanyNames()
        {
            string namesQuery = "Select [Company Name] from [Shipping Company] Where [Shipping Company ID] <> 1 ;";
            DataTable dt = Database.executeSelectQuery(namesQuery);
            return dt;
        }

        public static int getShippingCompanyID(string name)
        {
            string query = "select [Shipping Company ID] from [Shipping Company] where [Company Name] = @name";
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("name", name));

            DataTable dt = Database.executeSelectQuery(query, pars);
            return int.Parse(dt.Rows[0]["Shipping Company ID"].ToString());

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

        public static void addShippingCompany(String companyName, String contactInfo, double rate)
        {
            String query = "insert into [Shipping Company]([Company Name], [Contact Info],[Standard Shipping Rate]) " +
                           "values(@companyName, @contactInfo, @rate);";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("companyName", companyName));
            pars.Add(new SqlParameter("contactInfo", contactInfo));
            pars.Add(new SqlParameter("rate", rate));

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
