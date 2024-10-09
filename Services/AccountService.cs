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
        public ReturnModel UserAccountDetailsGet(ulong login)
        {
            ReturnModel returnModel = new ReturnModel();
            UserModel userRes = null;
            AccountModel accountRes = null;
            AccountDetailsModel detailsModel = null;

            try
            {
                using (var user = managerAPI.UserCreate())
                {
                    MTRetCode request = managerAPI.UserGet(login, user);
                    returnModel.MTRetCode = request;
                    if (request == MTRetCode.MT_RET_OK)
                    {
                        userRes = new UserModel(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnModel.Message = ex.InnerException.Message;
            }

            using (CIMTAccount account = managerAPI.UserCreateAccount())
            {
                MTRetCode accountGetRequest = managerAPI.UserAccountGet(login, account);
                returnModel.MTRetCode = accountGetRequest;
                if (accountGetRequest == MTRetCode.MT_RET_OK)
                {
                    accountRes = new AccountModel(account);                  
                    if (userRes != null && accountRes != null)
                    {
                        detailsModel = new AccountDetailsModel(accountRes, userRes);
                    }
                    returnModel.Data = detailsModel;
                }
            }
            return returnModel;
        }
        public ReturnModel UserAccountsDetailsGetByLogins(ulong[] logins)
        {
            ReturnModel returnModel = new ReturnModel();
            List<AccountModel> accountList = new List<AccountModel>();
            List<UserModel> userList = new List<UserModel>();
            List<AccountDetailsModel> detailList = new List<AccountDetailsModel>();

            try
            {
                using (CIMTUserArray users = managerAPI.UserCreateArray())
                {
                    returnModel.MTRetCode = managerAPI.UserRequestByLogins(logins, users);
                    if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                    {
                        foreach (var item in users.ToArray())
                        {
                            userList.Add(new UserModel(item));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                returnModel.Message = ex.InnerException.Message;
            }
            using (var accountsArray = managerAPI.UserCreateAccountArray())
            {
                var response = managerAPI.UserAccountRequestByLogins(logins, accountsArray);
                returnModel.MTRetCode = response;
                if (response == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in accountsArray.ToArray())
                    {
                        accountList.Add(new AccountModel(item));
                    }
                }
            }
            if(accountList.Count == userList.Count)
            {
                for (int i = 0; i < accountList.Count; i++)
                {
                    detailList.Add(new AccountDetailsModel(accountList[i], userList[i]));
                }
            }
            else { returnModel.Message = "Couldn't Merge as Some Users doesn't have Trading account"; }
            returnModel.Data = detailList;
            returnModel.TotalCount = detailList.Count;
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
            List<AccountModel> accountList = new List<AccountModel>();
            using (var accountsArray = managerAPI.UserCreateAccountArray())
            {
                var response = managerAPI.UserAccountRequestByLogins(logins, accountsArray);
                returnModel.MTRetCode = response;
                if (response == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in accountsArray.ToArray())
                    {
                        accountList.Add(new AccountModel(item));
                    }
                    returnModel.TotalCount = Convert.ToInt32(accountsArray.Total());
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
