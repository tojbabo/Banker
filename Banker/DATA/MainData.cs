using Banker.MODEL;
using Banker.UTIL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Banker.MODEL.ENUM;

namespace Banker.DATA
{
    public class MainData
    {
        private int _year;
        private int _month;
        private List<string> sources;

        public ObservableCollection<UsageItem> datalist { get; set; }

        public MainData(int year, int month)
        {
            this._year = year;
            this._month = month;
            sources = new List<string>();
            datalist = new ObservableCollection<UsageItem>();
        }

        public async void LoadData()
        {
            var read = await FileMaster.Read($"data{_year}");
            if (read == null) return;

            var temp = "";
            var count = 0;
            char[] target = { '{', '}' };
            while (true)
            {
                var idx = read.IndexOfAny(target);
                if (idx == -1) break;
                else if (read[idx] == '{')
                {
                    count++;
                }
                else if (read[idx] == '}')
                {
                    count--;
                }

                temp += read.Substring(0, idx + 1);
                read = read.Substring(idx + 1);

                if (count == 0)
                {
                    var start = temp.IndexOf('{');
                    var end = temp.LastIndexOf('}');
                    temp = temp.Substring(start + 1, end - start - 1);

                    sources.Add(temp);
                    temp = "";
                }
            }

            // 데이터를 객체로 변환
            foreach(var s in sources)
            {
                var datas = s.Split(',');
                var v = new UsageItem();

                foreach (var d in datas)
                {
                    var kv = d.Split(':');
                    var key = kv[0];
                    var val = kv[1];
                    if (key == KEYS.MONTH) { v.month = Convert.ToInt32(val); }
                    else if (key == KEYS.DAY) { v.day = Convert.ToInt32(val); }
                    else if (key == KEYS.BANK) { v.bank = (TypeBank)Convert.ToInt32(val); }
                    else if (key == KEYS.USAGE) { v.usage = (TypeUsage)Convert.ToInt32(val); }
                    else if (key == KEYS.PRICE) { v.price = Convert.ToInt32(val); }
                    else if (key == KEYS.CATEGORY) { v.category = Convert.ToInt32(val); }
                    else if (key == KEYS.DESC) { v.desc = val; }
                    else if (key == KEYS.TOBANK) { v.tobank = (TypeBank)Convert.ToInt32(val); }

                }
                datalist.Add(v);

            }

        }

        public async void SaveData()
        {
            var result = "";
            foreach(var v in datalist)
            {
                result += v.ToString()+",\n";
            }
            result = result.Remove(result.Length - 2);
                

            FileMaster.Write($"data{_year}", result, false);
        }
    }
}
