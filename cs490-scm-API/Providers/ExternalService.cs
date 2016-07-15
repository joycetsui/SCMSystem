using cs490_scm_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace cs490_scm_API.Providers
{
    public class ExternalService
    {
        //static string CNSTR = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Access.DB_PATH;
        //static string CNSTR = ConfigurationManager.ConnectionStrings["cs490scm"].ConnectionString.ToString();

        //static public DataTable executeSelectQuery(string query)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(CNSTR))
        //    {
        //        try {
        //            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        //            adapter.Fill(dt);
        //        }
        //        catch { }
        //    }

        //    return dt;
        //}

        //static public void executeInsertUpdateQuery(string query)
        //{
        //    using (SqlConnection conn = new SqlConnection(CNSTR))
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //            conn.Close();
        //        }
        //        catch { }
        //    }
        //}

        //static public void addNewProcurementOrder(int supplierId, int destinationSiteId, int rawMaterialId, int quantity)
        //{
        //    //int newOrderId = Database.ProcurementOrders.Last().orderId + 1;
        //    //Database.ProcurementOrders.Add(
        //    //    new ProcurementOrderItem { orderId = newOrderId, supplierId = supplierId, destinationSiteId = destinationSiteId, rawMaterialId = rawMaterialId, Quantity = quantity }
        //    //);
        //    //AppDomain.CurrentDomain.SetData("DataDirectory", );
        //    //System.IO.Path.

        //    string query = "insert into [Procurement Orders]([Supplier ID],[Raw Material ID],[Quantity]) values('" + supplierId + "','" + rawMaterialId + "','" + quantity + "')";
        //    OleDbConnection conn = new OleDbConnection(CNSTR);
        //    OleDbCommand cmd = new OleDbCommand();
        //    if (conn.State != ConnectionState.Open)
        //    {
        //        conn.Open();
        //    }
        //    cmd.Connection = conn;
        //    cmd.CommandText = query;
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //}

    //    // Accounting API expenses helper methods
    //    static public Expense[] Expenses = new[]
    //    {
    //        new Expense { Name = "Procurement Cost", amount = 500 },
    //        new Expense {Name = "Transportation Cost", amount = 100 },
    //        new Expense {Name = "Warehouse Rent Cost", amount = 5000 },
    //};

    //    static public Expense[] getExpenses()
    //    {
    //        return Expenses;
    //    }

    //    static public Expense getProcurementCost()
    //    {
    //        for (int i = 0; i < Expenses.Length; i++)
    //        {
    //            if (Expenses[i].Name == "Procurement Cost")
    //            {
    //                return Expenses[i];
    //            }
    //        }

    //        return new Expense { Name = "Invalid", amount = 0 };
    //    }

    //    static public Expense getTransportationCost()
    //    {
    //        for (int i = 0; i < Expenses.Length; i++)
    //        {
    //            if (Expenses[i].Name == "Transportation Cost")
    //            {
    //                return Expenses[i];
    //            }
    //        }

    //        return new Expense { Name = "Invalid", amount = 0 };
    //    }

        // Production API helper methods

        //public struct InventoryResp
        //{
        //    public string ItemName;
        //    public int AmountOnHand;
        //};

        //static public InventoryResp getRawMaterialsOnHandForId(int rawMaterialId)
        //{
        //    int amount = 0;
        //    string itemName = "Invalid";

        //    if (rawMaterialId < Database.RawMaterialsInventory.Count)
        //    {
        //        amount = Database.RawMaterialsInventory[rawMaterialId].unitsOnHand;
        //        itemName = Database.RawMaterialsInventory[rawMaterialId].Type;
        //    }

        //    return new InventoryResp { ItemName = itemName, AmountOnHand = amount };
        //}

        //static public int updateRawMaterialsOnHandForId(int rawMaterialId, int siteId, int newAmount)
        //{
        //    for (int i = 0; i < Database.RawMaterialsInventory.Count; i++)
        //    {
        //        if (Database.RawMaterialsInventory[i].id == rawMaterialId && Database.RawMaterialsInventory[i].siteId == siteId)
        //        {
        //            Database.RawMaterialsInventory[i].unitsOnHand = newAmount;
        //            return 0;
        //        }
        //    }

        //    return -1;
        //}

        //// Sales API helper methods
        //public struct OrderStatusResp
        //{
        //    public int OrderId;
        //    public string Status;
        //};
        //static public OrderStatusResp getCustomerOrderStatus(int orderId)
        //{
        //    string status = "Invalid";

        //    if (orderId < Database.CustomerShipping.Count)
        //    {
        //        status = Database.CustomerShipping[orderId].Status;
        //    }

        //    return new OrderStatusResp { OrderId = orderId, Status = status };
        //}

        //static public OrderStatusResp getRetailerOrderStatus(int orderId)
        //{
        //    string status = "Invalid";

        //    if (orderId < Database.DistributorShipping.Count)
        //    {
        //        status = Database.DistributorShipping[orderId].Status;
        //    }

        //    return new OrderStatusResp { OrderId = orderId, Status = status };
        //}
    }
}