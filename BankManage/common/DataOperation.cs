using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BankManage;
using BankManage.money;

namespace BankManage.common
{
    public class DataOperation
    {
        public static readonly string[] AccountType = { "活期存款", "定期存款", "零存整取" };
        public static string str;


        /// <summary>
        /// 获取操作员姓名
        /// </summary>
        /// <param name="id">操作员编号</param>
        /// <returns></returns>
        public static string GetOperateName(string id)
        {
            using (BankEntities c = new BankEntities())
            {
                var q = from t in c.EmployeeInfo
                         where t.EmployeeNo == id
                         select t;
                if (q != null && q.Count()>=1)
                {
                    return q.First().EmployeeName;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 根据存款类型创建存款用户
        /// </summary>
        /// <param name="accountType">存款类型</param>
        /// <returns></returns>
        public static Custom CreateCustom(string accountType)
        {
            Custom custom = null;
            switch (accountType)
            {
                case "活期存款":
                    custom = new CustomChecking();
                    break;
                case "定期存款":
                    custom = new CustomFixed();
                    break;
                case "零存整取":
                    custom = new CustomSmallSum();
                    break;
            }
           
            custom.AccountInfo.accountType = accountType;
            return custom;
        }

        /// <summary>
        /// 获取存款用户信息,并初始化余额
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public static Custom GetCustom(string accountNumber)
        {
            Custom custom = null;
            DataOperation.str = accountNumber;
            BankEntities c = new BankEntities();
            try
            {
                var query= from t in c.AccountInfo
                         where t.accountNo == accountNumber
                         select t;
                if (query.Count() > 0)
                {
                    var q = query.Single();
                    custom = CreateCustom(q.accountType);
                    custom.AccountInfo.accountNo = accountNumber;
                    custom.AccountInfo.accountName = q.accountName;
                    custom.AccountInfo.accountPass = q.accountPass;
                    custom.AccountInfo.IdCard = q.IdCard;
                    custom.AccountInfo.accountMark = q.accountMark;
                    custom.AccountInfo.accountClass = q.accountClass;
                }
            }
            catch
            {
                return null;
            }
            var qt = from t in c.MoneyInfo
                      where t.accountNo == accountNumber
                      select t;
            if (qt != null && qt.Count() > 0)
            {
                custom.AccountBalance = qt.Sum(x => x.dealMoney);
            }
            return custom;
        }

        /// <summary>
        /// 获取指定类别的利率
        /// </summary>
        /// <param name="rateType">利率类别</param>
        /// <returns>对应类别的利率值</returns>
        public static double GetRate(RateType rateType)
        {
            bool mark = true;
            string type = rateType.ToString();
            BankEntities context = new BankEntities();
            var qq = from t in context.AccountInfo
                     where t.accountNo == DataOperation.str
                     select t;
            var m = qq.FirstOrDefault();
            if (type == "零存整取1年" || type == "零存整取3年" || type == "零存整取5年")
            {
                if (type == "零存整取1年")
                {
                    for (int i = 2; i <= 12; i++)
                    {
                        if (!m.accountMark.Contains(i.ToString()))
                            mark = false;
                    }
                }else if(type == "零存整取3年")
                {
                    for (int i = 2; i <= 36; i++)
                    {
                        if (!m.accountMark.Contains(i.ToString()))
                            mark = false;
                    }
                }
                else
                {
                    for (int i = 2; i <= 60; i++)
                    {
                        if (!m.accountMark.Contains(i.ToString()))
                            mark = false;
                    }
                }
                if (mark == false)
                    type = "零存整取违规";
            }
            BankEntities c = new BankEntities();
            var q = (from t in c.RateInfo
                     where t.rationType == type
                     select t.rationValue).Single();
            return q.Value;
        }
    }
}
