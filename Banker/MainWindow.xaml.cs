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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            USAGE = new PAGE_USAGE();
            META = new PAGE_META();
            MONTH = new PAGE_MONTH();

            frame.Navigate(USAGE);
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
                frame.Navigate(USAGE);

            }
            else if(btn == btn_page_meta)
            {
                frame.Navigate(META);

            }

        }

        
    }
}
