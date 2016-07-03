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

    public static class Product
    {
        public static int SKU { get; set; }
        public static string Name
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
        public static string Description { get; set; }
        public static string Type { get; set; }
        public static string Style { get; set; }
        public static string Colour { get; set; }
        public static int Price { get; set; }
    }
    public static class ProductOrderItem
    {
        public static int orderId {get; set;}
        public static int customerId { get; set; }

        public static int Quantity { get; set; }
        public static int BatchPrice { get; set; }
    }

    public static class Database
    {
        public static string[] RawMaterials = { "Wood", "Rubber", "Eraser", "Lead" };
        public static string[] ProductsName = { "Product 1", "Product 2", "Product 3" };


        public static string[] SuppliersListName = { "Supplier 1", "Supplier 2", "Supplier 3" };
        public static string[] WarehousesListName = { "Warehouse 1", "Warehouse 2", "Warehouse 3" };

        public static Warehouse[] WarehouseList = new[]
        {
            new Warehouse { id = 0, RentCost = 400, Location = "Address 1", StockLevel = 200, Capacity = 300 },
            new Warehouse { id = 1, RentCost = 700, Location = "Address 2", StockLevel = 500, Capacity = 1000 },
            new Warehouse { id = 2, RentCost = 500, Location = "Address 3", StockLevel = 300, Capacity = 300 },
        };

        public static Supplier[] SuppliersList = new[]
        {
            new Supplier {SupplierId = 0, Location = "Address 1" },
            new Supplier {SupplierId = 1, Location = "Address 2" },
            new Supplier {SupplierId = 2, Location = "Address 3" },
        };

        public static ProcurementForecastItem[] ProcurementForecasts = new[]
        {
            new ProcurementForecastItem { Year = 2016, Week = 1, rawMaterialId = 0, Quantity = 0},
            new ProcurementForecastItem { Year = 2016, Week = 2, rawMaterialId = 1, Quantity = 1},
            new ProcurementForecastItem { Year = 2016, Week = 3, rawMaterialId = 2, Quantity = 2},
            new ProcurementForecastItem { Year = 2016, Week = 4, rawMaterialId = 3, Quantity = 3},
            new ProcurementForecastItem { Year = 2016, Week = 5, rawMaterialId = 1, Quantity = 4},
            new ProcurementForecastItem { Year = 2016, Week = 6, rawMaterialId = 1, Quantity = 5},
        };

        public static ProcurementOrderItem[] ProcurementOrders = new[]
        {
            new ProcurementOrderItem { orderId = 1, supplierId = 0, destinationSiteId = 1, rawMaterialId = 1 , Cost = 100},
            new ProcurementOrderItem { orderId = 2, supplierId = 0, destinationSiteId = 0, rawMaterialId = 0 , Cost = 200},
            new ProcurementOrderItem { orderId = 3, supplierId = 1, destinationSiteId = 1, rawMaterialId = 2 , Cost = 300},
            new ProcurementOrderItem { orderId = 4, supplierId = 0, destinationSiteId = 2, rawMaterialId = 3 , Cost = 400},
            new ProcurementOrderItem { orderId = 5, supplierId = 2, destinationSiteId = 0, rawMaterialId = 0 , Cost = 200},
            new ProcurementOrderItem { orderId = 6, supplierId = 0, destinationSiteId = 1, rawMaterialId = 1 , Cost = 100},
        };

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

    }
}
