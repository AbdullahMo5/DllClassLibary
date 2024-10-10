//+----------------------------------------------------------------------+
//|                            MetaTrader 5 API Manager for .NET Example |
//|                       Copyright 2001-2019, MetaQuotes Software Corp. |
//|                                            http://www.metaquotes.net |
//+----------------------------------------------------------------------+
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
//+----------------------------------------------------------------------+
//|                                                                      |
//+----------------------------------------------------------------------+
namespace Goorge
{
    class CManager : CIMTManagerSink
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
            Console.WriteLine("CIMTManagerSink initialize success");
            //--- 
            m_manager = manager;
            //---
            return (RegisterSink());
        }
        //+---------------------------------------------------------------+
        //|                                                               |
        //+---------------------------------------------------------------+
        public override void OnConnect()
        {
            string message = string.Empty;
            message = string.Format("OnConnect: manager connected");
            Console.WriteLine(message);
        }
        //+---------------------------------------------------------------+
        //|                                                               |
        //+---------------------------------------------------------------+
        public override void OnDisconnect()
        {
            string message = string.Empty;
            message = string.Format("OnDisconnect");
            Console.WriteLine(message);
        }
        public CIMTUser UserCreate()
        {
            CIMTUser user;
            user = m_manager.UserCreate();
            return user;
        }
    }
}
//+----------------------------------------------------------------------+