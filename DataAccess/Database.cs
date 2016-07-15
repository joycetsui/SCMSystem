using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Database
    {
        //static string CNSTR = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Access.DB_PATH;
        static string CNSTR = ConfigurationManager.ConnectionStrings["cs490scm"].ConnectionString.ToString();

        static public DataTable executeSelectQuery(string query, List<SqlParameter> pars)
        {
            return executeSelectQuery(query, pars.ToArray());
        }

        static public DataTable executeSelectQuery(string query, SqlParameter[] pars)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(CNSTR))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                if (pars.Length > 0)
                {
                    adapter.SelectCommand.Parameters.AddRange(pars);
                }
                adapter.Fill(dt);
            }

            return dt;
        }

        static public DataTable executeSelectQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(CNSTR))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }

            return dt;
        }

        static public void executeInsertUpdateQuery(string query, List<SqlParameter> pars)
        {
            executeInsertUpdateQuery(query, pars.ToArray());
        }

        static public void executeInsertUpdateQuery(string query, SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(CNSTR))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (pars.Length > 0)
                {
                    cmd.Parameters.AddRange(pars);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        static public int executeInsertQueryAndReturnId(string query, List<SqlParameter> pars)
        {
            return executeInsertQueryAndReturnId(query, pars.ToArray());
        }

        static public int executeInsertQueryAndReturnId(string query, SqlParameter[] pars)
        {
            int id = -1;
            using (SqlConnection conn = new SqlConnection(CNSTR))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (pars.Length > 0)
                {
                    cmd.Parameters.AddRange(pars);
                }

                conn.Open();
                object oid = cmd.ExecuteScalar();
                conn.Close();
                if (oid != null)
                {
                    int.TryParse(oid.ToString(), out id);
                }
            }

            return id;
        }
    }
}
