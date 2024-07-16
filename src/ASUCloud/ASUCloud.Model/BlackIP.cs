using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Model
{
    public class BlackIP
    {
        public Guid ID { get; set; }
        public string IP { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

    }
}
