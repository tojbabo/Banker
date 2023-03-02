using Banker.DATA;
using Banker.DATA.ABSTRACT;
using Banker.MODEL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Banker.MODEL.ENUM;

namespace Banker.VIEWMODEL
{
    public class MONTH :INPC
    {
        public List<MonthItem> datalist { get; set; }
        MASTER master;
        public MONTH()
        {
            master = MASTER.instance;
            datalist = new List<MonthItem>();

            datalist.Add(new MonthItem(TypeBank.suhyup));
            datalist.Add(new MonthItem(TypeBank.shinhan));
            datalist.Add(new MonthItem(TypeBank.kakao));
            datalist.Add(new MonthItem(TypeBank.ibk));
            datalist.Add(new MonthItem(TypeBank.credit_samsung));

        }
        public void LoadData()
        {
            var mains = master.maindata.datalist;
            var inits = master.metadata.inits;

            foreach(var i in inits)
            {
                var temp = datalist.Where(x => x.bank == i.bank).FirstOrDefault();
                if(temp.time == null || temp.time < i.time)
                {
                    temp.time = i.time;
                    temp.price = i.price;
                }

            }

            foreach(var m in mains)
            {
                var target = datalist.Where(x => x.bank == m.bank).FirstOrDefault();
                if (target == null) continue;
                
                if(m.usage == TypeUsage.make)
                {
                    target.price_make += m.price;
                }
                else if(m.usage == TypeUsage.use)
                {
                    target.price_use += m.price; 
                }
            }
            
        }

    }
}
