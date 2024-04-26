//+--------------------------------------------------------------------------+
//|                                                                          |
//+--------------------------------------------------------------------------+
namespace TradeRateSell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using MetaQuotes.MT5CommonAPI;
    using MetaQuotes.MT5ManagerAPI;
    //+----------------------------------------------------------------------+
    //| Dealer request sink                                                  |
    //+----------------------------------------------------------------------+
    public delegate void Notify();
    public class CClass2 : CIMTDealSink
    {
        CIMTManagerAPI m_manager = null;            // Manager interface
        //+------------------------------------------------------------------+
        //| Init native implementation                                       |
        //+------------------------------------------------------------------+
        public MTRetCode Initialize(CIMTManagerAPI manager)
        {
            //--- checking
            MTRetCode res = MTRetCode.MT_RET_ERROR;
            if (manager == null)
            {
                m_manager = manager;
            }
            //return (MTRetCode.MT_RET_ERR_PARAMS);
            //---
            //---
            Console.WriteLine("CIMTDealSink initialize success");
            //---
            return (RegisterSink()); //return (MTRetCode.MT_RET_OK);
        }
        //+------------------------------------------------------------------+
        //| Sink event handlers                                              |
        //+------------------------------------------------------------------+

        public override void OnDealAdd(CIMTDeal deal)
        {
            string message = string.Empty;

            message = string.Format("OnDealAdd deal = {0}, position = {1}, order = {2}, volume = {3}, Action = {4}, Entry = {5} ,  Profit = {6}", deal.Deal(), deal.PositionID(), deal.Order(), deal.Volume(),deal.Action(),deal.Entry(), deal.Profit());
            Console.WriteLine(message);
            //---
        }
        public override void OnDealDelete(CIMTDeal deal)
        {
            //EventArgs e = this;
            string message = string.Empty;
            message = string.Format("OnDealAdd deal = {0}, position = {1}, order = {2}, volume = {3} , Profit = {4}", deal.Deal(), deal.PositionID(), deal.Order(), deal.Volume(),deal.Profit());
            Console.WriteLine(message);
            //---
        }
        public override void OnDealUpdate(CIMTDeal deal)
        {
            //EventArgs e = this;
            string message = string.Empty;
            message = string.Format("OnDealAdd deal = {0}, position = {1}, order = {2}, volume = {3}", deal.Deal(), deal.PositionID(), deal.Order(), deal.Volume());
            Console.WriteLine(message);

            //---
        }
        public override void OnDealPerform(CIMTDeal deal, CIMTAccount account, CIMTPosition position)
        {
            //EventArgs e = this;
            string message = string.Empty;
            message = string.Format("OnDealAdd deal = {0}, position = {1}, order = {2}, volume = {3}", deal.Deal(), deal.PositionID(), deal.Order(), deal.Volume());
            Console.WriteLine(message);
            //---
        }
        public override void OnDealSync()
        {
            //EventArgs e = this;
            string message = string.Empty;
            //message = string.Format("OnDealAdd deal = {0}, position = {1}, order = {2}, volume = {3}", deal.Deal(), deal.PositionID(), deal.Order(), deal.Volume());
            Console.WriteLine(message);

            //---
        }
    }
}

//+--------------------------------------------------------------------------+