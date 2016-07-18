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
            loadTable();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {
            DataTable dt = Procurement.getProcurementOrders();
            procurementOrderDataGrid.ItemsSource = dt.AsDataView();
        }

        public void addNewOrder(object sender, RoutedEventArgs e)
        {
            ProcurementOrderDetails detailsWindow = new ProcurementOrderDetails(null, "Create New Order");
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void editRow(object sender, RoutedEventArgs e)
        {
            DataRowView item = (DataRowView)procurementOrderDataGrid.SelectedItems[0];
            ProcurementOrderDetails detailsWindow = new ProcurementOrderDetails(item, "Update Order Details");
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void deleteRow(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)procurementOrderDataGrid.SelectedItems[0];
            int id = int.Parse(row["Order ID"].ToString());
            Procurement.deleteProcurementOrder(id);
            loadTable();
        }
    }
}
