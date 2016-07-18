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
        // General Inventory
        public static int getInventoryItemIdByType(string type, string inventoryType)
        {
            if (inventoryType == "Raw Material")
            {
                return getRawMaterialIdByType(type);
            }
            else {
                return getProductIdByType(type);
            }
        }

        public static void updateInventoryAmount(string inventoryType, int id, int siteId, int newAmount)
        {
            if (inventoryType == "Raw Material")
            {
                updateRawMaterialAmountForId(id, siteId, newAmount);
            }
            else if (inventoryType == "WIP") {
                updateWIPAmountForId(id, siteId, newAmount);
            }
            else
            {
                updateFGAmountForId(id, siteId, newAmount);
            }
        }

        public static void updateOrCreateInventoryItemInboudUnits(string inventoryType, int id, int siteId, int inboundUnits)
        {
            string tableName = "";
            string idColumnName = "";
            if (inventoryType == "Raw Material")
            {
                tableName = "Raw Material Inventory";
                idColumnName = "RM Inventory ID";
            }
            else if (inventoryType == "WIP")
            {
                tableName = "WIP Inventory";
                idColumnName = "WIP Inventory ID";
            }
            else
            {
                tableName = "Finished Goods Inventory";
                idColumnName = "FG Inventory ID";
            }

            string query = "SELECT [" + idColumnName + "] FROM [" + tableName + "] WHERE [Material ID] = @materialId AND [Site ID] = @siteId;";
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("materialId", id));
            pars.Add(new SqlParameter("siteId", siteId));

            DataTable dt = Database.executeSelectQuery(query, pars);

            List<SqlParameter> pars2 = new List<SqlParameter>();
            if (dt.Rows.Count == 0)
            {
                // need to insert new item
                int units = 0;
                query = "insert into [" + tableName + "] ([Material ID],[Site ID], [Units],[Inbound Units]) " +
                            "values( @materialId, @siteId, @units, @inboundUnits);";

                pars2.Add(new SqlParameter("materialId", id));
                pars2.Add(new SqlParameter("siteId", siteId));
                pars2.Add(new SqlParameter("units", units));
                pars2.Add(new SqlParameter("inboundUnits", inboundUnits));
            }
            else
            {
                // need to update inbound units of existing item
                query = "update [" + tableName + "] set [Inbound Units] = [Inbound Units] + @inboundUnits where [Material ID]= @materialId AND [Site ID] = @siteId;";

                pars2.Add(new SqlParameter("materialId", id));
                pars2.Add(new SqlParameter("siteId", siteId));
                pars2.Add(new SqlParameter("inboundUnits", inboundUnits));
            }

            Database.executeInsertUpdateQuery(query, pars2);
        }

        // Raw Materials
        public static DataTable getRawMaterials()
        {
            string query = "SELECT inv.[Material ID] as [Material ID],  r.[Type] as [Type], inv.[Site ID], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units], inv.[Reorder Point] as [Reorder Point] " +
                            "FROM (([Raw Material Inventory] as inv " +
                            "INNER JOIN [Raw Materials] as r ON inv.[Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID]);";

            return Database.executeSelectQuery(query);
        }

        public static DataTable getRawMaterialById(int id)
        {
            string query = "SELECT inv.[Material ID] as [Material ID],  r.[Type] as [Type], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM(([Raw Material Inventory]  inv " +
                            "INNER JOIN[Raw Materials]  r ON inv.[Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN[Warehouse]  w ON inv.[Site ID] = w.[Site ID]) " +
                            "WHERE inv.[Material ID] = @id;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));

            return Database.executeSelectQuery(query, pars);
        }

        public static DataTable getRawMaterialByIdAndSiteId(int id, int siteId)
        {
            string query = "SELECT [Units] FROM [Raw Material Inventory] " +
                           "WHERE [Material ID] = @id AND [Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("id", id));
            pars.Add(new SqlParameter("siteId", siteId));

            return Database.executeSelectQuery(query, pars);
        }

        public static DataTable getRawMaterialTypes()
        {
            string query = "select [Type] from [Raw Materials]";
            return Database.executeSelectQuery(query);
        }

        public static int getRawMaterialIdByType(string type)
        {
            string query = "select [Raw Material ID] from [Raw Materials] where [Type]= @type;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("type", type));

            DataTable dt = Database.executeSelectQuery(query, pars);

            return int.Parse(dt.Rows[0][0].ToString());
        }

        public static void updateRawMaterialAmountForId(int id, int siteId, int newAmount)
        {
            string query = "update [Raw Material Inventory] set [Units] = @units where [Material ID]= @id AND [Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("units", newAmount));
            pars.Add(new SqlParameter("id", id));
            pars.Add(new SqlParameter("siteId", siteId));

            Database.executeInsertUpdateQuery(query, pars);
        }

        // WIP
        public static DataTable getWIP()
        {
            string query = "SELECT inv.[Material ID] as [Material ID],  p.[Type] as [Type], inv.[Site ID], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM [WIP Inventory] as inv " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID] " +
                            "INNER JOIN [Products] as p ON inv.[Material ID] = p.[Product ID];";

            return Database.executeSelectQuery(query);
        }

        public static DataTable getWIPTypes()
        {
            string query = "select [Type] from [Products]";
            return Database.executeSelectQuery(query);
        }

        public static int getProductIdByType(string type)
        {
            string query = "select [Product ID] from [Products] where [Type]= @type;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("type", type));

            DataTable dt = Database.executeSelectQuery(query, pars);

            return int.Parse(dt.Rows[0][0].ToString());
        }

        public static void updateWIPAmountForId(int id, int siteId, int newAmount)
        {
            string query = "update [WIP Inventory] set [Units] = @units where [Material ID]= @id AND [Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("units", newAmount));
            pars.Add(new SqlParameter("id", id));
            pars.Add(new SqlParameter("siteId", siteId));

            Database.executeInsertUpdateQuery(query, pars);
        }

        // Finished Goods
        public static DataTable getFinishedGoods()
        {
            string query = "SELECT inv.[Material ID] as [Material ID],  p.[Type] as [Type], inv.[Site ID], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM [Finished Goods Inventory] as inv " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID] " +
                            "INNER JOIN [Products] as p ON inv.[Material ID] = p.[Product ID];";

            return Database.executeSelectQuery(query);
        }

        public static DataTable getProductTypes()
        {
            string query = "select [Type] from [Products]";
            return Database.executeSelectQuery(query);
        }

        public static void updateFGAmountForId(int id, int siteId, int newAmount)
        {
            string query = "update [Finished Goods Inventory] set [Units] = @units where [Material ID]= @id AND [Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("units", newAmount));
            pars.Add(new SqlParameter("id", id));
            pars.Add(new SqlParameter("siteId", siteId));

            Database.executeInsertUpdateQuery(query, pars);
        }

        // Warehouses
        public static DataTable getWarehouses()
        {
            string query = "select * from [Warehouse]";
            return Database.executeSelectQuery(query);
        }

        public static DataTable getWarehouseNames()
        {
            string query = "select [Name] from [Warehouse]";
            return Database.executeSelectQuery(query);
        }

        public static int getWarehouseIdByName(string name)
        {
            string query = "select [Site ID] from [Warehouse] where [Name]= @name;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("name", name));

            DataTable dt = Database.executeSelectQuery(query, pars);

            return int.Parse(dt.Rows[0][0].ToString());
        }

        public static DataTable getWarehouseRawMaterialsInventoryItems(int warehouseId)
        {
            DataTable dt = new DataTable();

            string query = "SELECT inv.[Material ID], rm.[Type], inv.[Units], inv.[Inbound Units] " +
                           "FROM [Raw Material Inventory] inv " +
                           "INNER JOIN [Raw Materials] rm ON inv.[Material ID] = rm.[Raw Material ID] " +
                           "WHERE inv.[Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("siteId", warehouseId));

            return Database.executeSelectQuery(query, pars);
        }

        public static DataTable getWarehouseWIPInventoryItems(int warehouseId)
        {
            DataTable dt = new DataTable();

            string query = "SELECT inv.[Material ID], rm.[Type], inv.[Units], inv.[Inbound Units] " +
                           "FROM [WIP Inventory] inv " +
                           "INNER JOIN [Products] rm ON inv.[Material ID] = rm.[Product ID] " +
                           "WHERE inv.[Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("siteId", warehouseId));

            return Database.executeSelectQuery(query, pars);
        }

        public static DataTable getWarehouseFGInventoryItems(int warehouseId)
        {
            DataTable dt = new DataTable();

            string query = "SELECT inv.[Material ID], rm.[Type], inv.[Units], inv.[Inbound Units] " +
                           "FROM [Finished Goods Inventory] inv " +
                           "INNER JOIN [Products] rm ON inv.[Material ID] = rm.[Product ID] " +
                           "WHERE inv.[Site ID] = @siteId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("siteId", warehouseId));

            return Database.executeSelectQuery(query, pars);
        }

        public static int getWarehouseStockLevel(int warehouseId)
        {
            int stockLevel = 0;

            string query = "SELECT SUM([Count]) as [Stock Level] " +
                           "FROM (" +
                               "SELECT SUM(inv.[Units]) as [Count] " +
                               "FROM [Raw Material Inventory] inv " +
                               "INNER JOIN [Raw Materials] rm ON inv.[Material ID] = rm.[Raw Material ID] " +
                               "WHERE inv.[Site ID] = @siteId " +
                               "Union " +
                               "SELECT SUM(inv.[Units])  " +
                               "FROM [WIP Inventory] inv " +
                               "INNER JOIN [Products] rm ON inv.[Material ID] = rm.[Product ID] " +
                               "WHERE inv.[Site ID] = @siteId " +
                               "Union " +
                               "SELECT SUM(inv.[Units]) " +
                               "FROM [Finished Goods Inventory] inv " +
                               "INNER JOIN [Products] rm ON inv.[Material ID] = rm.[Product ID] " +
                               "WHERE inv.[Site ID] = @siteId " +
                            ") as sub;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("siteId", warehouseId));

            DataTable dt = Database.executeSelectQuery(query, pars);

            stockLevel = int.Parse(dt.Rows[0]["Stock Level"].ToString());

            return stockLevel;
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
