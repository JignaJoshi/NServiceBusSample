using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NServiceBus;

namespace Sender
{
    class ProductReceivedHandler : IHandleMessages<ProductReceived>
    {
        public void Handle(ProductReceived message)
        {
            Console.WriteLine("Order {0} accepted.", message.ProductId);
        }
    }
}
