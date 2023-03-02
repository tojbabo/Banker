using Banker.DATA;
using Banker.MODEL;
using Banker.UTIL;
using Banker.VIEWMODEL;
using System;
using System.Collections.Generic;
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
        private USAGE vm;

        public PAGE_USAGE()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm = master.usage;
            this.DataContext = vm;
            Create_CBItem();
        }


        private void BTN_DataInput(object sender, RoutedEventArgs e)
        {
            if (INPUT_price.Text == "") return;

            var bank = INPUT_bank.SelectedItem as TypeBank?;
            var date = list_date[INPUT_date.SelectedIndex];
            var usage = list_usage[INPUT_use.SelectedIndex];
            var price = Convert.ToInt32(INPUT_price.Text);

            if (usage == TypeUsage.pay || usage == TypeUsage.move)
            {
                var tobank = INPUT_bank_sub.SelectedItem as TypeBank?;
                vm.InputData(date, bank??TypeBank.none, usage, price, tobank??TypeBank.none);

            }
            else
            {
                var category = Convert.ToString(INPUT_category.SelectedItem);
                var desc = INPUT_desc.Text;
                vm.InputData(date, bank??TypeBank.none, usage, price, category, desc);

            }
        }

        List<int> list_date;
        List<TypeUsage> list_usage;
        List<TypeBank> list_bank;
        List<TypeBank> list_bank_credit;
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
            list_bank_credit = new List<TypeBank>();
            list_bank.Add(TypeBank.shinhan);
            list_bank.Add(TypeBank.kakao);
            list_bank.Add(TypeBank.suhyup);
            list_bank.Add(TypeBank.ibk);
            list_bank.Add(TypeBank.credit_samsung);

            list_bank_credit.Add(TypeBank.credit_samsung);

            INPUT_bank.ItemsSource = list_bank;
            INPUT_bank.SelectedItem = TypeBank.shinhan;




            #endregion

            #region use_category
            var categorys = master.metadata.categorys.Values;

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

            if(v == TypeUsage.move )
            {
                INPUT_bank_sub.Visibility = Visibility.Visible;
                INPUT_bank_sub.ItemsSource = list_bank;
                INPUT_bank_sub.SelectedItem = TypeBank.kakao;
            }
            else if( v == TypeUsage.pay)
            {
                INPUT_bank_sub.Visibility = Visibility.Visible;
                INPUT_bank_sub.ItemsSource = list_bank_credit;
                INPUT_bank_sub.SelectedItem = TypeBank.credit_samsung;
            }
            else
            {
                INPUT_bank_sub.Visibility = Visibility.Hidden;
            }
            

        }
    }
}
