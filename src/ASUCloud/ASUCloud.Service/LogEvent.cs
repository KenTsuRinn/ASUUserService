using ASUCloud.Model;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service
{
    public class LogEvent
    {
        public LogEventLevel Level { get; set; }
        public ASUCloudException Exception { get; set; }
        public string Message { get; set; }
    }
}
