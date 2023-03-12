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
    public class META : INPC
    {

        private MASTER master;
        private MetaData metadata { get => master.metadata; }
        private MainData maindata { get => master.maindata; }

        public Dictionary<int,string> categorys { get => metadata.categorys; }
        public ObservableCollection<InitItem> initcashs { get => metadata.inits; }


        public META() {
            master = MASTER.instance;
        }
        public void Input_Category(string n_ctg)
        {
            int n_key = 0;
            if(categorys.Count !=0) n_key = categorys.Keys.Max();
            categorys.Add(n_key + 1, n_ctg);

        }

        public void Input_InitCash(TypeBank? bank, int cash )
        {
            if(bank != null)
            {
                initcashs.Add(new InitItem()
                {
                    bank = bank??TypeBank.none ,
                    price = cash,
                    time = DateTime.Now
                });
            }
        }
    }
}
