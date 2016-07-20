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
            loadTable();
        }

        public void loadTable()
        {
            DataTable dt = Suppliers.getSupplier();
            suppliersDataGrid.ItemsSource = dt.AsDataView();
        }

        public void addNewSupplier(object sender, RoutedEventArgs e)
        {
            SupplierDetail detailsWindow = new SupplierDetail(null, "Create New Supplier");
            detailsWindow.ShowDialog();
            loadTable();
        }

        public void editSupplier(object sender, RoutedEventArgs e)
        {
            DataRowView item = (DataRowView)suppliersDataGrid.SelectedItems[0];
            SupplierDetail detail = new SupplierDetail(item, "Update Supplier Details");
            detail.ShowDialog();
            loadTable();
        }

        public void deleteSupplier(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)suppliersDataGrid.SelectedItems[0];
            int id = int.Parse(row["Supplier ID"].ToString());
            Suppliers.deleteSupplier(id);
            loadTable();
        }
    }
}
