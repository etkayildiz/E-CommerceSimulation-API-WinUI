using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazaWinUI.DTOClasses
{
    public class StockDTO
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitInStock { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}\t  Birim Fiyat: {UnitPrice}\t  Stok: {UnitInStock}\t  Ürün adı: {ProductName}";
        }
    }
}
