using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System.Threading;

namespace Goorge
{
    public class CRequestSink : CIMTRequestSink
    {
        CIMTManagerAPI m_manager = null;            // Manager interface
        EventWaitHandle m_event_request = null;            // request notifications event
                                                           //+------------------------------------------------------------------+
                                                           //| Init native implementation                                       |
                                                           //+------------------------------------------------------------------+
        public MTRetCode Initialize(CIMTManagerAPI manager, EventWaitHandle event_request)
        {
            //--- checking
            if (manager == null || event_request == null)
                return (MTRetCode.MT_RET_ERR_PARAMS);
            //--- 
            m_manager = manager;
            m_event_request = event_request;
            //---
            return (RegisterSink());
        }
        //+------------------------------------------------------------------+
        //| Sink event handlers                                              |
        //+------------------------------------------------------------------+
        public override void OnRequestAdd(CIMTRequest request) { NotifyRequest(); }
        public override void OnRequestUpdate(CIMTRequest request) { NotifyRequest(); }
        public override void OnRequestDelete(CIMTRequest request) { NotifyRequest(); }
        public override void OnRequestSync() { NotifyRequest(); }
        //+------------------------------------------------------------------+
        //| Request notifications                                            |
        //+------------------------------------------------------------------+
        void NotifyRequest()
        {
            //--- check for available requests
            if (m_manager.RequestTotal() > 0)
            {
                //--- request exists      
                if (!m_event_request.WaitOne(0))
                    m_event_request.Set();
            }
            else
            {
                //--- requests queue is empty
                if (m_event_request.WaitOne(0))
                    m_event_request.Reset();
            }
        }
    }
}
