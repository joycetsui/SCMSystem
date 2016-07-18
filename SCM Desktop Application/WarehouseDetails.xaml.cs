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
    /// Interaction logic for WarehouseDetails.xaml
    /// </summary>
    public partial class WarehouseDetails : Window
    {
        public WarehouseDetails(DataRowView item)
        {
            InitializeComponent();
            pageTitle.Content = item["Name"] + " Content Details";

            int warehouseId = int.Parse(item["Site ID"].ToString());

            DataTable dt = Inventory.getWarehouseRawMaterialsInventoryItems(warehouseId);
            rawMaterialsDataGrid.ItemsSource = dt.AsDataView();
            WIPDataGrid.ItemsSource = Inventory.getWarehouseWIPInventoryItems(warehouseId).AsDataView();
            finishedGoodsDataGrid.ItemsSource = Inventory.getWarehouseFGInventoryItems(warehouseId).AsDataView();
        }
    }
}
