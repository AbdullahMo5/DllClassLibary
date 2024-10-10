using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;

namespace Goorge
{
    public class COrderSink : CIMTOrderSink
    {
        CIMTManagerAPI m_manager = null;
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
