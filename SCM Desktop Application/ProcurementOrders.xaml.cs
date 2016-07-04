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
    /// Interaction logic for ProcurementOrders.xaml
    /// </summary>
    public partial class ProcurementOrders : Page
    {
        public ProcurementOrders()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            procurementOrderDataGrid.ItemsSource = Database.ProcurementOrders;
        }

        public void addNewOrder(object sender, RoutedEventArgs e)
        {
            ProcurementOrderItem item = new ProcurementOrderItem();
            ProcurementOrderDetails detailsWindow = new ProcurementOrderDetails(item, "Create New Order");
            detailsWindow.Show();
        }

        public void editRow(object sender, RoutedEventArgs e)
        {
            ProcurementOrderItem item = (sender as Button).DataContext as ProcurementOrderItem;
            ProcurementOrderDetails detailsWindow = new ProcurementOrderDetails(item, "Update Order Details");
            detailsWindow.Show();
        }

        public void deleteRow(object sender, RoutedEventArgs e)
        {
            ProcurementOrderItem item = (sender as Button).DataContext as ProcurementOrderItem;
            Database.ProcurementOrders.Remove(item);
        }
    }
}
