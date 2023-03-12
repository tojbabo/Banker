using Banker.DATA;
using Banker.MODEL;
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
    /// PAGE_MONTH.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PAGE_MONTH : Page
    {
        private MASTER master = MASTER.instance;
        private MONTH vm;

        public PAGE_MONTH()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm = master.month;
            this.DataContext = vm;

            vm.LoadData();

        }

        private void LB_SelectBank(object sender, SelectionChangedEventArgs e)
        {
            var lb = sender as ListBox;
            var item = lb.SelectedItem as MonthItem ;
            var bank = item.bank;

            master.main.ChangePage(bank);
        }
    }
}
