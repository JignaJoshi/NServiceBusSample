using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Handler.DAL.Contracts;

namespace Handler
{
   public class ProductShippingContext : DbContext, IProductShippingContext
   {
       public ProductShippingContext()
           : base("NServiceBus/Persistence")
       {
       }

       public ProductShippingContext(string connectionStringName)
           : base(connectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            
        }

       public ProductShippingContext(IDbConnection connection)
           : base((DbConnection)connection, false)
       {
       }

       public IDbSet<Product> Products { get; set; }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           modelBuilder.ComplexType<ProductShipped>();
           base.OnModelCreating(modelBuilder);
       }
   }
}
