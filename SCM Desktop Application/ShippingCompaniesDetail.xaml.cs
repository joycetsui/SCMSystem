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
    /// Interaction logic for ShippingCompaniesDetail.xaml
    /// </summary>
    public partial class ShippingCompaniesDetail : Window
    {

        private DataRowView company;
        private string title = "";
        public ShippingCompaniesDetail(DataRowView item, string title)
        {
            InitializeComponent();

            company = item;
            this.title = title;

            if (title == "Update Shipping Company")
            {
                CompanyNameTextBox.Text = item["Company Name"].ToString();
                ContactInfoTextBox.Text = item["Contact Info"].ToString();
                ShippingRateTextBox.Text = item["Standard Shipping Rate"].ToString();

                addBtn.Content = "Update";
            }
            else
            {
                addBtn.Content = "Add";
            }
        }

        public void updateCompany(object sender, RoutedEventArgs e)
        {
            string name = CompanyNameTextBox.Text;
            string contactInfo = ContactInfoTextBox.Text;
            double shippingRate = Double.Parse(ShippingRateTextBox.Text);

            if (title == "Update Shipping Company")
            {
                int id = int.Parse(company["Shipping Company ID"].ToString());
                Shipping.updateShippingCompany(id, name, contactInfo, shippingRate);
            }
            else
            {
                Shipping.addShippingCompany(name, contactInfo, shippingRate);
            }

            this.Close();
        }
    }
}
