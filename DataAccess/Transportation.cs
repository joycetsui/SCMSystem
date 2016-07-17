using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Transportation
    {
        public static DataTable getInternalTransfers()
        {
            string query = "SELECT it.[Stock Transfer ID], w.[Name] as [Origin Site], w2.[Name] as [Destination Site], transp.[Method], it.[Material ID], it.[Material Type], it.[Quantity], it.[Cost], it.[Departure Date], DATEADD(day, transp.[Delivery Time], it.[Departure Date]) as [Arrival Date] " +
                           "FROM[Internal Transfers] as it " +
                           "INNER JOIN[Internal Transportation] as transp ON it.[Delivery Method ID] = transp.[id] " +
                           "INNER JOIN[Warehouse] as w ON it.[Origin Site ID] = w.[Site ID] " +
                           "INNER JOIN[Warehouse] as w2 ON it.[Destination Site ID] = w2.[Site ID];";

            return Database.executeSelectQuery(query);
        }

        static public void createNewInventoryInternalTransferOrder(string inventoryType, int warehouseFromId, int warehouseToId, int transferMethodId, string departureDate, int materialId, string materialType, int transferQuantity, int newUnitsInFromWarehouse)
        {
            addNewInventoryInternalTransferOrder(warehouseFromId, warehouseToId, transferMethodId, departureDate, materialId, materialType, transferQuantity);
            Inventory.updateInventoryAmount(inventoryType, materialId, warehouseFromId, newUnitsInFromWarehouse);
            Inventory.updateOrCreateInventoryItemInboudUnits(inventoryType, materialId, warehouseToId, transferQuantity);
        }

        static private void addNewInventoryInternalTransferOrder(int originSiteId, int destinationSiteId, int deliveryMethodId, string departureDate, int materialId, string materialType, int quantity)
        {
            string query = "insert into [Internal Transfers]([Origin Site ID], [Destination Site ID], [Delivery Method ID], [Material ID], [Material Type], [Quantity], [Departure Date]) " +
                            "values(@originSiteId, @destinationSiteId, @deliveryMethodId, @materialId, @materialType, @quantity, @departureDate);";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("originSiteId", originSiteId));
            pars.Add(new SqlParameter("destinationSiteId", destinationSiteId));
            pars.Add(new SqlParameter("deliveryMethodId", deliveryMethodId));
            pars.Add(new SqlParameter("materialId", materialId));
            pars.Add(new SqlParameter("materialType", materialType));
            pars.Add(new SqlParameter("quantity", quantity));
            pars.Add(new SqlParameter("departureDate", departureDate));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static DataTable getInternalShippingMethods()
        {
            string query = "SELECT [Method] FROM [Internal Transportation];";
            return Database.executeSelectQuery(query);
        }

        public static int getInternalShippingMethodIdByType(string method)
        {
            string query = "SELECT [id] FROM [Internal Transportation] WHERE [Method] = @method;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("method", method));

            DataTable dt = Database.executeSelectQuery(query, pars);

            return int.Parse(dt.Rows[0][0].ToString());
        }

        public static void updateInternalTransferOrder(int stockId, int deliveryMethodId, double cost, string departureDate, string arrivalDate)
        {
            string query = "update [Internal Transfers] set [Delivery Method ID] = @deliveryMethodId, [Cost] = @cost, [Departure Date] = @departureDate, [Arrival Date] = @arrivalDate " +
                            "where [Stock Transfer ID]= @stockId;";

            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("deliveryMethodId", deliveryMethodId));
            pars.Add(new SqlParameter("cost", cost));
            pars.Add(new SqlParameter("departureDate", departureDate));
            pars.Add(new SqlParameter("arrivalDate", arrivalDate));
            pars.Add(new SqlParameter("stockId", stockId));

            Database.executeInsertUpdateQuery(query, pars);
        }

        public static double getTransportationCost()
        {
            string transportationCostQuery = "SELECT Sum(it.[Cost]) +  Sum(cs.[Shipping Cost]) as [Total Cost] " +
                                              "FROM [Internal Transfers] as it, [Customer Shipping] as cs;";
            DataTable dt = Database.executeSelectQuery(transportationCostQuery);

            double transportationCost = 0;
            if (dt.Rows.Count != 0 && dt.Rows[0]["Total Cost"].ToString() != "")
            {
                Double.TryParse(dt.Rows[0]["Total Cost"].ToString(), out transportationCost);
            }

            return transportationCost;
        }
    }
}
