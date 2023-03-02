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
    public class UsageItem
    {
        public string DAY { get => $"{month}.{day}"; }
        public string BANK { get => bank.desc(); }
        public string PRICE { get => STRING.Num2String(price); }
        public string CATEGORY { get => $"{MASTER.instance.metadata.categorys[category]}"; }
        public string DESC { get => desc; }

        public int month { get; set; }
        public int day { get; set; }
        public TypeBank bank { get; set; }
        public TypeBank? tobank { get; set; }
        public TypeUsage usage { get; set; }
        public int price { get; set; }
        public int category { get; set; }
        public string desc { get; set; }

        public new string ToString()
        {
            var str = $"month:{month},"
                + $"day:{day},"
                + $"bank:{(int)bank},"
                + $"usage:{(int)usage},"
                + $"price:{price},"
                + $"category:{category},"
                + $"desc:{desc}";

            return "{" + str + "}";
        }
    }
}
