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
    /// Interaction logic for Warehouses.xaml
    /// </summary>
    public partial class Warehouses : Page
    {
        public Warehouses()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            warehousesDataGrid.ItemsSource = Database.WarehouseList;
        }

        public void row_DoubleClick(object sender, RoutedEventArgs e)
        {
            Warehouse item = (sender as DataGridRow).DataContext as Warehouse;
            WarehouseDetails detailsWindow = new WarehouseDetails(item);
            detailsWindow.Show();
        }
    }
}
