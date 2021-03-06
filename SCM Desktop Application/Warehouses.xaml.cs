﻿using DataAccess;
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
    /// Interaction logic for Warehouses.xaml
    /// </summary>
    public partial class Warehouses : Page
    {
        public Warehouses()
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
            DataTable dt = Inventory.getWarehouses();
            warehousesDataGrid.ItemsSource = dt.AsDataView();
        }

        public void row_DoubleClick(object sender, RoutedEventArgs e)
        {
            DataRowView item = (DataRowView)warehousesDataGrid.SelectedItems[0];
            WarehouseDetails detailsWindow = new WarehouseDetails(item);
            detailsWindow.ShowDialog();
        }
    }
}
