using Goorge.Model;

namespace Goorge.Models
{
    public class AccountDetailsModel : AccountModel
    {
        public long LastAccess { get; set; }
        public string LastIP { get; set; }
        public long LastPassChange { get; set; }
        public uint Leverage { get; set; }
        public string Group { get; set; }
        public double CommissionAgentDaily { get; set; }
        public double CommissionAgentMonthly { get; set; }
        public double CommissionDaily { get; set; }
        public double CommissionMonthly { get; set; }

        public AccountDetailsModel(AccountModel accountModel, UserModel userModel)
        {
            Login = accountModel.Login;
            Balance = accountModel.Balance;
            Equity = accountModel.Equity;
            Margin = accountModel.Margin;
            MarginFree = accountModel.MarginFree;
            MarginLeverage = accountModel.MarginLeverage;
            MarginLevel = accountModel.MarginLevel;
            MarginInitial = accountModel.MarginInitial;
            MarginMaintainance = accountModel.MarginMaintainance;
            Swap = accountModel.Swap;
            //Comission = accountModel.Comission;
            Profit = accountModel.Profit;
            Floating = accountModel.Floating;
            CurrencyDigits = accountModel.CurrencyDigits;
            Credit = accountModel.Credit;
            SOActivation = accountModel.SOActivation;
            SOTime = accountModel.SOTime;
            SOLevel = accountModel.SOLevel;
            SOEquity = accountModel.SOEquity;
            SOMargin = accountModel.SOMargin;
            BlockedComission = accountModel.BlockedComission;
            Assets = accountModel.Assets;
            Liabilities = accountModel.Liabilities;

            LastAccess = userModel.LastAccess;
            LastIP = userModel.LastIP;
            LastPassChange = userModel.LastPassChange;
            Leverage = userModel.Leverage;
            Group = userModel.Group;
            CommissionAgentDaily = userModel.CommissionAgentDaily;
            CommissionAgentMonthly = userModel.CommissionAgentMonthly;
            CommissionDaily = userModel.CommissionDaily;
            CommissionMonthly = userModel.CommissionMonthly;
        }
    }
}
