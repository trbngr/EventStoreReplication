using System;
using System.IO;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReplicationService
{
    class ServiceState
    {
        private readonly string _file;

        public ServiceState()
        {
            _file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "state.json");
        }

        public AllEventsSlice Save(AllEventsSlice slice)
        {
            File.WriteAllText(_file, JsonConvert.SerializeObject(slice.NextPosition));
            return slice;
        }

        public Position Load()
        {
            if (!File.Exists(_file))
                return Position.Start;

            using (var reader = new JsonTextReader(new StreamReader(File.OpenRead(_file))))
            {
                JToken token = JToken.ReadFrom(reader);
                var commitPosition = token["CommitPosition"].Value<long>();
                var preparePosition = token["PreparePosition"].Value<long>();
                return new Position(commitPosition, preparePosition);
            }
        }
    }
}