using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using TradeRateSell;

namespace Goorge
{
    public class CSomeClientClass
    {
        //--- Manager API 
        private static CIMTManagerAPI m_manager = null;
        uint MT5_CONNECT_TIMEOUT = 30000;

        public CIMTManagerAPI Initialize()
        {
            string msg = string.Empty;
            MTRetCode res = MTRetCode.MT_RET_ERROR;
            //--- Initialize the factory
            if ((res = SMTManagerAPIFactory.Initialize(@"C:\MetaTrader5SDK\Libs")) != MTRetCode.MT_RET_OK)
            {
             
            }
            //--- Receive the API version
            uint version = 0;
            if ((res = SMTManagerAPIFactory.GetVersion(out version)) != MTRetCode.MT_RET_OK)
            {
               
            }
            //--- Compare the obtained version with the library one
            if (version != SMTManagerAPIFactory.ManagerAPIVersion)
            {
               
            }
            //--- Create an instance
            m_manager = SMTManagerAPIFactory.CreateManager(SMTManagerAPIFactory.ManagerAPIVersion, out res);
            if (res != MTRetCode.MT_RET_OK)
            {
               
            }
            //--- For some reasons, the creation method returned OK and the null pointer
            if (m_manager == null)
            {
             
            }
            //--- All is well
            msg = string.Format("Using ManagerAPI v. {0}", version);
            return (m_manager);
        }
        public MTRetCode Connect(string server, UInt64 login, string password)
        {
            string msg = string.Empty;
            MTRetCode res = MTRetCode.MT_RET_ERROR;
            if (m_manager == null)
            {
                msg = string.Format(EnMTLogCode.MTLogErr.ToString(), server, "Connection to {0} failed: .NET Manager API is NULL", server);
                return (res);
            }
            //--- 
            res = m_manager.Connect(server, login, password, null, CIMTManagerAPI.EnPumpModes.PUMP_MODE_POSITIONS, MT5_CONNECT_TIMEOUT);
            if (res != MTRetCode.MT_RET_OK)
            {
                msg = string.Format(EnMTLogCode.MTLogErr.ToString(), server, "Connection by Managed API to {0} failed: {1}", server, res);
                return (res);
            }
            //---
            msg = string.Format("Connected");
            return (res);
        }
        public bool Login(string server, UInt64 login, string password)
        {
            //initialize 
            Initialize();
            //--- connect
            MTRetCode res = Connect(server, login, password);
            if (res != MTRetCode.MT_RET_OK)
            {
                m_manager.LoggerOut(EnMTLogCode.MTLogErr, "Connection failed ({0})", res);
                return (false);
            }
            return (true);
        }
        public void DealAddSync()
        {
            //MTRetCode isDealerStart = m_manager.DealerStart();
            //ob2.Order(6979449);
            //ob2.Order(6979449);
            //ob2.Volume(0);
            //ob2.PositionID(0);
            //ob2.Order(0);
            //ob2.Deal()
            //deal.ProcessCompleted += bl_ProcessCompleted;
            //deal.OnDealSync();
            //deal.OnDealAdd(ob2);
            MTChartBar[] bars = requestSymbolBars();
           
        }
        static void Event()
        {

        }
        public static MTChartBar[] requestSymbolBars()
        {
            if (m_manager != null)
            {
                string message = string.Empty;
                string symbol = "EURUSD";
                long from = 0;
                long to = m_manager.TimeServer();
                MTRetCode result = MTRetCode.MT_RET_ERROR;
                //---
                MTChartBar[] bars = m_manager.ChartRequest(symbol, from, to, out result);
                if (result != MTRetCode.MT_RET_OK)
                {
                    message = string.Format("Library: ChartRequest failed ({0})", result);
                    Console.WriteLine(message);
                    return null;
                }
                else
                {
                    Console.WriteLine("Library: ChartRequest for {0} success, total bars = {1}", symbol, bars.Length);
                    //for(int i=0; i<bars.Length; i++)
                    //Console.WriteLine("open = {0}, close = {1}, high = {2}, low = {3}", bars[i].open, bars[i].close, bars[i].high, bars[i].low);
                    //---
                    return bars;
                }
            }
            else
            {
                return null;
            }
        }
        // public static MTChartBar[] requestTradingStates()
        //{
        //    if (m_manager != null)
        //    {
        //        string message = string.Empty;
        //        string symbol = "EURUSD";
        //        long from = 0;
        //        long to = m_manager.TimeServer();
        //        MTRetCode result = MTRetCode.MT_RET_ERROR;
        //        //---
        //        CIMTRequest obj22 = new CIMTRequest();
        //        MTChartBar[] bars = CIMTRequest.ResultRetcode();
        //        if (result != MTRetCode.MT_RET_OK)
        //        {
        //            message = string.Format("Library: ChartRequest failed ({0})", result);
        //            Console.WriteLine(message);
        //            return null;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Library: ChartRequest for {0} success, total bars = {1}", symbol, bars.Length);
        //            //for(int i=0; i<bars.Length; i++)
        //            //Console.WriteLine("open = {0}, close = {1}, high = {2}, low = {3}", bars[i].open, bars[i].close, bars[i].high, bars[i].low);
        //            //---
        //            return bars;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //+------------------------------------------------------------------+
        //| API call example                                                 |
        //+------------------------------------------------------------------+
        //private void OnRequestServerLogs(EnMTLogRequestMode requestMode,
        //                                 EnMTLogType logType,
        //                                 Int64 from,
        //                                 Int64 to,
        //                                 string filter = null)
        //  {
        //      if (m_manager == null)
        //      {
        //          LogOut("ERROR: Manager was not created");
        //          return;
        //      }
        //      //---
        //      LogOut(EnMTLogCode.MTLogAtt, "LogTests", CAPITester.LOGSEPARATOR);
        //      try
        //      {
        //          MTRetCode result = MTRetCode.MT_RET_ERROR;
        //          //---
        //          MTLogRecord[] records = m_manager.LoggerServerRequest(requestMode, logType, from, to, filter, out result);
        //          //---
        //          LogOutFormat("LoggerServerRequest {0} ==> [{1}] return {2} record(s)",
        //                       (result == MTRetCode.MT_RET_OK ? "ok" : "failed"),
        //                       result, (records != null ? records.Length : 0));
        //          //---
        //          if ((result == MTRetCode.MT_RET_OK) && (records != null))
        //          {
        //              foreach (MTLogRecord rec in records)
        //                  LogOut(rec);
        //          }
        //      }
        //      catch (Exception ex)
        //      {
        //     }
        //  }
    }
}
