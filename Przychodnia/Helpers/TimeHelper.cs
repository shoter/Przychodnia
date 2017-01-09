using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Przychodnia.Helpers
{
    public static class TimeHelper
    {
        public static double UnixTicks(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}