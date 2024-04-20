using Banker.DATA.ABSTRACT;
using Banker.MODEL;
using Banker.UTIL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using static Banker.MODEL.ENUM;

namespace Banker.DATA
{
    public class MasterMeta : INPC
    {
        private Dictionary<string, string> sources;
        public ObservableCollection<Category> categorys { get; set; }
        public ObservableCollection<DataInit> inits { get; set; }

        private DateTime __target;

        public MasterMeta()
        {
            categorys = new ObservableCollection<Category>();
            sources = new Dictionary<string, string>();

            inits = new ObservableCollection<DataInit>();
        }
        public void Close()
        {
            //SaveData();
        }

        public Task LoadData(DateTime target)
        {
            __target = target;
            categorys.Clear();
            inits.Clear();
            var task = Task.Run(async () =>
            {
                var data = await FileMaster.Read("meta");
                if (data == null) return;

                try
                {
                    JObject source = JObject.Parse(data);


                    // read category
                    var cds = source[KEYS.CATEGORY];
                    foreach (var c in cds)
                    {
                        categorys.Add(new Category()
                        {
                            CODE = Convert.ToInt16(c[KEYS.CODE].ToString()),
                            DESC = c[KEYS.DESC].ToString()
                        });
                    }

                    // read init cash
                    var ids = source[KEYS.INITCASH];

                    foreach (var t in ids)
                    {
                        DataInit m = new DataInit(t);
                        inits.Add(m);
                    }                    
                }
                catch (Exception ex)
                {
                    return;
                }

            });
            return task;
        }
        public async void SaveData()
        {
            string category_str = "";
            string init_str = "";
            JObject j = new JObject();

            JArray cateary = new JArray();
            if (categorys.Count != 0)
            {
                foreach (var v in categorys) cateary.Add(v.ToJson());
            }

            JArray initary = new JArray();
            if (inits.Count != 0)
            {
                foreach (var v in inits) initary.Add(v.ToJson());
            }
            
            j.Add(KEYS.CATEGORY, cateary);
            j.Add(KEYS.INITCASH, initary);

            var source = j.ToString();
            source = source.Replace("\n", "");
            source = source.Replace("\r", "");
            source = source.Replace("\t", "");
            source = source.Replace(" ", "");

            FileMaster.Write("meta", source, false);
        }

        public string GetCategory(int key)
        {
            foreach(var x in categorys)
            {
                if(x.CODE == key)
                {
                    return x.DESC;
                }
            }
            return "";
        }
        public string GetBankName(int code)
        {
            foreach(var v in inits)
            {
                if(v.bankcode == code)
                {
                    return v.name;
                }
            }
            return null;

        }
        public int GetBankCode(string name)
        {
            foreach(var i in inits)
            {
                if (i.name == name) return i.bankcode;
            }
            return -1;
        }
        public int GetLastCode()
        {
            if (inits.Count == 0) return 0;
            var v = inits.ToList().OrderBy(x => x.bankcode).Last();

            return v.bankcode + 1;
        }

        public List<string> GetBankList()
        {
            var v = inits.ToList().Distinct().ToList();
            var vv = v.Select(x => x.name).ToList();
            return vv;
        }

    }
}
