using DepoAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DepoAPI.Models.Context
{
    //Database ile haberleşmeyi sağlayan class'ımız
    public class MyContext : DbContext //Database ile haberleşmemizi sağliyacak olan yapıları barındıran class'ımızdan inheritance aldık
    {
        public MyContext() : base("MyAutConnection") //WebConfig'deki MyConnection isimli connectionStrings xml tag'imizi bulmasını sağladık
        {

        }

        public DbSet<Storage> Storages { get; set; } //Database'de Storages isimli tablo açılmasını sağladık
    }
}