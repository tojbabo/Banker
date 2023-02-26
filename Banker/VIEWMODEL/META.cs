using Banker.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.VIEWMODEL
{
    public class META
    {

        MASTER master;
        MetaData data;
        public Dictionary<int,string> categorys { get => data.categorys; }

        public META() {
            master = MASTER.instance;
            data = master.metadata;
        }
        public void Input_Category(string n_ctg)
        {
            int n_key = 0;
            if(categorys.Count !=0) n_key = categorys.Keys.Max();
            categorys.Add(n_key + 1, n_ctg);
        }
    }
}
