using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service.IntegratedTest
{
    [TestClass]
    public class LogHandlerTest
    {

        [TestMethod]
        public void TestLogToSqlite()
        {
            LogHandler.LogMessage(null, new EventArgs<LogEvent>(new LogEvent
            {
                Level = LogEventLevel.Error,
                Message = "test for logging error msg."
            }
          ));

        }
    }
}
