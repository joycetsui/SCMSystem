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


        public void loadTable()
        {
            string query = "SELECT po.[Procurement Order ID] as [Order ID],  r.[Type] as [Raw Material], s.[Company Name] as [Supplier], w.[Name] as [Site], po.[Quantity] as [Quantity], po.[Actual Arrival Date] as [Actual Arrival Date], po.[Expected Arrival Date] as [Expected Arrival Date], (r.[Unit Cost] * po.[Quantity]) as [Total Cost] " +
                            "FROM ((([Procurement Orders] as po " +
                            "INNER JOIN [Raw Materials] as r ON po.[Raw Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN [Warehouse] as w ON po.[Destination Site ID] = w.[Site ID]) " +
                            "INNER JOIN [Suppliers] as s ON po.[Supplier ID] = s.[Supplier ID]);";

            DataTable dt = External.executeSelectQuery(query);
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

            string query = "delete from [Procurement Orders] where [Procurement Order ID]=" + row["Order ID"].ToString();
            External.executeInsertUpdateQuery(query);
            loadTable();
        }
    }
}
