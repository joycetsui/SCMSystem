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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public Reports()
        {
            InitializeComponent();
            loadTable(); 
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {
            DataTable dt = Analytics.getReports();

            ReportList.ItemsSource = dt.AsDataView();
        }

        private void generateReport(object sender, RoutedEventArgs e)
        {
            //Generate Reports
            Analytics.createNewReport();
            loadTable();
        }

        public void openReport(object sender, MouseButtonEventArgs e)
        {
            DataRowView item = (DataRowView)ReportList.SelectedItems[0];
            ReportDetails details = new ReportDetails(item);
            details.ShowDialog();
        }

    }
}
