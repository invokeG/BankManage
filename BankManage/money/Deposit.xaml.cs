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
    /// Deposit.xaml 的交互逻辑
    /// </summary>
    public partial class Deposit : Page
    {
        public Deposit()
        {
            InitializeComponent();
            
        }
        //存款
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Custom custom = DataOperation.GetCustom(this.txtAccount.Text);
            if (custom == null)
            {
                MessageBox.Show("帐号不存在！");
                return;
            }
            if (custom.AccountInfo.IsLoss == 1)
            {
                MessageBox.Show("该卡已经被挂失！");
                return;
            }
            //获得combox selectindex值
            BankEntities context = new BankEntities();
            custom.MoneyInfo.accountNo = txtAccount.Text;
            if (combox.SelectedIndex == 0)
            {
                custom.Diposit("存款", double.Parse(this.txtmount.Text),combox.SelectedIndex);
            }
            if(combox.SelectedIndex>=1&&combox.SelectedIndex<=3)
            {
                custom.Diposit("存款", double.Parse(this.txtmount.Text), combox.SelectedIndex);
            }
            if(combox.SelectedIndex>3&&combox.SelectedIndex<=6)
            {
                var q = from t in context.MoneyInfo
                        where t.accountNo == this.txtAccount.Text && t.dealType == "开户"
                        select t;
                var w = q.FirstOrDefault();
                var qq = from t in context.AccountInfo                            
                        where t.accountNo == this.txtAccount.Text
                        select t;
                var m = qq.FirstOrDefault();
                if (txtmount.Text != w.balance.ToString())
                {
                    MessageBox.Show("此金额不正确，请重新填写！");
                    return;
                }
                else
                {
                    if(m.accountClass == null)
                    {
                        if (combox.SelectedIndex == 4) m.accountClass = 1.ToString();
                        else if (combox.SelectedIndex == 5) m.accountClass = 3.ToString();
                        else m.accountClass = 5.ToString();
                    }
                     if (m.accountMark.Contains(MouthTimeBox.Text))
                    {
                        MessageBox.Show("此月已经存入所需金额，请重新填写！");
                        return;
                    }
                    else
                    {
                        string str = m.accountMark;
                        m.accountMark = str+" "+ MouthTimeBox.Text;
                        context.SaveChanges();
                    }
                }
                custom.Diposit("存款", double.Parse(this.txtmount.Text) , combox.SelectedIndex);
            }
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
        //取消存款
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }


        private void combox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combox.SelectedIndex > 3 && combox.SelectedIndex <= 6)
            {
                MouthTimesLabel.Visibility = Visibility.Visible;
                MouthTimeBox.Visibility = Visibility.Visible;
            }
        }
    }
}
