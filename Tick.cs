using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;

namespace Goorge
{
    public class CTickClass : CIMTTickSink
    {
        CIMTManagerAPI m_manager = null;
        //+---------------------------------------------------------------+
        //| Init native implementation                                    |
        //+---------------------------------------------------------------+
        public MTRetCode Initialize(CIMTManagerAPI manager)
        {
            //--- checking
            if (manager == null) return (MTRetCode.MT_RET_ERR_PARAMS);
            //---
            Console.WriteLine("CIMTTickSink initialize success");
            //--- 
            m_manager = manager;
            //---
            return (RegisterSink());
        }
        //+---------------------------------------------------------------+
        //|                                                               |
        //+---------------------------------------------------------------+
        public override void OnTick(string symbol, MTTickShort tick)
        {
            string message = string.Empty;

            message = string.Format("OnTick symbol = {0}, bid = {1}, ask = {2}, last = {3}", symbol, tick.bid, tick.ask, tick.last);
            Console.WriteLine(message);
        }
        public override void OnTickStat(MTTickStat tick)
        {
            string message = string.Empty;
            message = string.Format("OnTickStat symbol = {0}, trade_deals = {1}, bid_high = {2}, bid_low = {3}, price_open = {4}, price_close = {5}", tick.symbol, tick.trade_deals, tick.bid_high, tick.bid_low, tick.price_open, tick.price_close);
            Console.WriteLine(message);
        }
        public override MTRetCode HookTickStat(int feeder, ref MTTickStat stat)
        {
            //--- checking
            MTRetCode res = MTRetCode.MT_RET_ERROR;
            Console.WriteLine("HookTickStat symbol = {0}, trade_deals = {1}, bid_high = {2}, bid_low = {3}, price_open = {4}, price_close = {5}", stat.symbol, stat.trade_deals, stat.bid_high, stat.bid_low, stat.price_open, stat.price_close);
            //---
            return (res); //return (MTRetCode.MT_RET_OK);
        }
        //+---------------------------------------------------------------+
        //|                                                               |
    }
}
