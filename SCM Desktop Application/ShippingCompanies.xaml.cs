using DataAccess;
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
            loadTable();
        }

        public void loadTable()
        {
            DataTable dt = Shipping.getShippingCompanies();
            shippingDataGrid.ItemsSource = dt.AsDataView();
        }

        public void addNewShipping(object sender, RoutedEventArgs e)
        {
            ShippingCompaniesDetail detailsWindow = new ShippingCompaniesDetail(null, "Add New Shipping Company");
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void editShipping(object sender, RoutedEventArgs e)
        {
            DataRowView item = (DataRowView)shippingDataGrid.SelectedItems[0];
            ShippingCompaniesDetail detailsWindow = new ShippingCompaniesDetail(item, "Update Shipping Company");
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void deleteShipping(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)shippingDataGrid.SelectedItems[0];
            int id = int.Parse(row["Shipping Company ID"].ToString());
            Shipping.deleteShippingCompany(id);
            loadTable();
        }
    }
}
