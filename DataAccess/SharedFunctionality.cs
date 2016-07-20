using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace DataAccess
{
    public class Expense
    {
        public string Name;
        public double amount;
    }

    public struct Payee_details
    {
        public string company;
        public string address;
        public string paymentDetails;
    }

    public class AISRequestObject
    {
        public string team_name;
        public List <KeyValuePair<string, int>> items_bought;
        public double value;
        public Payee_details payee_details;
    }

    public class SalesCustomer
    {
        public SalesCustomerInfo customer;
    }

    public class SalesCustomerInfo
    {
        public string name;
        public string shipping_addr;
        public string email;
        public string billing_addr;
    }

    public class ProcurementForecastItem
    {
        public int rawMaterialId;
        public int year;
        public int week;
        public int quantity;
    }

    //public static class SharedFunctionality
    //{
    //    public static void throwError(string message, string reason)
    //    {
    //        var error = new HttpResponseMessage(HttpStatusCode.NotFound)
    //        {
    //            Content = new StringContent(string.Format(message)),
    //            ReasonPhrase = reason,
    //        };

    //        throw new Exception(message);
    //    }
    //}
}
