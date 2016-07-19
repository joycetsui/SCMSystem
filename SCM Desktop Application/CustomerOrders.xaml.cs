using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CustomerOrders.xaml
    /// </summary>
    public partial class CustomerOrders : Page
    {
        
        
        public CustomerOrders()
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
            DataTable dt = ProductOrders.getCustomerOrders();
            customerOrderDataGrid.ItemsSource = dt.AsDataView();
        }

        public void editRow(object sender, RoutedEventArgs e)
        {
            DataRowView item = (DataRowView)customerOrderDataGrid.SelectedItems[0];
            CustomerOrderDetails detailsWindow = new CustomerOrderDetails(item);
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void deleteRow(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)customerOrderDataGrid.SelectedItems[0];
            int id = int.Parse(row["Customer Order ID"].ToString());
            ProductOrders.deleteCustomerOrder(id);
            loadTable();
        }

    }
}
