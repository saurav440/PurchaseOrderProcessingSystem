using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPS.API.Models
{
    public class CreateOrder
    {
        public string ItemCode { get; set; }
        public string SupCode { get; set; }
        public int Qty { get; set; }
    }
}