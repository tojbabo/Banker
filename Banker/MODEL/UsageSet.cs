using Banker.DATA;
using Banker.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.MODEL
{
    public class UsageSet
    {
        public string CATEGORY { get => (category==-1)?"신용카드":$"{MASTER.instance.metadata.GetCategory(category)}"; }
        public string PRICE { get => STRING.Num2String(totalprice); }
        public string USEPRICE { get => STRING.Num2String(useprice); }
        public int COUNT { get; set; }




        public int category;
        public int totalprice { get => makeprice - useprice; }
        public int useprice;
        public int makeprice;
        public int rate;
    }
}
