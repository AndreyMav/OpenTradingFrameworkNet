using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTFN.Core.Utils
{
    public class TimeUtil
    {
        public static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        public static DateTime UnixTimeStampToDateTime(ulong unixTimeStamp)
        {
            if (unixTimeStamp == 0)
                return DateTime.MinValue;
            // Unix timestamp is seconds past epoch
            return Epoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        public static ulong DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (ulong)(dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }
    }
}
