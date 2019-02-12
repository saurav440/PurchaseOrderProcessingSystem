using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPS.API.Models
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public decimal? ItemRate { get; set; }
        public string SuppNo { get; set; }
    }
}