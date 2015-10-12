using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using NServiceBus;
using NServiceBus.Persistence.NHibernate;

namespace Handler
{
    public class ProductShippedHandler : IHandleMessages<ProductShipped>
    {
        private readonly IRepository repository;

        public ProductShippedHandler(IBus bus, NHibernateStorageContext storageContext, IRepository _repository)
        {
            repository = _repository;
        }

        public void Handle(ProductShipped message)
        {
            var product = new Product() {ProductId = message.ProductId, ShippingAddress = message.ShippingAddress};
            repository.Add<Product>(product);
            Console.WriteLine("Product {0} is shipped at {1}", message.ProductId, message.ShippingAddress);
        }
    }
}
