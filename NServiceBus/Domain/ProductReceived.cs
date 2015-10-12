using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Domain
{
   public class ProductReceived : IMessage
    {
       public Guid ProductId { get; set; }
    }
}
