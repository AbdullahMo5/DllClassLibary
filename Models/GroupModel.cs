using MetaQuotes.MT5CommonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MetaQuotes.MT5CommonAPI.CIMTConGroup;

namespace Goorge.Models
{
    public class GroupModel
    {
        public GroupModel()
        {

        }
        public GroupModel(CIMTConGroup _group)
        {
            Group = _group.Group();
            Server = _group.Server();
            PermissionFlags = _group.PermissionsFlags();
            AuthMode = _group.AuthMode();
            AuthPasswordMin = _group.AuthPasswordMin();
            Company = _group.Company(); 
            CompanyEmail = _group.CompanyEmail();   
            CompanySupportEmail = _group.CompanySupportEmail();
            CompanyCatalog = _group.CompanyCatalog();
            CompanyDepositPage = _group.CompanyDepositPage();
            CompanyWidthdrawalPage = _group.CompanyWithdrawalPage();
            Currency = _group.Currency();
            CurrencyDigits = _group.CurrencyDigits();
            ReportsMode = _group.ReportsMode();
            ReportsFlags = _group.ReportsFlags();
            ReportsEmail = _group.ReportsEmail();
            ReportsSMTP = _group.ReportsSMTP();
            ReportsSMTPLogin = _group.ReportsSMTPLogin();
            ReportsSMTPPass = _group.ReportsSMTPPass();
            NewsMode = _group.NewsMode();
            NewsCategory = _group.NewsCategory();
            MailMode = _group.MailMode();
            TradeFlags = _group.TradeFlags();
            TradeInterestrate = _group.TradeInterestrate(); 
            TradeVirtualCredit = _group.TradeVirtualCredit();
            MarginFreeMode  = _group.MarginFreeMode();
            MarginSOMode = _group.MarginSOMode();
            MarginCall = _group.MarginCall();
            MarginStopOut = _group.MarginStopOut();
            MarginFreeProfitMode = _group.MarginFreeProfitMode();
            MarginFlags = _group.MarginFlags();
            DemoLeverage = _group.DemoLeverage();
            DemoDeposit = _group.DemoDeposit();
            DemoInactivityPeriod = _group.DemoInactivityPeriod();
            LimitHistory = _group.LimitHistory();
            LimitOrders = _group.LimitOrders();
            LimitSymbols = _group.LimitSymbols();   
            LimitPositions = _group.LimitPositions();
            TradeTransferMode = _group.TradeTransferMode();
                
        }
        public string Group { get; set; }
        public ulong Server { get; set;}
        public EnPermissionsFlags PermissionFlags { get; set; }
        public EnAuthMode AuthMode{ get; set; }
        public uint AuthPasswordMin { get; set; }
        public string Company { get; set; }
        public string CompanyPage { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanySupportPage { get; set; }
        public string CompanySupportEmail { get; set; }
        public string CompanyCatalog { get; set; }
        public string CompanyDepositPage { get; set; }
        public string CompanyWidthdrawalPage { get; set; }
        public string Currency { get; set; }
        public uint CurrencyDigits { get; set; }
        public EnReportsMode ReportsMode { get; set; }
        public EnReportsFlags ReportsFlags { get; set; }
        public string ReportsEmail { get; set; }
        public string ReportsSMTP { get; set; }
        public string ReportsSMTPLogin { get; set; }
        public string ReportsSMTPPass { get; set; }
        public EnNewsMode NewsMode { get; set; }
        public string NewsCategory { get; set; }
        public EnMailMode MailMode { get; set; }
        public EnTradeFlags TradeFlags{ get; set; }
        public double TradeInterestrate { get; set; }
        public double TradeVirtualCredit { get; set; }
        public EnFreeMarginMode MarginFreeMode { get; set; }
        public EnStopOutMode MarginSOMode{ get; set; }
        public double MarginCall { get; set; }
        public double MarginStopOut { get; set; }
        public uint MarginFreeProfitMode { get; set; }
        public uint MarginMode { get; set; }
        public EnMarginFlags MarginFlags { get; set; }
        public uint DemoLeverage { get; set; }
        public double DemoDeposit { get; set; }
        public uint  DemoInactivityPeriod{ get; set; }
        public EnHistoryLimit LimitHistory { get; set; }
        public uint LimitOrders { get; set; }
        public uint LimitSymbols { get; set; }
        public uint LimitPositions { get; set; }
        public EnTransferMode TradeTransferMode { get; set; }

            

    }
}
