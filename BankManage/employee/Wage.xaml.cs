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
using System.Windows.Shapes;

namespace BankManage.employee
{
    /// <summary>
    /// Wage.xaml 的交互逻辑
    /// </summary>
    public partial class Wage : Window
    {
        BankEntities context = new BankEntities();
        public Wage()
        {
            InitializeComponent();
            this.dataGrid.CanUserAddRows = false;
            this.dataGrid.CanUserDeleteRows = false;
            ShowResult();
        }
        private void ShowResult()
        {

            if (context != null)
            {
                context.Dispose();
                context = new BankEntities();
            }
            //step2
            var q = from t in context.WageInfo
                    select t;
            //step 3
            this.dataGrid.ItemsSource = q.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("请等待管理员的查看");
        }
    }
}
