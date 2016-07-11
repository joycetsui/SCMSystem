using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<InventoryItem> data;
        private string[] options;

        public InventoryTransferDetails(string title)
        {
            InitializeComponent();
            this.title = title;
            pageTitle.Content = title;

            if (title == "Raw Materials Transfer Request")
            {
                data = Database.RawMaterialsInventory;
                options = Database.RawMaterials;
            }
            else if (title == "WIP Transfer Request")
            {
                data = Database.WIPInventory;
                options = Database.ProductsName;
            }
            else
            {
                data = Database.FinishedGoodsInventory;
                options = Database.ProductsName;
            }

            for (int i = 0; i < Database.WarehousesListName.Length; i++)
            {
                ComboBoxItem cb = new ComboBoxItem();
                cb.Content = Database.WarehousesListName[i];
                destinationToCb.Items.Add(cb);
            }

            for (int i = 0; i < Database.WarehousesListName.Length; i++)
            {
                ComboBoxItem cb = new ComboBoxItem();
                cb.Content = Database.WarehousesListName[i];
                destinationFromCb.Items.Add(cb);
            }

            for (int i = 0; i < options.Length; i++)
            {
                ComboBoxItem cb = new ComboBoxItem();
                cb.Content = options[i];
                rawMaterialCb.Items.Add(cb);
            }

            for (int i = 0; i < Database.InternalShippingMethod.Length; i++)
            {
                ComboBoxItem cb = new ComboBoxItem();
                cb.Content = Database.InternalShippingMethod[i];
                transferMethodCB.Items.Add(cb);
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
                int rawMaterialId = 0;
                int warehouseFromId = 0;
                int warehouseToId = 0;
                int transferMethodId = 0;
                string departureDate = "July 4, 2016";

                for (int i = 0; i < options.Length; i++)
                {
                    if (options[i] == rawMaterialCb.Text)
                    {
                        rawMaterialId = i;
                        break;
                    }
                }

                for (int i = 0; i < Database.WarehousesListName.Length; i++)
                {
                    if (Database.WarehousesListName[i] == destinationFromCb.Text)
                    {
                        warehouseFromId = i;
                        break;
                    }
                }

                for (int i = 0; i < Database.WarehousesListName.Length; i++)
                {
                    if (Database.WarehousesListName[i] == destinationToCb.Text)
                    {
                        warehouseToId = i;
                        break;
                    }
                }

                for (int i = 0; i < Database.InternalShippingMethod.Length; i++)
                {
                    if (Database.InternalShippingMethod[i] == transferMethodCB.Text)
                    {
                        transferMethodId = i;
                        break;
                    }
                }

                departureDate = departureDateTb.Text;
                DateTime departureDateTime = Convert.ToDateTime(departureDate);
                DateTime arrivalDateTime = departureDateTime.AddDays(Database.InternalShippingMethodTransferTime[transferMethodId]);

                bool found = false;
                bool itemAlreadyInWarehouse = false;

                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].id == rawMaterialId && data[i].siteId == warehouseFromId)
                    {
                        found = true;
                        if (data[i].unitsOnHand >= quantity)
                        {
                            data[i].unitsOnHand -= quantity;
                            InventoryItem newItem;

                            for (int j = 0; j < data.Count; j++)
                            {
                                if (data[j].id == rawMaterialId && data[j].siteId == warehouseToId)
                                {
                                    itemAlreadyInWarehouse = true;
                                    data[j].InboundUnits += quantity;
                                    External.addNewInternalTransferOrder(warehouseFromId, warehouseToId, transferMethodId, departureDate, arrivalDateTime.ToLongDateString(), rawMaterialId, quantity);
                                    break;
                                }
                            }

                            if (!itemAlreadyInWarehouse)
                            {
                                if (title == "WIP Transfer Request")
                                {
                                    newItem = new InventoryItem { id = rawMaterialId, siteId = warehouseToId, unitsOnHand = 0, InboundUnits = quantity, unitsOnOrder = 0, reorderPoint = data[i].reorderPoint, type = "WIP" };
                                }
                                else if (title == "Finished Goods Transfer Request")
                                {
                                    newItem = new InventoryItem { id = rawMaterialId, siteId = warehouseToId, unitsOnHand = 0, InboundUnits = quantity, unitsOnOrder = 0, reorderPoint = data[i].reorderPoint, type = "Finished Goods" };
                                }
                                else
                                {
                                    newItem = new InventoryItem { id = rawMaterialId, siteId = warehouseToId, unitsOnHand = 0, InboundUnits = quantity, unitsOnOrder = 0, reorderPoint = data[i].reorderPoint };
                                }

                                data.Add(newItem);
                                External.addNewInternalTransferOrder(warehouseFromId, warehouseToId, transferMethodId, departureDate, arrivalDateTime.ToLongDateString(), rawMaterialId, quantity);
                            }
                            
                            this.Close();
                        }
                        else
                        {
                            MessageBoxResult error = MessageBox.Show("Not enough " + rawMaterialCb.Text + " in " + destinationFromCb.Text + " available to transfer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        break;
                    }
                }

                if (!found) {
                    MessageBoxResult error = MessageBox.Show("There is no " + rawMaterialCb.Text + " in " + destinationFromCb.Text + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
