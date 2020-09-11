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
   
    public partial class EditEmployee : Window
    {
        BankEntities context = new BankEntities();
        EmployeeInfo editEmployeeInfo;
        public EditEmployee(string EmployeeNo)
        {
            InitializeComponent();
            showEmployeeInfo(EmployeeNo);
        }
       

        public void showEmployeeInfo(string id)
        {
            var q = from t in context.EmployeeInfo
                    where t.EmployeeNo == id
                    select t;
            if (q != null)
            {
                editEmployeeInfo = q.FirstOrDefault();
                this.txtEmployeeNo.Text = id;
                this.txtEmployeeName.Text = editEmployeeInfo.EmployeeName;
                if (editEmployeeInfo.sex == "男")
                    this.radioMan.IsChecked = true;
                else
                    this.radioWoman.IsChecked = true;
                this.datePickerworkDate.SelectedDate = editEmployeeInfo.workDate;
                this.txttelphone.Text = editEmployeeInfo.telphone.ToString();
                this.txtidCard.Text = editEmployeeInfo.idCard.ToString();
                if (editEmployeeInfo.photo != null)
                {
                    MemoryStream ms = new MemoryStream(editEmployeeInfo.photo);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    this.imagephoto.Source = bi;
                }
            }
        }
        string photofilePath = "";
        private void buttonbro_Click(object sender, RoutedEventArgs e)
        {
            this.imagephoto.Source = null;
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
            editEmployeeInfo.EmployeeNo = this.txtEmployeeNo.Text;
            editEmployeeInfo.EmployeeName = this.txtEmployeeName.Text;
            editEmployeeInfo.sex = this.radioMan.IsChecked == true ? "男" : "女";
            editEmployeeInfo.workDate = this.datePickerworkDate.SelectedDate;
            editEmployeeInfo.telphone = this.txttelphone.Text;
            editEmployeeInfo.idCard = this.txtidCard.Text;

            if (photofilePath != "")
            {
                Stream mystream = File.OpenRead(photofilePath);
                byte[] bt = new byte[mystream.Length];
                mystream.Read(bt, 0, (int)mystream.Length);
                editEmployeeInfo.photo = bt;
            }
            try
            {
                int i = context.SaveChanges();
                MessageBox.Show(string.Format("成功修改{0}个职员信息", i));
            }
            catch
            {
                MessageBox.Show("修改职员失败！");
            }
            this.Close();
            editEmployeeInfo.EmployeeNo = this.txtEmployeeNo.Text;
            editEmployeeInfo.EmployeeName = this.txtEmployeeName.Text;
            editEmployeeInfo.sex = this.radioMan.IsChecked == true ? "男" : "女";
            editEmployeeInfo.workDate = this.datePickerworkDate.SelectedDate;
            editEmployeeInfo.telphone = this.txttelphone.Text;
            editEmployeeInfo.idCard = this.txtidCard.Text;
            if (photofilePath != "")
            {
                Stream mystream = File.OpenRead(photofilePath);
                byte[] bt = new byte[mystream.Length];
                mystream.Read(bt, 0, (int)mystream.Length);
                editEmployeeInfo.photo = bt;
            }
            try
            {
                int i = context.SaveChanges();
                MessageBox.Show(string.Format("成功修改{0}个职员信息", i));
            }
            catch
            {
                MessageBox.Show("修改职员失败！");
            }
            this.Close();
        }

        private void buttonOK1_Click(object sender, RoutedEventArgs e)
        {
            this.txttelphone.Text = "";
            this.txtEmployeeNo.Text = "";
            this.txtEmployeeName.Text = "";
            this.txtidCard.Text = "";
            this.imagephoto.Source = null;
        }
    }
}
