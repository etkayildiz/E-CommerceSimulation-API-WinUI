using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KargoAPI.DTOClasses
{

    //API ile haberleşmek isteyen sitelere ham veriyi değil DTO class'ında belirttiğimiz bilgileri göstermek ve bu tipte veri almak için CargoDTO Class'ını kullanıyoruz.

    public class CargoDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Country { get; set; } 
        public string City { get; set; } 
        public string Address { get; set; } 
        public string Phone { get; set; } 
        public string Email { get; set; } 
        public DateTime? ShippedDate { get; set; } 
        public string Status { get; set; } 
        public DateTime? DeliveryDate { get; set; } 


    }
}