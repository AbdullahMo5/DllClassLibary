using MetaQuotes.MT5CommonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Model
{
    public class OrderModel
    {
        public OrderModel()
        {
                
        }
        public OrderModel(CIMTOrder order)
        {
            ContractSize = order.ContractSize();
            Dealer = order.Dealer();
            Digits = order.Digits();
            ExpertID = order.ExpertID();
            ExternalID = order.ExternalID();
            Login = order.Login();
            Comment = order.Comment();
            ModificationFlags = order.ModificationFlags();
            Order = order.Order();
            PositionByID = order.PositionByID();
            PositionID = order.PositionID();
            PriceCurrent = order.PriceCurrent();
            PriceOrder = order.PriceOrder();
            PriceSL = order.PriceSL();
            PriceTP = order.PriceTP();
            PriceTrigger = order.PriceTrigger();
            Print=order.Print();
            RateMargin = order.RateMargin();
            Reason = order.Reason();
            State = order.State();
            Symbol = order.Symbol();
            TimeDone = order.TimeDone();
            TimeExpiration = order.TimeExpiration();
            VolumeInitial = order.VolumeInitial();
            VolumeInitialExt = order.VolumeInitialExt();
        }
        public double ContractSize { get;  set; }
        public ulong Dealer { get;  set; }
        public uint Digits { get;  set; }
        public ulong ExpertID { get;  set; }
        public string ExternalID { get;  set; }
        public ulong Login { get;  set; }
        public string Comment { get;  set; }
        public uint ModificationFlags { get;  set; }
        public ulong Order { get;  set; }
        public ulong PositionByID { get;  set; }
        public ulong PositionID { get;  set; }
        public double PriceCurrent { get;  set; }
        public double PriceOrder { get;  set; }
        public double PriceSL { get;  set; }
        public double PriceTP { get;  set; }
        public double PriceTrigger { get;  set; }
        public string Print { get;  set; }
        public double RateMargin { get;  set; }
        public uint Reason { get;  set; }
        public uint State { get;  set; }
        public string Symbol { get;  set; }
        public long TimeDone { get;  set; }
        public long TimeExpiration { get;  set; }
        public ulong VolumeInitial { get;  set; }
        public ulong VolumeInitialExt { get;  set; }
    }
}
