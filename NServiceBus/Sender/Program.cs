//using System;
//using Domain;
//using NServiceBus;

//namespace Sender
//{
//    public class Program
//    {

//        private static void Main(string[] args)
//        {
//            var busConfiguration = new BusConfiguration();
//            busConfiguration.Conventions().DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("Atlas") && t.Namespace.Contains("Messages.Commands"));
//            busConfiguration.UsePersistence<InMemoryPersistence>();
//            busConfiguration.UseDataBus<FileShareDataBus>().BasePath("C:\\FileShare");
            

//            //busConfiguration.UseTransport<MsmqTransport>();
//            //busConfiguration.UsePersistence<NHibernatePersistence>();
//            //busConfiguration.EnableOutbox();

//            using (var bus = Bus.Create(busConfiguration).Start())
//            {
//                Console.WriteLine("Press enter to send a message");
//                Console.WriteLine("Press any key to exit");

//                while (true)
//                {
//                    ConsoleKeyInfo key = Console.ReadKey();
//                    Console.WriteLine();

//                    if (key.Key != ConsoleKey.Enter)
//                    {
//                        return;
//                    }

//                    var productId = Guid.NewGuid();
                        
//                    bus.Publish(new ProductShipped
//                    {
//                        ProductId = productId,
//                        ShippingAddress = "Pune"
//                    });

//                }
//            }
//        }
//    }
//}
