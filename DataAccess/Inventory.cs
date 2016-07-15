using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Inventory
    {
        public static DataTable getRawMaterials()
        {
            string query = "SELECT inv.[Raw Material ID] as [Raw Material ID],  r.[Type] as [Type], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM (([Raw Material Inventory] as inv " +
                            "INNER JOIN [Raw Materials] as r ON inv.[Raw Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID]);";

            return Database.executeSelectQuery(query);
        }

        public static DataTable getRawMaterialById(int id)
        {
            string query = "SELECT inv.[Raw Material ID] as [Raw Material ID],  r.[Type] as [Type], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM(([Raw Material Inventory]  inv " +
                            "INNER JOIN[Raw Materials]  r ON inv.[Raw Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN[Warehouse]  w ON inv.[Site ID] = w.[Site ID]) " +
                            "WHERE inv.[Raw Material ID] = @id;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            return Database.executeSelectQuery(query, pars);
        }

        public static DataTable getRawMaterialByIdAndSiteId(int id, int siteId)
        {
            string query = "SELECT [Units] FROM [Raw Material Inventory] " +
                           "WHERE [Raw Material ID] = @id AND [Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));
            pars.Add(new SqlParameter("siteId", siteId));

            return Database.executeSelectQuery(query, pars);
        }

        public static void updateRawMaterialAmountForId(int id, int siteId, int newAmount)
        {
            string query = "update [Raw Material Inventory] set [Units] = @units where [Raw Material ID]= @id AND [Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("units", newAmount));
            pars.Add(new SqlParameter("id", id));
            pars.Add(new SqlParameter("siteId", siteId));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static double getWarehouseRent()
        {
            string warehouseRentQuery = "SELECT Sum([Rent Cost]) as [Total Cost] " +
                                        "FROM [Warehouse];";

            DataTable dt = Database.executeSelectQuery(warehouseRentQuery);

            double rentCost = 0;
            if (dt.Rows.Count != 0 && dt.Rows[0]["Total Cost"].ToString() != "")
            {
                Double.TryParse(dt.Rows[0]["Total Cost"].ToString(), out rentCost);
            }

            return rentCost;
        }
    }
}
