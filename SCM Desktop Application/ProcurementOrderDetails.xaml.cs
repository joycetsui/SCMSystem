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
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for ProcurementOrderDetails.xaml
    /// </summary>
    public partial class ProcurementOrderDetails : Window
    {
        private DataRowView order;
        private string title = "";
        public ProcurementOrderDetails(DataRowView item, string title)
        {
            InitializeComponent();

            order = item;
            this.title = title;      

            pageTitle.Content = title;

            if (title == "Update Order Details")
            {
                orderTb.Text = item["Procurement Order ID"].ToString();
                supplierTb.Text = item["Supplier ID"].ToString();
                rawmaterialTb.Text = item["Raw Material ID"].ToString();
                quantityTb.Text = item["Quantity"].ToString();

                updateBtn.Content = "Update";
            }
            else
            {
                updateBtn.Content = "Place Order";
            }
        }

        public void updateOrder(object sender, RoutedEventArgs e)
        {
            if (title == "Update Order Details")
            {
                string query = "update [Procurement Orders] set [Supplier ID] ='" + supplierTb.Text + "', [Raw Material ID] = '" + rawmaterialTb.Text + "' , [Quantity] = '" + quantityTb.Text + "' where [Procurement Order ID]=" + orderTb.Text;
                OleDbConnection conn = new OleDbConnection(access.cnStr);
                OleDbCommand cmd = new OleDbCommand();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }    
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                conn.Close();
                
            }
            else
            {
                string query = "insert into [Procurement Orders]([Supplier ID],[Raw Material ID],[Quantity]) values('" + supplierTb.Text + "','" + rawmaterialTb.Text + "','" + quantityTb.Text + "')";
                OleDbConnection conn = new OleDbConnection(access.cnStr);
                OleDbCommand cmd = new OleDbCommand();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                conn.Close();

            }

            this.Close();
        }
    }
}
