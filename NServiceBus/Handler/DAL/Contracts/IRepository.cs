using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
   public interface IRepository
    {
       TAggregate Add<TAggregate>(TAggregate aggregate) where TAggregate : class;
       void CommitChanges();
    }
}
