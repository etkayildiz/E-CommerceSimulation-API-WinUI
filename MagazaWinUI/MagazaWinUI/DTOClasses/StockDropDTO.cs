using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazaWinUI.DTOClasses
{
    public class StockDropDTO
    {
        public int ID { get; set; }
        public short Quantity { get; set; }

        public override string ToString()
        {
            return $"{ID} ID'li üründen {Quantity} adet.";
        }
    }
}
