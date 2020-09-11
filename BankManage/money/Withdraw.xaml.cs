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
using BankManage.common;

namespace BankManage.money
{
    /// <summary>
    /// Withdraw.xaml 的交互逻辑
    /// </summary>
    public partial class Withdraw : Page
    {
        BankEntities context = new BankEntities();
        public Withdraw()
        {
            InitializeComponent();
        }
        //取款
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string str;
            Custom custom = DataOperation.GetCustom(this.txtAccount.Text);
           // CustomFixed customFixed = (CustomFixed)DataOperation.GetCustom(this.txtAccount.Text);
          //  CustomSmallSum customSmallSum = (CustomSmallSum)DataOperation.GetCustom(this.txtAccount.Text);
            if (custom.AccountInfo.accountPass != this.txtPassword.Password)
            {
                MessageBox.Show("密码不正确");
                return;
            }
            if (custom.AccountInfo.IsLoss == 1)
            {
                MessageBox.Show("该卡已经被挂失！");
                return;
            }
            int mark = this.txtAccount.Text.IndexOf("3");
            if (mark == 0)
            {
                var q = from t in context.MoneyInfo
                        where t.accountNo == this.txtAccount.Text
                        select t.balance;
                
                str = q.ToList().Last().ToString();
                //var qq = q.Last();
                if (str != this.txtmount.Text)
                {
                    MessageBox.Show("请一次将零存整取账户中的余额取完\n" + "当前余额：" + str + " 元");
                    return;
                }

            }
            if (this.txtAccount.Text.IndexOf("3")==0)
            {
                CustomSmallSum css = new CustomSmallSum();
                css.setaccount(this.txtAccount.Text);
            }else if(this.txtAccount.Text.IndexOf("2") == 0)
            {
                CustomFixed cf = new CustomFixed();
                cf.setaccount(this.txtAccount.Text);
            }
            custom.Withdraw(double.Parse(this.txtmount.Text));
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
        //取消取款
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
    }
}
