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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SCM_Desktop_Application
{
    /// <summary>
    /// Interaction logic for ProcurementForecasts.xaml
    /// </summary>
    public partial class ProcurementForecasts : Page
    {
        public ProcurementForecasts()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {
            DataTable dt = Procurement.getProcurementForecasts();
            procurementForecastDataGrid.ItemsSource = dt.AsDataView();
        }

        public void updateForecats(object sender, RoutedEventArgs e)
        {
            ProcurementForecastItem[] newForecats = Procurement.getNewForecasts();
            for (int i = 0; i < newForecats.Length; i++)
            {
                Procurement.addNewProcurementForecast(newForecats[i].year, newForecats[i].week, newForecats[i].rawMaterialId, newForecats[i].quantity);
            }

            loadTable();
        }

        private int currentWeek = 25;

        public void placeOrders(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("Do you want to place orders from Week " + (currentWeek+1) + " to Week " + (currentWeek + 4), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                int beginForecast = currentWeek;
                int endForecast = currentWeek + 4;

                DataTable dt = Procurement.getProcurementForecastsForWeekRange(beginForecast, endForecast);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow item = dt.Rows[i];
                    int rawMaterialId = int.Parse(item["Raw Material ID"].ToString());
                    int supplierId = Procurement.getSupplierIdByRawMaterialId(rawMaterialId);
                    int quantity = int.Parse(item["Quantity"].ToString());
                    int destinationId = 1;

                    Procurement.addNewProcurementOrder(supplierId, destinationId, rawMaterialId, quantity);
                }

                currentWeek = endForecast;

                Procurement.deleteProcurementForecastsForWeekRange(beginForecast, endForecast);

                MessageBoxResult msg = MessageBox.Show("Created Orders from week " + (currentWeek+1) + " to week " + (currentWeek + 4) + ". Forecasts for these weeks will be removed from the Forecast table.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Question);

                loadTable();
            }
        }
    }
}
