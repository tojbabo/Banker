using Banker.DATA;
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

        }
        private void BTN_CategoryInput(object sender, RoutedEventArgs e)
        {
            var category = INPUT_category.Text;
            vm.Input_Category(category);

        }

        private void BTN_InitCashInput(object sender, RoutedEventArgs e)
        {

        }
    }
}
