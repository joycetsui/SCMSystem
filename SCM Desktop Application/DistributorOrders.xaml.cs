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
    /// Interaction logic for DistributorOrders.xaml
    /// </summary>
    public partial class DistributorOrders : Page
    {
        public DistributorOrders()
        {
            InitializeComponent();
            this.distributorOrderDataGrid.MouseLeftButtonUp += new MouseButtonEventHandler(distributorOrderDataGrid_MouseClick);
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            distributorOrderDataGrid.ItemsSource = Database.DistributorShipping;
        }

        private DataGridRow selectedRow;

        void distributorOrderDataGrid_MouseClick(object sender, MouseEventArgs e)
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
                OrderIdTextBlock.Text = Database.DistributorShipping[index].OrderId.ToString();

                int i;
                for (i = 0; i < Database.InternalTransfer.Count; i++)
                {
                    if (Database.InternalTransfer[i].StockTransferId == Database.DistributorShipping[index].StockTransferID)
                    {
                        ShippingMethodTextBlock.Text = Database.InternalTransfer[i].DeliveryMethod;
                        DepartureDateTextBox.Text = Database.InternalTransfer[index].DepartureDate;
                        return;
                    }
                }

                if (Database.DistributorShipping[index].Status == "Shipped")
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

                //if (ShippingCompanyComboBox.Text != "")
                //{
                //    int i;
                //    for (i = 0; i < Database.ShippingCompanies.Count; i++)
                //    {
                //        if (ShippingCompanyComboBox.Text == Database.ShippingCompanies[i].CompanyName)
                //        {
                //            Database.CustomerShipping[index].ShippingCompanyId = Database.ShippingCompanies[i].companyId;
                //            break;
                //        }
                //    }


                //}
                //if (DateShippedTextBox.Text != "")
                //{
                //    Database.CustomerShipping[index].DateShipped = DateShippedTextBox.Text;

                //}
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
