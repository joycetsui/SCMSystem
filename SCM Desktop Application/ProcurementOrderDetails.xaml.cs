using DataAccess;
using System.Data;
using System.Windows;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for ProcurementOrderDetails.xaml
    /// </summary>
    public partial class ProcurementOrderDetails : Window
    {
        private DataRowView order;
        private string title = "";
        public ProcurementOrderDetails(DataRowView item, string title)
        {
            InitializeComponent();

            order = item;
            this.title = title;      

            pageTitle.Content = title;

            // Fill combo boxes
            DataTable dt = Procurement.getSuppliersNames();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                supplierCb.Items.Add(dt.Rows[i][0].ToString());
            }

            dt = Inventory.getWarehouseNames();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                destinationCb.Items.Add(dt.Rows[i][0].ToString());
            }

            dt = Inventory.getRawMaterialTypes();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rawMaterialCb.Items.Add(dt.Rows[i][0].ToString());
            }

            if (title == "Update Order Details")
            {
                orderTb.Text = item["Order ID"].ToString();
                supplierCb.Text = item["Supplier"].ToString();
                rawMaterialCb.Text = item["Raw Material"].ToString();
                quantityTb.Text = item["Quantity"].ToString();
                destinationCb.Text = item["Site"].ToString();

                updateBtn.Content = "Update";
            }
            else
            {
                supplierCb.Text = supplierCb.Items[0].ToString();
                destinationCb.Text = destinationCb.Items[0].ToString();
                rawMaterialCb.Text = rawMaterialCb.Items[0].ToString();

                updateBtn.Content = "Place Order";
            }
        }

        public void updateOrder(object sender, RoutedEventArgs e)
        {
            string orderIdStr = orderTb.Text;
            int orderId = 0;

            if (orderIdStr != "")
            {
                orderId = int.Parse(orderTb.Text);
            }

            int rawMaterialId = Inventory.getRawMaterialIdByType(rawMaterialCb.Text);
            int destinationId = Inventory.getWarehouseIdByName(destinationCb.Text);
            int supplierId = Procurement.getSupplierIdByName(supplierCb.Text);

            int quantity = 0;
            int.TryParse(quantityTb.Text, out quantity);

            if (title == "Update Order Details")
            {
                Procurement.updateProcurementOrder(orderId, supplierId, destinationId, rawMaterialId, quantity);
            }
            else
            {
                Procurement.addNewProcurementOrder(supplierId, destinationId, rawMaterialId, quantity);
            }

            this.Close();
        }
    }
}
