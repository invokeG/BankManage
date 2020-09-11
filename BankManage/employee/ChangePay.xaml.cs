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

namespace BankManage.employee
{
    /// <summary>
    /// ChangePay.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePay : Page
    {
        DateTime date1;
        DateTime date2;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public ChangePay()
        {
            InitializeComponent();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("你已经成功上班打卡");
            date1 = DateTime.Now;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BankEntities Content = new BankEntities();
            if (date1 != null)
            {
                DateTime date2 = DateTime.Now;
                if ((date2 - date1).TotalHours < 8)
                {
                    MessageBox.Show("您未完成今日的工作任务");
                }
                else
                {
                    MessageBox.Show("你已经成功下班打卡");
                    WageInfo wg1 = new WageInfo();
                    wg1.worktime = this.date1;
                    wg1.closingtime = this.date2;
                    wg1.normaltime = 320;
                    wg1.overtime = 0;
                    if ((date2 - date1).TotalHours == 1)
                    {
                        wg1.overtime = 20;
                    }
                    if ((date2 - date1).TotalHours == 2)
                    {
                        wg1.overtime = 40;
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Wage wg = new Wage();
            wg.ShowDialog();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Label1.Content = DateTime.Now.ToString("HH:mm:ss dddd,dd,MM,yyyy");
        }
    }
}
