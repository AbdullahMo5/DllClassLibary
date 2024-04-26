//+----------------------------------------------------------------------+
//|                            MetaTrader 5 API Manager for .NET Example |
//|                       Copyright 2001-2019, MetaQuotes Software Corp. |
//|                                            http://www.metaquotes.net |
//+----------------------------------------------------------------------+
namespace Goorge
{
//+----------------------------------------------------------------------+
//|                                                                      |
//+----------------------------------------------------------------------+
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MetaQuotes.MT5CommonAPI;
    using MetaQuotes.MT5ManagerAPI;
    //+------------------------------------------------------------------+
    //|                                                                  |
    //+------------------------------------------------------------------+
    class CBooks: CIMTBookSink
    {
      CIMTManagerAPI m_manager = null;     
       //+---------------------------------------------------------------+
       //| Init native implementation                                    |
       //+---------------------------------------------------------------+
        public MTRetCode Initialize(CIMTManagerAPI manager)
        {
            //--- checking
            if(manager==null) return (MTRetCode.MT_RET_ERR_PARAMS);
            //---
            Console.WriteLine("CBooks initialize success");
            //--- 
            m_manager=manager;
            //---
            return(RegisterSink());
        }
        //+---------------------------------------------------------------+
        //|                                                               |
        //+---------------------------------------------------------------+
        public override void OnBook(MTBook book)
        {
            string message = string.Empty;
            Console.WriteLine("i'm working");
            message=string.Format("OnBook for {0}, total items {1}", book.symbol, book.items_total);
            Console.WriteLine(message);
        }
    }
}
//+----------------------------------------------------------------------+