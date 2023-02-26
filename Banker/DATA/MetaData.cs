using Banker.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.DATA
{
    public class MetaData
    {
        private Dictionary<string, string> sources;
        public Dictionary<int,string> categorys { get; set; }

        public MetaData()
        {
            categorys = new Dictionary<int, string>();
            sources = new Dictionary<string, string>();

            LoadData();
        }
        public void Close()
        {
            SaveData();
        }
        private async void LoadData()
        {
            var data = await FileMaster.Read("meta");
            if (data == null) return;

            var temp = "";
            var count = 0;
            char[] target = { '{', '}' };
            while (true) {
                var idx = data.IndexOfAny(target);
                if (idx == -1) break ;
                else if (data[idx] == '{')
                {
                    count++;
                }
                else if(data[idx] == '}')
                {
                    count--;
                }

                temp += data.Substring(0, idx+1);
                data = data.Substring(idx+1);

                if(count == 0)
                {
                    var start = temp.IndexOf('{');
                    var end = temp.LastIndexOf('}');
                    temp = temp.Substring(start+1, end - start -1);
                    var keyidx = temp.IndexOf(":");

                    sources.Add(temp.Substring(0, keyidx), temp.Substring(keyidx+1)) ;
                    temp = "";
                }
            }

            #region read category
            var categorydata = sources[KEYS.CATEGORY];
            categorydata = categorydata.Substring(1, categorydata.Length - 2);
            var categorydata_list = categorydata.Split(',');

            foreach( var c in categorydata_list)
            {
                var temp_category = c.Split(':');
                categorys.Add(Convert.ToInt32( temp_category[0]), temp_category[1]);
            }




            #endregion

        }
        private async void SaveData()
        {
            string category_str = "";
            foreach(var v in categorys)
            {
                category_str += $"{v.Key}:{v.Value},";
            }
            category_str = category_str.Remove(category_str.Length - 1);


            string source = "{" + KEYS.CATEGORY + ":{" + category_str + "}}";
            FileMaster.Write("meta", source, false);
        }
        
    }
}
