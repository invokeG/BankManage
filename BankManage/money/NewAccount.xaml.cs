﻿using System;
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
    /// NewAccount.xaml 的交互逻辑
    /// </summary>
    public partial class NewAccount : Page
    {
        public NewAccount()
        {
            InitializeComponent();
            InitComboBox();
        }

        private void InitComboBox()
        {
            string[] items = Enum.GetNames(typeof(MoneyAccountType));
            foreach (var s in items)
            {
                comboBoxAccountType.Items.Add(s);
            }
            comboBoxAccountType.SelectedIndex = 0;
        }
        //开户
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAccountType.SelectedItem.ToString() == "零存整取")
                if (int.Parse(this.txtMoney.Text) < 5)
                {
                    MessageBox.Show("请存入大于5的金额！");
                    return;
                }
                    
            Custom custom = DataOperation.CreateCustom(comboBoxAccountType.SelectedItem.ToString());
            custom.AccountInfo.accountNo = this.txtAccountNo.Text;
            custom.AccountInfo.IdCard = this.txtIDCard.Text;
            custom.AccountInfo.accountName = this.txtAccountName.Text;
            custom.AccountInfo.accountPass = this.txtPass.Password;
            custom.Create(this.txtAccountNo.Text, double.Parse(this.txtMoney.Text));
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);  
            ns.Navigate(page);
        }
        //取消开户
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.txtAccountName.Clear();
            this.txtIDCard.Clear();
            this.txtAccountName.Clear();
            this.txtPass.Clear();
            this.txtMoney.Clear();
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }

        private void comboBoxAccountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = comboBoxAccountType.SelectedItem.ToString();
            using(BankEntities c = new BankEntities())
            {
                var q = from t in c.AccountInfo
                        where t.accountType == s
                        select t;
                if (q.Count() > 0)
                {
                    this.txtAccountNo.Text = string.Format("{0}", int.Parse(q.Max(x => x.accountNo)) + 1);
                }
                else
                {
                    txtAccountNo.Text = string.Format("{0}00001", comboBoxAccountType.SelectedIndex + 1);
                }
            }
        }
    }
}
