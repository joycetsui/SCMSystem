using DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for InventoryTransferDetails.xaml
    /// </summary>
    public partial class InventoryTransferDetails : Window
    {
        private string title = "";
        private DataTable data;
        private DataTable options;

        public InventoryTransferDetails(string title)
        {
            InitializeComponent();
            this.title = title;
            pageTitle.Content = title;

            if (title == "Raw Materials Transfer Request")
            {
                data = Inventory.getRawMaterials();
                options = Inventory.getRawMaterialTypes();
            }
            else if (title == "WIP Transfer Request")
            {
                data = Inventory.getWIP();
                options = Inventory.getWIPTypes();
            }
            else
            {
                data = Inventory.getFinishedGoods();
                options = Inventory.getProductTypes();
            }

            DataTable dt = Inventory.getWarehouseNames();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                destinationToCb.Items.Add(dt.Rows[i]["Name"].ToString());
            }
            destinationToCb.SelectedIndex = 1;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                destinationFromCb.Items.Add(dt.Rows[i]["Name"].ToString());
            }
            destinationFromCb.SelectedIndex = 0;

            for (int i = 0; i < options.Rows.Count; i++)
            {
                rawMaterialCb.Items.Add(options.Rows[i]["Type"].ToString());
            }
            rawMaterialCb.SelectedIndex = 0;

            dt = Transportation.getInternalShippingMethods();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                transferMethodCB.Items.Add(dt.Rows[i]["Method"].ToString());
            }

            transferMethodCB.SelectedIndex = 0;
        }

        public void requestTransfer(object sender, RoutedEventArgs e)
        {
            if (destinationFromCb.Text == destinationToCb.Text)
            {
                MessageBoxResult error = MessageBox.Show("Cannot request transfer to same warehouse.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int quantity = int.Parse(quantityTb.Text);
                string departureDate = "July 4, 2016";

                string inventoryType = "FG";
                if (title == "Raw Materials Transfer Request")
                {
                    inventoryType = "Raw Material";
                }
                else if (title == "WIP Transfer Request")
                {
                    inventoryType = "WIP";
                }

                int materialId = Inventory.getInventoryItemIdByType(rawMaterialCb.Text, inventoryType);
                int warehouseFromId = Inventory.getWarehouseIdByName(destinationFromCb.Text);
                int warehouseToId = Inventory.getWarehouseIdByName(destinationToCb.Text);
                int transferMethodId = Transportation.getInternalShippingMethodIdByType(transferMethodCB.Text);
                departureDate = departureDateTb.Text;
                
                bool found = false;

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    if (int.Parse(data.Rows[i]["Material ID"].ToString()) == materialId && data.Rows[i]["Site ID"].ToString() == warehouseFromId.ToString())
                    {
                        found = true;
                        int units = int.Parse(data.Rows[i]["Units"].ToString());
                        if (units >= quantity)
                        {
                            int newUnitsAmountFromWarehouse = units - quantity;
                            Transportation.createNewInventoryInternalTransferOrder(inventoryType, warehouseFromId, warehouseToId, transferMethodId, departureDate, materialId, rawMaterialCb.Text, quantity, newUnitsAmountFromWarehouse);
                            this.Close();
                        }
                        else
                        {
                            MessageBoxResult error = MessageBox.Show("Not enough " + rawMaterialCb.Text + " in " + destinationFromCb.Text + " available to transfer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;
                    }
                }

                if (!found)
                {
                    MessageBoxResult error = MessageBox.Show("There is no " + rawMaterialCb.Text + " in " + destinationFromCb.Text + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
