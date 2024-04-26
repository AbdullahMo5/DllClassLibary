//+----------------------------------------------------------------------+
//|                            MetaTrader 5 API Manager for .NET Example |
//|                       Copyright 2001-2020, MetaQuotes Software Corp. |
//|                                            http://www.metaquotes.net |
//+----------------------------------------------------------------------+
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Goorge.Model;
using Goorge.Models;
using Goorge.Services;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using TradeRateSell;
//+----------------------------------------------------------------------+
//|                                                                      |
//+----------------------------------------------------------------------+
namespace Goorge
{
    //+------------------------------------------------------------------+
    //|                                                                  |
    //+------------------------------------------------------------------+
    public class Program
    {
        //+--------------------------------------------------------------+
        //|                                                              |
        //+--------------------------------------------------------------+
        //private static CIMTDeal m_manager = null;
        private static CIMTManagerAPI m_manager = null;
       
        static void Shutdown(CIMTManagerAPI manager)
        {
            if (manager != null)
            {
                manager.Dispose();
            }
            SMTManagerAPIFactory.Shutdown();
        }

        //change to static for testing in console app
        public static MTRetCode Mt5Connection()
        {
            MTRetCode res = MTRetCode.MT_RET_ERROR;
            string message = string.Empty;
            CIMTManagerAPI m_manager = null;
            //---
            Console.WriteLine("Hello world");
            //--- loading manager API
            if ((res = SMTManagerAPIFactory.Initialize(@"C:\MetaTrader5SDK\Libs")) != MTRetCode.MT_RET_OK)
            {
                message = string.Format("Loading manager API failed ({0})", res);
                Console.WriteLine(message);
                //---
                ////Console.Read();
                return res;
            }
            else
            {
                Console.WriteLine("Loading manager API success");
            }
            //--- creating manager interface
            m_manager = SMTManagerAPIFactory.CreateManager(SMTManagerAPIFactory.ManagerAPIVersion, out res);
            if ((res != MTRetCode.MT_RET_OK) || (m_manager == null))
            {
                SMTManagerAPIFactory.Shutdown();
                message = string.Format("Creating manager interface failed ({0})", res);
                Console.WriteLine(message);
                //---
                //Console.Read();
                return res;
            }
            else
            {
                Console.WriteLine("Creating manager interface success");
            }
            //---
            CManager manager_class = new CManager();
            manager_class.Initialize(m_manager);
            m_manager.Subscribe(manager_class);  //m_manager.ManagerSubscribe(manager_class); тут подписка на CIMTConManagerSink, а не на CIMTManagerSink
            //---
            res = m_manager.Connect("198.244.201.66:443", 1117, "5bvxadup", null, CIMTManagerAPI.EnPumpModes.PUMP_MODE_FULL, 300000);
            if (res != MTRetCode.MT_RET_OK)
            {
                message = string.Format("Connect failed ({0})", res);
                Console.WriteLine(message);
                Shutdown(m_manager);
                //---
                //Console.Read();
                return res;
            }
            else
            {
                Console.WriteLine("Connect success");
            }
            Console.WriteLine(ConnectionMT5API.ConnectMT5server("198.244.201.65", 1950, 2023, "123Nm,.com"));

            DateTimeOffset unixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            long from = ((DateTimeOffset)DateTime.Now.AddYears(-10).Date).ToUnixTimeSeconds();
            long to = ((DateTimeOffset)DateTime.Now.Date).ToUnixTimeSeconds();

            var response = ConnectionMT5API.GetUserRights(880413).Data;

            Console.WriteLine("Rights: " + response);

            Console.WriteLine("Press any Key To Close......");
            Console.ReadLine();

            return res;
            //---
            CUsers users_class = new CUsers();
            users_class.Initialize(m_manager);
            m_manager.UserSubscribe(users_class);
            //Deal Sink
            CClass2 dealObj = new CClass2();
            dealObj.Initialize(m_manager);
            m_manager.DealSubscribe(dealObj);
            //Position Sink
            CPositionsClass positionObj = new CPositionsClass();
            positionObj.Initialize(m_manager);
            m_manager.PositionSubscribe(positionObj);
            //Tick Sink
            CTickClass tickObj = new CTickClass();
            tickObj.Initialize(m_manager);
            m_manager.TickSubscribe(tickObj);
            //Book Sink
            CBooks bookObj = new CBooks();
            bookObj.Initialize(m_manager);
            MTBook book = new MTBook();
            bookObj.OnBook(book);
            //order
            Console.WriteLine("order");
            COrderSink orderObj = new COrderSink();
            orderObj.Initialize(m_manager);
            //CIMTOrder newOrder = new CIMTOrder(newOrder);
            //orderObj.OnOrderAdd(newOrder);
            Console.WriteLine("en-order");
            //Console.WriteLine(OrderServices.DealerSendOrder(617633, 1.07, 1, 201, 2, "EURUSD", 0,0));
            //Console.WriteLine(ConnectionMT5API.DealerSendOrder(617633, 1.07, 1, 201, 2, "EURUSD", 0,0));
            //bookObj.OnBook(book);
            //m_manager.BookSubscribe("EURUSD", bookObj);
            //---
            CIMTECNHistoryMatchingArray ecn_match_arr = m_manager.ECNCreateHistoryMatchingArray();
            CIMTECNHistoryFillingArray ecn_fill_arr = m_manager.ECNCreateHistoryFillingArray();
            CIMTECNHistoryDealArray ecn_deal_arr = m_manager.ECNCreateHistoryDealArray();
            CIMTECNProviderArray ecn_prov_arr = m_manager.ECNCreateProviderArray();
            //---
            long from_time = Date.ConvertToUnixTimestamp(Convert.ToDateTime("2020-07-28 00:00:00"));                  //(long)ConvertDateTime(Convert.ToDateTime("2020-07-29 00:00:00")); 
            long to_time = Date.ConvertToUnixTimestamp(Convert.ToDateTime("2020-07-30 23:59:59"));                  // DateTime.Now; //(long)ConvertDateTime(Convert.ToDateTime("2020-07-29 23:59:59")); 
            //---
            res = m_manager.ECNRequestHistoryByGroup("*", from_time, to_time, ecn_match_arr, ecn_fill_arr, ecn_deal_arr, ecn_prov_arr);
            if (res != MTRetCode.MT_RET_OK)
            {
                message = string.Format("ECNRequestHistoryByGroup failed ({0})", res);
                Console.WriteLine(message);
                //Console.Read();
                return res;
            }
            else
            {
                CIMTECNHistoryDeal[] ecn_deals = ecn_deal_arr.ToArray();
                var j = 0;
                for (var i = 0; i < ecn_deals.Length; i++)
                {
                    Console.WriteLine("======== ECN Deal History " + ++j + "========");                //DateTime time_tmp = Date.ConvertFromUnixTime(ecn_deals[i].TimeMsc());
                                                                                                       //Console.WriteLine("TimeMsc: " + Date.ConvertFromUnixTime(ecn_deals[i].TimeMsc())); //ConvertFromUnixTime //ConvertLong(ecn_deals[i].TimeMsc));
                    Console.WriteLine("Login: " + ecn_deals[i].Login());
                    Console.WriteLine("Order: " + ecn_deals[i].Order());
                    Console.WriteLine("GatewayOrder: " + ecn_deals[i].OrderGateway());
                    Console.WriteLine("GatewayDeal: " + ecn_deals[i].DealGateway());
                    Console.WriteLine("ID: " + ecn_deals[i].ExternalID());
                    Console.WriteLine("Server: " + ecn_deals[i].Server());
                    Console.WriteLine("Symbol: " + ecn_deals[i].Symbol());
                    Console.WriteLine("Volume: " + ecn_deals[i].VolumeExt());
                    Console.WriteLine("Type: " + ecn_deals[i].Action());
                    Console.WriteLine("DealPrice: " + ecn_deals[i].Price());
                    Console.WriteLine("GatewayPrice: " + ecn_deals[i].PriceGateway());
                    Console.WriteLine("Commission: " + ecn_deals[i].Commission());
                    Console.WriteLine("Provider: " + ecn_deals[i].Provider());
                    Console.WriteLine("=============================================");
                    Console.WriteLine();
                }
            }
            //testing
            ulong[] loginsss = {617633};
            Console.WriteLine(ConnectionMT5API.ConnectMT5server("47.91.18.177", 6558, 1003, "123Nm,.com"));
            Console.WriteLine(ConnectionMT5API.UserAccountGet(17).Data);
            //Console.WriteLine(ConnectionMT5API.HistoryRequest(617633, 0, ((DateTimeOffset)fromDate).ToUnixTimeSeconds()).TotalCount);
            //Console.WriteLine(ConnectionMT5API.DealerSendOrder(617633, 1.15, 1000, 201, 3, "EURUSD", 0, 0).Message);
            //Console.WriteLine(ConnectionMT5API.DealerSendOrderCloseAndModify(617633, 1.10, 1000, 204, 3, "EURUSD", 0, 0, 8785780).Message);
            //Console.WriteLine(ConnectionMT5API.DealerBalance(17, 50, 10, "comment", true));
            //Console.WriteLine(ConnectionMT5API.DealerSendCloseAndModifyPosition(617633, 1.06, 1, 202, 1, "CHFJPY", 153.1, 0, 6081248).Message);
            Console.WriteLine("Iwork");
            //CBooks bookObj = new CBooks();
            m_manager.BookSubscribe("EURUSD", bookObj);
            MTBook resoul = new MTBook();
            m_manager.BookGet("EURUSD",out resoul);
            Console.WriteLine(resoul.symbol);
            
            //---
            //ulong[] logins = { 1, 2, 3 };
            //long from = 0;
            //long to = 1723157552;
            //CIMTDailyArray daily = m_manager.DailyCreateArray();
            //m_manager.DailyRequestByLogins(logins, from, to, daily);
            //---
            //ConnectionMT5API.requestSymbolBars(m_manager);
            Console.WriteLine("Press enter key to end..");
            //Console.Read();
            //---
            if (m_manager != null)
            {
                m_manager.Disconnect();
                m_manager.Release();
                m_manager = null;
            }
            //---
            SMTManagerAPIFactory.Shutdown();
            return res;
        }
        //+--------------------------------------------------------------+
        //|                                                              |
        //+--------------------------------------------------------------+
        static void Main(string[] args)
        {
            Mt5Connection();
        }
        public MTRetCode ChangeLeverage(ulong login, int leverage)
        {
            MTRetCode res = new MTRetCode();
            res = MTRetCode.MT_RET_OK_NONE;
            using (CIMTUser user = m_manager.UserCreate())
            {
                res = user.Leverage(400);
                res = m_manager.UserUpdate(user);
            }
            return res;
        }
       
    }
}
//+--------------------------------------------------------------------+