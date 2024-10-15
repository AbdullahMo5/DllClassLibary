using Goorge.Model;
using Goorge.Models;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;

namespace Goorge.Services
{
    public class GroupService : IService
    {
        CIMTManagerAPI managerAPI = null;
        //CIMTConManager cIMTConManager = null;
        CIMTAdminAPI adminAPI = null;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            managerAPI = m_manager;
            //cIMTConManager = managerAPI.ManagerCreate();
            //cIMTConManager.Login(2023);
        }
        public void Initialize(CIMTManagerAPI m_manager, CIMTAdminAPI m_admin)
        {
            if (m_manager != null)
            {
                managerAPI = m_manager;
            }
            if (m_admin != null)
            {
                adminAPI = m_admin;
            }
        }
        public ReturnModel GetGroupTotal()
        {
            var returnModel = new ReturnModel();
            var total = managerAPI.GroupTotal();
            returnModel.Data = total;

            return returnModel;
        }
        public ReturnModel GetGroup(string _group)
        {
            var returnModel = new ReturnModel();
            using (var groupData = managerAPI.GroupCreate())
            {
                var resp = managerAPI.GroupGet(_group, groupData);
                returnModel.MTRetCode = resp;
                if (resp == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = groupData;
                }

            }
            return returnModel;
        }
        public ReturnModel GetAllGroups()
        {
            var returnModel = new ReturnModel();
            using (var group = managerAPI.GroupCreate())
            {
                List<GroupModel> groupsArray = new List<GroupModel>();
                var total = managerAPI.GroupTotal();
                for (uint i = 0; i < total; i++)
                {
                    group.Clear();
                    var resp = managerAPI.GroupNext(i, group);
                    if (resp == MTRetCode.MT_RET_OK)
                    {
                        groupsArray.Add(new GroupModel(group));
                    }
                }
                returnModel.Data = groupsArray;
                returnModel.TotalCount = groupsArray.Count;

            }

            return returnModel;

        }
        public ReturnModel UpdateGroup(GroupModel group)
        {
            ReturnModel returnModel = new ReturnModel();
            using (var groupToUpdate = adminAPI.GroupCreate())
            {
                adminAPI.GroupGet(group.Group, groupToUpdate);
                //groupToUpdate.Clear();
                groupToUpdate.Server(group.Server);
                groupToUpdate.PermissionsFlags(group.PermissionFlags);
                groupToUpdate.AuthMode(group.AuthMode);
                groupToUpdate.AuthPasswordMin(group.AuthPasswordMin);
                groupToUpdate.Company(group.Company);
                groupToUpdate.CompanyEmail(group.CompanyEmail);
                groupToUpdate.CompanySupportEmail(group.CompanySupportEmail);
                groupToUpdate.CompanyCatalog(group.CompanyCatalog);
                groupToUpdate.CompanyDepositPage(group.CompanyDepositPage);
                groupToUpdate.CompanyWithdrawalPage(group.CompanyWidthdrawalPage);
                groupToUpdate.Currency(group.Currency);
                groupToUpdate.CurrencyDigitsSet(group.CurrencyDigits);
                groupToUpdate.ReportsMode(group.ReportsMode);
                groupToUpdate.ReportsFlags(group.ReportsFlags);
                groupToUpdate.ReportsEmail(group.ReportsEmail);
                groupToUpdate.ReportsSMTP(group.ReportsSMTP);
                groupToUpdate.ReportsSMTPLogin(group.ReportsSMTPLogin);
                groupToUpdate.ReportsSMTPPass(group.ReportsSMTPPass);
                groupToUpdate.NewsMode(group.NewsMode);
                groupToUpdate.NewsCategory(group.NewsCategory);
                groupToUpdate.MailMode(group.MailMode);
                groupToUpdate.TradeFlags(group.TradeFlags);
                groupToUpdate.TradeInterestrate(group.TradeInterestrate);
                groupToUpdate.TradeVirtualCredit(group.TradeVirtualCredit);
                groupToUpdate.MarginFreeMode(group.MarginFreeMode);
                groupToUpdate.MarginSOMode(group.MarginSOMode);
                groupToUpdate.MarginCall(group.MarginCall);
                groupToUpdate.MarginStopOut(group.MarginStopOut);
                groupToUpdate.MarginFreeProfitMode(group.MarginFreeProfitMode);
                groupToUpdate.MarginFlags(group.MarginFlags);
                groupToUpdate.DemoLeverage(group.DemoLeverage);
                groupToUpdate.DemoDeposit(group.DemoDeposit);
                groupToUpdate.DemoInactivityPeriod(group.DemoInactivityPeriod);
                groupToUpdate.LimitHistory(group.LimitHistory);
                groupToUpdate.LimitOrders(group.LimitOrders);
                groupToUpdate.LimitSymbols(group.LimitSymbols);
                groupToUpdate.LimitPositions(group.LimitPositions);
                groupToUpdate.TradeTransferMode(group.TradeTransferMode);

                var resp = adminAPI.GroupUpdate(groupToUpdate);
                returnModel.MTRetCode = resp;

                if (resp == MTRetCode.MT_RET_OK)
                {
                    returnModel.Message = "OK";
                }
                else
                {
                    returnModel.Message = "Failed";
                }

            }
            return returnModel;
        }

        public ReturnModel CreateGroup(CreateGroupModel model)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                using (var groupToCreate = adminAPI.GroupCreate())
                {
                    groupToCreate.Group(model.Group);
                    groupToCreate.Company(model.Company);
                    groupToCreate.CompanyEmail(model.CompanyEmail);
                    groupToCreate.Server(model.Server);
                    groupToCreate.PermissionsFlags(model.PermissionFlags);
                    groupToCreate.AuthMode(model.AuthMode);
                    groupToCreate.AuthPasswordMin(model.AuthPasswordMin);
                    groupToCreate.Company(model.Company);
                    groupToCreate.CompanyEmail(model.CompanyEmail);
                    groupToCreate.CompanySupportEmail(model.CompanySupportEmail);
                    groupToCreate.CompanyCatalog(model.CompanyCatalog);
                    groupToCreate.CompanyDepositPage(model.CompanyDepositPage);
                    groupToCreate.CompanyWithdrawalPage(model.CompanyWidthdrawalPage);
                    groupToCreate.Currency(model.Currency);
                    groupToCreate.CurrencyDigitsSet(model.CurrencyDigits);
                    groupToCreate.ReportsMode(model.ReportsMode);
                    groupToCreate.ReportsFlags(model.ReportsFlags);
                    groupToCreate.ReportsEmail(model.ReportsEmail);
                    groupToCreate.ReportsSMTP(model.ReportsSMTP);
                    groupToCreate.ReportsSMTPLogin(model.ReportsSMTPLogin);
                    groupToCreate.ReportsSMTPPass(model.ReportsSMTPPass);
                    groupToCreate.NewsMode(model.NewsMode);
                    groupToCreate.NewsCategory(model.NewsCategory);
                    groupToCreate.MailMode(model.MailMode);
                    groupToCreate.TradeFlags(model.TradeFlags);
                    groupToCreate.TradeInterestrate(model.TradeInterestrate);
                    groupToCreate.TradeVirtualCredit(model.TradeVirtualCredit);
                    groupToCreate.MarginFreeMode(model.MarginFreeMode);
                    groupToCreate.MarginSOMode(model.MarginSOMode);
                    groupToCreate.MarginCall(model.MarginCall);
                    groupToCreate.MarginStopOut(model.MarginStopOut);
                    groupToCreate.MarginFreeProfitMode(model.MarginFreeProfitMode);
                    groupToCreate.MarginFlags(model.MarginFlags);
                    groupToCreate.DemoLeverage(model.DemoLeverage);
                    groupToCreate.DemoDeposit(model.DemoDeposit);
                    groupToCreate.DemoInactivityPeriod(model.DemoInactivityPeriod);
                    groupToCreate.LimitHistory(model.LimitHistory);
                    groupToCreate.LimitOrders(model.LimitOrders);
                    groupToCreate.LimitSymbols(model.LimitSymbols);
                    groupToCreate.LimitPositions(model.LimitPositions);
                    groupToCreate.TradeTransferMode(model.TradeTransferMode);
                    returnModel.MTRetCode = adminAPI.GroupUpdate(groupToCreate);
                }
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = "Group Has Created";
                }
                else
                {
                    returnModel.Message = returnModel.MTRetCode.ToString();
                }
            }
            catch (Exception ex)
            {
                returnModel.Message += ex.ToString();
            }
            return returnModel;
        }

        public MTRetCode UserUpdateGroup(ulong login, string group)
        {
            MTRetCode mTRetCode = MTRetCode.MT_RET_OK;
            mTRetCode = MTRetCode.MT_RET_OK_NONE;
            using (var user = managerAPI.UserCreate())
            {
                mTRetCode = managerAPI.UserGet(login, user);
                mTRetCode = user.Group(group);
                mTRetCode = managerAPI.UserUpdate(user);
            }

            return mTRetCode;
        }
    }
}
