using Banker.DATA;
using Banker.MODEL;
using Banker.UTIL;
using Banker.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Banker.MODEL.ENUM;

namespace Banker.VIEW
{
    /// <summary>
    /// PAGE_USAGE.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PAGE_USAGE : Page
    {
        private MASTER master = MASTER.instance;
        private MasterMeta meta;
        private USAGE vm;
        private int nowbank = -1;
        private TYPEUSAGEDATA pagetype = TYPEUSAGEDATA.all;

        public PAGE_USAGE()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm = master.usage;
            meta = master.metadata;
            this.DataContext = vm;

            vm.LoadData(pagetype, nowbank);
            Create_CBItem();
        }


        private void BTN_DataInput(object sender, RoutedEventArgs e)
        {
            if (INPUT_price.Text == "") return;

            var bank = INPUT_bank.SelectedItem as string;
            var date = list_date[INPUT_date.SelectedIndex];
            var usage = list_usage[INPUT_use.SelectedIndex];

            var price_str = INPUT_price.Text.Replace(",", "");
            var price = Convert.ToInt32(price_str);

            if (usage == TypeUsage.pay || usage == TypeUsage.move)
            {
                var tobank = INPUT_bank_sub.SelectedItem as string;
                vm.InputData(date, bank, usage, price, tobank);

            }
            else
            {
                var category = Convert.ToString(INPUT_category.SelectedItem);
                var desc = INPUT_desc.Text;
                vm.InputData(date, bank, usage, price, category, desc);

            }
            INPUT_price.Text = "";
            INPUT_desc.Text = "";
        }

        private List<int> list_date;
        private List<TypeUsage> list_usage;

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
            var v = meta.GetBankList();
            INPUT_bank.ItemsSource = v;
            INPUT_bank.SelectedIndex = nowbank;

            #endregion

            #region use_category
            List<Category> categories = new List<Category>();

            
            var categorys = meta.categorys.Select(x => x.DESC).ToList();

            INPUT_category.ItemsSource = categorys;
            INPUT_category.SelectedIndex = 0;
            #endregion
        }

        private void OnlyNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void NumberDecoration(object sender, TextChangedEventArgs e)
        {
            var target = sender as TextBox;
            if (target.Text == "") return;
            var temp = Convert.ToInt64(target.Text.Replace(",", ""));
            target.Text = STRING.Num2String(temp);
            target.SelectionStart = target.Text.Length;
        }

        private void Usage_Change(object sender, SelectionChangedEventArgs e)
        {
            var v = (sender as ComboBox).SelectedItem as TypeUsage?;

            var sublist = meta.GetBankList();

            if(v == TypeUsage.move )
            {
                INPUT_bank_sub.Visibility = Visibility.Visible;
                INPUT_bank_sub.ItemsSource = sublist;
                INPUT_bank_sub.SelectedIndex = 0;
            }
            else if(v == TypeUsage.pay)
            {
                INPUT_bank_sub.Visibility = Visibility.Visible;
                INPUT_bank_sub.ItemsSource = sublist;
                INPUT_bank_sub.SelectedIndex = 0;
            }
            else
            {
                INPUT_bank_sub.Visibility = Visibility.Hidden;
            }
            

        }
    
        public void LoadBankData(int bankcode = -1)
        {
            nowbank = bankcode;
            pagetype = TYPEUSAGEDATA.all;
        }

        private void BTN_ListType(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if(btn == btn_entire)
            {
                grid_entire.Visibility = Visibility.Visible;
                grid_set.Visibility = Visibility.Collapsed;
                pagetype = TYPEUSAGEDATA.all;
            }
            else if(btn == btn_cluster)
            {
                grid_entire.Visibility = Visibility.Collapsed;
                grid_set.Visibility = Visibility.Visible;
                pagetype = TYPEUSAGEDATA.statistics;
            }

            vm.LoadData(pagetype, nowbank);
        }

        private void BTN_UsageDelete(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            var contents = b.DataContext as DataUsage;
            vm.RemoveItem(contents);
        }
    }
}
