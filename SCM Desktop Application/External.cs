using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCM_Desktop_Application
{
    static public class External
    {
        //static public DataTable executeSelectQuery(string query)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(access.cnStr))
        //    {
        //        try
        //        {
        //            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        //            adapter.Fill(dt);
        //        }
        //        catch { }
        //    }

        //    return dt;
        //}

        //static public void executeInsertUpdateQuery(string query)
        //{
        //    using (SqlConnection conn = new SqlConnection(access.cnStr))
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //            conn.Close();
        //        }
        //        catch { }
        //    }
        //}

        static public ProcurementForecastItem[] getNewForecasts()
        {
            ProcurementForecastItem[] ProcurementForecasts = new[]
            {
                new ProcurementForecastItem { Year = 2016, Week = 7, rawMaterialId = 3, Quantity = 30},
                new ProcurementForecastItem { Year = 2016, Week = 8, rawMaterialId = 1, Quantity = 100},
                new ProcurementForecastItem { Year = 2016, Week = 9, rawMaterialId = 2, Quantity = 42},
                new ProcurementForecastItem { Year = 2016, Week = 10, rawMaterialId = 3, Quantity = 13},
                new ProcurementForecastItem { Year = 2016, Week = 11, rawMaterialId = 1, Quantity = 44},
                new ProcurementForecastItem { Year = 2016, Week = 12, rawMaterialId = 1, Quantity = 445},
            };

            return ProcurementForecasts;
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
