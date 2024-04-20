using Banker.DATA;
using Banker.DATA.ABSTRACT;
using Banker.MODEL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Banker.MODEL.ENUM;

namespace Banker.VIEWMODEL
{
    public class USAGE:INPC
    {
        #region property
        public ObservableCollection<DataUsage> DATALIST { get; set; }
        public List<UsageSet> DATASET { get; set; }
        #endregion

        private MASTER _master;
        private MasterUsage data { get => _master.maindata; }
        private MasterMeta meta { get => _master.metadata; }
        private ObservableCollection<DataUsage> _sources { get => data.usages; }
        private DateTime _today { get => _master.targetdate; }
        TYPEUSAGEDATA nowtype; int nowbank;

        public USAGE()
        {
            _master = MASTER.instance;
            DATALIST = new ObservableCollection<DataUsage>();
            DATASET = new List<UsageSet>();
        }

        public bool InputData(int date, string bank, TypeUsage usage, int price, string category, string desc)
        {
            var v = meta.categorys.FirstOrDefault(x => x.DESC == category);
            if (v.DESC == null) return false;

            var code = meta.GetBankCode(bank);
            if (code == -1) return false;

            var d = new DataUsage
            {
                year = _today.Year,
                month = _today.Month,
                day = date,
                bankcode = code,
                usage = usage,
                category = v.CODE,
                price = price,
                desc = desc,
            };


            DATALIST.Add(d);
            _sources.Add(d);
            data.SaveData();

            return true;
        }
        public bool InputData(int date, string bank, TypeUsage usage, int price, string to)
        {
            var code = meta.GetBankCode(bank);
            var tocode = meta.GetBankCode(to);
            if (code == -1 || tocode == -1) return false;
            var d = new DataUsage
            {
                year = _today.Year,
                month = _today.Month,
                day = date,
                bankcode = code,
                tocode = tocode,
                usage = usage,
                price = price,
            };

            DATALIST.Add(d);
            _sources.Add(d);
            data.SaveData();

            return true;
        }
    
        public void LoadData(TYPEUSAGEDATA type, int bank)
        {
            nowtype = type;
            nowbank = bank;
            // 항목별 통계 데이터
            if(type == TYPEUSAGEDATA.statistics)
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
            // 은행별 지출 내역
            else if(type == TYPEUSAGEDATA.all)
            {
                //var temp_source = _sources.Where(x => x.month == _today.Month).ToList();
                var temp_source = _sources.ToList();
                DATALIST.Clear();
                if (bank == -1)
                {
                    temp_source
                        .OrderBy(x=>x.date).ToList()
                        .ForEach(x => DATALIST.Add(x));
                }
                else
                {
                    var a = temp_source.Where(x => (x.bankcode == bank || x.tocode == bank)).ToList();
                    var b = a.OrderBy(x => x.date).ToList();
                    b.ForEach( x=> DATALIST.Add(x));
                }

            }

        }
        
        public void RemoveItem(DataUsage target)
        {
            _sources.Remove(target);
            data.SaveData();
            LoadData(nowtype, nowbank);
        }
    }

    public enum TYPEUSAGEDATA
    {
        // 전체 지출 내역
        all,
        // 통계
        statistics,
    }

}