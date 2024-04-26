using MetaQuotes.MT5CommonAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Models
{
    public class DealModel
    {
        public DealModel()
        {
            Print = "";
            ExternalID = "";
            Login = 0;
            Dealer = 0;
            Order = 0;
            Action = 0;
            Entry = 0;
            Digits = 0;
            DigitsCurrency = 0;
            ContractSize = 0;
            Time = 0;
            Symbol = "";
            Price = 0;
            PriceTP = 0;
            PriceSL = 0;
            Volume = 0;
            VolumeExt = 0;
            VolumeClosed = 0;
            VolumeClosedExt = 0;
            Profit = 0;
            Value = 0;
            Storage = 0;
            Commission = 0;
            Fee = 0;
            RateProfit = 0;
            RateMargin = 0;
            ExpertID = 0;
            PositionID = 0;
            Comment = "";
            ProfitRaw = 0;
            PricePosition = 0;
            TickValue = 0;
            TickSize = 0;
            Flags = 0;
            TimeMsc = 0;
            Reason = 0;
            Gateway = "" ;
            PriceGateway = 0;
            MarketBid = 0;
            MarketAsk = 0;
            MarketLast = 0;
            ModificationFlags = 0;
        }
        public DealModel(CIMTDeal deal)
        {
            Print = deal.Print();
            ExternalID = deal.ExternalID();
            Login = deal.Login();
            Dealer = deal.Dealer();
            Order = deal.Order();   
            Action = deal.Action();
            Entry = deal.Entry();
            Digits = deal.Digits();
            DigitsCurrency = deal.DigitsCurrency();
            ContractSize = deal.ContractSize(); 
            Time = deal.Time();
            Symbol = deal.Symbol();
            Price = deal.Price();
            PriceTP = deal.PriceTP();
            PriceSL = deal.PriceSL();   
            Volume = deal.Volume();
            VolumeExt = deal.VolumeExt();
            VolumeClosed = deal.VolumeClosed();
            VolumeClosedExt = deal.VolumeClosedExt();
            Profit = deal.Profit();
            Value = deal.Value();
            Storage = deal.Storage();
            Commission = deal.Commission();
            Fee = deal.Fee();
            RateProfit = deal.RateProfit();
            RateMargin= deal.RateMargin();
            ExpertID = deal.ExpertID();
            PositionID = deal.PositionID();
            Comment = deal.Comment();
            ProfitRaw = deal.ProfitRaw();
            PricePosition = deal.PricePosition();
            TickValue = deal.TickValue();
            TickSize = deal.TickSize();
            Flags = deal.Flags();
            TimeMsc = deal.TimeMsc();
            Reason = deal.Reason();
            Gateway = deal.Gateway();
            PriceGateway = deal.PriceGateway();
            MarketBid = deal.MarketBid();
            MarketAsk = deal.MarketAsk();
            MarketLast = deal.MarketLast();
            ModificationFlags = deal.ModificationFlags();

        }
        public string Print { get; set; }
        public string ExternalID { get; set; }
        public ulong Login { get; set; }
        public ulong Dealer { get; set; }
        public ulong Order { get; set; }
        public uint Action { get; set; }
        public uint Entry { get; set; }
        public uint Digits { get; set; }
        public uint DigitsCurrency { get; set; }
        public double ContractSize { get; set; }
        public long Time { get; set; }
        public string Symbol{ get; set; }
        public double Price{ get; set; }
        public double PriceTP{ get; set; }
        public double PriceSL { get; set; }
        public ulong Volume{ get; set; }
        public ulong VolumeExt { get; set; }
        public ulong VolumeClosed { get; set; }
        public ulong VolumeClosedExt { get; set; }
        public double Profit { get; set; }
        public double Value{ get; set; }
        public double Storage{ get; set; }
        public double Commission{ get; set; }
        public double Fee { get; set; }
        public double RateProfit { get; set; }
        public double RateMargin { get; set; }
        public ulong ExpertID { get; set; }
        public ulong PositionID { get; set; }
        public string Comment { get; set; }
        public double ProfitRaw { get; set; }
        public double PricePosition { get; set; }
        public double TickValue { get; set; }
        public double TickSize { get; set; }
        public ulong Flags { get; set; }
        public long TimeMsc { get; set; }
        public uint Reason { get; set; }
        public string Gateway { get; set; }
        public double PriceGateway { get; set; }
        public double MarketBid { get; set; }
        public double MarketAsk { get; set; }
        public double MarketLast { get; set; }
        public uint ModificationFlags { get; set; }

    }
    public class DealResponseModel
    {
        public DealResponseModel(ulong ticket, MTRetCode response) {
            Ticket = ticket;
            Response = response;
        }
        public ulong Ticket { get; set; }
        public MTRetCode Response { get;set; }
    }
}
