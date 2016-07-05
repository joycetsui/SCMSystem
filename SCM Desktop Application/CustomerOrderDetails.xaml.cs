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
    /// Interaction logic for CustomerOrderDetails.xaml
    /// </summary>
    public partial class CustomerOrderDetails : Window
    {
        int index;
        private string title = "";
        public CustomerOrderDetails(int j, string title)
        {
            InitializeComponent();

            index = j;
            this.title = title;

            OrderIdTextBlock2.Text = Database.CustomerShipping[index].OrderId.ToString();

            TrackingTextBox.Text = Database.CustomerShipping[index].TrackingNumber.ToString();

            for (int i = 0; i < Database.ShippingCompanies.Count; i++)
            {
                ComboBoxItem cboxitem = new ComboBoxItem();
                cboxitem.Content = Database.ShippingCompanies[i].CompanyName;
                ShippingCompanyComboBox.Items.Add(cboxitem);
                if (Database.CustomerShipping[index].ShippingCompany == Database.ShippingCompanies[i].CompanyName)
                {
                    ShippingCompanyComboBox.SelectedIndex = i;
                }
            }

            DateShippedTextBox.Text = Database.CustomerShipping[index].DateShipped;

            if (Database.CustomerShipping[index].Status == "Shipped")
            {
                cShippingStatus.IsChecked = true;
            }

        }
    

        // Update database
        void updateDetails(object sender, RoutedEventArgs e)
        {
            
            if (TrackingTextBox.Text != "")
            {
                Database.CustomerShipping[index].TrackingNumber = int.Parse(TrackingTextBox.Text);
            }

            if (ShippingCompanyComboBox.Text != "")
            {
                int i;
                for (i = 0; i < Database.ShippingCompanies.Count; i++)
                {
                    if (ShippingCompanyComboBox.Text == Database.ShippingCompanies[i].CompanyName)
                    {
                        Database.CustomerShipping[index].ShippingCompanyId = Database.ShippingCompanies[i].companyId;
                        break;
                    }
                }


            }
            if (DateShippedTextBox.Text != "")
            {
                Database.CustomerShipping[index].DateShipped = DateShippedTextBox.Text;

            }
            if (cShippingStatus.IsChecked == true)
            {
                Database.CustomerShipping[index].Status = "Shipped";

            }
            if (cShippingStatus.IsChecked == false)
            {
                Database.CustomerShipping[index].Status = "Not Shipped";

            }

            this.Close();

        }
    }
}
