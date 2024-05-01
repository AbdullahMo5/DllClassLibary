using Goorge.Model;
using Goorge.Models;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Goorge.Services
{
    public class AccountService : IService
    {
        private CIMTManagerAPI managerAPI = null;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            if (m_manager != null)
            {
                managerAPI = m_manager;
            }
        }
        public ReturnModel UserAccountGet(ulong login)
        {

            ReturnModel returnModel = new ReturnModel();
            using (CIMTAccount account = managerAPI.UserCreateAccount())
            {
                returnModel.MTRetCode = managerAPI.UserAccountGet(login, account);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = new AccountModel(account);
                }
            }
            return returnModel;
        }
        public ReturnModel UserAccountGetByLogin(ulong login)
        {
            ReturnModel returnModel = new ReturnModel();
            using (var account = managerAPI.UserCreateAccount())
            {
                var response = managerAPI.UserAccountGet(login, account);
                returnModel.MTRetCode = response;
                if (response == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = account;
                }

            }
            return returnModel;
        }
        public ReturnModel UserAccountsGetByLogins(ulong[] logins)
        {
            ReturnModel returnModel = new ReturnModel();
            using (var accountsArray = managerAPI.UserCreateAccountArray())
            {
                var response = managerAPI.UserAccountRequestByLogins(logins, accountsArray);
                returnModel.MTRetCode = response;
                if (response == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = accountsArray;
                }

            }
            return returnModel;

        }
        public ReturnModel UserAccountsGetByGroup(string group)
        {
            ReturnModel model = new ReturnModel();
            try
            {
                using (var accountsArray = managerAPI.UserCreateAccountArray())
                {
                    var response = managerAPI.UserAccountRequestArray(group, accountsArray);
                    model.MTRetCode = response;
                    if (response == MTRetCode.MT_RET_OK)
                    {
                        var accountsList = new List<AccountModel>();
                        foreach (var item in accountsArray.ToArray())
                        {
                            accountsList.Add(new AccountModel(item));
                        }
                        model.Data = accountsList;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                model.Message = ex.ToString();
            }
            return model;
        }
        public ReturnModel UserAccountGetLoginsByGroup(string group)
        {
            ReturnModel model = new ReturnModel();
            try
            {
                using (var accountsArray = managerAPI.UserCreateAccountArray())
                {
                    var response = managerAPI.UserAccountRequestArray(group, accountsArray);
                    model.MTRetCode = response;
                    if (response == MTRetCode.MT_RET_OK)
                    {
                        var accountsList = new List<ulong>();
                        foreach (var item in accountsArray.ToArray())
                        {
                            var acc = new AccountModel(item);
                            accountsList.Add(acc.Login);
                        }
                        model.Data = accountsList.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                model.Message = ex.ToString();
            }
            return model;
        }
        public ReturnModel UserAccountGetEquity(ulong login)
        {

            ReturnModel returnModel = new ReturnModel();
            using (CIMTAccount account = managerAPI.UserCreateAccount())
            {
                returnModel.MTRetCode = managerAPI.UserAccountGet(login, account);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    var userAccount = new AccountModel(account);
                    returnModel.Data = userAccount.Equity;
                }
            }
            return returnModel;
        }
        public ReturnModel UserAccountGetEquity(ulong[] logins)
        {
            ReturnModel returnModel = new ReturnModel();
            using (var accountsArray = managerAPI.UserCreateAccountArray())
            {
                Dictionary<ulong, double> equities = new Dictionary<ulong, double>();
                var response = managerAPI.UserAccountRequestByLogins(logins, accountsArray);
                returnModel.MTRetCode = response;
                if (response == MTRetCode.MT_RET_OK)
                {
                    for (uint i = 0; i < accountsArray.Total(); i++)
                    {
                        var account = accountsArray.Next(i);
                        equities.Add(account.Login(), account.Equity());
                    }
                    returnModel.Data = equities;
                    returnModel.TotalCount = equities.Count;
                }
            }
            return returnModel;
        }
        //public ReturnModel UserAccountCreate(AccountModel accountModel)
        //{
        //    ReturnModel returnModel = new ReturnModel();
        //    try
        //    {
        //        using (CIMTAccount account = managerAPI.UserCreateAccount())
        //        {
        //            account.Login(accountModel.Login);
        //            account.Balance(accountModel.Balance);
        //            account.Equity(accountModel.Equity);
        //            account.Margin(accountModel.Margin);
        //            account.MarginFree(accountModel.MarginFree);
        //            account.MarginLeverage(accountModel.MarginLeverage);
        //            account.MarginLevel(accountModel.MarginLevel);
        //            account.MarginInitial(accountModel.MarginInitial);
        //            account.MarginMaintenance(accountModel.MarginMaintainance);
        //            account.Storage(accountModel.Swap);
        //            account.Profit(accountModel.Profit);
        //            account.Floating(accountModel.Floating);
        //            account.CurrencyDigits(accountModel.CurrencyDigits);
        //            account.Credit(accountModel.Credit);
        //            account.SOActivation(accountModel.SOActivation);
        //            account.SOTime(accountModel.SOTime);
        //            account.SOLevel(accountModel.SOLevel);
        //            account.SOEquity(accountModel.SOEquity);
        //            account.SOMargin(accountModel.SOMargin);
        //            account.BlockedCommission(accountModel.BlockedComission);
        //            account.Assets(accountModel.Assets);
        //            account.Liabilities(accountModel.Liabilities);

        //            returnModel.MTRetCode = managerAPI.UserAccountGet(accountModel.Login, account);
        //            if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
        //            {
        //                returnModel.Data = accountModel;
        //            }
        //            else
        //            {
        //                returnModel.Message = returnModel.MTRetCode.ToString();
        //            }
        //        }
        //        return returnModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        returnModel.Message = exception.InnerException.Message;
        //    }
        //    return returnModel;
        //}
    }
}
