using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCM_Desktop_Application
{
    static class ServicesOffered
    {

        static public void getService(string apiUrl)
        {
            string url = apiUrl;
            url = "ip/expenses";

            if (url == "ip/expenses")
            {
                Expense[] expenses = External.getExpenses();
            }
            else if (url == "ip/expenses/procurement_cost")
            {
                Expense procurementCost = External.getProcurementCost();
            }
            else if (url == "ip/expenses/transportation_cost")
            {
                Expense transportationCost = External.getTransportationCost();
            }
            else if (url == "ip/inventory/raw_materials/{raw material id}")
            {
                int rawMaterialId = 0;
                int rawMaterialsOnHand = External.getRawMaterialsOnHandForId(rawMaterialId);
            }
            else if (url == "ip/inventory/update_raw_materials/{raw material id}")
            {
                int rawMaterialId = 0;
                int amountUsed = 5;
                int currentAmount = 0;

                if (rawMaterialId < Database.RawMaterialsInventory.Count)
                {
                    currentAmount = Database.RawMaterialsInventory[rawMaterialId].unitsOnHand;
                }

                int newAmount = currentAmount - amountUsed;
                if (newAmount < 0)
                {
                    newAmount = 0;
                }
                External.updateRawMaterialsOnHandForId(rawMaterialId, newAmount);
            }
            else if (url == "ip/inventory/procurement_order")
            {
                int rawMaterialId = 0;
                int orderAmount = 5;
                int destinationSiteId = 0;

                External.addNewProcurementOrder(0, destinationSiteId, rawMaterialId, orderAmount);
            }
            else if (url == "ip/orders/status/{order id}")
            {
                int orderId = 3;
                string type = "Cusotmer";
                string status = "";

                if (type == "Cutomer")
                {
                    status = External.getCustomerOrderStatus(orderId);
                }
                else
                {
                    status = External.getRetailerOrderStatus(orderId);
                }
            }
        }
    }
}
