using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCM_Desktop_Application
{
    static public class External
    {
        static public ProcurementForecastItem[] getNewForecasts()
        {
            ProcurementForecastItem[] ProcurementForecasts = new[]
            {
                new ProcurementForecastItem { Year = 2016, Week = 7, rawMaterialId = 0, Quantity = 30},
                new ProcurementForecastItem { Year = 2016, Week = 8, rawMaterialId = 1, Quantity = 100},
                new ProcurementForecastItem { Year = 2016, Week = 9, rawMaterialId = 2, Quantity = 42},
                new ProcurementForecastItem { Year = 2016, Week = 10, rawMaterialId = 3, Quantity = 13},
                new ProcurementForecastItem { Year = 2016, Week = 11, rawMaterialId = 1, Quantity = 44},
                new ProcurementForecastItem { Year = 2016, Week = 12, rawMaterialId = 1, Quantity = 445},
            };

            return ProcurementForecasts;
        }

        static public void addNewProcurementOrder(int supplierId, int destinationSiteId, int rawMaterialId, int quantity)
        {
            int newOrderId = Database.ProcurementOrders.Last().orderId + 1;
            Database.ProcurementOrders.Add(
                new ProcurementOrderItem { orderId = newOrderId, supplierId = supplierId, destinationSiteId = destinationSiteId, rawMaterialId = rawMaterialId, Quantity = quantity }
            );
        }

        static public void addNewInternalTransferOrder(int originSiteId, int destinationSiteId, int deliveryMethodId, string departureDate, string arrivalDate, int rawMaterialId, int quantity)
        {
            int newtransferId = Database.InternalTransfer.Last().StockTransferId + 1;
            Database.InternalTransfer.Add(
                            new InternalTransfer { StockTransferId = newtransferId, OriginSiteId = originSiteId, DestinationSiteId = destinationSiteId, DeliveryMethodID = deliveryMethodId, TotalCost = 10, DepartureDate = departureDate, ArrivalDate = arrivalDate, Quantity = quantity }
            );
        }


        // Accounting API expenses helper methods
        static public Expense[] Expenses = new[]
        {
                new Expense { Name = "Procurement Cost", amount = 500 },
                new Expense {Name = "Transportation Cost", amount = 100 },
                new Expense {Name = "Warehouse Rent Cost", amount = 5000 },
        };

        static public Expense[] getExpenses()
        {
            return Expenses;
        }

        static public Expense getProcurementCost()
        {
           for (int i = 0; i < Expenses.Length; i++)
            {
                if (Expenses[i].Name == "Procurement Cost")
                {
                    return Expenses[i];
                }
            }

            return new Expense { Name = "Invalid", amount = 0 };
        }

        static public Expense getTransportationCost()
        {
            for (int i = 0; i < Expenses.Length; i++)
            {
                if (Expenses[i].Name == "Transportation Cost")
                {
                    return Expenses[i];
                }
            }

            return new Expense { Name = "Invalid", amount = 0 };
        }

        // Production API helper methods

        public struct InventoryResp
        {
            public string ItemName;
            public int AmountOnHand;
        };

        static public InventoryResp getRawMaterialsOnHandForId(int rawMaterialId)
        {
            int amount = 0;
            string itemName = "Invalid";

            if (rawMaterialId < Database.RawMaterialsInventory.Count)
            {
                amount = Database.RawMaterialsInventory[rawMaterialId].unitsOnHand;
                itemName = Database.RawMaterialsInventory[rawMaterialId].Type;
            }

            return new InventoryResp { ItemName = itemName, AmountOnHand = amount};
        }

        static public void updateRawMaterialsOnHandForId(int rawMaterialId, int newAmount)
        {
            if (rawMaterialId < Database.RawMaterialsInventory.Count)
            {
                Database.RawMaterialsInventory[rawMaterialId].unitsOnHand = newAmount;
            }
        }

        // Sales API helper methods
        public struct OrderStatusResp
        {
            public int OrderId;
            public string Status;
        };
        static public OrderStatusResp getCustomerOrderStatus(int orderId)
        {
            string status = "Invalid";

            if (orderId < Database.ProductOrders.Count && orderId < Database.CustomerShipping.Count)
            {
                status = Database.CustomerShipping[orderId].Status;
            }

            return new OrderStatusResp { OrderId = orderId, Status = status };
        }

        static public OrderStatusResp getRetailerOrderStatus(int orderId)
        {
            string status = "Invalid";

            if (orderId < Database.DistributorShipping.Count)
            {
                status = Database.DistributorShipping[orderId].Status;
            }

            return new OrderStatusResp { OrderId = orderId, Status = status };
        }

        static public void addNewShippingCompany(string companyName, string shippingMethod, string contactInfo, double shippingRate)
        {
            int newCompanyId = Database.ShippingCompanies.Last().companyId + 1;
            Database.ShippingCompaniesName.Add(companyName);
            Database.ShippingCompanies.Add(
                new ShippingCompany
                {
                    companyId = newCompanyId,
                    CompanyName = companyName,
                    ShippingMethod = shippingMethod,
                    ContactInfo = contactInfo,
                    ShippingRate = shippingRate
                }
            );

        }

        static public void addNewSupplier(string supplierName, string location)
        {
            int newSupplierId = Database.SuppliersList.Last().SupplierId + 1;
            Database.SuppliersList.Add(
                new Supplier { SupplierId = newSupplierId, Location = location, Name = supplierName }
            );
        }
    }
}
