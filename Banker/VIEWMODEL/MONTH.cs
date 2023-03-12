using Banker.DATA;
using Banker.DATA.ABSTRACT;
using Banker.MODEL;
using Banker.UTIL;
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
        #region property
        public List<MonthItem> datalist { get; set; }
        public string PRICE_TOTAL { get
            {
                int price = 0;
                datalist.ForEach(x => price += x.price);
                return STRING.Num2String(price);
            }
        }
        public string PRICE_TOTAL_USE
        {
            get
            {
                int price = 0;
                datalist.ForEach(x => price += x.price_use);
                return STRING.Num2String(price);
            }
        }
        public string PRICE_TOTAL_GET
        {
            get
            {
                int price = 0;
                datalist.ForEach(x => price += x.price_make);
                return STRING.Num2String(price);
            }
        }
        #endregion
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

                if (m.usage == TypeUsage.make)
                {
                    target.price_make += m.price;
                }
                else if (m.usage == TypeUsage.use)
                {
                    target.price_use += m.price;
                }
                //else if (m.usage == TypeUsage.pay || m.usage == TypeUsage.move)
                else
                {
                    var target_to = datalist.Where(x => x.bank == m.tobank).FirstOrDefault();
                    target.price -= m.price;
                    target_to.price += m.price;
                }
            }

            int total = 0;
            int use = 0;
            int make = 0;
            foreach(var v in datalist)
            {
                total += v.price;
                use += v.price_use;
                make += v.price_make;
            }

            OnpropertyChanged(nameof(PRICE_TOTAL));
            OnpropertyChanged(nameof(PRICE_TOTAL_GET));
            OnpropertyChanged(nameof(PRICE_TOTAL_USE));


        }
    }
}
