using Banker.DATA;
using Banker.VIEW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        public DateTime date { get => master.targetdate; }
        public int year { get => date.Year; }
        public int month { get => date.Month; }
        public MainWindow()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;

            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);

            master = MASTER.instance;
            master.Init(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            USAGE = new PAGE_USAGE();
            META = new PAGE_META();
            MONTH = new PAGE_MONTH();
            frame.Navigate(MONTH);

            BTN_Target.Content = $"{date.Year}-{date.Month.ToString().PadLeft(2, '0')}";
            
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            master.Done();
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
                USAGE.LoadBankData();
                frame.Navigate(USAGE);

            }
            else if(btn == btn_page_meta)
            {
                frame.Navigate(META);

            }

        }

        public void ChangePage(int bankcode)
        {

            USAGE.LoadBankData(bankcode);
            frame.Navigate(USAGE);

        }

        private void BTN_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BTN_Save(object sender, RoutedEventArgs e)
        {
            master.Save();
        }

        private void BTN_Date(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == BTN_Pre) master.DateChange(-1);
            else if (btn == BTN_Next) master.DateChange(1);
            else if (btn == BTN_Target) master.DateChange(0);
            BTN_Target.Content = $"{date.Year}-{date.Month.ToString().PadLeft(2, '0')}";
        }
    }
}
