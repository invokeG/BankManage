using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BankManage;

namespace BankManage
{
    /// <summary>
    /// 保存客户发生的最新业务信息
    /// </summary>
    public class Custom
    {
        /// <summary>
        /// 存款客户的帐户基本信息
        /// </summary>
        public AccountInfo AccountInfo { get; set; }
        /// <summary>
        /// 存款发生信息
        /// </summary>
        public MoneyInfo MoneyInfo { get; set; }
        /// <summary>
        /// 帐户余额
        /// </summary>
        ///  /// 学生短期借款信息
        /// </summary>
        public StudentBorrowMoneyInfo StudentBorrowMoneyInfo { get; set; }
        public double AccountBalance { get; set; }
        public Custom()
        {
            AccountInfo = new AccountInfo();
            MoneyInfo = new MoneyInfo();
            StudentBorrowMoneyInfo = new StudentBorrowMoneyInfo();
        }

        BankEntities context = new BankEntities();
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        public virtual void Create(string accountNumber, double money)
        {
           int mark = accountNumber.IndexOf("3");
            if(mark==0)
            {
                AccountInfo.accountMark = "1";
            }
            this.AccountBalance = money;
            bool success = false;
            //插入到数据库
             try
             {
                  context.AccountInfo.Add(AccountInfo);
                  context.SaveChanges();
                  success = true;
              }
             catch(Exception err)
             {
                success = false;
                MessageBox.Show("开户失败！"+err.Message);
             }
            if (success)
            {
                this.MoneyInfo.accountNo = accountNumber;
                InsertData("开户", money);
            }
        }

        /// <summary>
        ///存款 
        /// </summary>
        /// <param name="genType">类型</param>
        /// <param name="money">金额</param>
        public virtual void Diposit(string genType, double money)
        {
            if (money <= 0)
            {
                throw new Exception("存款金额不能为零或负值");
            }
            else
            {
                //修改余额
                AccountBalance += money;
                InsertData(genType, money);
            }
        }
        public virtual void Diposit(string genType, double money, int year)
        {
            if (money <= 0)
            {
                throw new Exception("存款金额不能为零或负值");
            }
            else
            {
                //修改余额
                AccountBalance += money;
                InsertData(genType, money);
            }
        }

        /// <summary>
        ///检查是否允许取款，允许返回true，否则返回false
        /// </summary>
        /// <param name="money">取款金额</param>
        public bool ValidBeforeWithdraw(double money)
        {
            if (money <= 0)
            {
                MessageBox.Show("取款金额不能为零或负值");
                return false;
            }
            if (money > AccountBalance)
            {
                MessageBox.Show("取款数不能比余额大");
                return false;
            }
            return true;
        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public virtual void Withdraw(double money)
        {
            AccountBalance -= money;
            InsertData("取款", -money);
        }

        /// <summary>
        /// 在表中添加新记录
        /// </summary>
        /// <param name="genType">发生类别</param>
        /// <param name="money">发生金额</param>
        public void InsertData(string genType, double money)
        {

            MoneyInfo.accountNo = this.AccountInfo.accountNo;
            MoneyInfo.dealDate = DateTime.Now;
            MoneyInfo.dealType = genType;
            MoneyInfo.dealMoney = money;
            if (this.AccountInfo.accountNo.IndexOf("3") == 0)
                MoneyInfo.balance = double.Parse(string.Format("{0:F5}", AccountBalance));
            else
                MoneyInfo.balance = AccountBalance;
            try
            {
                context.MoneyInfo.Add(MoneyInfo);
                context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("添加交易记录失败：" );
            }
           
        }
        public bool BankReportLoss(Custom LossCustomInfo)
        {
            bool success = false;
            var query = from t in context.AccountInfo
                        where t.accountNo == LossCustomInfo.AccountInfo.accountNo
                        select t;
            if (query.Count() <= 0)
            {
                MessageBox.Show("您输入的账户信息有误！");
                success = false;
            }

            else
            {
                var q = query.First();
                if (q.IsLoss == 1)
                {
                    MessageBox.Show("该卡号已经被挂失！");
                    success = false;
                }
                else
                {
                    q.IsLoss = 1;
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("挂失成功！");

                        success = true;
                    }
                    catch
                    {
                        MessageBox.Show("挂失失败！");
                        success = false;
                    }
                }
                context.Dispose();
            }
            return success;
        }
        public bool StudentBorrowMoney(Custom studentCustom)
        {

            bool success = false;
            //插入到数据库
            try
            {
                context.StudentBorrowMoneyInfo.Add(StudentBorrowMoneyInfo);
                context.SaveChanges();
                studentCustom.MoneyInfo.accountNo = studentCustom.StudentBorrowMoneyInfo.accountNo;
                studentCustom.Diposit("存款", double.Parse(studentCustom.StudentBorrowMoneyInfo.BorrowMoney));
                MessageBox.Show("借款成功！");

                success = true;
            }
            catch (Exception err)
            {
                success = false;
                MessageBox.Show("借款失败！" + err.Message);
            }
            context.Dispose();
            return success;
        }
    }
}
