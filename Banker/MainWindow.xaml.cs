using Banker.DATA;
using Banker.VIEW;
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

namespace Banker
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        PAGE_MONTH MONTH;
        PAGE_USAGE USAGE;
        PAGE_META META;
        MASTER master;
        public MainWindow()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;

            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            master = MASTER.instance;
            master.Init(this);
            USAGE = new PAGE_USAGE();
            META = new PAGE_META();
            MONTH = new PAGE_MONTH();

            frame.Navigate(MONTH);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            MASTER.instance.Done();
        }

        private void BTN_PageChange(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            
            
            if (btn == btn_page_month)
            {
                frame.Navigate(MONTH);

            }
            else if (btn == btn_page_usage)
            {
                USAGE.LoadBankData(TypeBank.none);
                frame.Navigate(USAGE);

            }
            else if(btn == btn_page_meta)
            {
                frame.Navigate(META);

            }

        }

        public void ChangePage(TypeBank bank)
        {

            USAGE.LoadBankData(bank);
            frame.Navigate(USAGE);

        }
        
    }
}
