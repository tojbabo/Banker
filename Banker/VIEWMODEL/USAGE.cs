using Banker.DATA;
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
    public class USAGE
    {
        #region property
        public ObservableCollection<UsageItem> DATALIST { get => data.datalist; }

        #endregion

        private MASTER _master;
        private MainData data { get => _master.maindata; }
        private DateTime _today;

        public USAGE()
        {
            _master = MASTER.instance;
            _today = DateTime.Now;
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
            data.SaveData();

            return true;
        }
    }
}