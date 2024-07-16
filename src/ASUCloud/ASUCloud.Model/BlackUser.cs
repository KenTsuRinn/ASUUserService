using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Model
{
    public class BlackUser
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
