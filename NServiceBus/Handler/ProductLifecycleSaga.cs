using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NServiceBus.Saga;

namespace Handler
{
    public class ProductLifecycleSaga : Saga<ProductLifecycleSagaData>,
        IAmStartedByMessages<ProductShipped>,
        IHandleTimeouts<OrderTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ProductLifecycleSagaData> mapper)
        {
        }

        public void Handle(ProductShipped message)
        {
            Data.ProductId = message.ProductId;
            RequestTimeout<OrderTimeout>(TimeSpan.FromSeconds(5));
        }

        public void Timeout(OrderTimeout state)
        {
            Console.WriteLine("Got timeout");
        }
    }
}
