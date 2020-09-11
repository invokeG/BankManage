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
    /// EmployeeBase.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeBase : Page
    {
        BankEntities context = new BankEntities();
        public EmployeeBase()
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
            var q = from t in context.EmployeeInfo
                    select t;
            //step 3
            this.dataGrid.ItemsSource = q.ToList();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee ad = new AddEmployee ();
            ad.ShowDialog();
            ShowResult();
        }

        private void button_Update_Click(object sender, RoutedEventArgs e)
        {
            var item = dataGrid.SelectedItem as EmployeeInfo;
            if (item == null)
            {
                MessageBox.Show("请先选择要修改的职员信息！");
                return;
            }
            EditEmployee ed = new EditEmployee(item.EmployeeNo);
            ed.ShowDialog();
            ShowResult();
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = dataGrid.SelectedItem as EmployeeInfo;
            if (item == null)
            {
                MessageBox.Show("请先选择要删除的职员信息！");
                return;
            }
            MessageBoxResult result = MessageBox.Show("您确定要删除该职员的信息吗？", "删除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                context = new BankEntities();
                var q = from t in context.EmployeeInfo
                        where t.EmployeeNo == item.EmployeeNo
                        select t;
                if (q != null)
                {
                    try
                    {
                        context.EmployeeInfo.Remove(q.FirstOrDefault());
                        int i = context.SaveChanges();
                        MessageBox.Show(string.Format("成功删除{0}个职员信息", i));
                    }
                    catch
                    {
                        MessageBox.Show("删除失败！");
                    }
                }
            }
            ShowResult();
        }
    }

  
}

