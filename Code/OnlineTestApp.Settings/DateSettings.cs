using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestApp
{
    public static class DateSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public static DateTime CurrentDateTime
        {
            get
            {
                return DateTime.Now.ToUniversalTime();
            }
        }        

    }
}
