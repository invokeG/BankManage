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
using BankManage;
using BankManage.common;

namespace BankManage
{
    /// <summary>
    /// LoginForm.xaml 的交互逻辑
    /// </summary>
    public partial class LoginForm : Window
    {
        public string UserName { get; set; }
        private BankEntities dbEntity = new BankEntities();
        public LoginForm()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            UserName = string.Empty;
        }
        //单击登录时进行身份验证
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var query = from t in dbEntity.LoginInfo
                        where t.Bno == this.combox.Text && t.Password == this.pass.Password
                        select t;
            if (query.Count() > 0)
            {
                var q = query.First();
                UserName = DataOperation.GetOperateName(q.Bno);
                this.Close();
            }
            else
            {
                MessageBox.Show("登录失败！");
                this.pass.Clear();
                this.pass.Focus();
            }

        }
        //关闭窗体
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //窗体关闭时进行关闭操作
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.UserName) == true)
            {
                App.Current.Shutdown();
            }
        }
        //将账户表中的账号信息显示到此处
        private void Window_SourceInitialized_1(object sender, EventArgs e)
        {
            var query = from t in dbEntity.LoginInfo
                        select t.Bno;
            this.combox.ItemsSource = query.ToList();
        }

    }
}
