using Banker.DATA;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Banker.MODEL
{
    public class Category
    {
        public int CODE { get; set; }
        public string DESC { get; set; }
        public JObject ToJson()
        {
            JObject j = new JObject();
            j.Add(KEYS.CODE, CODE);
            j.Add(KEYS.DESC, DESC);
            return j;
        }
    }
}
