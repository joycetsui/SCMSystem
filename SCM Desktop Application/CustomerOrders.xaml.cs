using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        string cnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../database/scm.accdb";
        
        public CustomerOrders()
        {
            InitializeComponent();
            this.customerOrderDataGrid.MouseLeftButtonUp += new MouseButtonEventHandler(customerOrderDataGrid_MouseClick);
            
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            //string query = "Select [Product Order ID] [Customer ID] [Status] [Origin Warehouse ID] [Destination] from [Customer Shipping]";
            //https://www.microsoft.com/en-us/download/details.aspx?id=13255
            //OleDbConnection conn = new OleDbConnection(cnStr);
            //OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            //DataSet ds = new DataSet();
            //// if error at next line, download this (32 bit) https://www.microsoft.com/en-us/download/details.aspx?id=13255
            //adapter.Fill(ds);
            //customerOrderDataGrid.ItemsSource = ds.Tables[0].DefaultView;
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
                OrderIdTextBlock.Text = Database.CustomerShipping[index].OrderId.ToString();

                if (TrackingTextBlock.Text != null)
                {
                    TrackingTextBlock.Text = Database.CustomerShipping[index].TrackingNumber.ToString();
                }
                if (ShippingCompanyTextBlock.Text != null)
                {
                    ShippingCompanyTextBlock.Text = Database.CustomerShipping[index].ShippingCompany;
                }
                if (DateShippedTextBlock.Text != null)
                {
                    DateShippedTextBlock.Text = Database.CustomerShipping[index].DateShipped;
                }
            }                    
        }

        public void openDetailsWindow(object sender, RoutedEventArgs e)
        {
            if (selectedRow != null)
            {
                int index = selectedRow.GetIndex();
                CustomerOrderDetails detailsWindow = new CustomerOrderDetails(index, "Update Customer Order Details");
                detailsWindow.Show();

            }
        }
    }
}
