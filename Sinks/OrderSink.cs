﻿using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Sinks
{
    internal class OrderSink : CIMTOrderSink
    {
        CIMTManagerAPI m_manager = null;            // Manager interface
                //+------------------------------------------------------------------+
                //| Init native implementation                                       |
                //+------------------------------------------------------------------+
                public MTRetCode Initialize(CIMTManagerAPI manager)
                {
                    //--- checking
                    if (manager == null)
                        return (MTRetCode.MT_RET_ERR_PARAMS);
                    //--- 
                    m_manager = manager;
                    //---
                    return (RegisterSink());
                }
        //+------------------------------------------------------------------+
        //|                                                                  |
        //+------------------------------------------------------------------+
        public override void OnOrderAdd(CIMTOrder order)
        {
            if (order != null)
            {
                string str = order.Print();
                m_manager.LoggerOut(EnMTLogCode.MTLogOK, "{0} has been added", str);
            }
        }
        //+------------------------------------------------------------------+
        //|                                                                  |
        //+------------------------------------------------------------------+
        public override void OnOrderUpdate(CIMTOrder order)
        {
            if (order != null)
            {
                string str = order.Print();
                m_manager.LoggerOut(EnMTLogCode.MTLogOK, "{0} has been updated", str);
            }
        }
        //+------------------------------------------------------------------+
        //|                                                                  |
        //+------------------------------------------------------------------+
        public override void OnOrderDelete(CIMTOrder order)
        {
            if (order != null)
            {
                string str = order.Print();
                m_manager.LoggerOut(EnMTLogCode.MTLogOK, "{0} has been deleted", str);
            }
        }
    }
}

