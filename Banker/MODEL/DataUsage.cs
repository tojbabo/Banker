using Banker.DATA;
using Banker.UTIL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static Banker.MODEL.ENUM;

namespace Banker.MODEL
{
    public class DataUsage
    {
        public string DAY { get => $"{month}/{day}"; }
        public int bankcode { get; set; }
        public int tocode { get; set; }
        public string BANK { get => MASTER.instance.metadata.GetBankName(bankcode); }
        public string PRICE { get => ((usage == TypeUsage.use)?"- ":"") + STRING.Num2String(price); }
        public SolidColorBrush COLOR
        {
            get
            {
                switch (usage)
                {
                    case TypeUsage.use:
                        return COLORS.USE;
                    case TypeUsage.make:
                        return COLORS.MAKE;
                    default:
                        return COLORS.NORMAL;
                }
            }
        }
        public string CATEGORY{get=> $"{MASTER.instance.metadata.GetCategory(category)}";}
        public string DESC
        {
            get => (usage != TypeUsage.move && usage != TypeUsage.pay)
                ? desc
                : MASTER.instance.metadata.GetBankName(tocode);
        }
        
        public string date { get; set; }
        public int year;
        public int month;
        public int day;
        public TypeUsage usage { get; set; }
        public int price { get; set; }
        public int category { get; set; }
        public string desc { get; set; }

        public DataUsage(){}
        public DataUsage(JToken data)
        {
            date = data[KEYS.DATE].ToString();
            bankcode = Convert.ToInt16(data[KEYS.BANK].ToString());
            usage = (TypeUsage)Convert.ToInt16(data[KEYS.USAGE].ToString());
            price = Convert.ToInt32(data[KEYS.PRICE].ToString());

            year = 23;
            month = Convert.ToInt32(date.Substring(0,2));
            day = Convert.ToInt32(date.Substring(2));


            if (usage == TypeUsage.make || usage == TypeUsage.use)
            {
                category = Convert.ToInt16(data[KEYS.CATEGORY].ToString());
                desc = data[KEYS.DESC].ToString().Replace("!@#"," ");
                tocode = -1;
            }
            else
            {
                category = -1;
                tocode = Convert.ToInt16(data[KEYS.TOBANK].ToString());
            }
        }

        public JObject ToJson()
        {
            date = $"{month.ToString().PadLeft(2, '0')}{day.ToString().PadLeft(2, '0')}";
            JObject json = new JObject
            {
                { KEYS.DATE, date},
                { KEYS.BANK, (int)bankcode },
                { KEYS.USAGE, (int)usage },
                { KEYS.PRICE, price }
            };

            if (usage == TypeUsage.make || usage == TypeUsage.use)
            {
                json.Add(KEYS.CATEGORY, category);
                json.Add(KEYS.DESC, desc.Replace(" ","!@#"));
            }
            else
            {
                json.Add(KEYS.TOBANK, (int)tocode);
            }

            return json;
        }

        public new string ToString()
        {
            var str = ToJson().ToString() ;
            str = str.Replace("\r", "");
            str = str.Replace("\n", "");
            str = str.Replace(" ", "");

            return str;
        }
    }
}
