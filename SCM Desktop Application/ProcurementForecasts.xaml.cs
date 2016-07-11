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
            procurementForecastDataGrid.DataContext = Database.ProcurementForecasts;
        }

        public void updateForecats(object sender, RoutedEventArgs e)
        {
            ProcurementForecastItem[] newForecats = External.getNewForecasts();
            for (int i = 0; i < newForecats.Length; i++)
            {
                Database.ProcurementForecasts.Add(newForecats[i]);
            }
        }

        private int currentWeek = 0;

        public void placeOrders(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show("Do you want to place orders from Week " + (currentWeek+1) + " to Week " + (currentWeek + 4), "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                int beginForecast = currentWeek;
                int endForecast = currentWeek + 4;

                for (int i = 0; i < Database.ProcurementForecasts.Count;)
                {
                    ProcurementForecastItem item = Database.ProcurementForecasts[i];
                    if (item.Week > beginForecast && item.Week <= endForecast)
                    {
                        int supplierId = Database.RawMaterialsList[item.rawMaterialId].supplierId;
                        int quantity = item.Quantity;
                        double unitCost = Database.RawMaterialsList[item.rawMaterialId].cost;
                        double totalCost = item.Quantity * unitCost;

                        External.addNewProcurementOrder(supplierId, 0, item.rawMaterialId, quantity);
                        Database.ProcurementForecasts.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                MessageBoxResult msg = MessageBox.Show("Created Orders from week " + (currentWeek+1) + " to week " + (currentWeek + 4) + ". Forecasts for these weeks will be removed from the Forecast table.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }
    }
}
