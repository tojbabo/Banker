using Banker.MODEL;
using Banker.UTIL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Banker.MODEL.ENUM;

namespace Banker.DATA
{
    public class MasterUsage
    {
        private int _year;
        private int _month;
        private JObject sources;

        public ObservableCollection<DataUsage> usages { get; set; }

        public MasterUsage(int year, int month)
        {
            this._year = year;
            this._month = month;
            sources = null;
            usages = new ObservableCollection<DataUsage>();
        }

        public Task LoadData(int year = -1,int month = - 1)
        {
            usages.Clear();
            var t = Task.Run( async () =>
            {
                if(month != -1) this._month = month; 

                if(sources == null)
                {
                    var read = await FileMaster.Read($"data{_year}");
                    if (read == null) return;
                    sources = JObject.Parse(read);
                }

                JArray ary = sources[KEYS.USAGE] as JArray;
                foreach (var j in ary)
                {
                    var tempusage = new DataUsage(j);
                    if(tempusage.month == this._month)
                    {
                        usages.Add(tempusage);
                    }

                }
            });
            return t;
        }

        public async void SaveData()
        {
            JObject obj = new JObject();
            JArray ary = new JArray();
            foreach(var v in usages)
            {
                ary.Add(v.ToJson());
            }
            obj.Add(KEYS.USAGE, ary);
            var str = obj.ToString();
            str = str.Trim();
            str = str.Replace("},\r\n","},\n");
            str = str.Replace("\r\n","");
            str = str.Replace(" ","");

            FileMaster.Write($"data{_year}", str, false);
        }
    }
}
