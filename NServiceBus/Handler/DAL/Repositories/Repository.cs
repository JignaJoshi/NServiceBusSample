using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Handler;
using Handler.DAL.Contracts;

namespace Domain.Repositories
{
    public class Repository : IRepository, IDisposable
    {
        readonly Lazy<IProductShippingContext> _context;


        public Repository(IProductShippingContext context)
        {
            _context = new Lazy<IProductShippingContext>(() => context);
        }
        public Repository(Func<IProductShippingContext> getContext)
        {
            _context = new Lazy<IProductShippingContext>(getContext);
        }

        public TAggregate Add<TAggregate>(TAggregate aggregate) where TAggregate : class
        {
            return ((ProductShippingContext)_context.Value).Set<TAggregate>().Add(aggregate);
        }

        void Commit()
        {
            if (_context.IsValueCreated)
                _context.Value.SaveChanges();
        }
        public void CommitChanges()
        {
            Commit();
        }

        public void Dispose()
        {

        }

       
    }
}
