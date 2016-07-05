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
    /// Interaction logic for DistributorOrdersDetails.xaml
    /// </summary>
    public partial class DistributorOrdersDetails : Window
    {
        int index;
        private string title = "";
        public DistributorOrdersDetails(int j, string title)
        {
            InitializeComponent();

            index = j;
            this.title = title;

            OrderIdTextBlock.Text = Database.DistributorShipping[index].OrderId.ToString();

            if (Database.DistributorShipping[index].StockTransferID != -1)
            {
                InternalTranferIdTextBox.Text = Database.DistributorShipping[index].StockTransferID.ToString();
            }

            if (Database.DistributorShipping[index].Status == "Shipped")
            {
                dShippingStatus.IsChecked = true;
            }

        }


        void updateDetails(object sender, RoutedEventArgs e)
        {
            if (InternalTranferIdTextBox.Text != "")
            {
                Database.DistributorShipping[index].StockTransferID = int.Parse(InternalTranferIdTextBox.Text);
            }
            if (dShippingStatus.IsChecked == true)
            {
                Database.DistributorShipping[index].Status = "Shipped";

            }
            if (dShippingStatus.IsChecked == false)
            {
                Database.DistributorShipping[index].Status = "Not Shipped";

            }

            this.Close();
        }
    }
}
