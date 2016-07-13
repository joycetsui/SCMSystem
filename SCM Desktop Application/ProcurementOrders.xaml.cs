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
            string query = "Select * from [Procurement Orders]";
            OleDbConnection conn = new OleDbConnection(access.cnStr);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            DataTable ds = new DataTable();
            // if error at next line, download this (32 bit) https://www.microsoft.com/en-us/download/details.aspx?id=13255
            adapter.Fill(ds);
            procurementOrderDataGrid.ItemsSource = ds.AsDataView();

            conn.Close();
            
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
            //ProcurementOrderItem item = (sender as Button).DataContext as ProcurementOrderItem;
            ProcurementOrderDetails detailsWindow = new ProcurementOrderDetails(item, "Update Order Details");
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void deleteRow(object sender, RoutedEventArgs e)
        {

            DataRowView row = (DataRowView)procurementOrderDataGrid.SelectedItems[0];

            OleDbConnection conn = new OleDbConnection(access.cnStr);
            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "delete from [Procurement Orders] where [Procurement Order ID]=" + row["Procurement Order ID"].ToString();
            cmd.ExecuteNonQuery();
            loadTable();
        }
    }
}
