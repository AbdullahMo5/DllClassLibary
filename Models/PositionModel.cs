using MetaQuotes.MT5CommonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Model
{
    public class PositionModel
    {
        public double ContractSize { get;  set; }
        public ulong Dealer { get;  set; }
        public uint Digits { get;  set; }
        public ulong ExpertID { get;  set; }
        public string ExternalID { get;  set; }
        public ulong Login { get;  set; }
        public string Comment { get;  set; }
        public uint ModificationFlags { get;  set; }
        public ulong VolumeExt { get;  set; }
        public ulong Volume { get;  set; }
        public double RateProfit { get;  set; }
        public double Profit { get;  set; }
        public double PriceOpen { get;  set; }
        public double PriceCurrent { get;  set; }
        public string Symbol { get;  set; }
        public ulong Position { get;  set; }
        public uint Reason { get;  set; }
        public double RateMargin { get;  set; }
        public string Print { get;  set; }
        public long ActivationTime { get;  set; }
        public double PriceTP { get;  set; }
        public double PriceSL { get;  set; }
        public double ActivationPrice { get;  set; }
        public uint ActivationMode { get;  set; }
        public uint ActivationFlags { get;  set; }
        public uint Action { get;  set; }
        public long Time { get; set; }
        public long TimeCreated { get; set; }
        public long TimeCreatedMsc { get; set; }
        public long TimeUpdated { get; set; }
        public long TimeUpdatedMsc { get; set; }


        public PositionModel(double contractSize, ulong dealer, uint digits, ulong expertID, string externalID, ulong login, string comment, uint modificationFlags, ulong volumeExt, ulong volume, double rateProfit, double profit, double priceOpen, double priceCurrent, string symbol, ulong position, uint reason, double rateMargin, string print, long activationTime, double priceTP, double priceSL, double activationPrice, uint activationMode, uint activationFlags, uint action, long time, long timeCreated, long timeCreatedMsc, long timeUpdated, long timeUpdatedMsc)
        {
            ContractSize = contractSize;
            Dealer = dealer;
            Digits = digits;
            ExpertID = expertID;
            ExternalID = externalID;
            Login = login;
            Comment = comment;
            ModificationFlags = modificationFlags;
            VolumeExt = volumeExt;
            Volume = volume;
            RateProfit = rateProfit;
            Profit = profit;
            PriceOpen = priceOpen;
            PriceCurrent = priceCurrent;
            Symbol = symbol;
            Position = position;
            Reason = reason;
            RateMargin = rateMargin;
            Print = print;
            ActivationTime = activationTime;
            PriceTP = priceTP;
            PriceSL = priceSL;
            ActivationPrice = activationPrice;
            ActivationMode = activationMode;
            ActivationFlags = activationFlags;
            Action = action;
            Time = time;
            TimeCreated = timeCreated;
            TimeCreatedMsc = timeCreatedMsc;
            TimeUpdated = timeUpdated;
            TimeUpdatedMsc = timeUpdatedMsc;
        }

        public PositionModel(CIMTPosition position)
        {
            ContractSize = position.ContractSize();
            Dealer = position.Dealer();
            Digits = position.Digits();
            ExpertID = position.ExpertID();
            ExternalID = position.ExternalID();
            Login=  position.Login();
            Comment = position.Comment();
            ModificationFlags = position.ModificationFlags();
            VolumeExt = position.VolumeExt();
            Volume = position.Volume();
            RateProfit = position.RateProfit();
            Profit = position.Profit();
            PriceOpen = position.PriceOpen();
            PriceCurrent = position.PriceCurrent();
            Symbol = position.Symbol();
            Position = position.Position();
            Reason = position.Reason();
            RateMargin = position.RateMargin();
            Print = position.Print();
            ActivationTime = position.ActivationTime();
            PriceTP = position.PriceTP();
            PriceSL = position.PriceSL();
            ActivationFlags = position.ActivationFlags();
            Time = position.TimeCreate();
            TimeCreated = position.TimeCreate();
            TimeCreatedMsc = position.TimeCreateMsc();
            TimeUpdated = position.TimeUpdate();
            TimeUpdatedMsc = position.TimeUpdateMsc();
           

        }

        public PositionModel()
        {
        }
    }
}
