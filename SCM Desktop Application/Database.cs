using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCM_Desktop_Application
{
    public class InventoryItem
    {
        public int id;

        public string type = "raw material";

        public string Type
        {
            get
            {
                if (type == "raw material") {
                    return Database.RawMaterials[this.id];
                }
                else
                {
                    return Database.ProductsName[this.id];
                }
            }

            set
            {
                this.Type = value;
            }
        }
        public int siteId;
        public string Location
        {
            get
            {
                return Database.WarehousesListName[this.siteId];
            }

            set
            {
                this.Location = value;
            }
        }
        public int unitsOnHand { get; set; }
        public string status { get; set; }
        public int unitsOnOrder { get; set; }
        public int reorderPoint { get; set; }
    }

    public class ProcurementForecastItem
    {
        public int rawMaterialId;

        public int Year { get; set; }
        public int Week { get; set; }

        public string rawMaterial {
            get
            {
                return Database.RawMaterials[this.rawMaterialId];
            }

            set
            {
                this.rawMaterial = value;
            }
        }
        public int Quantity { get; set; }
    }

    public class ProcurementOrderItem
    {
        public int orderId { get; set; }

        public int supplierId;
        public string Supplier {
            get
            {
                return Database.SuppliersListName[this.supplierId];
            }

            set
            {
                this.Supplier = value;
            }
        }

        public int destinationSiteId;
        public string Destination {
            get
            {
                return Database.WarehousesListName[this.destinationSiteId];
            }

            set
            {
                this.Destination = value;
            }
        }

        public int rawMaterialId;
        public string rawMaterial {
            get
            {
                return Database.RawMaterials[this.rawMaterialId];
            }

            set
            {
                this.rawMaterial = value;
            }
        }

        public int Cost { get; set; }
    }

    public class Warehouse
    {
        public int id;

        public string Name
        {
            get
            {
                return Database.WarehousesListName[id];
            }
            set
            {
                this.Name = value;
            }
        }

        public int RentCost { get; set; }

        public string Location { get; set; }
        public int StockLevel { get; set; }
        public int Capacity { get; set; }
    }

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name
        {
            get
            {
                return Database.SuppliersListName[SupplierId];
            }
            set
            {
                this.Name = value;
            }
        }
        public string Location { get; set; }
    }

    public class Product
    {
        public int SKU { get; set; }
        public string Name
        {
            get
            {
                return Database.ProductsName[SKU];
            }
            set
            {
                Name = value;
            }
        }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public string Colour { get; set; }
        public int Price { get; set; }
    }

    public class ProductOrderContent
    {
        public int orderId { get; set; }
        public int SKU { get; set; }
        public int Quantity { get; set; }
        public int BatchPrice { get; set; }
    }
    public class ProductOrderItem
    {
        public int orderId {get; set;}

        public int customerId;
        public string CustomerName
        {
            get
            {
                return Database.CustomersName[customerId];
            }
            set
            {
                CustomerName = value;
            }
        }

        public ProductOrderContent[] productsList;

        public string CustomerType { get; set; }
        public string DatePlaced { get; set; }
        public string DateCompleted { get; set; }
        public string Destination { get; set; }
        public int TotalQuantity
        {
            get
            {
                int total = 0;
                for (int i = 0; i < productsList.Length; i++)
                {
                    total += productsList[i].Quantity;
                }

                return total;
            }
            set
            {
                TotalQuantity = value;
            }
        }
        public int TotalRevenue
        {
            get
            {
                int total = 0;
                for (int i = 0; i < productsList.Length; i++)
                {
                    total += productsList[i].BatchPrice;
                }

                return total;
            }
            set
            {
                TotalRevenue = value;
            }
        }

        public string ShipDateRequested { get; set; }
    }

    public class ShippingCompany
    {
        public int companyId;
        public string CompanyName
        {
            get
            {
                if (companyId != -1)
                {
                    return Database.ShippingCompaniesName[companyId];
                }
                else
                {
                    return "Company Delivery";
                }
            }
            set
            {
                CompanyName = value;
            }
        }
        public string ShippingMethod
        {
            get
            {
                if (CompanyName == "Company Delivery")
                {
                    return "Own Truck";
                }
                else
                {
                    return "Shipping Company";
                }
            }
            set
            {
                ShippingMethod = value;
            }
        }
        public string ContactInfo { get; set; }
        public int ShippingRate { get; set; }
    }

    public class CustomerShipping
    {
        public int trackingNumber { get; set; }
        public int shippingCompanyId;
        public string ShippingCompany
        {
            get
            {
                return Database.ShippingCompaniesName[shippingCompanyId];
            }
            set
            {
                ShippingCompany = value;
            }
        }

        public int originWarehouseId;
        public string originWarehouse
        {
            get
            {
                return Database.WarehousesListName[originWarehouseId];
            }
            set
            {
                originWarehouse = value;
            }
        }

        public int orderId { get; set; }
        public int customerId;
        public string Customer
        {
            get
            {
                return Database.CustomersName[customerId];
            }
            set
            {
                Customer = value;
            }
        }

        public string Destination { get; set; }
        public string DateShipped { get; set; }
    }

    public static class Database
    {
        public static string[] RawMaterials = { "Wood", "Rubber", "Eraser", "Lead" };
        public static string[] ProductsName = { "Product 1", "Product 2", "Product 3", "Prodcut 4" };
        public static string[] CustomersName = { "Customer 1", "Customer 2", "Retailer 1", "Retailer 2" };
        public static string[] SuppliersListName = { "Supplier 1", "Supplier 2", "Supplier 3" };
        public static string[] WarehousesListName = { "Warehouse 1", "Warehouse 2", "Warehouse 3" };
        public static string[] ShippingCompaniesName = { "Shipping 1", "Shipping 2" };

        // Warehouses Table
        public static Warehouse[] WarehouseList = new[]
        {
            new Warehouse { id = 0, RentCost = 400, Location = "Address 1", StockLevel = 200, Capacity = 300 },
            new Warehouse { id = 1, RentCost = 700, Location = "Address 2", StockLevel = 500, Capacity = 1000 },
            new Warehouse { id = 2, RentCost = 500, Location = "Address 3", StockLevel = 300, Capacity = 300 },
        };

        // Suppliers Table
        public static Supplier[] SuppliersList = new[]
        {
            new Supplier {SupplierId = 0, Location = "Address 1" },
            new Supplier {SupplierId = 1, Location = "Address 2" },
            new Supplier {SupplierId = 2, Location = "Address 3" },
        };

        // Procurement Forecasts Table
        public static ProcurementForecastItem[] ProcurementForecasts = new[]
        {
            new ProcurementForecastItem { Year = 2016, Week = 1, rawMaterialId = 0, Quantity = 0},
            new ProcurementForecastItem { Year = 2016, Week = 2, rawMaterialId = 1, Quantity = 1},
            new ProcurementForecastItem { Year = 2016, Week = 3, rawMaterialId = 2, Quantity = 2},
            new ProcurementForecastItem { Year = 2016, Week = 4, rawMaterialId = 3, Quantity = 3},
            new ProcurementForecastItem { Year = 2016, Week = 5, rawMaterialId = 1, Quantity = 4},
            new ProcurementForecastItem { Year = 2016, Week = 6, rawMaterialId = 1, Quantity = 5},
        };

        // Procurement Orders Table
        public static ProcurementOrderItem[] ProcurementOrders = new[]
        {
            new ProcurementOrderItem { orderId = 1, supplierId = 0, destinationSiteId = 1, rawMaterialId = 1 , Cost = 100},
            new ProcurementOrderItem { orderId = 2, supplierId = 0, destinationSiteId = 0, rawMaterialId = 0 , Cost = 200},
            new ProcurementOrderItem { orderId = 3, supplierId = 1, destinationSiteId = 1, rawMaterialId = 2 , Cost = 300},
            new ProcurementOrderItem { orderId = 4, supplierId = 0, destinationSiteId = 2, rawMaterialId = 3 , Cost = 400},
            new ProcurementOrderItem { orderId = 5, supplierId = 2, destinationSiteId = 0, rawMaterialId = 0 , Cost = 200},
            new ProcurementOrderItem { orderId = 6, supplierId = 0, destinationSiteId = 1, rawMaterialId = 1 , Cost = 100},
        };

        // Inventory Tables
        public static InventoryItem[] RawMaterialsInventory = new[]
        {
            new InventoryItem { id = 1, siteId = 1, unitsOnHand = 10, status = "Status", unitsOnOrder = 2,
            reorderPoint = 0},
            new InventoryItem { id = 2, siteId = 1, unitsOnHand = 20, status = "Status", unitsOnOrder = 4,
            reorderPoint = 2},
        };

        public static InventoryItem[] WIPInventory = new[]
        {
            new InventoryItem { id = 1, siteId = 1, unitsOnHand = 10, status = "Status", unitsOnOrder = 2,
            reorderPoint = 0, type = "WIP"},
            new InventoryItem { id = 2, siteId = 1, unitsOnHand = 20, status = "Status", unitsOnOrder = 4,
            reorderPoint = 2, type = "WIP"},
        };

        public static InventoryItem[] FinishedGoodsInventory = new[]
        {
            new InventoryItem { id = 0, siteId = 0, unitsOnHand = 10, status = "Status", unitsOnOrder = 20,
            reorderPoint = 0, type = "Finished Goods"},
            new InventoryItem { id = 2, siteId = 1, unitsOnHand = 20, status = "Status", unitsOnOrder = 40,
            reorderPoint = 2, type = "Finished Goods"},
        };

        // Product Orders Table
        public static ProductOrderContent[] productsList0 = new[]
        {
            new ProductOrderContent {orderId = 0, SKU = 0, Quantity = 4 },
            new ProductOrderContent {orderId = 0, SKU = 1, Quantity = 2 },
            new ProductOrderContent {orderId = 0, SKU = 2, Quantity = 10 },
            new ProductOrderContent {orderId = 0, SKU = 3, Quantity = 5 },
        };
        public static ProductOrderContent[] productsList1 = new[]
        {
            new ProductOrderContent {orderId = 1, SKU = 0, Quantity = 14 },
            new ProductOrderContent {orderId = 1, SKU = 2, Quantity = 10 },
        };
        public static ProductOrderContent[] productsList2 = new[]
        {
            new ProductOrderContent {orderId = 2, SKU = 0, Quantity = 50 },
            new ProductOrderContent {orderId = 2, SKU = 1, Quantity = 100 },
            new ProductOrderContent {orderId = 2, SKU = 2, Quantity = 40 },
            new ProductOrderContent {orderId = 2, SKU = 3, Quantity = 52 },
        };
        public static ProductOrderItem[] ProductOrders = new[]
        {
            new ProductOrderItem {orderId = 0, customerId = 0, CustomerType = "Customer", DatePlaced = "January 1, 2016", DateCompleted = "January 30, 2016", Destination = "Address 1", ShipDateRequested = "January 28, 2016", productsList = productsList0 },
            new ProductOrderItem {orderId = 1, customerId = 1, CustomerType = "Customer", DatePlaced = "April 1, 2016", DateCompleted = "April 24, 2016", Destination = "Address 2", ShipDateRequested = "April 15, 2016", productsList = productsList1 },
            new ProductOrderItem {orderId = 2, customerId = 2, CustomerType = "Retailer", DatePlaced = "June 1, 2016", DateCompleted = "June 30, 2016", Destination = "Address 3", ShipDateRequested = "June 28, 2016", productsList = productsList2 },
        };

        // Shipping Company Table
        public static ShippingCompany[] ShippingCompanies = new[]
        {
            new ShippingCompany {companyId = 0, ContactInfo = "555-555-5555", ShippingRate = 10},
            new ShippingCompany {companyId = 1, ContactInfo = "666-666-6666", ShippingRate = 15 },
        };

        public static CustomerShipping[] CustomerShipping = new[]
        {
            new CustomerShipping {trackingNumber = 0, shippingCompanyId = 0, customerId = 0, orderId = 0, originWarehouseId = 0, DateShipped = "January 20, 2016", Destination = "Address 1" },
            new CustomerShipping {trackingNumber = 1, shippingCompanyId = 0, customerId = 1, orderId = 1, originWarehouseId = 1, DateShipped = "April 20, 2016", Destination = "Address 2" },
            new CustomerShipping {trackingNumber = 2, shippingCompanyId = -1, customerId = 2, orderId = 2, originWarehouseId = 1, DateShipped = "June 20, 2016", Destination = "Address 3" },
        };
    }
}
