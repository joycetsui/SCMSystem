using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCM_Desktop_Application
{
    static public class External
    {
        static public DataTable executeSelectQuery(string query)
        {
            OleDbConnection conn = new OleDbConnection(access.cnStr);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            DataTable ds = new DataTable();
            adapter.Fill(ds);

            conn.Close();
            return ds;
        }

        static public void executeInsertUpdateQuery(string query)
        {
            OleDbConnection conn = new OleDbConnection(access.cnStr);
            OleDbCommand cmd = new OleDbCommand();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        static public ProcurementForecastItem[] getNewForecasts()
        {
            ProcurementForecastItem[] ProcurementForecasts = new[]
            {
                new ProcurementForecastItem { Year = 2016, Week = 7, rawMaterialId = 0, Quantity = 30},
                new ProcurementForecastItem { Year = 2016, Week = 8, rawMaterialId = 1, Quantity = 100},
                new ProcurementForecastItem { Year = 2016, Week = 9, rawMaterialId = 2, Quantity = 42},
                new ProcurementForecastItem { Year = 2016, Week = 10, rawMaterialId = 3, Quantity = 13},
                new ProcurementForecastItem { Year = 2016, Week = 11, rawMaterialId = 1, Quantity = 44},
                new ProcurementForecastItem { Year = 2016, Week = 12, rawMaterialId = 1, Quantity = 445},
            };

            return ProcurementForecasts;
        }

        static public void addNewProcurementOrder(int supplierId, int destinationSiteId, int rawMaterialId, int quantity)
        {
            string query = "insert into [Procurement Orders]([Supplier ID],[Destination Site ID], [Raw Material ID],[Quantity]) values('" + supplierId + "','" + destinationSiteId + "','" + rawMaterialId + "','" + quantity + "')";
            executeInsertUpdateQuery(query);
        }

        static public void addNewInternalTransferOrder(int originSiteId, int destinationSiteId, int deliveryMethodId, string departureDate, string arrivalDate, int rawMaterialId, int quantity)
        {
            int newtransferId = Database.InternalTransfer.Last().StockTransferId + 1;
            Database.InternalTransfer.Add(
                            new InternalTransfer { StockTransferId = newtransferId, OriginSiteId = originSiteId, DestinationSiteId = destinationSiteId, DeliveryMethodID = deliveryMethodId, TotalCost = 10, DepartureDate = departureDate, ArrivalDate = arrivalDate, Quantity = quantity }
            );
        }

        static public void addNewShippingCompany(string companyName, string shippingMethod, string contactInfo, double shippingRate)
        {
            int newCompanyId = Database.ShippingCompanies.Last().companyId + 1;
            Database.ShippingCompaniesName.Add(companyName);
            Database.ShippingCompanies.Add(
                new ShippingCompany
                {
                    companyId = newCompanyId,
                    CompanyName = companyName,
                    ShippingMethod = shippingMethod,
                    ContactInfo = contactInfo,
                    ShippingRate = shippingRate
                }
            );

        }

        static public void addNewSupplier(string supplierName, string location)
        {
            int newSupplierId = Database.SuppliersList.Last().SupplierId + 1;
            Database.SuppliersList.Add(
                new Supplier { SupplierId = newSupplierId, Location = location, Name = supplierName }
            );
        }
    }
}
