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
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for SupplierDetail.xaml
    /// </summary>
    public partial class SupplierDetail : Window
    {
        private DataRowView supplier;
        private string title = "";
        public SupplierDetail(DataRowView item, string title)
        {
            InitializeComponent();

            supplier = item;
            this.title = title;

            if (title == "Update Supplier Details")
            {
                updateBtn.Content = "Update";
                nametbx.Text = supplier["Company Name"].ToString();
                locationtbx.Text = supplier["Location"].ToString();
                paymentDetails.Text = supplier["Payment Details"].ToString();
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
            string details = paymentDetails.Text;

            if (title == "Update Supplier Details")
            {
                int id = int.Parse(supplier["Supplier ID"].ToString());
                Suppliers.updateSupplier(id, name, location, details);
            }
            else
            {
                Suppliers.addSupplier(name, location, details);
            }
            this.Close();
        }
    }
}