using Banker.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Banker.MODEL.ENUM;

namespace Banker.MODEL
{
    public class MonthItem
    {
        public string BANK { get => bank.desc(); }
        public string PRICE { get => STRING.Num2String(price + price_make - price_use); }
        public string USE { get => STRING.Num2String(price_use); }
        public string MAKE { get => STRING.Num2String(price_make); }
        public string TOTAL { get => STRING.Num2String(price_make - price_use); }

        public DateTime? time;
        public TypeBank bank;
        public int price;
        public int price_use;
        public int price_make;

        public MonthItem(TypeBank bank)
        {
            this.bank = bank;
        }
    }
}
