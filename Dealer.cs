//+--------------------------------------------------------------------------+
//|                                                                          |
//+--------------------------------------------------------------------------+
namespace TradeRateSell
{
    using Goorge;
    using MetaQuotes.MT5CommonAPI;
    using MetaQuotes.MT5ManagerAPI;
    using System;
    using System.Collections;

    //+----------------------------------------------------------------------+
    //| Dealer request sink                                                  |
    //+----------------------------------------------------------------------+
    public class CDealer : CIMTDealerSink
    {
        public Queue my_queue = null;
        CIMTManagerAPI m_manager = null;            // Manager interface
        //+------------------------------------------------------------------+
        //| Init native implementation                                       |
        //+------------------------------------------------------------------+
        public MTRetCode Initialize(CIMTManagerAPI manager)
        {
            m_manager = manager;
            Console.WriteLine("CIMTDealerSink initialize success");
            return (RegisterSink()); //return (MTRetCode.MT_RET_OK);
        }
        //+------------------------------------------------------------------+
        //| Sink event handlers                                              |
        //+------------------------------------------------------------------+

        public override void OnDealerResult(CIMTConfirm result)
        {
            string message = string.Empty;
            message = string.Format("OnDealerResult: id = {0}", result.ID());
            Console.WriteLine(message);
        }
        public override void OnDealerAnswer(CIMTRequest request)
        {
            //Globals.CIMTRequest = request;
            string message = string.Empty;
            message = "OnDealerAnswer " + request.Print();
            Console.WriteLine(message);
            ConnectionMT5API.GetLastRequest(request);
            message = string.Empty;
            message = string.Format("OnDealerAnswer: retcode= {2}, deal = {0}, order = {1}", request.ResultDeal(), request.ResultOrder(), request.ResultRetcode());
        }
    }
}

//+--------------------------------------------------------------------------+