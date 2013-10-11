using System;
using log4net.Config;
using ReplicationService.Configuration;
using Topshelf;

namespace ReplicationService
{
    internal class Program
    {
        private static void Main()
        {
            XmlConfigurator.Configure();

            HostFactory.Run(x =>
            {
                x.Service<Replicator>(s =>
                {
                    s.ConstructUsing(f => new Replicator(ReplicationServiceConfiguration.Instance));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => { });
                });
                x.RunAsLocalService();
                
                x.SetDescription("EventStore Replication Service");
                x.SetDisplayName("EventStore Replication");
                x.SetServiceName("eventstorereplication");
            });

            Console.ReadLine();
        }
    }
}