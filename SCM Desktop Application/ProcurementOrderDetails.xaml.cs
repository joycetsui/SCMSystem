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

            // Fill combo boxes
            string sql = "select [Company Name] from [Suppliers]";
            DataTable dt = External.executeSelectQuery(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                supplierCb.Items.Add(dt.Rows[i][0].ToString());
            }

            sql = "select [Name] from [Warehouse]";
            dt = External.executeSelectQuery(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                destinationCb.Items.Add(dt.Rows[i][0].ToString());
            }

            sql = "select [Type] from [Raw Materials]";
            dt = External.executeSelectQuery(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rawMaterialCb.Items.Add(dt.Rows[i][0].ToString());
            }

            if (title == "Update Order Details")
            {
                orderTb.Text = item["Order ID"].ToString();
                supplierCb.Text = item["Supplier"].ToString();
                rawMaterialCb.Text = item["Raw Material"].ToString();
                quantityTb.Text = item["Quantity"].ToString();
                destinationCb.Text = item["Site"].ToString();

                updateBtn.Content = "Update";
            }
            else
            {
                supplierCb.Text = supplierCb.Items[0].ToString();
                destinationCb.Text = destinationCb.Items[0].ToString();
                rawMaterialCb.Text = rawMaterialCb.Items[0].ToString();

                updateBtn.Content = "Place Order";
            }
        }

        public void updateOrder(object sender, RoutedEventArgs e)
        {
            string orderIdStr = orderTb.Text;
            int orderId = 0;

            if (orderIdStr != "")
            {
                orderId = int.Parse(orderTb.Text);
            }

            string sql = "select [Raw Material ID] from [Raw Materials] where [Type]='" + rawMaterialCb.Text + "';";
            DataTable dt = External.executeSelectQuery(sql);
            int rawMaterialId = int.Parse(dt.Rows[0][0].ToString());

            sql = "select [Site ID] from [Warehouse] where [Name]='" + destinationCb.Text + "';";
            dt = External.executeSelectQuery(sql);
            int destinationId = int.Parse(dt.Rows[0][0].ToString());

            sql = "select [Supplier ID] from [Suppliers] where [Company Name]='" + supplierCb.Text + "';";
            dt = External.executeSelectQuery(sql);
            int supplierId = int.Parse(dt.Rows[0][0].ToString());

            string quantityStr = quantityTb.Text;
            int quantity = 0;
            if (quantityStr != "")
            {
                quantity = int.Parse(quantityStr);
            }

            if (title == "Update Order Details")
            {
                string query = "update [Procurement Orders] set [Supplier ID] ='" + supplierId + "', [Destination Site ID] = '" + destinationId + "', [Raw Material ID] = '" + rawMaterialId + "' , [Quantity] = '" + quantity + "' where [Procurement Order ID]=" + orderId;
                External.executeInsertUpdateQuery(query);
            }
            else
            {
                External.addNewProcurementOrder(supplierId, destinationId, rawMaterialId, quantity);
            }

            this.Close();
        }
    }
}
