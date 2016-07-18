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
    /// Interaction logic for ReportDetails.xaml
    /// </summary>
    public partial class ReportDetails : Window
    {
        public string reportDate;
        public ReportDetails(DataRowView report)
        {
            InitializeComponent();
            Date.Text = report["Date"].ToString();
            SupplierTime.Text = report["Supplier Response Time"].ToString();
            ProductionTime.Text = report["Production Time"].ToString();
            OrderFullfillment.Text = report["Order Fullfillment Time"].ToString();
            SCMCost.Text = report["Supply Chain Cost"].ToString();
        }

    }
}
