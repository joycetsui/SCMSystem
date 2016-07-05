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
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for SupplierDetail.xaml
    /// </summary>
    public partial class SupplierDetail : Window
    {
        private Supplier supplier;
        private string title = "";
        public SupplierDetail(Supplier item, string title)
        {
            InitializeComponent();

            supplier = item;
            this.title = title;

            if (title == "Update Supplier Details")
            {
                updateBtn.Content = "Update";
                nametbx.Text = supplier.Name;
                locationtbx.Text = supplier.Location;
            }
            else
            {
                updateBtn.Content = "Add Supplier";
            }
        }

        public void updateSupplier(object sender, RoutedEventArgs e)
        {
            string name = nametbx.Text;
            string location = locationtbx.Text;
            if (title == "Update Supplier Details")
            {
                int index = Database.SuppliersList.IndexOf(supplier);
                if (name != "")
                {
                    int nameIndex = Database.SuppliersListName.IndexOf(supplier.Name);
                    Database.SuppliersListName[nameIndex] = name;
                }
                else
                {
                    name = supplier.Name;
                }
                if (location == "")
                {
                    location = supplier.Location;
                }
                Supplier newItem = new Supplier {SupplierId = supplier.SupplierId, Name = name, Location = location};
                Database.SuppliersList[index] = newItem;
            }
            else
            {
                Database.SuppliersListName.Add(name);
                MainWindow main = Application.Current.MainWindow as MainWindow;
                main.addNewSupplier(name, location);
            }
            this.Close();
        }
    }
}