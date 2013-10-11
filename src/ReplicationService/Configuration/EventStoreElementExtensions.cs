using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;

namespace ReplicationService.Configuration
{
    public static class EventStoreElementExtensions
    {
        public static EventStoreConnection CreateConnection(this EventStoreElement element)
        {
            string host = element.Host;
            int port = element.TcpPort;

            EventStoreConnection connection = EventStoreConnection.Create(ConnectionSettings
                .Create()
                .UseCustomLogger(new Log4NetEventStoreLogger())
                .KeepReconnecting(),
                host);

            connection.Connect(new IPEndPoint(GetIpAddress(host), port));
            return connection;
        }

        private static IPAddress GetIpAddress(string host)
        {
            var addresses = Dns.GetHostAddresses(host)
                .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                .ToArray();

            if (!addresses.Any())
                throw new Exception("Can't find suitable IP address");

            IPAddress ipAddress = addresses.First();
            return ipAddress;
        }
    }
}