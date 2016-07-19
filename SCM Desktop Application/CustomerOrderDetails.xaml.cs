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
    /// Interaction logic for CustomerOrderDetails.xaml
    /// </summary>
    public partial class CustomerOrderDetails : Window
    {
        private DataRowView row;

        public CustomerOrderDetails(DataRowView item)
        {
            InitializeComponent();

            row = item;

            pageTitle.Content = "Update Customer Order Details";

            OrderIdTextBlock2.Text = item["Customer Order ID"].ToString();

            TrackingTextBox.Text = item["Tracking Number"].ToString();

            DataTable dt = Transportation.getShippingCompanyNames();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShippingCompanyComboBox.Items.Add(dt.Rows[i][0].ToString());
                //if (Database.CustomerShipping[index].ShippingCompany == Database.ShippingCompanies[i].CompanyName)
                //{
                //    ShippingCompanyComboBox.SelectedIndex = i;
                //}
            }

            DateShippedTextBox.Text = Transportation.getCustomerDateShipped(int.Parse(item["Customer Order ID"].ToString()));

            if (Transportation.getCustomerShippingStatus(int.Parse(item["Customer Order ID"].ToString())) == "Shipped")
            {
                cShippingStatus.IsChecked = true;
            }

        }
    

        // Update database
        void updateDetails(object sender, RoutedEventArgs e)
        {

            int id = int.Parse(row["Customer Order ID"].ToString());

            int tracking = int.Parse(TrackingTextBox.Text);

            int ship = Transportation.getShippingCompanyID(ShippingCompanyComboBox.Text);

            DateTime date = DateTime.Parse(DateShippedTextBox.Text);


            string status;
            if (cShippingStatus.IsChecked == true)
            {
                status = "Shipped";

            }
            else
            {
               status = "Not Shipped";

            }

            ProductOrders.updateCustomerOrder(id, tracking, ship, date, status);

            this.Close();

        }
    }
}
