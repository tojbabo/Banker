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
    public class MetaData
    {
        private Dictionary<string, string> sources;
        public Dictionary<int,string> categorys { get; set; }
        public ObservableCollection<InitItem> inits { get; set; }


        public MetaData()
        {
            categorys = new Dictionary<int, string>();
            sources = new Dictionary<string, string>();

            inits = new ObservableCollection<InitItem>();
        }
        public void Close()
        {
            SaveData();
        }
        public async void LoadData()
        {
            var data = await FileMaster.Read("meta");
            if (data == null) return;

            var temp_source_list = STRING.BreakBracket(data);
            foreach(var t in temp_source_list)
            {
                var keyidx = t.IndexOf(":");

                sources.Add(t.Substring(0, keyidx), t.Substring(keyidx + 1));
            }

            // read category
            try
            {
                var categorydata = sources[KEYS.CATEGORY];
                categorydata = categorydata.Substring(1, categorydata.Length - 2);
                var categorydata_list = categorydata.Split(',');

                foreach (var c in categorydata_list)
                {
                    var temp_category = c.Split(':');
                    categorys.Add(Convert.ToInt32(temp_category[0]), temp_category[1]);
                }
            }
            catch (Exception e){ }

            // read init cash
            try
            {
                var initdata = sources[KEYS.INITCASH];
                initdata = initdata.Substring(1, initdata.Length - 2);
                var temp_init_list = STRING.BreakBracket(initdata);
                foreach (var t in temp_init_list)
                {
                    InitItem m = new InitItem();
                    var temp_strs = t.Split(',');
                    foreach (var s in temp_strs)
                    {
                        var temp_s = s.Split(':');
                        var k = temp_s[0];
                        var v = temp_s[1];
                        if (k == KEYS.DATE)
                        {
                            m.time = DateTime.Parse(v);
                        }
                        else if (k == KEYS.BANK)
                        {
                            m.bank = (TypeBank)Convert.ToInt32(v);

                        }
                        else if (k == KEYS.PRICE)
                        {
                            m.price = Convert.ToInt32(v);
                        }

                    }
                    inits.Add(m);
                }
            }
            catch (Exception e) { }
          
    
        }
        public async void SaveData()
        {
            string category_str = "";
            string init_str = "";

            if (categorys.Count != 0)
            {
                foreach (var v in categorys)
                {
                    category_str += $"{v.Key}:{v.Value},";
                }
                category_str = category_str.Remove(category_str.Length - 1);
            }
            if (inits.Count != 0)
            {
                foreach (var v in inits)
                {
                    var time = v.time;
                    init_str += $"{v.ToString()},";
                }
                init_str = init_str.Remove(init_str.Length - 1);
            }


            string source = $"{{{KEYS.CATEGORY}:{{{category_str}}}}}\n" +
                $"{{{KEYS.INITCASH}:{{{init_str}}}}}";
            FileMaster.Write("meta", source, false);
        }    
    }
}
