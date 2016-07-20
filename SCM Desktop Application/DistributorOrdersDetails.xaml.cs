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
    /// Interaction logic for DistributorOrdersDetails.xaml
    /// </summary>
    public partial class DistributorOrdersDetails : Window
    {
        private DataRowView row;
        public DistributorOrdersDetails(DataRowView item)
        {
            InitializeComponent();

            row = item;

            pageTitle.Content = "Update Distributor/Retailer Order Details";

            OrderIdTextBlock.Text = item["Distributor Order ID"].ToString();

            DataTable dt = Transportation.getInternalShippingMethods();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                InternalTransportationComboBox.Items.Add(dt.Rows[i][0].ToString());
            }
            InternalTransportationComboBox.Text = item["Method"].ToString();

            DateShippedTextBox.Text = item["Date Shipped"].ToString();

            if (item["Status"].ToString() == "Shipped")
            {
                dShippingStatus.IsChecked = true;
            }
        }


        void updateDetails(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(row["Distributor Order ID"].ToString());
            int stockTransferId = Transportation.getInternalShippingMethodIdByType(InternalTransportationComboBox.Text);

            string date = DateTime.Parse(DateShippedTextBox.Text).ToLongDateString();

            string status;
            if (dShippingStatus.IsChecked == true)
            {
                status = "Shipped";
            }
            else
            {
                status = "Not Shipped";
                stockTransferId = 1;
                date = "";
            }

            ProductOrders.updateRetailerOrder(id, stockTransferId, status, date);

            this.Close();
        }
    }
}
