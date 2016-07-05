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
        public void getDetails()
        {
            if (selectedRow != null)
            {
                int index = selectedRow.GetIndex();
                dOrderIdTextBlock.Text = Database.DistributorShipping[index].OrderId.ToString();

                int i;
                for (i = 0; i < Database.InternalTransfer.Count; i++)
                {
                    if (Database.InternalTransfer[i].StockTransferId == Database.DistributorShipping[index].StockTransferID)
                    {
                        dShippingMethodTextBlock.Text = Database.InternalTransfer[i].DeliveryMethod;
                        dDepartureDateTextBox.Text = Database.InternalTransfer[index].DepartureDate;
                        return;
                    }
                }

                return;
            }

        }

        // Update database
        void openDetailsWindow(object sender, RoutedEventArgs e)
        {
            if (selectedRow != null)
            {
                int index = selectedRow.GetIndex();
                DistributorOrdersDetails detailsWindow = new DistributorOrdersDetails(index, "Update Distributor/Retail Order Details");
                detailsWindow.Show();

            }

        }
    }
}
