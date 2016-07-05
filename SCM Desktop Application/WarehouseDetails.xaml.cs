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
    /// Interaction logic for WarehouseDetails.xaml
    /// </summary>
    public partial class WarehouseDetails : Window
    {
        public WarehouseDetails(Warehouse item)
        {
            InitializeComponent();

            pageTitle.Content = item.Name + " Content Details";

            rawMaterialsDataGrid.ItemsSource = item.rawMaterials;
            WIPDataGrid.ItemsSource = item.WIPs;
            finishedGoodsDataGrid.ItemsSource = item.FinishedGoods;
        }
    }
}
