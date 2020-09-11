using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace BankManage.employee
{
  
    public partial class AddEmployee : Window
    {
        BankEntities context = new BankEntities();
        public AddEmployee()
        {
            InitializeComponent();
        }
        string photofilePath = "";
        private void buttonbro_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                photofilePath = ofd.FileName;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(photofilePath, UriKind.RelativeOrAbsolute);
                bi.EndInit();
                this.imagephoto.Source = bi;
            }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            EmployeeInfo Empl = new EmployeeInfo();
            Empl.EmployeeNo = this.txtEmployeeNo.Text;
            Empl.EmployeeName = this.txtEmployeeName.Text;
            Empl.sex = this.radioMan.IsChecked == true ? "男" : "女";
            Empl.workDate = this.datePickerworkDate.SelectedDate;
            Empl.telphone = this.txttelphone.Text;
            Empl.idCard = this.txtidCard.Text;
            if (photofilePath != "")
            {
                Stream mystream = File.OpenRead(photofilePath);
                byte[] bt = new byte[mystream.Length];
                mystream.Read(bt, 0, (int)mystream.Length);
                Empl.photo = bt;
            }
            try
            {
                context.EmployeeInfo.Add(Empl);
                int i = context.SaveChanges();
                MessageBox.Show(string.Format("成功添加{0}个职员信息", i));
            }
            catch
            {
                MessageBox.Show("添加职员失败！");
            }
            this.Close();
        }

        private void buttonOK1_Click(object sender, RoutedEventArgs e)
        {

            this.txtEmployeeName.Text = "";
            this.txtEmployeeNo.Text = "";
            this.txttelphone.Text = "";
            this.imagephoto.Source = null;
            this.txtidCard.Text = "";
        }
    }
}

