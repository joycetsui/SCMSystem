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
        public string name;
        public string address;
    }

    public class AISRequestObject
    {
        public string team_name;
        public List <KeyValuePair<string, int>> items_bought;
        public double value;
        public Payee_details payee_details;
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
