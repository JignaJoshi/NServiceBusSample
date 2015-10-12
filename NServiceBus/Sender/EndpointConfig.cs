using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NServiceBus;

namespace Sender
{
    public class EndpointConfig : IConfigureThisEndpoint, IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Customize(BusConfiguration configuration)
        {
            configuration.Conventions().DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("Domain"));
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseDataBus<FileShareDataBus>().BasePath("C:\\FileShare");
        }

        public void Start()
        {
            ProductShippedCommand(Bus);
        }

        public void ProductShippedCommand(IBus bus)
        {
            Console.WriteLine("Please press enter to ship product");
            Console.ReadLine();
            
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key != ConsoleKey.Enter) return;
            var product = new ProductShipped()
            {
                ProductId = Guid.NewGuid(),
                ShippingAddress = "Pune"
            };

            bus.Send("host",product);
            Console.WriteLine("Product shipped of the id:{0}", product.ProductId);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
