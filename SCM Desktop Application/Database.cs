using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public class ProcurementOrderItem : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int orderId { get; set; }

        private int _suplierId;
        public int supplierId
        {
            get
            {
                return _suplierId;
            }
            set
            {
                _suplierId = value;
                Supplier = Database.SuppliersListName[_suplierId];
                NotifyPropertyChanged();
            }
        }
        public string Supplier {
            get
            {
                return Database.SuppliersListName[this.supplierId];
            }

            set
            {
                NotifyPropertyChanged();
            }
        }

        private int _destinationSiteId;
        public int destinationSiteId
        {
            get
            {
                return _destinationSiteId;
            }
            set
            {
                _destinationSiteId = value;
                Destination = Database.WarehousesListName[_destinationSiteId];
                NotifyPropertyChanged();
            }
        }
        public string Destination {
            get
            {
                return Database.WarehousesListName[this.destinationSiteId];
            }

            set
            {
                NotifyPropertyChanged();
            }
        }

        private int _rawMaterialId;
        public int rawMaterialId
        {
            get
            {
                return _rawMaterialId;
            }
            set
            {
                _rawMaterialId = value;
                rawMaterial = Database.RawMaterials[_rawMaterialId];
                NotifyPropertyChanged();
            }
        }
        public string rawMaterial {
            get
            {
                return Database.RawMaterials[this.rawMaterialId];
            }

            set
            {
                NotifyPropertyChanged();
            }
        }

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                Cost = _quantity * Database.RawMaterialsList[_rawMaterialId].cost;
                NotifyPropertyChanged();
            }
        }

        private double _cost;
        public double Cost
        {
            get
            {
                return Database.RawMaterialsList[_rawMaterialId].cost * Quantity;
            }
            set
            {
                NotifyPropertyChanged();
            }
        }
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

    public class CustomerShipping : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int OrderId { get; set; }

        public int CustomerId;
        public string Customer
        {
            get
            {
                return Database.CustomersName[CustomerId];
            }
            set
            {
                Customer = value;
                NotifyPropertyChanged();
            }
        }

        public string Status { get; set; }

        public int OriginWarehouseId;
        public string OriginWarehouse
        {
            get
            {
                return Database.WarehousesListName[OriginWarehouseId];
            }
            set
            {
                OriginWarehouse = value;
                NotifyPropertyChanged();
            }
        }

        public string Destination { get; set; }

        public int TrackingNumber;
        public int _ShippingCompanyId;
        public int ShippingCompanyId
        {
            get
            {
                return _ShippingCompanyId;
            }
            set
            {
                _ShippingCompanyId = value;
                ShippingCompany = Database.ShippingCompaniesName[_ShippingCompanyId];
                NotifyPropertyChanged();
            }
        }
        public string ShippingCompany
        {
            get
            {
                return Database.ShippingCompaniesName[ShippingCompanyId];
            }
            set
            {
                NotifyPropertyChanged();
            }
        }


        public string DateShipped;
    }

    public class DistributorShipping
    {
        public int trackingNumber { get; set; }
        public int stockTransferID;

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

    public class InternalTransfer
    {
        public int stockTransferId;

        public int originSiteId;
        public string OriginSite
        {
            get
            {
                return Database.WarehousesListName[originSiteId];
            }
            set
            {
                OriginSite = value;
            }
        }

        public int destinationSiteId;
        public string DestinationSite
        {
            get
            {
                return Database.WarehousesListName[destinationSiteId];
            }
            set
            {
                DestinationSite = value;
            }
        }

        public string deliveryMethod { get; set; }

        public double totalCost { get; set; }
        public string departureDate { get; set; }
        public string arrivalDate { get; set; }
    }

    public class RawMaterial
    {
        public int rawMaterialId;
        public int supplierId;
        public string type
        {
            get
            {
                return Database.RawMaterials[rawMaterialId];
            }
            set
            {

            }
        }

        public double cost; 
    }

    public class AnalyticsData
    {
        public int supplierResponseTime;
        public int productionTime;
        public int orderFullfillmentTime;
        public int scmCost;
        public string date;
    }

    public static class Database
    {
        public static string[] RawMaterials = { "Wood", "Ink", "Eraser", "Lead", "Metal" };
        public static string[] ProductsName = { "Product 1", "Product 2", "Product 3", "Prodcut 4" };
        public static string[] CustomersName = { "Customer 1", "Customer 2", "Retailer 1", "Retailer 2" };
        public static string[] SuppliersListName = { "Supplier 1", "Supplier 2", "Supplier 3", "Supplier 4" };
        public static string[] WarehousesListName = { "Warehouse 1", "Warehouse 2", "Warehouse 3" };
        public static string[] ShippingCompaniesName = { "Shipping 1", "Shipping 2" };

        // Raw Materials Tables
        public static RawMaterial[] RawMaterialsList = new[]
        {
            new RawMaterial {rawMaterialId = 0, supplierId = 0, cost = 5 },
            new RawMaterial {rawMaterialId = 1, supplierId = 1, cost = 10 },
            new RawMaterial {rawMaterialId = 2, supplierId = 2, cost = 0.05 },
            new RawMaterial {rawMaterialId = 3, supplierId = 3, cost = 2 },
            new RawMaterial {rawMaterialId = 4, supplierId = 0, cost = 6 },
        };

        // Warehouses Table
        public static ObservableCollection<Warehouse> WarehouseList = new ObservableCollection<Warehouse>
        {
            new Warehouse { id = 0, RentCost = 400, Location = "Address 1", StockLevel = 200, Capacity = 300 },
            new Warehouse { id = 1, RentCost = 700, Location = "Address 2", StockLevel = 500, Capacity = 1000 },
            new Warehouse { id = 2, RentCost = 500, Location = "Address 3", StockLevel = 300, Capacity = 300 },
        };

        // Suppliers Table
        public static ObservableCollection<Supplier> SuppliersList = new ObservableCollection<Supplier>
        {
            new Supplier {SupplierId = 0, Location = "Address 1" },
            new Supplier {SupplierId = 1, Location = "Address 2" },
            new Supplier {SupplierId = 2, Location = "Address 3" },
        };

        // Procurement Forecasts Table
        public static ObservableCollection<ProcurementForecastItem> ProcurementForecasts = new ObservableCollection<ProcurementForecastItem>
        {
            new ProcurementForecastItem { Year = 2016, Week = 1, rawMaterialId = 0, Quantity = 0},
            new ProcurementForecastItem { Year = 2016, Week = 2, rawMaterialId = 1, Quantity = 1},
            new ProcurementForecastItem { Year = 2016, Week = 3, rawMaterialId = 2, Quantity = 2},
            new ProcurementForecastItem { Year = 2016, Week = 4, rawMaterialId = 3, Quantity = 3},
            new ProcurementForecastItem { Year = 2016, Week = 5, rawMaterialId = 1, Quantity = 4},
            new ProcurementForecastItem { Year = 2016, Week = 6, rawMaterialId = 1, Quantity = 5},
        };

        // Procurement Orders Table
        public static ObservableCollection<ProcurementOrderItem> ProcurementOrders = new ObservableCollection<ProcurementOrderItem>
        {
            new ProcurementOrderItem { orderId = 0, supplierId = 0, destinationSiteId = 1, rawMaterialId = 1 , Quantity = 100},
            new ProcurementOrderItem { orderId = 1, supplierId = 0, destinationSiteId = 0, rawMaterialId = 0 , Quantity = 200},
            new ProcurementOrderItem { orderId = 2, supplierId = 1, destinationSiteId = 1, rawMaterialId = 2 , Quantity = 300},
            new ProcurementOrderItem { orderId = 3, supplierId = 0, destinationSiteId = 2, rawMaterialId = 3 , Quantity = 400},
            new ProcurementOrderItem { orderId = 4, supplierId = 2, destinationSiteId = 0, rawMaterialId = 0 , Quantity = 200},
            new ProcurementOrderItem { orderId = 5, supplierId = 0, destinationSiteId = 1, rawMaterialId = 1 , Quantity = 100},
        };

        // Inventory Tables
        public static ObservableCollection<InventoryItem> RawMaterialsInventory = new ObservableCollection<InventoryItem>
        {
            new InventoryItem { id = 0, siteId = 0, unitsOnHand = 10, status = "Status", unitsOnOrder = 2,
            reorderPoint = 0},
            new InventoryItem { id = 1, siteId = 1, unitsOnHand = 20, status = "Status", unitsOnOrder = 4,
            reorderPoint = 2},
        };

        public static ObservableCollection<InventoryItem> WIPInventory = new ObservableCollection<InventoryItem>
        {
            new InventoryItem { id = 0, siteId = 1, unitsOnHand = 10, status = "Status", unitsOnOrder = 2,
            reorderPoint = 0, type = "WIP"},
            new InventoryItem { id = 1, siteId = 1, unitsOnHand = 20, status = "Status", unitsOnOrder = 4,
            reorderPoint = 2, type = "WIP"},
        };

        public static ObservableCollection<InventoryItem> FinishedGoodsInventory = new ObservableCollection<InventoryItem>
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

        public static ObservableCollection<ProductOrderItem> ProductOrders = new ObservableCollection<ProductOrderItem>
        {
            new ProductOrderItem {orderId = 0, customerId = 0, CustomerType = "Customer", DatePlaced = "January 1, 2016", DateCompleted = "January 30, 2016", Destination = "Address 1", ShipDateRequested = "January 28, 2016", productsList = productsList0 },
            new ProductOrderItem {orderId = 1, customerId = 1, CustomerType = "Customer", DatePlaced = "April 1, 2016", DateCompleted = "April 24, 2016", Destination = "Address 2", ShipDateRequested = "April 15, 2016", productsList = productsList1 },
            new ProductOrderItem {orderId = 2, customerId = 2, CustomerType = "Retailer", DatePlaced = "June 1, 2016", DateCompleted = "June 30, 2016", Destination = "Address 3", ShipDateRequested = "June 28, 2016", productsList = productsList2 },
        };

        // Shipping Company Table
        public static ObservableCollection<ShippingCompany> ShippingCompanies = new ObservableCollection<ShippingCompany>
        {
            new ShippingCompany {companyId = 0, ContactInfo = "555-555-5555", ShippingRate = 10},
            new ShippingCompany {companyId = 1, ContactInfo = "666-666-6666", ShippingRate = 15 },
        };

        public static ObservableCollection<CustomerShipping> CustomerShipping = new ObservableCollection<CustomerShipping>
        {
            new CustomerShipping {OrderId = 0, CustomerId = 0, Status = "Not Shipped", OriginWarehouseId = 0, Destination = "Address 1" },
            new CustomerShipping {OrderId = 1, CustomerId = 1, Status = "Shipped", OriginWarehouseId = 1, Destination = "Address 2", TrackingNumber = 2, ShippingCompanyId = 0, DateShipped = "April 20, 2016"},
            new CustomerShipping {OrderId = 2, CustomerId = 2, Status = "Shipped", OriginWarehouseId = 1, Destination = "Address 3", TrackingNumber = 32, ShippingCompanyId = 0, DateShipped = "May 20, 2016" },
        };

        public static DistributorShipping[] DistributorShipping = new[]
        {
            new DistributorShipping {trackingNumber = 0, stockTransferID = 0, customerId = 0, orderId = 0, originWarehouseId = 0, DateShipped = "March 2, 2016", Destination = "Address 1" },
            new DistributorShipping {trackingNumber = 1, stockTransferID = 2, customerId = 1, orderId = 1, originWarehouseId = 1, DateShipped = "May 20, 2016", Destination = "Address 2" },
            new DistributorShipping {trackingNumber = 2, stockTransferID = 1, customerId = 2, orderId = 2, originWarehouseId = 1, DateShipped = "June 20, 2016", Destination = "Address 3" },
        };

        public static InternalTransfer[] InternalTransfer = new[]
        {
            new InternalTransfer {stockTransferId = 0, originSiteId = 0, destinationSiteId = 1, deliveryMethod = "Truck 1", totalCost = 10, departureDate = "March 2, 2016", arrivalDate = "March 10, 2016"},
            new InternalTransfer {stockTransferId = 1, originSiteId = 1, destinationSiteId = 0, deliveryMethod = "Truck 2", totalCost = 100, departureDate = "May 20, 2016", arrivalDate = "May 30, 2016"},
            new InternalTransfer {stockTransferId = 2, originSiteId = 1, destinationSiteId = 0, deliveryMethod = "Train 3", totalCost = 50, departureDate = "June 20, 2016", arrivalDate = "June 30, 2016"}
        };

        //Analytics
        public static ObservableCollection<AnalyticsData> Analytics = new ObservableCollection<AnalyticsData>
        {
            new AnalyticsData {supplierResponseTime = 3, productionTime = 1, orderFullfillmentTime = 4, scmCost = 4000, date = "May 15"}, 
            new AnalyticsData {supplierResponseTime = 1, productionTime = 5, orderFullfillmentTime = 9, scmCost = 5000, date = "June 5"},
            new AnalyticsData {supplierResponseTime = 5, productionTime = 1, orderFullfillmentTime = 8, scmCost = 8000, date = "April 4"}
        };
    }
}
