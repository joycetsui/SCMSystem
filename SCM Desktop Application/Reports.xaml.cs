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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Page
    {
        public int reportCounter;
        public List<ReportDetails> reportList = new List<ReportDetails>();
        public Reports()
        {
            InitializeComponent();
            reportCounter = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Generate Reports
            ReportDetails details = new ReportDetails(reportCounter);
            details.Show();
            reportCounter++;
            reportList.Add(details);
            String date = details.reportDate;
            ReportList.Items.Add(date);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ReportList.SelectedIndex;
            ReportDetails details = new ReportDetails(index);
            details.Show();
        }

    }
}
