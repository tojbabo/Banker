using Banker.DATA;
using Banker.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Banker.MODEL.ENUM;

namespace Banker.MODEL
{
    public class UsageItem
    {
        public string DAY { get => $"{month}.{day}"; }
        public string BANK { get => bank.desc(); }
        public string PRICE { get => ((usage == TypeUsage.use)?"- ":"") + STRING.Num2String(price); }
        public SolidColorBrush COLOR { get => (usage == TypeUsage.use) ? COLORS.USE : COLORS.MAKE; }
        public string CATEGORY{get=> $"{MASTER.instance.metadata.GetCategory(category)}";}
        public string DESC
        {
            get => (usage != TypeUsage.move && usage != TypeUsage.pay)
                ? desc
                : tobank?.desc();
        }
        

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
            var str = "";
            if (usage == TypeUsage.make || usage == TypeUsage.use)
            {

                str = $"{KEYS.MONTH}:{month},"
                   + $"{KEYS.DAY}:{day},"
                   + $"{KEYS.BANK}:{(int)bank},"
                   + $"{KEYS.USAGE}:{(int)usage},"
                   + $"{KEYS.PRICE}:{price},"
                   + $"{KEYS.CATEGORY}:{category},"
                   + $"{KEYS.DESC}:{desc}";
            }
            //else if(usage == TypeUsage.pay || usage == TypeUsage.move)

            else
            {
                str = $"{KEYS.MONTH}:{month},"
                   + $"{KEYS.DAY}:{day},"
                   + $"{KEYS.BANK}:{(int)bank},"
                   + $"{KEYS.USAGE}:{(int)usage},"
                   + $"{KEYS.PRICE}:{price},"
                   + $"{KEYS.TOBANK}:{(int)tobank}";

            }
            return "{" + str + "}";
        }
    }
}
