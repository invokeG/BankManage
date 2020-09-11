using BankManage.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.money
{
    public class CustomSmallSum: Custom
    {
        private static string account1;
        //BankEntities context = new BankEntities();
        public RateType type { get; set; }
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        public override void Create(string accountNumber, double money)
        {
            base.Create(accountNumber, money);
        }

        /// <summary>
        ///存款 
        /// </summary>
        public override void Diposit(string genType, double money)
        {
            base.Diposit("存款", money);
            //结算利息
            //base.Diposit("结息", DataOperation.GetRate(RateType.定期1年) * money);
        }
        public void setaccount(string account)
        {
            account1 = account;
        }
        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public override void Withdraw(double money)
        {
            if (!ValidBeforeWithdraw(money)) return;
            //计算利息
            BankEntities context = new BankEntities();
            var q = from t in context.AccountInfo
                    where t.accountNo == account1
                    select t;
            var qq = q.First();
            double rate =0;
            if(qq.accountClass=="1")
                rate = DataOperation.GetRate(RateType.零存整取1年) * AccountBalance;
            else if(qq.accountClass == "3")
                rate = DataOperation.GetRate(RateType.零存整取3年) * AccountBalance;
            else if (qq.accountClass == "5")
                rate = DataOperation.GetRate(RateType.零存整取5年) * AccountBalance;
            else if (qq.accountClass == null)
                rate = DataOperation.GetRate(RateType.零存整取违规) * AccountBalance;
            //添加利息
            string str = string.Format("{0:F5}", rate.ToString());
            rate = double.Parse(str);
            AccountBalance += rate;
            //取款
            base.Withdraw(money);
        }
    }
}
