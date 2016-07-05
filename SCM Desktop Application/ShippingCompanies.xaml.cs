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
    /// Interaction logic for ShippingCompanies.xaml
    /// </summary>
    public partial class ShippingCompanies : Page
    {
        public ShippingCompanies()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            shippingDataGrid.ItemsSource = Database.ShippingCompanies;
        }

        public void addNewShipping(object sender, RoutedEventArgs e)
        {
            ShippingCompany item = new ShippingCompany();
            ShippingCompaniesDetail detailsWindow = new ShippingCompaniesDetail(item, "Add New Shipping Company");
            detailsWindow.Show();
        }

        public void editShipping(object sender, RoutedEventArgs e)
        {
            ShippingCompany item = (sender as Button).DataContext as ShippingCompany;
            ShippingCompaniesDetail detailsWindow = new ShippingCompaniesDetail(item, "Update Shipping Company");
            detailsWindow.Show();
        }

        public void deleteShipping(object sender, RoutedEventArgs e)
        {
            ShippingCompany item = (sender as Button).DataContext as ShippingCompany;
            Database.ShippingCompanies.Remove(item);
        }
    }
}
