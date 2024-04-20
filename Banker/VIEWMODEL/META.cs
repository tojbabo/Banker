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
        private MasterMeta metadata { get => master.metadata; }
        private MasterUsage maindata { get => master.maindata; }

        public ObservableCollection<Category> categorys { get => metadata.categorys; }
        public ObservableCollection<DataInit> initcashs { get => metadata.inits; }


        public META() {
            master = MASTER.instance;
        }
        public void Input_Category(string n_ctg)
        {
            int n_key = 0;

            if(categorys.Count != 0)
            {
                var v = categorys.ToList().OrderBy(x => x.CODE).Last();
                n_key = v.CODE;
            }

            categorys.Add(new Category()
            {
                CODE = n_key+1,
                DESC = n_ctg
            });

        }

        public void Input_InitCash(string name, EBank bank, int cash)
        {
            var code = metadata.GetLastCode();
            initcashs.Add(new DataInit()
                {
                    name = name,
                    banktype = bank,
                    bankcode = code,
                    price = cash,
                    time = DateTime.Now
                });

            //리로드 
        }

        public void save()
        {
            metadata.SaveData();
        }
        
    }
}
