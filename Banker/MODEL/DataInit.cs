using Banker.DATA;
using Banker.UTIL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Banker.MODEL.ENUM;

namespace Banker.MODEL
{
    public class DataInit
    {
        public string name { get; set; }
        public int bankcode { get; set; }
        public EBank banktype { get; set; }

        public string TIME { get => $"{time.Year}-{time.Month}-{time.Day}"; }
        //public string BANK { get => bank.desc(); }
        public string PRICE { get => STRING.Num2String(price); }

        public DateTime time;
        //public TypeBank bank;
        public int price;

        public DataInit() { }
        public DataInit(JToken j)
        {
            var v = j[KEYS.DATE].ToString() ;
            var date = DateTime.Parse(v);

            time = date;
            name = j[KEYS.DESC].ToString();
            bankcode = Convert.ToInt16(j[KEYS.BANK].ToString());
            banktype = (EBank)Convert.ToInt16(j[KEYS.BANKTYPE].ToString());
            price = Convert.ToInt32(j[KEYS.PRICE].ToString());

        }

        public JObject ToJson()
        {
            JObject j = new JObject();
            
            j.Add(KEYS.DATE, TIME);
            j.Add(KEYS.DESC, name);
            j.Add(KEYS.BANK, bankcode);
            j.Add(KEYS.BANKTYPE, (int)banktype);
            j.Add(KEYS.PRICE, price);

            return j;
        }
        public new string ToString()
        {
            var v = ToJson().ToString();
            /*v = v.Replace("\n", "");
            v = v.Replace("\r", "");*/
            return v;
        }
    }
}
