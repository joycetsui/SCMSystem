using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCM_Desktop_Application
{
    class ServicesOffered
    {

        ServicesOffered(string apiUrl)
        {
            string url = apiUrl;
            url = "ip/expenses";

            MainWindow main = Application.Current.MainWindow as MainWindow;

            if (url == "ip/expenses")
            {
                Expense[] expenses = main.getExpenses();
            }
            else if (url == "ip/expenses/procurement_cost")
            {
                double procurementCost = main.getProcurementCost();
            }
            else if (url == "ip/expenses/transportation_cost")
            {
                double transportationCost = main.getTransportationCost();
            }
            else if (url == "ip/inventory/raw_materials/{raw material id}")
            {
                int rawMaterialId = 0;
                int rawMaterialsOnHand = main.getRawMaterialsOnHandForId(rawMaterialId);
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
                main.updateRawMaterialsOnHandForId(rawMaterialId, newAmount);
            }
            else if (url == "ip/inventory/procurement_order")
            {
                int rawMaterialId = 0;
                int orderAmount = 5;
                int destinationSiteId = 0;

                main.addNewProcurementOrder(0, destinationSiteId, rawMaterialId, orderAmount);
            }
            else if (url == "ip/orders/status/{order id}")
            {
                int orderId = 3;
                string type = "Cusotmer";
                string status = "";

                if (type == "Cutomer")
                {
                    status = main.getCustomerOrderStatus(orderId);
                }
                else
                {
                    status = main.getRetailerOrderStatus(orderId);
                }
            }
        }
    }
}
