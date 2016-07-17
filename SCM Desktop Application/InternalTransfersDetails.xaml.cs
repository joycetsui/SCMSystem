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
    /// Interaction logic for InternalTransfersDetails.xaml
    /// </summary>
    public partial class InternalTransfersDetails : Window
    {

        DataRowView item;
        private string title = "";
        public InternalTransfersDetails(DataRowView item, string title)
        {
            InitializeComponent();

            this.item = item;
            this.title = title;

            TransferIdTextBlock.Text = item["Stock Transfer ID"].ToString();

            DataTable dt = Transportation.getInternalShippingMethods();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeliveryMethodComboBox.Items.Add(dt.Rows[i]["Method"].ToString());
            }

            DeliveryMethodComboBox.Text = item["Method"].ToString();
            TotalCostTextBox.Text = item["Cost"].ToString();
            DepartureDate.Text = item["Departure Date"].ToString();
            ArrivalDate.Text = item["Arrival Date"].ToString();
        }

        void updateTransferDetails(object sender, RoutedEventArgs e)
        {
            int deliveryMethodId = Transportation.getInternalShippingMethodIdByType(DeliveryMethodComboBox.Text);
            double totalCost = Double.Parse(TotalCostTextBox.Text);
            string departureDate = DepartureDate.Text;
            string arrivalDate = ArrivalDate.Text;
            int stockId = int.Parse(TransferIdTextBlock.Text);

            Transportation.updateInternalTransferOrder(stockId, deliveryMethodId, totalCost, departureDate, arrivalDate);

            this.Close();
        }
    }
}
