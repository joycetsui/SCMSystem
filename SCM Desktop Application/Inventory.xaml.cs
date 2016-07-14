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

            string query = "";

            if (header == "Raw Material Inventory")
            {
                query = "SELECT inv.[Raw Material ID] as [Raw Material ID],  r.[Type] as [Type], w.[Name] as [Site], inv.[Units] as [Units], inv.[Inbound Units] as [Inbound Units], inv.[Reorder Point] as [Reorder Point] " +
                            "FROM (([Raw Material Inventory] as inv " +
                            "INNER JOIN [Raw Materials] as r ON inv.[Raw Material ID] = r.[Raw Material ID]) " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID]);";
            }
            else if (header == "WIP Inventory")
            {
                query = "SELECT inv.[WIP Inventory ID] as [WIP Inventory ID],  inv.[Type] as [Type], w.[Name] as [Site], inv.[Quantity] as [Quantity], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM ([WIP Inventory] as inv " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID]);";
            }
            else
            {
                query = "SELECT inv.[FG Inventory ID] as [FG Inventory ID],  inv.[Type] as [Type], w.[Name] as [Site], inv.[Quantity] as [Quantity], inv.[Inbound Units] as [Inbound Units] " +
                            "FROM ([Finished Goods Inventory] as inv " +
                            "INNER JOIN [Warehouse] as w ON inv.[Site ID] = w.[Site ID]);";
            }

            DataTable dt = External.executeSelectQuery(query);
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
            details.Show();
        }
    }
}
