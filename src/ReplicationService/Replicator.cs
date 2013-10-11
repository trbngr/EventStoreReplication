using System;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using log4net;
using ReplicationService.Configuration;

namespace ReplicationService
{
    internal class Replicator
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Replicator));

        private const int MaxNumberOfOperationsBeforeReconnect = 25;

        private readonly int _sliceSize;
        private readonly EventStoreElement _source;
        private readonly ServiceState _state;
        private readonly EventStoreElement _target;
        private readonly TimeSpan _interval;

        public Replicator()
        {
        }

        public Replicator(ReplicationServiceConfiguration configuration)
        {
            _source = configuration.SourceEventStore;
            _target = configuration.TargetEventStore;
            _sliceSize = configuration.SliceSize;
            _interval = configuration.Interval;

            _state = new ServiceState();
        }

        public void Start()
        {
            Replicate(_state.Load());

            Task.Factory.StartNewDelayed((int) _interval.TotalMilliseconds, Start);
        }

        private void Replicate(Position currentPosition)
        {
            var endOfStream = false;
            AllEventsSlice slice = null;

            using(EventStoreConnection source = _source.CreateConnection())
            using (EventStoreConnection target = _target.CreateConnection())
            {
                for (int i = 0; i < MaxNumberOfOperationsBeforeReconnect; i++)
                {
                    Position position = slice == null ? currentPosition : slice.NextPosition;

                    Log.InfoFormat("READ: {0}", position);

                    slice = source.ReadAllEventsForward(position, _sliceSize, false);

                    WriteToTargetEventStore(slice, target);
                    
                    _state.Save(slice);

                    if (slice.IsEndOfStream)
                    {
                        endOfStream = true;
                        Log.InfoFormat("End of stream: {0}", slice.NextPosition);
                        break;
                    }
                }
            }

            if (!endOfStream && slice != null)
            {
                Replicate(slice.NextPosition);
            }
        }

        private void WriteToTargetEventStore(AllEventsSlice slice, EventStoreConnection connection)
        {
            var streamGroups = slice.Events
                .Where(e => !e.OriginalStreamId.StartsWith("$"))
                .Select(e => e.OriginalEvent)
                .GroupBy(e => e.EventStreamId);

            Parallel.ForEach(streamGroups, g =>
            {
                string stream = g.Key;
                EventData[] data = g.Select(e => new EventData(e.EventId, e.EventType, true, e.Data, e.Metadata)).ToArray();
                Log.InfoFormat("{0} - {1:0000} events", stream, data.Length);
                connection.AppendToStream(stream, ExpectedVersion.Any, data);
            });
        }
    }
}