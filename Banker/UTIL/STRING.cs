using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.UTIL
{
    public class STRING
    {
        /// <summary>
        /// 10000 -> 10,000
        /// </summary>
        /// <returns></returns>
        public static string Num2String(int num)
        {
            return String.Format("{0:#,###}", num); ;
        }
    }
}
