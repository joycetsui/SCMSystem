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
    /// Interaction logic for SuppliersList.xaml
    /// </summary>
    public partial class SuppliersList : Page
    {
        public SuppliersList()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            suppliersDataGrid.ItemsSource = Database.SuppliersList;
        }

        public void addNewSupplier(object sender, RoutedEventArgs e)
        {
            Supplier item = new Supplier();
            SupplierDetail detail = new SupplierDetail(item, "Create New Supplier");
            detail.Show();
        }

        public void editSupplier(object sender, RoutedEventArgs e)
        {
            Supplier item = (sender as Button).DataContext as Supplier ;
            SupplierDetail detail = new SupplierDetail(item, "Update Supplier Details");
            detail.Show();
        }

        public void deleteSupplier(object sender, RoutedEventArgs e)
        {
            Supplier item = (sender as Button).DataContext as Supplier;
            Database.SuppliersList.Remove(item);
            Database.SuppliersListName.Remove(item.Name);
        }
    }
}
