using DataAccess;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class InventoryList : Page
    {
        public InventoryList()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {
            string header = MainWindow.selectedTabName;
            PageTitle.Content = header;

            DataTable dt = new DataTable();

            if (header == "Raw Material Inventory")
            {
                dt = Inventory.getRawMaterials();
            }
            else if (header == "WIP Inventory")
            {
                dt = Inventory.getWIP();
            }
            else
            {
                dt = Inventory.getFinishedGoods();
            }

            inventoryDataGrid.ItemsSource = dt.AsDataView();
        }

        public void requestTransfer(object sender, RoutedEventArgs e)
        {
            string title = "";
            string header = MainWindow.selectedTabName;
            PageTitle.Content = header;

            if (header == "Raw Material Inventory")
            {
                title = "Raw Materials Transfer Request";
            }
            else if (header == "WIP Inventory")
            {
                title = "WIP Transfer Request";
            }
            else
            {
                title = "Finished Goods Transfer Request";
            }

            InventoryTransferDetails details = new InventoryTransferDetails(title);
            details.ShowDialog();
            loadTable();
        }
    }
}
