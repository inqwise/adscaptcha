using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditGuard
{
    public class ExecuteException: Exception
    {
        public ExecuteException(ErrorResponse response)
        {
            this.Error = response;
        }

        public ErrorResponse Error { get; set; }
    }
}
