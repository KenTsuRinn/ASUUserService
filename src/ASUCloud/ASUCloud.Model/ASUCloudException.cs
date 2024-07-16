using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Model
{
    [Serializable]
    public class ASUCloudException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public ASUCloudException() { }

        public ASUCloudException(ErrorCode code, string message)
            : base(message)
        {
            ErrorCode = code;
        }

        public ASUCloudException(ErrorCode code, string message, Exception inner)
            : base(message, inner)
        {
            ErrorCode = code;
        }

    }
}
