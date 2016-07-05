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
    /// Interaction logic for ShippingCompaniesDetail.xaml
    /// </summary>
    public partial class ShippingCompaniesDetail : Window
    {

        private ShippingCompany company;
        private string title = "";
        public ShippingCompaniesDetail(ShippingCompany item, string title)
        {
            InitializeComponent();

            company = item;
            this.title = title;

            CompanyNameTextBox.Text = item.CompanyName;
            ShippingMethodTextBox.Text = item.ShippingMethod;
            ContactInfoTextBox.Text = item.ContactInfo;
            ShippingRateTextBox.Text = item.ShippingRate.ToString();

            if (title == "Update Shipping Company")
            {
                addBtn.Content = "Update";
            }
            else
            {
                addBtn.Content = "Add";
            }
        }

        public void updateCompany(object sender, RoutedEventArgs e)
        {
            if (title == "Update Shipping Company")
            {
                int index = Database.ShippingCompanies.IndexOf(company);
                int i;
                for (i = 0; i < Database.ShippingCompanies.Count; i++)
                {
                    if (Database.ShippingCompanies[i].companyId == company.companyId)
                    {
                        break;
                    }
                }

                if (CompanyNameTextBox.Text != "")
                {
                    Database.ShippingCompanies[i].CompanyName = CompanyNameTextBox.Text;
                }

                if (ShippingMethodTextBox.Text != "")
                {
                    Database.ShippingCompanies[i].ShippingMethod = ShippingMethodTextBox.Text;
                }

                if (ContactInfoTextBox.Text != "")
                {
                    Database.ShippingCompanies[i].ContactInfo = ContactInfoTextBox.Text;
                }

                if (ShippingRateTextBox.Text != "")
                {
                    Database.ShippingCompanies[i].ShippingRate = double.Parse(ShippingRateTextBox.Text);
                }

            }

            else
            {
                string companyName = CompanyNameTextBox.Text;
                string shippingMethod = ShippingMethodTextBox.Text;
                string contactInfo = ContactInfoTextBox.Text;
                double shippingRate = double.Parse(ShippingRateTextBox.Text);
                MainWindow main = Application.Current.MainWindow as MainWindow;
                main.addNewShippingCompany(companyName, shippingMethod, contactInfo, shippingRate);
            }

            this.Close();
        }
    }
}
