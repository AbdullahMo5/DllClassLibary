using MetaQuotes.MT5CommonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Models
{
    public class AccountModel
    {
        public AccountModel(CIMTAccount account)
        {
            Login = account.Login();
            Balance = account.Balance();
            Equity = account.Equity();
            Margin = account.Margin();
            MarginFree = account.MarginFree();
            MarginLeverage = account.MarginLeverage();
            MarginLevel = account.MarginLevel();
            MarginInitial = account.MarginInitial();
            MarginMaintainance = account.MarginMaintenance();
            Swap = account.Storage();
            //Comission = account.Commission();
            Profit = account.Profit();
            Floating = account.Floating();
            CurrencyDigits = account.CurrencyDigits();
            Credit = account.Credit();
            SOActivation = account.SOActivation();
            SOTime = account.SOTime();
            SOLevel = account.SOLevel();
            SOEquity = account.SOEquity();
            SOMargin = account.SOMargin();
            BlockedComission = account.BlockedCommission();
            Assets = account.Assets();
            Liabilities = account.Liabilities();

        }
        public AccountModel()
        {
            Login = 0;
            Balance = 0;
            Equity = 0;
            Margin = 0;
            MarginFree = 0;
            MarginLeverage = 0;
            MarginLevel = 0;
            MarginInitial = 0;
            MarginMaintainance = 0;
            Swap = 0;
            Comission = 0;
            Profit = 0;
            Floating = 0;
            CurrencyDigits = 0;
            Credit = 0;
            SOActivation = 0;
            SOTime = 0;
            SOLevel = 0;
            SOEquity = 0;
            SOMargin = 0;
            BlockedComission = 0;
            Assets = 0;
            Liabilities = 0;
        }
        public ulong Login { get; set; }
        public double Balance { get; set; }
        public double Equity { get; set; }
        public double Margin { get; set; }
        public double MarginFree { get; set; }
        public uint MarginLeverage { get; set; }
        public double MarginLevel { get; set; }
        public double MarginInitial { get; set; }
        public double MarginMaintainance { get; set; }
        public double Swap { get; set; }
        public double Comission { get; set; }
        public double Profit { get; set; }
        public double Floating { get; set; }
        public uint CurrencyDigits { get; set; }
        public double Credit { get; set; }
        public uint SOActivation { get; set; }  
        public long SOTime { get; set; }
        public double SOLevel { get; set; }
        public double SOEquity { get; set; }
        public double SOMargin { get; set; }
        public double BlockedComission { get; set; }
        public double Assets { get; set; }
        public double Liabilities { get; set; }



    }
}
