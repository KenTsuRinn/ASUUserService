using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Model
{
    public static class ErrorMessage
    {
        public const string INSERT_DUPLICATE_USER = "ERROR: insert duplicate user into database.";
        public const string OUT_OF_SWITCH_RANGE = "ERROR: out of switch range.";
        public const string VIEW_MODEL_BIND_FAILED = "ERROR: view model binding failed.";
    }
}
