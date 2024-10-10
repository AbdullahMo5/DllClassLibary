using MetaQuotes.MT5CommonAPI;
using static MetaQuotes.MT5CommonAPI.CIMTConGroup;

namespace Goorge.Models
{
    public class CreateGroupModel
    {
        public string Group { get; set; }
        public CIMTConCommission CommisionAdd { get; set; }
        public CIMTConGroupSymbol SymbolAdd { get; set; }
        public ulong Server { get; set; }
        public EnPermissionsFlags PermissionFlags { get; set; }
        public EnAuthMode AuthMode { get; set; }
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
        public EnTradeFlags TradeFlags { get; set; }
        public double TradeInterestrate { get; set; }
        public double TradeVirtualCredit { get; set; }
        public EnFreeMarginMode MarginFreeMode { get; set; }
        public EnStopOutMode MarginSOMode { get; set; }
        public double MarginCall { get; set; }
        public double MarginStopOut { get; set; }
        public uint MarginFreeProfitMode { get; set; }
        public EnMarginMode MarginMode { get; set; }
        public EnMarginFlags MarginFlags { get; set; }
        public uint DemoLeverage { get; set; }
        public double DemoDeposit { get; set; }
        public uint DemoInactivityPeriod { get; set; }
        public EnHistoryLimit LimitHistory { get; set; }
        public uint LimitOrders { get; set; }
        public uint LimitSymbols { get; set; }
        public uint LimitPositions { get; set; }
        public EnTransferMode TradeTransferMode { get; set; }
    }
}
