using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Repositories;
using Handler;
using Handler.DAL.Contracts;
using log4net;
using log4net.Config;
using Ninject;
using NServiceBus;
using NServiceBus.Persistence;
using NServiceBus.Persistence.NHibernate;
using NServiceBus.Transports.SQLServer;

namespace Host
{
    public class EndpointConfig: IConfigureThisEndpoint, INeedInitialization, AsA_Server, IWantToRunBeforeConfigurationIsFinalized
    {
        IKernel CreateKernel()
        {
            var kernal = new StandardKernel();
            kernal.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            return kernal;
        }


        public void Customize(BusConfiguration configuration)
        {
            XmlConfigurator.Configure();
            configuration.UseContainer<NinjectBuilder>(k => k.ExistingKernel(CreateKernel()));
            configuration.RegisterComponents(components => components.ConfigureComponent<UnitOfWork>(DependencyLifecycle.InstancePerUnitOfWork));

            configuration.RegisterComponents(x => x.ConfigureComponent<IRepository>(
                builder =>
                {
                    return new Repository(() =>
                    {
                        var nh = builder.Build<NHibernateStorageContext>();
                        var e = new ProductShippingContext(nh.Connection);
                        return e;
                    });
                },
                DependencyLifecycle.InstancePerUnitOfWork));
            configuration.UsePersistence<NHibernatePersistence>();
            configuration.UseTransport<MsmqTransport>();
            configuration.UseDataBus<FileShareDataBus>().BasePath("NServiceBus.DataBus.BasePath");
            configuration.RijndaelEncryptionService();
            configuration.Transactions();
            configuration.EnableOutbox();
        }

        public void Run(Configure config)
        {
             SetupDevelopmentDatabase();
        }

        [Conditional("DEBUG")]
        void SetupDevelopmentDatabase()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProductShippingContext>());
            using (var context = new ProductShippingContext("NServiceBus/Persistence"))context.Database.Initialize(true);
        } 
    }
}
