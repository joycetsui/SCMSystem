using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public string selectedTabName = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectedIndexChanged(Object sender, EventArgs e)
        {
            var current = (sender as TabControl).SelectedItem;
            MainWindow.selectedTabName = (current as TabItem).Header.ToString();
        }

        public void addNewProcurementOrder(int supplierId, int destinationSiteId, int rawMaterialId, int quantity)
        {
            int newOrderId = Database.ProcurementOrders.Last().orderId + 1;
            Database.ProcurementOrders.Add(
                new ProcurementOrderItem { orderId = newOrderId, supplierId = supplierId, destinationSiteId = destinationSiteId, rawMaterialId = rawMaterialId, Quantity = quantity }
            );
        }

        public void addNewInternalTransferOrder(int originSiteId, int destinationSiteId, int deliveryMethodId, string departureDate, string arrivalDate, int rawMaterialId, int quantity)
        {
            int newtransferId = Database.InternalTransfer.Last().StockTransferId + 1;
            Database.InternalTransfer.Add(
                            new InternalTransfer { StockTransferId = newtransferId, OriginSiteId = originSiteId, DestinationSiteId = destinationSiteId, DeliveryMethodID = deliveryMethodId, TotalCost = 10, DepartureDate = departureDate, ArrivalDate = arrivalDate, Quantity = quantity}
            );
        }

        public Expense[] getExpenses()
        {
            int transpCost = getTransportationCost();
            int procCost = getProcurementCost();
            int rentCost = 5000;

            Expense[] expenses = new[]
            {
                new Expense { Name = "Procurement Cost", amount = procCost },
                new Expense {Name = "Transportation Cost", amount = transpCost },
                new Expense {Name = "Warehouse Rent Cost", amount = rentCost },
            };

            return expenses;
        }

        public int getProcurementCost()
        {
            return 500;
        }

        public int getTransportationCost()
        {
            return 100;
        }

        public int getRawMaterialsOnHandForId(int rawMaterialId)
        {
            if (rawMaterialId < Database.RawMaterialsInventory.Count)
            {
                return Database.RawMaterialsInventory[rawMaterialId].unitsOnHand;
            }

            return 0;
        }

        public void updateRawMaterialsOnHandForId(int rawMaterialId, int newAmount)
        {
            if (rawMaterialId < Database.RawMaterialsInventory.Count)
            {
                Database.RawMaterialsInventory[rawMaterialId].unitsOnHand = newAmount;
            }
        }

        public string getCustomerOrderStatus(int orderId)
        {
            if (orderId < Database.ProductOrders.Count)
            {
               return Database.CustomerShipping[orderId].Status;
            }

            return "Customer order id " + orderId + " does not exist.";
        }

        public string getRetailerOrderStatus(int orderId)
        {
            if (orderId < Database.DistributorShipping.Count)
            {
                return Database.DistributorShipping[orderId].Status;
            }

            return "Retailer order id " + orderId + " does not exist.";
        }

        public void addNewShippingCompany(string companyName, string shippingMethod, string contactInfo, double shippingRate)
        {
            int newCompanyId = Database.ShippingCompanies.Last().companyId + 1;
            Database.ShippingCompaniesName.Add(companyName);
            Database.ShippingCompanies.Add(
                new ShippingCompany {
                    companyId = newCompanyId,
                    CompanyName = companyName,
                    ShippingMethod = shippingMethod,
                    ContactInfo = contactInfo,
                    ShippingRate = shippingRate }
            );
          
        }

        public void addNewSupplier(string supplierName, string location)
        {
            int newSupplierId = Database.SuppliersList.Last().SupplierId + 1;
            Database.SuppliersList.Add(
                new Supplier { SupplierId = newSupplierId, Location = location, Name = supplierName }
            );
        }

    }
}
