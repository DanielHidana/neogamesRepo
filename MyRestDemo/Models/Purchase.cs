using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyRestDemo.Models
{
    public class Purchase
    {
        public double Amount { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public int ItemNumber { get; set; }
    }
}