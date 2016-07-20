using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for DistributorOrders.xaml
    /// </summary>
    public partial class DistributorOrders : Page
    {
        public DistributorOrders()
        {
            InitializeComponent();
            loadTable();

        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {
            DataTable dt = ProductOrders.getRetailerOrders();
            distributorOrderDataGrid.ItemsSource = dt.AsDataView();
        }

        public void editRow(object sender, RoutedEventArgs e)
        {
            DataRowView item = (DataRowView)distributorOrderDataGrid.SelectedItems[0];
            DistributorOrdersDetails detailsWindow = new DistributorOrdersDetails(item);
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void deleteRow(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)distributorOrderDataGrid.SelectedItems[0];
            int id = int.Parse(row["Distributor Order ID"].ToString());
            ProductOrders.deleteDistributorOrder(id);
            loadTable();
        }
    }
}
