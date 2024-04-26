using Goorge.Model;
using Goorge.Models;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Goorge.Services
{
    public class GroupService : IService
    {
        CIMTManagerAPI managerAPI = null;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            managerAPI = m_manager;
        }
        public ReturnModel GetGroupTotal() {
            var returnModel = new ReturnModel();
            var total = managerAPI.GroupTotal();
            returnModel.Data = total;           
            
            return returnModel;
        }
        public ReturnModel GetGroup(string _group) {
            var returnModel = new ReturnModel();
            using (var groupData = managerAPI.GroupCreate()) {
                var resp = managerAPI.GroupGet(_group, groupData);
                returnModel.MTRetCode = resp;
                if(resp == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = groupData;
                }
            
            }
            return returnModel;
        }
        public ReturnModel GetAllGroups() { 
            var returnModel = new ReturnModel();
            using(var group = managerAPI.GroupCreate())
            {
                List<GroupModel> groupsArray = new List<GroupModel>();
                var total = managerAPI.GroupTotal();
                for(uint i = 0; i < total; i++)
                {
                    group.Clear();
                    var resp = managerAPI.GroupNext(i, group);
                    if(resp == MTRetCode.MT_RET_OK)
                    {
                        groupsArray.Add(new GroupModel(group));
                    }
                }
                returnModel.Data = groupsArray;
                returnModel.TotalCount = groupsArray.Count;

            }

            return returnModel;

        }
        public ReturnModel UpdateGroup(GroupModel group) { 
            ReturnModel returnModel = new ReturnModel();
            using (var groupToUpdate = managerAPI.GroupCreate()) {
                groupToUpdate.Clear();
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
                
                var resp = managerAPI.GroupUpdate(groupToUpdate);
                returnModel.MTRetCode = resp;

                if (resp == MTRetCode.MT_RET_OK) { 
                    returnModel.Message = "OK";
                }
                else
                {
                    returnModel.Message = "Failed";
                }
            
            }
            return returnModel;            
        }
    }
}
