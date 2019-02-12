using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPS.API.Models
{
    public class OrderDetail
    {
        public int OrderNumber { get; set; }
        public string ItemCode { get; set; }
        public int? Quantity { get; set; }
        public string SupplierCode { get; set; }
        public string OrderDate { get; set; }
    }
}