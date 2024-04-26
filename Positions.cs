//+--------------------------------------------------------------------------+
//|                                                                          |
//+--------------------------------------------------------------------------+
namespace Goorge
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
    class CPositionsClass : CIMTPositionSink
    {
        CIMTManagerAPI m_manager = null;   
        //+------------------------------------------------------------------+
        //| Init native implementation                                       |
        //+------------------------------------------------------------------+
        public MTRetCode Initialize(CIMTManagerAPI manager)
        {
            //--- checking
            if (manager == null) return (MTRetCode.MT_RET_ERR_PARAMS);
            //---
            Console.WriteLine("CIMTPositionSink initialize success");
            //--- 
            m_manager = manager;
            //---
            return (RegisterSink());
        }
        //+------------------------------------------------------------------+
        //| Sink event handlers                                              |
        //+------------------------------------------------------------------+
        public override void OnPositionAdd(CIMTPosition position)
        {
            string message = string.Empty;
            message = string.Format("OnPositionAdd Position = {0}, Price Open = {1},  Stop Loss = {2}, Take Profit = {3}, Action = {4}", position.PriceOpen(), position.Position(),position.PriceSL(), position.PriceTP(), position.Action());
            Console.WriteLine(message);
        }
    }
}
//+--------------------------------------------------------------------------+