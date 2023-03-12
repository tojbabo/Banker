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
    public class USAGE:INPC
    {
        #region property
        public ObservableCollection<UsageItem> DATALIST { get; set; }
        public List<UsageSet> DATASET { get; set; }
        #endregion

        private MASTER _master;
        private MainData data { get => _master.maindata; }
        private ObservableCollection<UsageItem> _sources { get => data.datalist; }
        private DateTime _today;

        public USAGE()
        {
            _master = MASTER.instance;
            _today = DateTime.Now;
            DATALIST = new ObservableCollection<UsageItem>();
            DATASET = new List<UsageSet>();
        }

        public bool InputData(int date, TypeBank bank, TypeUsage usage, int price, string category, string desc)
        {
            var v = _master.metadata.categorys.FirstOrDefault(x => x.Value == category);
            if (v.Value == null) return false;

            var d = new UsageItem
            {
                month = _today.Month,
                day = date,
                bank = bank,
                usage = usage,
                category = v.Key,
                price = price,
                desc = desc,
            };

            DATALIST.Add(d);
            _sources.Add(d);
            data.SaveData();

            return true;
        }
        public bool InputData(int date, TypeBank bank, TypeUsage usage, int price, TypeBank to)
        {
            var d = new UsageItem
            {
                month = _today.Month,
                day = date,
                bank = bank,
                tobank = to,
                usage = usage,
                price = price,
            };

            DATALIST.Add(d);
            _sources.Add(d);
            data.SaveData();

            return true;
        }
    
        public void LoadData(TYPEUSAGEDATA type, TypeBank bank)
        {

            if(type == TYPEUSAGEDATA.cluster)
            {
                DATASET.Clear();
                _sources.ToList().ForEach(x =>
                {
                    var cat = x.category;
                    if (x.usage == TypeUsage.move) return;
                    else if(x.usage == TypeUsage.pay)
                    {
                        cat = -1;
                    }

                    var res = DATASET.Where(y => y.category == x.category).FirstOrDefault();
                    var use = (x.usage == TypeUsage.use) ? x.price : 0;
                    var make = (x.usage == TypeUsage.make) ? x.price : 0;
                    if (res == null)
                    {
                        DATASET.Add(new UsageSet
                        {
                            category = cat,
                            COUNT = 1,

                            useprice = use,
                            makeprice = make,
                        });
                    }
                    else
                    {
                        res.COUNT++;
                        res.useprice += use;
                        res.makeprice += make;
                    }
                });

                DATASET = DATASET.OrderBy(x => x.makeprice - x.useprice).ToList();
                OnpropertyChanged(nameof(DATASET));

            }
            else if(type == TYPEUSAGEDATA.entire)
            {
                DATALIST.Clear();
                if (bank == TypeBank.none)
                {
                    _sources.ToList().OrderBy(x=>x.day).ToList().ForEach(x => DATALIST.Add(x));
                }
                else
                {
                    var a = _sources.Where(x => x.bank == bank).ToList();
                    var b = a.OrderBy(x => x.day).ToList();
                    b.ForEach( x=> DATALIST.Add(x));

                }

            }

        }
    
    }

    public enum TYPEUSAGEDATA
    {
        entire,
        cluster,
    }

}