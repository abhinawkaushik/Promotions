using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionsEngine.Models
{
    public class ReturnCode
    {
        public const long SUCCESS = 0;
        public const long FAIL = -1;
    }
    public enum DataSourceType
    {
        FILE = 1,
        SQLDB = 2,
        ORACLEB = 3
    }
}
