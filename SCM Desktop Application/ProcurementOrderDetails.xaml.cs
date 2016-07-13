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

            //for (int i = 0; i < Database.WarehousesListName.Length; i++)
            //{
            //    ComboBoxItem cb = new ComboBoxItem();
            //    cb.Content = Database.WarehousesListName[i];
            //    destinationCb.Items.Add(cb);
            //}

            //for (int i = 0; i < Database.SuppliersListName.Count; i++)
            //{
            //    ComboBoxItem cb = new ComboBoxItem();
            //    cb.Content = Database.SuppliersListName[i];
            //    supplierCb.Items.Add(cb);
            //}

            //for (int i = 0; i < Database.RawMaterials.Length; i++)
            //{
            //    ComboBoxItem cb = new ComboBoxItem();
            //    cb.Content = Database.RawMaterials[i];
            //    rawMaterialCb.Items.Add(cb);
            //}

            //supplierCb.Text = item.Supplier;
            //destinationCb.Text = item.Destination;
            //rawMaterialCb.Text = item.rawMaterial;
            //quantityTb.Text = item.Quantity.ToString();

            

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

                string cnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../database/scm.accdb";
                string query = "update [Procurement Orders] set [Supplier ID] ='" + supplierTb.Text + "', [Raw Material ID] = '" + rawmaterialTb.Text + "' , [Quantity] = '" + quantityTb.Text + "' where [Procurement Order ID]=" + orderTb.Text;
                OleDbConnection conn = new OleDbConnection(cnStr);
                OleDbCommand cmd = new OleDbCommand();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }    
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                conn.Close();
                

                //int index = Database.ProcurementOrders.IndexOf(order);

                //if (supplierCb.Text != "" && order.Supplier != supplierCb.Text)
                //{
                //    for (int i = 0; i < Database.SuppliersListName.Count; i++)
                //    {
                //        if (supplierCb.Text == Database.SuppliersListName[i])
                //        {
                //            Database.ProcurementOrders[index].supplierId = i;
                //            break;
                //        }
                //    }
                //}

                //if (destinationCb.Text != "" && order.Destination != destinationCb.Text)
                //{
                //    for (int i = 0; i < Database.WarehousesListName.Length; i++)
                //    {
                //        if (destinationCb.Text == Database.WarehousesListName[i])
                //        {
                //            Database.ProcurementOrders[index].destinationSiteId = i;
                //            break;
                //        }
                //    }
                //}

                //if (rawMaterialCb.Text != "" && order.rawMaterial != rawMaterialCb.Text)
                //{
                //    for (int i = 0; i < Database.RawMaterials.Length; i++)
                //    {
                //        if (rawMaterialCb.Text == Database.RawMaterials[i])
                //        {
                //            Database.ProcurementOrders[index].rawMaterialId = i;
                //            break;
                //        }
                //    }
                //}

                //if (quantityTb.Text != "" && order.Quantity.ToString() != quantityTb.Text)
                //{
                //    Database.ProcurementOrders[index].Quantity = int.Parse(quantityTb.Text);
                //}
            }
            else
            {

                string cnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../database/scm.accdb";
                string query = "insert into [Procurement Orders]([Supplier ID],[Raw Material ID],[Quantity]) values('" + supplierTb.Text + "','" + rawmaterialTb.Text + "','" + quantityTb.Text + "')";
                OleDbConnection conn = new OleDbConnection(cnStr);
                OleDbCommand cmd = new OleDbCommand();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                conn.Close();


                //int supplierId = order.supplierId;
                //int destinationId = order.destinationSiteId;
                //int rawMaterialId = order.rawMaterialId;
                //int quantity = order.Quantity;

                //for (int i = 0; i < Database.SuppliersListName.Count; i++)
                //{
                //    if (supplierCb.Text == Database.SuppliersListName[i])
                //    {
                //        supplierId = i;
                //        break;
                //    }
                //}

                //if (destinationCb.Text != "" && order.Destination != destinationCb.Text)
                //{
                //    for (int i = 0; i < Database.WarehousesListName.Length; i++)
                //    {
                //        if (destinationCb.Text == Database.WarehousesListName[i])
                //        {
                //            destinationId = i;
                //            break;
                //        }
                //    }
                //}

                //if (rawMaterialCb.Text != "" && order.rawMaterial != rawMaterialCb.Text)
                //{
                //    for (int i = 0; i < Database.RawMaterials.Length; i++)
                //    {
                //        if (rawMaterialCb.Text == Database.RawMaterials[i])
                //        {
                //            rawMaterialId = i;
                //            break;
                //        }
                //    }
                //}

                //if (quantityTb.Text != "" && order.Quantity.ToString() != quantityTb.Text)
                //{
                //    quantity = int.Parse(quantityTb.Text);
                //}

                //External.addNewProcurementOrder(supplierId, destinationId, rawMaterialId, quantity);
            }

            this.Close();
        }
    }
}
