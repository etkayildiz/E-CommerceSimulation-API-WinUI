using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DepoAPI.Models.Entities
{
    //WinForm ve MVC projeleri bu API'dan stok bilgisi çekiceği için ürüne ait bilgileri içericek bu class
    public class Storage
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitInStock { get; set; }

    }
}