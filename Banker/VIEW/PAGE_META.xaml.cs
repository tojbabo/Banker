using Banker.DATA;
using Banker.UTIL;
using Banker.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// PAGE_META.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PAGE_META : Page
    {
        MASTER master = MASTER.instance;
        META vm;
        public PAGE_META()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm = master.META;
            this.DataContext = vm;
            Create_Combox();

        }
        
        private void BTN_CategoryInput(object sender, RoutedEventArgs e)
        {
            var category = INPUT_category.Text;
            vm.Input_Category(category);

            INPUT_category.Text = "";

        }

        private void BTN_InitCashInput(object sender, RoutedEventArgs e)
        {
            var idx = COMBO_banktype.SelectedIndex;
            var name = INPUT_initbank.Text;

            if (name == "") return;

            var cash = Convert.ToInt32(INPUT_initcash.Text.Replace(",", ""));

            EBank b = (EBank)idx;


            vm.Input_InitCash(name, b, cash);

            INPUT_initcash.Text = "";
            INPUT_initbank.Text = "";


        }

        List<String> list_bank;
        private void Create_Combox()
        {
            #region bank
            list_bank = new List<String>();
            list_bank.Add(EBank.io.desc());
            list_bank.Add(EBank.credit.desc());
            list_bank.Add(EBank.saving.desc());

            COMBO_banktype.ItemsSource = list_bank;
            COMBO_banktype.SelectedIndex = 0;

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

        private void BTN_MetaSave(object sender, RoutedEventArgs e)
        {
            vm.save();
        }
    }
}
