using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Model
{
    public enum ErrorCode
    {
        INVALID_TOKEN = 40001,
        INVALID_REQUEST = 40002,
    
        SERVER_ERROR_BUSINESS = 50001,
        SERVER_ERROR_DATABASE = 50002,
        SERVER_ERROR_CACHE = 50003,
        SERVER_ERROR_BACKGROUNDJOB = 50004
    }
}
