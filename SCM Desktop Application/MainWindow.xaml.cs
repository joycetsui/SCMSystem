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
                            new InternalTransfer { StockTransferId = newtransferId, OriginSiteId = originSiteId, DestinationSiteId = destinationSiteId, DeliveryMethodID = deliveryMethodId, TotalCost = 10, DepartureDate = departureDate, ArrivalDate = arrivalDate}
            );
        }
    }
}
