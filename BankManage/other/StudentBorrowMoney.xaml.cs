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
    /// StudentBorrowMoney.xaml 的交互逻辑
    /// </summary>
    public partial class StudentBorrowMoney : Page
    {
        public StudentBorrowMoney()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Custom StudentCustom = new Custom();
            StudentCustom.StudentBorrowMoneyInfo.studentNum = txtStudentNum.Text.Trim();
            StudentCustom.StudentBorrowMoneyInfo.studentName = txtStudentName.Text.Trim();
            StudentCustom.StudentBorrowMoneyInfo.accountNo = txtAccountNo.Text.Trim();
            StudentCustom.StudentBorrowMoneyInfo.IdCard = txtIDCard.Text.Trim();
            StudentCustom.StudentBorrowMoneyInfo.borrowTime = txtBorrowTime.Text.Trim();
            StudentCustom.StudentBorrowMoneyInfo.BorrowMoney = txtBorrowMoney.Text.Trim();
          bool flag=  StudentCustom.StudentBorrowMoney(StudentCustom);
            if (flag)
            {
                this.txtStudentName.Clear();
                this.txtStudentNum.Clear();
                this.txtIDCard.Clear();
                this.txtBorrowTime.Clear();
                this.txtAccountNo.Clear();
                this.txtBorrowMoney.Clear();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.txtStudentName.Clear();
            this.txtStudentNum.Clear();
            this.txtIDCard.Clear();
            this.txtBorrowTime.Clear();
            this.txtAccountNo.Clear();
            this.txtBorrowMoney.Clear();
           
        }
    }
}
