using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CustomerOrders.xaml
    /// </summary>
    public partial class CustomerOrders : Page
    {
        public CustomerOrders()
        {
            InitializeComponent();
            this.customerOrderDataGrid.MouseLeftButtonUp += new MouseButtonEventHandler(customerOrderDataGrid_MouseClick);
            for (int i = 0; i < Database.ShippingCompanies.Length; i++)
            {
                ComboBoxItem cboxitem = new ComboBoxItem();
                cboxitem.Content = Database.ShippingCompanies[i].CompanyName;
                ShippingCompanyComboBox.Items.Add(cboxitem);
            }


        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            customerOrderDataGrid.ItemsSource = Database.CustomerShipping;
        }

        private DataGridRow selectedRow;

        void customerOrderDataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            // iteratively traverse the visual tree
            while ((dep != null) &&
                    !(dep is DataGridRow))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;

            if (dep is DataGridRow)
            {
                DataGridRow row = dep as DataGridRow;
                selectedRow = row;
            }

            getDetails();
        }

        // Display order details
        void getDetails()
        {
            if (selectedRow != null)
            {
                int index = selectedRow.GetIndex();
                OrderIdTextBox.Text = Database.CustomerShipping[index].OrderId.ToString();

                if (TrackingTextBox.Text != null)
                {
                    TrackingTextBox.Text = Database.CustomerShipping[index].TrackingNumber.ToString();
                }
                if (ShippingCompanyComboBox.Text != null)
                {
                    ShippingCompanyComboBox.Text = Database.CustomerShipping[index].ShippingCompany;
                }
                if (DateShippedTextBox.Text != null)
                {
                    DateShippedTextBox.Text = Database.CustomerShipping[index].DateShipped;
                }
                if (Database.CustomerShipping[index].Status == "Shipped")
                {
                    ShippingStatus.IsChecked = true;
                }
                else
                {
                    ShippingStatus.IsChecked = false;
                }
                return;
            }            
            
        }

        // Update database
        void updateDetails(object sender, RoutedEventArgs e)
        {
            if (selectedRow != null)
            {
                int index = selectedRow.GetIndex();

                if (TrackingTextBox.Text != "")
                {
                    Database.CustomerShipping[index].TrackingNumber = int.Parse(TrackingTextBox.Text);       
                }

                if (ShippingCompanyComboBox.Text != "")
                {
                    int i;
                    for (i = 0; i < Database.ShippingCompanies.Length; i++)
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
                if (ShippingStatus.IsChecked == true)
                {
                    Database.CustomerShipping[index].Status = "Shipped";

                }
                if (ShippingStatus.IsChecked == false)
                        {
                    Database.CustomerShipping[index].Status = "Not Shipped";

                }                  
                
            }
            
        }
    }
}
