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
    /// Interaction logic for ProcurementOrders.xaml
    /// </summary>
    public partial class ProcurementOrders : Page
    {
        public ICommand Edit
        {
            get
            {
               showEditDetails();
               return Edit;
            }
            set { }
        }
        public ProcurementOrders()
        {
            InitializeComponent();
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            procurementOrderDataGrid.ItemsSource = Database.ProcurementOrders;

            //DataGridButtonColumn btn = new DataGridViewButtonColumn();
            //procurementOrderDataGrid.Columns.Add(btn)
            //btn.HeaderText = "Click Data";
            //btn.Text = "Click Here";
            //btn.Name = "btn";
            //btn.UseColumnTextForButtonValue = true;
        }

        public void addNewOrder(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            main.addNewProcurementOrder(0, 0, 0, 900);
        }

        public void showEditDetails()
        {
            var temp = 0;
        }
    }
}
