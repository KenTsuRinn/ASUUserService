using ASUCloud.Model;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service.IntegratedTest
{
    [TestClass]
    public class EventBusTest
    {
        private EventBus _eventBus;

        [TestInitialize]
        public void Initialize()
        {
            _eventBus = EventBus.Instance.Subscribe();
        }

        [TestMethod]
        public void TestPublishLogEvent()
        {
            _eventBus.Publish<EventArgs<LogEvent>>(
                new EventArgs<LogEvent>(
                    new LogEvent
                    {
                        Level = LogEventLevel.Error,
                        Message = "test event bus publishing log event"
                    }));
        }
    }
}
