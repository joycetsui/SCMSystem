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
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for ProcurementOrderDetails.xaml
    /// </summary>
    public partial class ProcurementOrderDetails : Window
    {
        private ProcurementOrderItem order;
        private string title = "";
        public ProcurementOrderDetails(ProcurementOrderItem item, string title)
        {
            InitializeComponent();

            order = item;
            this.title = title;

            for (int i = 0; i < Database.WarehousesListName.Length; i++)
            {
                ComboBoxItem cb = new ComboBoxItem();
                cb.Content = Database.WarehousesListName[i];
                destinationCb.Items.Add(cb);
            }

            for (int i = 0; i < Database.SuppliersListName.Length; i++)
            {
                ComboBoxItem cb = new ComboBoxItem();
                cb.Content = Database.SuppliersListName[i];
                supplierCb.Items.Add(cb);
            }

            for (int i = 0; i < Database.RawMaterials.Length; i++)
            {
                ComboBoxItem cb = new ComboBoxItem();
                cb.Content = Database.RawMaterials[i];
                rawMaterialCb.Items.Add(cb);
            }

            supplierCb.Text = item.Supplier;
            destinationCb.Text = item.Destination;
            rawMaterialCb.Text = item.rawMaterial;
            quantityTb.Text = item.Quantity.ToString();
            pageTitle.Content = title;

            if (title == "Update Order Details")
            {
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
                int index = Database.ProcurementOrders.IndexOf(order);

                if (supplierCb.Text != "" && order.Supplier != supplierCb.Text)
                {
                    for (int i = 0; i < Database.SuppliersListName.Length; i++)
                    {
                        if (supplierCb.Text == Database.SuppliersListName[i])
                        {
                            Database.ProcurementOrders[index].supplierId = i;
                            break;
                        }
                    }
                }

                if (destinationCb.Text != "" && order.Destination != destinationCb.Text)
                {
                    for (int i = 0; i < Database.WarehousesListName.Length; i++)
                    {
                        if (destinationCb.Text == Database.WarehousesListName[i])
                        {
                            Database.ProcurementOrders[index].destinationSiteId = i;
                            break;
                        }
                    }
                }

                if (rawMaterialCb.Text != "" && order.rawMaterial != rawMaterialCb.Text)
                {
                    for (int i = 0; i < Database.RawMaterials.Length; i++)
                    {
                        if (rawMaterialCb.Text == Database.RawMaterials[i])
                        {
                            Database.ProcurementOrders[index].rawMaterialId = i;
                            break;
                        }
                    }
                }

                if (quantityTb.Text != "" && order.Quantity.ToString() != quantityTb.Text)
                {
                    Database.ProcurementOrders[index].Quantity = int.Parse(quantityTb.Text);
                }
            }
            else
            {
                int supplierId = order.supplierId;
                int destinationId = order.destinationSiteId;
                int rawMaterialId = order.rawMaterialId;
                int quantity = order.Quantity;

                for (int i = 0; i < Database.SuppliersListName.Length; i++)
                {
                    if (supplierCb.Text == Database.SuppliersListName[i])
                    {
                        supplierId = i;
                        break;
                    }
                }

                if (destinationCb.Text != "" && order.Destination != destinationCb.Text)
                {
                    for (int i = 0; i < Database.WarehousesListName.Length; i++)
                    {
                        if (destinationCb.Text == Database.WarehousesListName[i])
                        {
                            destinationId = i;
                            break;
                        }
                    }
                }

                if (rawMaterialCb.Text != "" && order.rawMaterial != rawMaterialCb.Text)
                {
                    for (int i = 0; i < Database.RawMaterials.Length; i++)
                    {
                        if (rawMaterialCb.Text == Database.RawMaterials[i])
                        {
                            rawMaterialId = i;
                            break;
                        }
                    }
                }

                if (quantityTb.Text != "" && order.Quantity.ToString() != quantityTb.Text)
                {
                    quantity = int.Parse(quantityTb.Text);
                }

                MainWindow main = Application.Current.MainWindow as MainWindow;
                main.addNewProcurementOrder(supplierId, destinationId, rawMaterialId, quantity);
            }

            this.Close();
        }
    }
}
