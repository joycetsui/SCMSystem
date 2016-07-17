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
    /// Interaction logic for InternalTransfers.xaml
    /// </summary>
    public partial class InternalTransfers : Page
    {
        public InternalTransfers()
        {
            InitializeComponent();
            this.internalTransfersDataGrid.MouseLeftButtonUp += new MouseButtonEventHandler(internalTransfersDataGrid_MouseClick);
        }

        public void loadTable(object sender, RoutedEventArgs e)
        {
            loadTable();
        }

        public void loadTable()
        {
            DataTable dt = Transportation.getInternalTransfers();
            internalTransfersDataGrid.ItemsSource = dt.AsDataView();
        }

        private DataGridRow selectedRow;

        void internalTransfersDataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            // iteratively traverse the visual tree
            while ((dep != null) &&
                    !(dep is DataGridRow))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return;

            if (dep is DataGridRow)
            {
                DataGridRow row = dep as DataGridRow;
                selectedRow = row;
            }
        }


        void editTransfer(object sender, RoutedEventArgs e)
        {
            if (selectedRow != null)
            {
                DataRowView item = (DataRowView)internalTransfersDataGrid.SelectedItems[0];
                InternalTransfersDetails detailsWindow = new InternalTransfersDetails(item, "Update Internal Transfer Details");
                detailsWindow.ShowDialog();
                loadTable();
            }
        }


    }
}
