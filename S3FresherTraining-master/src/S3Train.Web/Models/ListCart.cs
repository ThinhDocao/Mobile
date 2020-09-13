using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S3Train.Web.Models
{
    public static class ListCart
    {
        public static string CartSession = "CartSession";
        public static string CartCookie = "0";
        public static List<CartItem> listCart { get; set; }
    }
}