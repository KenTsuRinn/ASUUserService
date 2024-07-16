using ASUCloud.Model;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service
{
    public sealed class EventBus
    {
        private static readonly Lazy<EventBus> lazy = new Lazy<EventBus>(() => new EventBus());

        private EventBus()
        {
        }
        public static EventBus Instance { get { return lazy.Value; } }

        public EventBus Subscribe()
        {
            LogHandlers += new EventHandler<EventArgs<LogEvent>>(LogHandler.LogMessage);
            return this;
        }

        public void Publish<T>(T eventargs) where T : EventArgs
        {
            try
            {
                switch (eventargs)
                {
                    case EventArgs<LogEvent> e:
                        LogHandlers?.Invoke(this, e);
                        break;
                    default:
                        throw new ASUCloudException(ErrorCode.SERVER_ERROR_OUTOFRANGE, ErrorMessage.OUT_OF_SWITCH_RANGE);

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogMessage(this, new EventArgs<LogEvent>(
                    new LogEvent
                    {
                        Level = LogEventLevel.Error,
                        Exception = new ASUCloudException(ErrorCode.SERVER_ERROR_BACKGROUNDJOB, ex.Message, ex),
                        Message = ex.Message
                    }));
            }
        }

        public event EventHandler<EventArgs<LogEvent>> LogHandlers;

    }
}
