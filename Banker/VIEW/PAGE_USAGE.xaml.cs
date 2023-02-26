using Banker.DATA;
using Banker.MODEL;
using Banker.UTIL;
using Banker.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Banker.MODEL.ENUM;

namespace Banker.VIEW
{
    /// <summary>
    /// PAGE_USAGE.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PAGE_USAGE : Page
    {
        private MASTER master = MASTER.instance;
        private USAGE vm;

        public PAGE_USAGE()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            master.Init();

            vm = master.usage;
            this.DataContext = vm;
            Create_CBItem();
        }


        private void BTN_DataInput(object sender, RoutedEventArgs e)
        {
            if (INPUT_price.Text == "") return;

            var bank = list_bank[INPUT_bank.SelectedIndex];
            var date = list_date[INPUT_date.SelectedIndex];
            var usage = list_usage[INPUT_use.SelectedIndex];
            var category = Convert.ToString(INPUT_category.SelectedItem);
            var price = Convert.ToInt32(INPUT_price.Text);
            var desc = INPUT_desc.Text;

            vm.InputData(date, bank, usage, price, category, desc);

        }

        List<int> list_date;
        List<TypeUsage> list_usage;
        List<TypeBank> list_bank;
        private void Create_CBItem()
        {
            #region date
            var today = DateTime.Now;

            var datemax = (today.Month == 2) ? 29 :
                ((today.Month < 8)
                ? ((today.Month % 2 == 0) ? 31 : 30)
                : ((today.Month % 2 == 0) ? 30 : 31));

            list_date = new List<int>();
            for (var i = 1; i <= datemax; i++)
            {
                list_date.Add(i);
            }

            INPUT_date.ItemsSource = list_date;
            INPUT_date.SelectedItem = today.Day;
            #endregion

            #region usage
            list_usage = new List<TypeUsage>();
            list_usage.Add(TypeUsage.make);
            list_usage.Add(TypeUsage.use);
            list_usage.Add(TypeUsage.move);
            list_usage.Add(TypeUsage.pay);

            INPUT_use.ItemsSource = list_usage;
            INPUT_use.SelectedItem = TypeUsage.use;

            #endregion

            #region bank
            list_bank = new List<TypeBank>();
            list_bank.Add(TypeBank.shinhan);
            list_bank.Add(TypeBank.kakao);
            list_bank.Add(TypeBank.suhyup);
            list_bank.Add(TypeBank.ibk);
            list_bank.Add(TypeBank.credit_samsung);

            INPUT_bank.ItemsSource = list_bank;
            INPUT_bank.SelectedItem = TypeBank.shinhan;

            #endregion

            #region use_category
            var categorys = master.metadata.categorys.Values;

            INPUT_category.ItemsSource = categorys;
            INPUT_category.SelectedIndex = 0;
            #endregion

        }



    }
}
