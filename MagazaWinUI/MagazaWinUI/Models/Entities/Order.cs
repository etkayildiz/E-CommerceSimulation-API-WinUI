using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazaWinUI.Models.Entities
{
   public class Order
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public byte AptNo { get; set; }
        public byte? Flat { get; set; }//Müstakil daireler için null geçilebilir yaptık


        //Relational Properties
        public List<OrderDetail> Details { get; set; }
    }
}
