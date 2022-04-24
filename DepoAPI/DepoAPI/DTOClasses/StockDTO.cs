using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DepoAPI.DTOClasses
{
    public class StockDTO
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitInStock { get; set; }
    }
}