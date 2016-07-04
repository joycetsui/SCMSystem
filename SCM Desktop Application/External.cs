using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCM_Desktop_Application
{
    static class External
    {
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
    }
}
