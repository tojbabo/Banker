using Banker.UTIL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Banker.MODEL.ENUM;

namespace Banker.MODEL
{
    public class MonthData
    {
        public int bankcode { get; set; }
        public string NAME { get; set; }
        public string PRICE
        {
            get =>
                STRING.Num2String(price + (
                    (accounttype == EBank.credit)
                    ? (price_use - price_make)
                    : (price_make - price_use)));
        }
        public string USE { get => STRING.Num2String(price_use); }
        public string MAKE { get => STRING.Num2String(price_make); }
        public string TOTAL
        {
            get => STRING.Num2String(
                (accounttype == EBank.credit)
                    ? (price_use - price_make)
                    : (price_make - price_use));
        }

        public EBank accounttype;
        public List<DataUsage> usages;

        public DateTime? time;
        public int price;
        public int price_use;
        public int price_make;

        public MonthData()
        {
            usages = new List<DataUsage>();
        }

        public void AddUsage(DataUsage usage)
        {
            usages.Add(usage); 
            if (usage.bankcode == this.bankcode)
            {
                if (usage.usage == TypeUsage.make)
                {
                    price_make += usage.price;
                }
                else if (usage.usage == TypeUsage.use)
                {
                    price_use += usage.price;
                }
                else
                {
                    price -= usage.price;
                }
            }
            else
            {
                price += usage.price * ((usage.usage == TypeUsage.pay)?(-1):(1));
            }
        }
    }
}
