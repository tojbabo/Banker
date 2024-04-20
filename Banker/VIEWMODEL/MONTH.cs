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
        public List<MonthData> datalist { get; set; }
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

        }
        public void LoadData()
        {
            datalist = new List<MonthData>();
            var mains = master.maindata.usages;
            var inits = master.metadata.inits;

            foreach (var i in inits)
            {
                /// 은행 추가 여부 확인
                var temp = datalist.Where(x => x.bankcode == i.bankcode).FirstOrDefault();

                /// 은행이 아직 추가가 안된 경우
                if (temp == null)
                {
                    datalist.Add(new MonthData()
                    {
                        accounttype = i.banktype,
                        NAME = i.name,
                        bankcode = i.bankcode,
                        price = i.price,
                        time = i.time
                    });
                }

                /// 추가 된게 없거나, 더 최근 데이터가 나온 경우
                /// 데이터 변경
                else if (temp.time == null || temp.time < i.time)
                {
                    temp.time = i.time;
                    temp.price = i.price;
                }

            }

            foreach (var m in mains)
            {
                var target = datalist.Where(x => x.bankcode == m.bankcode).FirstOrDefault();
                if (target == null) continue;

                target.AddUsage(m);
                if (m.usage == TypeUsage.pay || m.usage == TypeUsage.move)
                {
                    var target_to = datalist.Where(x => x.bankcode == m.tocode).FirstOrDefault();
                    if (target_to != null) target_to.AddUsage(m);
                }
            }

            int total = 0;
            int use = 0;
            int make = 0;

            /// 월 총 계산
            foreach (var v in datalist)
            {
                total += v.price;
                use += v.price_use;
                make += v.price_make;
            }


            OnpropertyChanged(nameof(PRICE_TOTAL));
            OnpropertyChanged(nameof(PRICE_TOTAL_GET));
            OnpropertyChanged(nameof(PRICE_TOTAL_USE));
            OnpropertyChanged(nameof(datalist));
        }
    }
}
