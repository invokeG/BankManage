using BankManage.money;
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

namespace BankManage.other
{
    /// <summary>
    /// BankReportLoss.xaml 的交互逻辑
    /// </summary>
    public partial class BankReportLoss : Page
    {
        public BankReportLoss()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Custom LossCustom = new Custom();
            LossCustom.AccountInfo.accountNo = txtAccountNo.Text.Trim();
            LossCustom.AccountInfo.accountName = txtAccountName.Text.Trim();
            LossCustom.AccountInfo.accountPass = txtPass.Password.Trim();
            LossCustom.AccountInfo.IdCard = txtIDCard.Text.Trim();
            LossCustom.BankReportLoss(LossCustom);
         
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.txtAccountName.Clear();
            this.txtIDCard.Clear();
            this.txtAccountName.Clear();
            this.txtPass.Clear();
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
    }
}
