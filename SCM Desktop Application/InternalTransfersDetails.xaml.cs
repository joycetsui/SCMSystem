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
    /// Interaction logic for InternalTransfersDetails.xaml
    /// </summary>
    public partial class InternalTransfersDetails : Window
    {

        int index;
        private string title = "";
        public InternalTransfersDetails(int j, string title)
        {
            InitializeComponent();

            index = j;
            this.title = title;

            TransferIdTextBlock.Text = Database.InternalTransfer[index].StockTransferId.ToString();

            for (int i = 0; i < Database.InternalShippingMethod.Length; i++)
            {
                ComboBoxItem cboxitem = new ComboBoxItem();
                cboxitem.Content = Database.InternalShippingMethod[i];
                DeliveryMethodComboBox.Items.Add(cboxitem);
                if (Database.InternalTransfer[index].DeliveryMethod == Database.InternalShippingMethod[i])
                {
                    DeliveryMethodComboBox.SelectedIndex = i;
                }
            }

            if (Database.InternalTransfer[index].TotalCost != -1)
            {
                TotalCostTextBox.Text = Database.InternalTransfer[index].TotalCost.ToString();
            }

            if (Database.InternalTransfer[index].DepartureDate != null)
            {
                DepartureDate.Text = Database.InternalTransfer[index].DepartureDate;
            }

            if (Database.InternalTransfer[index].ArrivalDate != null)
            {
                ArrivalDate.Text = Database.InternalTransfer[index].ArrivalDate;
            }
        }

        void updateTransferDetails(object sender, RoutedEventArgs e)
        {

            if (DeliveryMethodComboBox.Text != "")
            {
                int i;
                for (i = 0; i < Database.InternalShippingMethod.Length; i++)
                {
                    if (DeliveryMethodComboBox.Text == Database.InternalShippingMethod[i])
                    {
                        Database.InternalTransfer[index].DeliveryMethodID = i;
                        break;
                    }
                }
            }

            if (TotalCostTextBox.Text != null)
            {
                Database.InternalTransfer[index].TotalCost = double.Parse(TotalCostTextBox.Text);
            }

            if (DepartureDate.Text != null)
            {
                Database.InternalTransfer[index].DepartureDate = DepartureDate.Text;
            }

            if (ArrivalDate.Text != null)
            {
                Database.InternalTransfer[index].ArrivalDate = ArrivalDate.Text;
            }

            this.Close();
        }
    }
}
