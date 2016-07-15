using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Transportation
    {
        public static double getTransportationCost()
        {
            string transportationCostQuery = "SELECT Sum(it.[Cost]) +  Sum(cs.[Shipping Cost]) as [Total Cost] " +
                                              "FROM [Internal Transfers] as it, [Customer Shipping] as cs;";
            DataTable dt = Database.executeSelectQuery(transportationCostQuery);

            double transportationCost = 0;
            if (dt.Rows.Count != 0 && dt.Rows[0]["Total Cost"].ToString() != "")
            {
                Double.TryParse(dt.Rows[0]["Total Cost"].ToString(), out transportationCost);
            }

            return transportationCost;
        }
    }
}
