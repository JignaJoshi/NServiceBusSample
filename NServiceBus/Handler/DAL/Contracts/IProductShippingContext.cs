using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Handler.DAL.Contracts
{
   public interface IProductShippingContext
    {
        IDbSet<Product> Products { get; set; }
        int SaveChanges();
    }
}
