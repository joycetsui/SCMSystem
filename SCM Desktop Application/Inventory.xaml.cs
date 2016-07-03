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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        public Inventory()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            string header = MainWindow.selectedTabName;
            PageTitle.Content = header;

            if (header == "Raw Material Inventory")
            {
                inventoryDataGrid.DataContext = Database.RawMaterialsInventory;
            }
            else if (header == "WIP Inventory")
            {
                inventoryDataGrid.DataContext = Database.WIPInventory;
            }
            else
            {
                inventoryDataGrid.DataContext = Database.FinishedGoodsInventory;
            }
        }
    }
}
