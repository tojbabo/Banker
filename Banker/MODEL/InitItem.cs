using Banker.DATA;
using Banker.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Banker.MODEL.ENUM;

namespace Banker.MODEL
{
    public class InitItem
    {
        public string TIME { get => $"{time.Year}-{time.Month}-{time.Day}"; }
        public string BANK { get => bank.desc(); }
        public string PRICE { get => STRING.Num2String(price); }

        public DateTime time;
        public TypeBank bank;
        public int price;

        public new string ToString()
        {
            return $"{{{KEYS.DATE}:{TIME}" +
                $",{KEYS.BANK}:{(int)bank}" +
                $",{KEYS.PRICE}:{price}}}";
        }
    }
}
