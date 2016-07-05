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
    /// Interaction logic for ReportDetails.xaml
    /// </summary>
    public partial class ReportDetails : Window
    {
        public string reportDate;
        public ReportDetails(int counter)
        {
            InitializeComponent();
            reportDate = Database.Analytics[counter].date.ToString();
            Date.Text = reportDate;
            SupplierTime.Text = Database.Analytics[counter].supplierResponseTime.ToString();
            ProductionTime.Text = Database.Analytics[counter].productionTime.ToString();
            OrderFullfillment.Text = Database.Analytics[counter].orderFullfillmentTime.ToString();
            SCMCost.Text = Database.Analytics[counter].scmCost.ToString();
        }

    }
}
