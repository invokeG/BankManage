using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankManage.common;
using BankManage.money;

namespace BankManage
{
    /// <summary>
    /// 定期存款
    /// </summary>
    public class CustomFixed : Custom
    {
        public RateType type { get; set; }
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        public override void Create(string accountNumber, double money)
        {
            base.Create(accountNumber, money);
        }
        public static double preRate;
        int DYear;
        DateTime date1;
        public static string account1;
        /// <summary>
        ///存款 
        /// </summary>
        public override void Diposit(string genType, double money, int year)
        {
            BankEntities context = new BankEntities();
            base.Diposit("存款", money, year);
            if (year == 1)
            {
                //定期类型
                type = RateType.定期1年;
                base.Diposit("结息", DataOperation.GetRate(RateType.定期1年) * money, year);
                //到期利息
                preRate = DataOperation.GetRate(RateType.定期1年) * money;
            }
            if (year == 2)
            {
                base.Diposit("结息", DataOperation.GetRate(RateType.定期3年) * money, year);
                type = RateType.定期3年;
                preRate = DataOperation.GetRate(RateType.定期3年) * money;
            }
            if (year == 3)
            {
                base.Diposit("结息", DataOperation.GetRate(RateType.定期5年) * money, year);
                type = RateType.定期5年;
                preRate = DataOperation.GetRate(RateType.定期5年) * money;
            }
        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        /// 
        public void setaccount(string account)
        {
            account1 = account;
        }

        public override void Withdraw(double money)
        {
            BankEntities context = new BankEntities();
            var qq = from t in context.MoneyInfo
                     where t.dealType == "存款" && t.accountNo == account1
                     select t;
            var q = qq.FirstOrDefault();
            if (!ValidBeforeWithdraw(money)) return;
            //存款时间
            date1 = Convert.ToDateTime(q.dealDate);
            //计算利息

            //取款时间
            DateTime date2 = DateTime.Now;
            if (type == RateType.定期1年)
            {
                DYear = 1;
            }
            if (type == RateType.定期3年)
            {
                DYear = 3;
            }
            if (type == RateType.定期5年)
            {
                DYear = 5;
            }
            //1年
            if (DYear == 1 && (date2 - date1).TotalDays >= 365)
            {
                if ((date2 - date1).TotalDays > 365)
                {
                    //超期利息
                    double rate = DataOperation.GetRate(RateType.定期超期部分) * AccountBalance;
                    //添加利息
                    AccountBalance += rate;
                    //取款
                    base.Withdraw(money);
                }
                else
                    base.Withdraw(money);
            }
            else if (DYear == 1 && (date2 - date1).TotalDays < 365)
            {
                AccountBalance -= preRate;
                //不足日期
                double rate = DataOperation.GetRate(RateType.定期提前支取) * AccountBalance;
                //添加利息
                AccountBalance += rate;
                AccountBalance -= preRate;
                //取款
                base.Withdraw(money);
            }



            //3年
            if (DYear == 3 && (date2 - date1).TotalDays >= 365 * 3)
            {
                if ((date2 - date1).TotalDays > 365 * 3)
                {
                    //超期利息
                    double rate = DataOperation.GetRate(RateType.定期超期部分) * AccountBalance;
                    //添加利息
                    AccountBalance += rate;
                    //取款
                    base.Withdraw(money);
                }
                else
                    base.Withdraw(money);
            }
            else if (DYear == 3 && (date2 - date1).TotalDays < 365 * 3)
            {
                AccountBalance -= preRate;
                //不足日期
                double rate = DataOperation.GetRate(RateType.定期提前支取) * AccountBalance;
                //添加利息
                AccountBalance += rate;
                AccountBalance -= preRate;
                //取款
                base.Withdraw(money);
            }


            //5年
            if (DYear == 5 && (date2 - date1).TotalDays >= 365 * 5)
            {
                if ((date2 - date1).TotalDays > 365 * 5)
                {
                    //超期利息
                    double rate = DataOperation.GetRate(RateType.定期超期部分) * AccountBalance;
                    //添加利息
                    AccountBalance += rate;
                    //取款
                    base.Withdraw(money);
                }
                else
                    base.Withdraw(money);
            }
            else if (DYear == 5 && (date2 - date1).TotalDays < 365 * 5)
            {
                AccountBalance -= preRate;
                //不足日期
                double rate = DataOperation.GetRate(RateType.定期提前支取) * AccountBalance;
                //添加利息
                AccountBalance += rate;
                AccountBalance -= preRate;
                //取款
                base.Withdraw(money);
            }

        }
    }
}
