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

        private int currentWeek = 3;

        public void placeOrders(object sender, RoutedEventArgs e)
        {
            int beginForecast = currentWeek;
            int endForecast = currentWeek + 4;

            MainWindow main = Application.Current.MainWindow as MainWindow;

            for (int i = 0; i < Database.ProcurementForecasts.Count; i++)
            {
                ProcurementForecastItem item = Database.ProcurementForecasts[i];
                if (item.Week >= beginForecast && item.Week <= endForecast)
                {
                    int supplierId = Database.RawMaterialsList[item.rawMaterialId].supplierId;
                    double unitCost = Database.RawMaterialsList[item.rawMaterialId].cost;
                    double totalCost = item.Quantity * unitCost;

                    main.addNewProcurementOrder(supplierId, 0, item.rawMaterialId, totalCost);
                }
                {

                }
            }
        }
    }
}
