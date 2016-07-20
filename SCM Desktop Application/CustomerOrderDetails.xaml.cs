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
            }
            ShippingCompanyComboBox.Text = item["Shipping Company"].ToString();

            DateShippedTextBox.Text = item["Date Shipped"].ToString();

            if (item["Status"].ToString() == "Shipped")
            {
                cShippingStatus.IsChecked = true;
            }
        }
    

        // Update database
        void updateDetails(object sender, RoutedEventArgs e)
        {

            int id = int.Parse(row["Customer Order ID"].ToString());

            int tracking = int.Parse(TrackingTextBox.Text);

            int shippingCompanyId = Transportation.getShippingCompanyID(ShippingCompanyComboBox.Text);

            string date = DateTime.Parse(DateShippedTextBox.Text).ToLongDateString();

            string status;

            if (cShippingStatus.IsChecked == true)
            {
                status = "Shipped";
            }
            else
            {
                status = "Not Shipped";
                shippingCompanyId = 1;
                date = "";
            }

            ProductOrders.updateCustomerOrder(id, tracking, shippingCompanyId, date, status);

            this.Close();
        }
    }
}
