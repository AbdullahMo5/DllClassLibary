using Goorge.Model;
using Goorge.Models;
using Goorge.Services;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradeRateSell;
using static IMTRequest;

namespace Goorge
{

    public static class ConnectionMT5API
    {
        //private static string m_connect_str = Auth.SERVER + ":" + Auth.PORT.ToString();
        private static CIMTManagerAPI m_manager = null;
        public static CIMTRequest admin;
        private static CIMTRequest m_request = null;            // request interface
        private static CIMTConfirm m_confirm = null;            // confirmation interface
        private static CDealer m_dealer = null;            // confirmation interface
        private static CManager _manager = null;            // confirmation interface
        private static CIMTPosition m_position = null;
        private static CIMTDeal m_deal = null;
        private static CIMTOrder m_order;
        private static MTRetCode bookData;
        private static UserService userService = null;
        private static PositionService positionService = null;
        private static AccountService accountService = null;
        private static DealerService dealerService = null;
        private static OrderServices orderService = null;
        private static GroupService groupService = null;

        //+----------------------------------------------------------+
        //|                                                          |
        //+----------------------------------------------------------+
        public static bool StartsWithUpper(this String str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return false;
            //---
            Char ch = str[0];
            return Char.IsUpper(ch);
        }
        //+----------------------------------------------------------+
        //|                                                          |
        //+----------------------------------------------------------+
        public static String GetMyString()
        {
            return "My string from library";
        }
        //+----------------------------------------------------------+
        //|                                                          |
        //+----------------------------------------------------------+
        public static void Shutdown()
        {
            if (m_manager != null)
            {
                m_manager.Dispose();
                m_manager.Release();
            }
            SMTManagerAPIFactory.Shutdown();
        }
        //+----------------------------------------------------------+
        //|                                                          |
        //+----------------------------------------------------------+
        public static String ConnectMT5server(string server, int port, ulong login, string man_pass)
        {
            //string m_connect_str = Auth.SERVER + ":" + Auth.PORT.ToString();
            string m_connect_str = server + ":" + port.ToString();

            //CDealer m_dealer = null;
            if (m_manager == null || admin == null)
            {
                MetaQuotes.MT5CommonAPI.MTRetCode adminRes = MetaQuotes.MT5CommonAPI.MTRetCode.MT_RET_ERROR;
                MetaQuotes.MT5CommonAPI.MTRetCode res = MetaQuotes.MT5CommonAPI.MTRetCode.MT_RET_ERROR;
                //--- loading manager API
                if ((res = SMTManagerAPIFactory.Initialize(@"C:\MetaTrader5SDK\Libs")) != MetaQuotes.MT5CommonAPI.MTRetCode.MT_RET_OK) //Auth.MAN_API_PATH
                {
                    return "Library: Loading manager API failed";
                }
                //--- creating manager interface
                m_manager = SMTManagerAPIFactory.CreateManager(SMTManagerAPIFactory.ManagerAPIVersion, out res);
                if ((res != MetaQuotes.MT5CommonAPI.MTRetCode.MT_RET_OK) || (m_manager == null))
                {
                    SMTManagerAPIFactory.Shutdown();
                    return "Library: Creating manager interface failed";
                }
                //---
                res = m_manager.Connect(m_connect_str, login, man_pass, null, CIMTManagerAPI.EnPumpModes.PUMP_MODE_SYMBOLS |
                                               CIMTManagerAPI.EnPumpModes.PUMP_MODE_GROUPS |
                                               CIMTManagerAPI.EnPumpModes.PUMP_MODE_USERS | CIMTManagerAPI.EnPumpModes.PUMP_MODE_POSITIONS |
                                               CIMTManagerAPI.EnPumpModes.PUMP_MODE_ORDERS, 30000);
                if (res == MTRetCode.MT_RET_OK)
                {
                    CTickClass cTick = new CTickClass();
                    m_dealer = new CDealer();
                    userService = new UserService();
                    positionService = new PositionService();
                    accountService = new AccountService();
                    dealerService = new DealerService();
                    orderService = new OrderServices();
                    groupService = new GroupService();
                    userService.Initialize(m_manager);
                    positionService.Initialize(m_manager);
                    accountService.Initialize(m_manager);
                    dealerService.Initialize(m_manager);
                    orderService.Initialize(m_manager);
                    groupService.Initialize(m_manager);
                    Console.WriteLine("Initialized");
                }
                if (res != MetaQuotes.MT5CommonAPI.MTRetCode.MT_RET_OK)
                {
                    Shutdown();
                    return "Library: Connect failed";
                }
                else
                {
                    return "Library: Connect success";
                }
            }
            else
            {
                return "Library: Connect success";
            }
        }

        //+----------------------------------------------------------+
        //|                                                          |
        //+----------------------------------------------------------+

    #region Accounts
        public static ReturnModel AccountGetAll()
        {
            var returnModel = new ReturnModel();
            var groups = groupService.GetAllGroups().Data as List<GroupModel>;
            List<AccountModel> returnModels = new List<AccountModel>();

            foreach (var group in groups)
            {
                var accountModel = accountService.UserAccountsGetByGroup(group.Group).Data as List<AccountModel>;
                if (accountModel != null)
                    returnModels.AddRange(accountModel);
            }
            returnModel.Data = returnModels;
            returnModel.TotalCount = returnModels.Count;

            return returnModel;
        }
        public static ReturnModel AccountGetByLogin(ulong login)
        {
            return accountService.UserAccountGet(login);
        }
        public static ReturnModel AccountGetByLogins(ulong[] logins)
        {
            return accountService.UserAccountsGetByLogins(logins);
        }
        public static ReturnModel AccountGetByGroup(string group)
        {
            return accountService.UserAccountsGetByGroup(group);
        }        
        public static MTChartBar[] requestSymbolBars(string symbol, long from, long to)
        {
            if (m_manager != null)
            {
                string message = string.Empty;
                //string symbol = "EURUSD";
                //long from = 0;
                //long to = m_manager.TimeServer();
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
                    //Console.WriteLine(bars);
                    return bars;
                }
            }
            else
            {
                return null;
            }
        }
        public static MTRetCode ChangeLeverage(ulong login, uint leverage)
        {
            var response = userService.UserUpdateLeverage(login, leverage);
            return response;
        }
        public static ReturnModel UserAccountGetEquity(ulong login)
        {
            return accountService.UserAccountGetEquity(login);
        }
        public static ReturnModel UserAccountsGetEquity(ulong[] login)
        {
            return accountService.UserAccountGetEquity(login);
        }
        public static ReturnModel UserAccountsGetEquityByGroup(string group)
        {
            var acc = accountService.UserAccountGetLoginsByGroup(group).Data as ulong[];
            if (acc != null)
            {
                var returnModel = accountService.UserAccountGetEquity(acc);
                return returnModel;
            }
            return new ReturnModel();
        }
        #endregion

    #region Users
        public static ReturnModel UserGetAll()
        {
            var returnModel = new ReturnModel();
            var groups = groupService.GetAllGroups().Data as List<GroupModel>;
            List<UserModel> returnModels = new List<UserModel>();

            foreach (var group in groups)
            {
                var userModel = userService.GetUsersByGroup(group.Group).Data as List<UserModel>;
                if(userModel!=null)
                returnModels.AddRange(userModel);
            }
            returnModel.Data = returnModels;
            returnModel.TotalCount = returnModels.Count;

            return returnModel;
        }
        public static ReturnModel GetUserLoginsByGroup(string group)
        {
            return userService.GetUserLoginsByGroup(group);
        }
        public static ReturnModel GetUserRights(ulong login)
        {
            return userService.GetUserRights(login);
        }
        public static ReturnModel SetUserRights(ulong login, ulong right)
        {
            return userService.SetUserRights(login, right);
        }
        public static ReturnModel GetBalanceByGroup(string group)
        {
            var users = userService.GetUserLoginsByGroup(group).Data as ulong[];
            if (users != null)
            {
                var returnModel = userService.GetUserBalance(users);
                return returnModel;
            }
            return new ReturnModel();
        }
        public static ReturnModel CreateUser(CreateUserModel model)
        {
            var res = userService.CreateUser(model);
            return res;
        }
        public static ReturnModel GetUser(ulong login)
        {
            var user = userService.GetUser(login);            
            return user;
        }
        public static ReturnModel UpdateUser(CreateUserModel model)
        {
            var resp = userService.UpdateUser(model);            
            return resp;
        }
        public static MTRetCode ChangePassword(ulong login, string password)
        {
            var response = userService.ChangePassword(0,login, password);
            return response.MTRetCode;
        }
        public static MTRetCode UserDelete(ulong login)
        {
            var resp = userService.DeleteUser(login);
            return resp;
        }
        public static ReturnModel UserRequest(ulong login)
        {
            var resp = userService.GetUser(login);            
            return resp;
        }
        public static ReturnModel UserAccountGet(ulong login)
        {
            var resp = accountService.UserAccountGet(login);
            return resp;
        }
        public static ReturnModel PasswordCheck(int type, ulong login, string password)
        {
            return userService.UserPasswordCheck(type, login, password);            
        }
        public static ReturnModel UserBalanceCheck(ulong login, bool fixflag)
        {
            return userService.GetUserBalance(login, fixflag);            
        }
        public static ReturnModel UserBalanceCheck(ulong[] logins)
        {
            return userService.GetUserBalance(logins);
        }
        public static ReturnModel UserLogins(string group)
        {
            return userService.GetUserLoginsByGroup(group);
        }
        public static ReturnModel UserRequestArray(string group)
        {
            return  userService.GetUsersByGroup(group);
        }
        public static ReturnModel UserCertGet(ulong login)
        {
            ReturnModel returnModel = new ReturnModel();
            MTRetCode res = new MTRetCode();
            //var logins = new Array<int>(); ;
            using (CIMTCertificate certificate = m_manager.UserCertCreate())
            {
                returnModel.MTRetCode = m_manager.UserCertGet(login, certificate);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = new
                    {
                        IsCA = certificate.IsCA(),
                        IsOpened = certificate.IsOpened(),
                        SerialNumber = certificate.SerialNumber(),
                        ValidFrom = certificate.ValidFrom(),
                        ValidTo = certificate.ValidTo(),
                    };
                }
            }
            return returnModel;
        }


        public static ReturnModel UserRequestByLogins(ulong[] logins)
        {
           var res = userService.GetUserByLogins(logins);
            return res;
        }

        #endregion

    #region History   
        public static ReturnModel HistoryRequest(ulong login, long from, long to)
        {
            return orderService.HistoryRequest(login, from, to);

            ReturnModel returnModel = new ReturnModel();
            List<OrderModel> orderList = new List<OrderModel>();
            using (CIMTOrderArray Orders = m_manager.OrderCreateArray())
            {
                returnModel.MTRetCode = m_manager.HistoryRequest(login, from, to, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in Orders.ToArray())
                    {
                        var obj = new OrderModel()
                        {
                            ContractSize = item.ContractSize(),
                            Dealer = item.Dealer(),
                            Digits = item.Digits(),
                            ExpertID = item.ExpertID(),
                            ExternalID = item.ExternalID(),
                            Login = item.Login(),
                            Comment = item.Comment(),
                            ModificationFlags = item.ModificationFlags(),
                            Order = item.Order(),
                            PositionByID = item.PositionByID(),
                            PositionID = item.PositionID(),
                            PriceCurrent = item.PriceCurrent(),
                            PriceOrder = item.PriceOrder(),
                            PriceSL = item.PriceSL(),
                            PriceTP = item.PriceTP(),
                            PriceTrigger = item.PriceTrigger(),
                            Print = item.Print(),
                            RateMargin = item.RateMargin(),
                            Reason = item.Reason(),
                            State = item.State(),
                            Symbol = item.Symbol(),
                            TimeDone = item.TimeDone(),
                            TimeExpiration = item.TimeExpiration(),
                            VolumeInitial = item.VolumeInitial(),
                            VolumeInitialExt = item.VolumeInitialExt(),
                        };
                        orderList.Add(obj);
                    }
                }
                returnModel.Data = orderList;
                returnModel.TotalCount = orderList.Count();
            }

            return returnModel;
        }
        public static ReturnModel HistoryRequestByLogin(ulong[] logins, long from, long to)
        {
            return orderService.HistoryRequestByLogin(logins, from, to);

            ReturnModel returnModel = new ReturnModel();
            List<OrderModel> orderList = new List<OrderModel>();
            using (CIMTOrderArray Orders = m_manager.OrderCreateArray())
            {
                returnModel.MTRetCode = m_manager.HistoryRequestByLogins(logins, from, to, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in Orders.ToArray())
                    {
                        var obj = new OrderModel()
                        {
                            ContractSize = item.ContractSize(),
                            Dealer = item.Dealer(),
                            Digits = item.Digits(),
                            ExpertID = item.ExpertID(),
                            ExternalID = item.ExternalID(),
                            Login = item.Login(),
                            Comment = item.Comment(),
                            ModificationFlags = item.ModificationFlags(),
                            Order = item.Order(),
                            PositionByID = item.PositionByID(),
                            PositionID = item.PositionID(),
                            PriceCurrent = item.PriceCurrent(),
                            PriceOrder = item.PriceOrder(),
                            PriceSL = item.PriceSL(),
                            PriceTP = item.PriceTP(),
                            PriceTrigger = item.PriceTrigger(),
                            Print = item.Print(),
                            RateMargin = item.RateMargin(),
                            Reason = item.Reason(),
                            State = item.State(),
                            Symbol = item.Symbol(),
                            TimeDone = item.TimeDone(),
                            TimeExpiration = item.TimeExpiration(),
                            VolumeInitial = item.VolumeInitial(),
                            VolumeInitialExt = item.VolumeInitialExt(),
                        };
                        orderList.Add(obj);
                    }
                }
                returnModel.Data = orderList;
                returnModel.TotalCount = orderList.Count();
            }

            return returnModel;
        }
    #endregion

    #region Positions    
        public static ReturnModel PositionRequest(ulong login)
        {    
            return positionService.PositionRequest(login);
        }
        public static ReturnModel ClosePositionRequest(ulong login, long from, long to)
        {
            ReturnModel returnModel = new ReturnModel();
            var res = dealerService.DealsRequest(login, from, to).Data as List<DealModel>;
            var deals = new Dictionary<ulong, DealModel>();

            foreach (var deal in res)
            {
                if (!deals.ContainsKey(deal.PositionID) && deal.Entry == 1)
                {
                    //Console.WriteLine("Positions: " + deal.PositionID);
                    deals.Add(deal.PositionID, deal);
                }
            }
            //Console.WriteLine("Positions Total: " + deals.Count);
            returnModel.Data = deals;
            returnModel.TotalCount = deals.Count;
            return returnModel;
        }
        public static ReturnModel PositionsGetAll(ulong login, long from, long to)
        {
            ReturnModel returnModel = new ReturnModel();
            var res = dealerService.DealsRequest(login, from, to).Data as List<DealModel>;
            var deals = new Dictionary<ulong, DealModel>();

            foreach (var deal in res)
            {
                if (!deals.ContainsKey(deal.PositionID) && deal.PositionID != 0)
                {
                    //Console.WriteLine("Positions: " + deal.PositionID);
                    deals.Add(deal.PositionID,deal);
                }
            }
            //Console.WriteLine("Positions Total: " + deals.Count);
            returnModel.Data = deals;
            returnModel.TotalCount = deals.Count;
            return returnModel;
        }
        public static ReturnModel PositionsGetAllPaginated(ulong login, long from, long to, int pageNumber, int pageSize)
        {
            ReturnModel returnModel = new ReturnModel();
            var res = dealerService.DealsRequest(login, from, to).Data as List<DealModel>;
            var deals = new Dictionary<ulong, DealModel>();

            foreach (var deal in res)
            {
                if (!deals.ContainsKey(deal.PositionID) && deal.PositionID != 0)
                {
                    //Console.WriteLine("Positions: " + deal.PositionID);
                    deals.Add(deal.PositionID, deal);
                }
            }
            var paginatedDeals = deals.Values
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //Console.WriteLine("Positions Total: " + deals.Count);
            returnModel.Data = paginatedDeals;
            returnModel.TotalCount = deals.Count;
            return returnModel;
        }
        public static ReturnModel PositionRequest(ulong login, int pageNumber, int pageSize)
        {
            return positionService.PositionRequest(login, pageNumber, pageSize);
        }
        public static ReturnModel PositionRequestBySymbol(string symbol, string group)
        {          
            return positionService.PositionsRequestBySymbol(group, symbol);
        }
        public static ReturnModel PositionRequestAll()
        {      
            return positionService.PositionsRequestByGroupAll();
        }
        public static ReturnModel PositionRequestByLogins(ulong[] logins)
        {
            return positionService.PositionsRequestByLogins(logins);
        }
        public static ReturnModel PositionRequestByLogin(ulong login)
        {
            return positionService.PositionsRequestByLogin(login);
        }
        public static ReturnModel PositionRequestByTicket(ulong ticket)
        {
            return positionService.PositionRequestByTicket(ticket);
        }
        public static ReturnModel PositionRequestByTickets(ulong[] tickets)
        {
            return positionService.PositionRequestByTickets(tickets);
        }
        //public static ReturnModel PositionRequest(ulong login)
        //{
        //    ReturnModel returnModel = new ReturnModel();
        //    MTRetCode res = new MTRetCode();
        //    using (CIMTPositionArray positions = m_manager.PositionCreateArray())
        //    {
        //        returnModel.MTRetCode = m_manager.PositionRequest(login, positions);
        //    }
        //    return returnModel;
        //}

        //Positions
        //public static ReturnModel PositionUpdate(ulong login)
        //{
        //    ReturnModel returnModel = new ReturnModel();
        //    using (CIMTPosition position = m_manager.PositionCreate())
        //    {
        //        m_manager.PositionGet()
        //        position.Action(200);
        //        position.Symbol("EURUSD");
        //        returnModel.MTRetCode = m_manager.PositionUpdate(position);
        //    }

        //    return returnModel;
        //}

        #endregion

        //public static async Task OnDealerResult()
        //{
        //    CIMTDealerSink sink = new CIMTDealerSink();
        //    await sink.OnDealerResult()
        //}
        public static ReturnModel OnRequestServerLogs(int requestMode,
                                       int logType,
                                       Int64 from,
                                       Int64 to,
                                       string filter = null)
        {
            ReturnModel returnModel = new ReturnModel();
            EnMTLogRequestMode request = EnMTLogRequestMode.MTLogModeFirst;
            EnMTLogType log = EnMTLogType.MTLogTypeAll;

            try
            {
                switch (requestMode)
                {
                    case 0:
                        request = EnMTLogRequestMode.MTLogModeFirst;
                        break;
                    case 1:
                        request = EnMTLogRequestMode.MTLogModeErr;
                        break;
                    case 2:
                        request = EnMTLogRequestMode.MTLogModeFull;
                        break;
                    default:
                        request = EnMTLogRequestMode.MTLogModeFull;
                        break;
                }
                switch (logType)
                {
                    case 0:
                        log = EnMTLogType.MTLogTypeAll;
                        break;
                    case 1:
                        log = EnMTLogType.MTLogTypeCfg;
                        break;
                    case 2:
                        log = EnMTLogType.MTLogTypeSys;
                        break;
                    case 3:
                        log = EnMTLogType.MTLogTypeNet;
                        break;
                    case 4:
                        log = EnMTLogType.MTLogTypeHst;
                        break;
                    case 5:
                        log = EnMTLogType.MTLogTypeUser;
                        break;
                    case 6:
                        log = EnMTLogType.MTLogTypeTrade;
                        break;
                    case 7:
                        log = EnMTLogType.MTLogTypeAPI;
                        break;
                    case 8:
                        log = EnMTLogType.MTLogTypeNotify;
                        break;
                    case 16:
                        log = EnMTLogType.MTLogTypeLiveUpdate;
                        break;
                    case 17:
                        log = EnMTLogType.MTLogTypeSendMail;
                        break;
                    default:
                        log = EnMTLogType.MTLogTypeAll;
                        break;
                }

                MTRetCode result = MTRetCode.MT_RET_ERROR;
                List<MTLogRecord> Loglist = new List<MTLogRecord>();
                //---
                MTLogRecord[] records = m_manager.LoggerServerRequest(request, log, from, to, filter, out result);
                //---
                string LogerServerRequest = string.Format("LoggerServerRequest {0} ==> [{1}] return {2} record(s)",
                             (result == MTRetCode.MT_RET_OK ? "ok" : "failed"),
                             result, (records != null ? records.Length : 0));
                //---
                if ((result == MTRetCode.MT_RET_OK) && (records != null))
                {
                    foreach (MTLogRecord rec in records)
                    {
                        Loglist.Add(rec);
                    }
                    returnModel.Data = Loglist;
                }
                returnModel.MTRetCode = result;
            }
            catch (Exception ex)
            {

            }
            return returnModel;
        }
        
    #region Orders
        public static ReturnModel OrderGet(ulong login)
        {
            ulong[] logins = { login };
            return orderService.OrderRequestByLogin(logins);
            
            ReturnModel returnModel = new ReturnModel();
            using (CIMTOrder order = m_manager.OrderCreate())
            {
                returnModel.MTRetCode = m_manager.OrderRequest(login, order);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = new
                    {
                        Login = order.Login(),
                        Order = order.Order(),
                        Dealer = order.Dealer(),
                        Symbol = order.Symbol(),
                        PriceOrder = order.PriceOrder(),
                        PriceCurrent = order.PriceCurrent(),
                        PriceSL = order.PriceSL(),
                        PriceTP = order.PriceTP(),
                        Comment = order.Comment(),
                        RateMargin = order.RateMargin(),
                    };
                }
            }
            return returnModel;
        }
        public static ReturnModel GetAllOpenOrders(ulong[] login)
        {
            return orderService.GetAllOpenOrders(login);

            ReturnModel returnModel = new ReturnModel();
            List<object> orderList = new List<object>();
            using (CIMTOrderArray orders = m_manager.OrderCreateArray())
            {
                returnModel.MTRetCode = m_manager.OrderGetByLogins(login, orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    
                    foreach (var order in orders.ToArray())
                    {
                        var obj = new
                        {
                            Login = order.Login(),
                            Order = order.Order(),
                            Dealer = order.Dealer(),
                            Symbol = order.Symbol(),
                            volume = order.VolumeInitial(),
                            volumeCurrent = order.VolumeCurrent(),
                            type = order.Type(),
                            PriceOrder = order.PriceOrder(),
                            PriceCurrent = order.PriceCurrent(),
                            PriceSL = order.PriceSL(),
                            PriceTP = order.PriceTP(),
                            Comment = order.Comment(),
                            RateMargin = order.RateMargin(),
                        };
                        orderList.Add(obj);
                    }
                    if (orderList.Count > 0)
                    {
                        returnModel.Data = orderList;
                        returnModel.Message = string.Format("Total Orders is '{0}' ", orderList.Count);
                    }
                    else
                    {
                        returnModel.Message = "No Order Found";
                    }
                }
                else
                {
                    returnModel.Message = returnModel.MTRetCode.ToString();
                }
            }
            return returnModel;
        }
        public static ReturnModel GetOrdersByTickets(ulong[] tickets)
        {
            return orderService.OrderRequestByTickets(tickets);
        }
        public static ReturnModel GetOrdersByTicket(ulong ticket)
        {
            return orderService.OrderRequestByTicket(ticket);
        }
        //public static ReturnModel OrderUpdate(ulong login)
        //{
        //    ReturnModel returnModel = new ReturnModel();
        //    using (CIMTOrder order = m_manager.OrderCreate())
        //    {

        //        order.Symbol("EURUSD");
        //        order.Type(0);
        //        order.TypeFill(0);
        //        order.VolumeInitial(5000);
        //        order.price
        //        returnModel.MTRetCode = m_manager.PositionUpdate(position);
        //    }

        //    return returnModel;
        //}

        //History
        
    #endregion

    #region Deals
        public static ReturnModel GetDeal(ulong ticket)
        {
            return dealerService.DealRequestByTicket(ticket);

            ReturnModel returnModel = new ReturnModel();
            using (CIMTDeal deal = m_manager.DealCreate())
            {
                returnModel.MTRetCode = m_manager.DealRequest(ticket, deal);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = new
                    {
                        Login = deal.Login(),
                        Deal = deal.Deal(),
                        Order = deal.Order(),
                        Action = deal.Action(),
                        Entry = deal.Entry(),
                        Symbol = deal.Symbol(),
                        Price = deal.Price(),
                        PriceSL = deal.PriceSL(),
                        PriceTP = deal.PriceTP(),
                        Profit = deal.Profit(),
                        Volume = deal.Volume(),
                        Value = deal.Value(),
                        Commission = deal.Commission(),
                        Comment = deal.Comment(),
                    };
                }
            }
            return returnModel;
        }
        public static ReturnModel GetAllDeal(ulong login, long from, long to)
        {
            return dealerService.DealsRequest(login, from, to);    
            
            ReturnModel returnModel = new ReturnModel();
            List<object> dealList = new List<object>();
            using (CIMTDealArray deals = m_manager.DealCreateArray())
            {
                returnModel.MTRetCode = m_manager.DealRequest(login, from, to, deals);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var deal in deals.ToArray())
                    {
                        var obj = new
                        {
                            Login = deal.Login(),
                            Deal = deal.Deal(),
                            Order = deal.Order(),
                            Action = deal.Action(),
                            Entry = deal.Entry(),
                            Symbol = deal.Symbol(),
                            Price = deal.Price(),
                            PriceSL = deal.PriceSL(),
                            PriceTP = deal.PriceTP(),
                            Profit = deal.Profit(),
                            Volume = deal.Volume(),
                            Value = deal.Value(),
                            Commission = deal.Commission(),
                            Comment = deal.Comment(),
                        };
                        dealList.Add(obj);
                    }
                    if (dealList.Count > 0)
                    {
                        returnModel.Data = dealList;
                        returnModel.Message = string.Format("Total Deals is '{0}' ", dealList.Count);
                    }
                    else
                    {
                        returnModel.Message = "No Deal Found";
                    }
                }
                else
                {
                    returnModel.Message = returnModel.MTRetCode.ToString();
                }
            }
            return returnModel;
        }
        public static ReturnModel DealRequestByPosition(ulong positionId)
        {
            ReturnModel returnModel = new ReturnModel();

            var pos = PositionRequestByTicket(positionId).Data as PositionModel;
            if(pos == null) { returnModel.Message = "Position Not FOund"; return returnModel; }
            var deals = GetAllDeal(pos.Login, pos.TimeCreated, pos.TimeCreated).Data as List<DealModel>;
            if (pos == null) { returnModel.Message = "Deals Not FOund"; return returnModel; }
            List<DealModel> dealList = new List<DealModel>();
            foreach (var deal in deals)
            {
                dealList.Add(deal);
            }
            returnModel.Data = dealList;
            returnModel.Message = string.Format("Total Deals is '{0}' ", dealList.Count);
            return returnModel;
        }
        public static ReturnModel DealCommissionUpdate(ulong ticket, double commission)
        {
            return dealerService.DealCommissionUpdate(ticket, commission);

            ReturnModel returnModel = new ReturnModel();
            using (CIMTDeal deal = m_manager.DealCreate())
            {
                returnModel.MTRetCode = m_manager.DealRequest(ticket, deal);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    deal.Commission(commission);
                    returnModel.MTRetCode = m_manager.DealUpdate(deal);
                    if (returnModel.MTRetCode == MTRetCode.MT_RET_OK && deal.Commission() == commission)
                    {
                        returnModel.Data = deal.Commission();
                    }
                    else
                    {
                        returnModel.Message = "Commission Not Updated";
                    }
                }
                else
                {
                    returnModel.Message = "Deal not found";
                }
            }
            return returnModel;
        }

    #endregion

    # region Dealer
        public static ReturnModel DealerBalance(UInt64 login, double amount, uint type, string comment, bool deposit)
        {
            return dealerService.DealerBalance(login, amount, type, comment, deposit);

            ReturnModel returnModel = new ReturnModel();
            ulong deal_id;
            returnModel.MTRetCode = m_manager.DealerBalanceRaw(login, deposit ? amount : -amount, type, comment, out deal_id);

            if (returnModel.MTRetCode == MTRetCode.MT_RET_REQUEST_DONE)
            {
                string opName = string.Empty;
                switch (type)
                {
                    case 2:
                        opName = "Balance Operation";
                        break;
                    case 3:
                        opName = "Credit Operation";
                        break;
                    case 4:
                        opName = "Charge Operation";
                        break;
                    case 5:
                        opName = "Correction Operation";
                        break;
                    case 6:
                        opName = "Bonus Operation";
                        break;
                    case 7:
                        opName = "Commission Operation";
                        break;
                    case 10:
                        opName = "Rebate Operation";
                        break;
                    default:
                        opName = string.Format("Other Operation Type is {0}", type);
                        break;
                }
                returnModel.Message = string.Format("'{0}' is Perform and Deal is '{1}'", opName, deal_id);
                returnModel.Data = deal_id;
            }
            else
            {
                returnModel.Message = returnModel.MTRetCode.ToString();
            }
            return returnModel;
        }        
        public static ReturnModel DealerSend(ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP)
        {
            return dealerService.DealerSend(m_dealer, login, orderPrice, volume, enTradeAction, enOrderType, symbol, priceSL, priceTP);

            ReturnModel returnModel = new ReturnModel();

            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
            CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
            m_request = m_manager.RequestCreate();
            m_position = m_manager.PositionCreate();
            m_deal = m_manager.DealCreate();

            switch (enTradeAction)
            {
                case 200:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
                case 201:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_PENDING;
                    break;
                default:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
            }
            switch (enOrderType)
            {
                case 0:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
                case 1:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_SELL;
                    break;
                case 2:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY_LIMIT;
                    break;
                default:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
            }
            {
                uint id = 0;
                //m_request.SourceLogin(login);
                m_request.Action(enTradeActions);
                m_request.Login(login);
                //m_request.SourceLogin(1003);
                //m_request.Symbol("EURUSD");
                m_request.Symbol(symbol);
                m_request.Type(enOrderType1);
                m_request.TypeFill(CIMTOrder.EnOrderFilling.ORDER_FILL_FOK);
                m_request.Volume(volume);
                m_request.PriceOrder(orderPrice);
                m_request.PriceSL(priceSL);
                m_request.PriceTP(priceTP);
                //m_request.Digits(5);
                returnModel.MTRetCode = m_manager.DealerSend(m_request, m_dealer, out id);
                Thread.Sleep(3000);
                //m_dealer.OnDealerAnswer(m_request);
                //m_request2 = Globals.CIMTRequest;
                if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                {
                    returnModel.MTRetCode = m_manager.DealRequest(m_request.ResultDeal(), m_deal);
                    //returnModel.MTRetCode = m_manager.PositionGet(login, m_request.Symbol(), m_position);
                    returnModel.MTRetCode = m_manager.PositionGetByTicket(m_deal.PositionID(), m_position);

                    if (m_deal.Deal() == m_request.ResultDeal() && returnModel.MTRetCode == MTRetCode.MT_RET_OK && m_deal.PositionID() == m_position.Position())
                    {
                        returnModel.MTRetCode = m_position.PriceOpen(orderPrice);
                        returnModel.MTRetCode = m_position.PriceCurrent(orderPrice);
                        returnModel.MTRetCode = m_manager.PositionUpdate(m_position);
                        if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                        {
                            returnModel.Message = string.Format("Position '{0}' is created with '{1}' price ", m_position.Position(), orderPrice);
                        }
                        else
                        {
                            returnModel.Message = "Position Create but price not  Update";
                        }
                    }
                    else
                    {
                        returnModel.Message = "Position Create but price not  Update";
                    }
                }
                else
                {
                    returnModel.Message = m_request.ResultRetcode().ToString();
                }
            }

            return returnModel;
        }
        public static ReturnModel DealerSendCloseAndModifyPosition(ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP, ulong positionId)
        {
            return dealerService.DealerSendCloseAndModifyPosition(m_dealer, login, orderPrice, volume, enTradeAction, enOrderType, symbol, priceSL, priceTP, positionId);

            ReturnModel returnModel = new ReturnModel();

            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
            CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
            m_request = m_manager.RequestCreate();
            m_position = m_manager.PositionCreate();
            m_deal = m_manager.DealCreate();

            switch (enTradeAction)
            {
                case 200:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
                case 202:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_MODIFY;
                    break;
                default:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
            }
            switch (enOrderType)
            {
                case 0:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
                case 1:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_SELL;
                    break;
                default:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
            }
            {
                uint id = 0;
                //m_request.SourceLogin(login);
                m_request.Action(enTradeActions);
                m_request.Login(login);
                //m_request.SourceLogin(1003);
                //m_request.Symbol("EURUSD");
                m_request.Symbol(symbol);
                m_request.Type(enOrderType1);
                m_request.TypeFill(CIMTOrder.EnOrderFilling.ORDER_FILL_FOK);
                m_request.Volume(volume);
                m_request.PriceOrder(orderPrice);
                m_request.PriceSL(priceSL);
                m_request.PriceTP(priceTP);
                m_request.Position(positionId);
                //m_request.Digits(5);
                returnModel.MTRetCode = m_manager.DealerSend(m_request, m_dealer, out id);
                Thread.Sleep(3000);
                //m_dealer.OnDealerAnswer(m_request);
                //m_request2 = Globals.CIMTRequest;
                if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                {
                    returnModel.MTRetCode = m_manager.DealRequest(m_request.ResultDeal(), m_deal);
                    //returnModel.MTRetCode = m_manager.PositionGet(login, m_request.Symbol(), m_position);
                    returnModel.MTRetCode = m_manager.PositionGetByTicket(m_deal.PositionID(), m_position);

                    if (m_deal.Deal() == m_request.ResultDeal() && returnModel.MTRetCode == MTRetCode.MT_RET_OK && m_deal.PositionID() == m_position.Position())
                    {
                        returnModel.MTRetCode = m_position.PriceOpen(orderPrice);
                        returnModel.MTRetCode = m_position.PriceCurrent(orderPrice);
                        returnModel.MTRetCode = m_manager.PositionUpdate(m_position);
                        if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                        {
                            returnModel.Message = string.Format("Position '{0}' is closed with '{1}' price ", m_position.Position(), orderPrice);
                        }
                        else
                        {
                            returnModel.Message = "Position Closed";
                        }
                    }
                    else
                    {
                        returnModel.Message = "Position Closed";
                    }
                }
                else
                {
                    returnModel.Message = m_request.ResultRetcode().ToString();
                }
            }

            return returnModel;
        }
        public static ReturnModel DealerSendOrder(ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP)
        {
            return dealerService.DealerSendOrder(m_dealer, login, orderPrice, volume, enTradeAction, enOrderType, symbol, priceSL, priceTP);

            ReturnModel returnModel = new ReturnModel();

            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
            CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
            m_request = m_manager.RequestCreate();
            m_order = m_manager.OrderCreate();
            m_deal = m_manager.DealCreate();

            switch (enTradeAction)
            {
                case 5:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_PENDING;
                    break;
                case 200:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
                case 201:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_PENDING;
                    break;
                case 202:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_MODIFY;
                    break;
                case 203:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_MODIFY;
                    break;
                case 204:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_REMOVE;
                    break;
                case 205:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_ACTIVATE;
                    break;
                default:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
            }
            switch (enOrderType)
            {
                case 0:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
                case 1:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_SELL;
                    break;
                case 2:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY_LIMIT;
                    break;
                case 3:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_SELL_LIMIT;
                    break;
                default:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
            }
            {
                uint id = 0;
                //m_request.SourceLogin(login);
                //m_request.Action(enTradeActions);
                //m_request.SourceLogin(1117);
                //m_request.Login(login);
                ////m_request.Symbol("EURUSD");
                //m_request.Symbol(symbol);
                //m_request.Volume(volume);
                //m_request.Type(enOrderType1);
                //m_request.PriceOrder(orderPrice);
                m_request.TypeTime(CIMTOrder.EnOrderTime.ORDER_TIME_GTC);
                //m_request.TypeFill(CIMTOrder.EnOrderFilling.ORDER_FILL_FOK);
                m_request.Action(enTradeActions);
                m_request.Login(login);
                //m_request.SourceLogin(1003);
                //m_request.Symbol("EURUSD");

                m_request.Symbol(symbol);
                m_request.Type(enOrderType1);
                m_request.TypeFill(CIMTOrder.EnOrderFilling.ORDER_FILL_FOK);
                m_request.Volume(volume);
                m_request.PriceOrder(orderPrice);

                m_request.PriceSL(priceSL);
                m_request.PriceTP(priceTP);

                //m_request.PriceTrigger(0);
                //m_request.TimeExpiration();
                //m_request.Digits(5);
                //m_request.Assign(m_request);
                returnModel.MTRetCode = m_manager.DealerSend(m_request, m_dealer, out id);
                Console.WriteLine(m_manager.DealerSend(m_request, m_dealer, out id));
                Thread.Sleep(3000);
                Console.WriteLine(m_request);
                Console.WriteLine(id);
                Console.WriteLine(m_request.ResultRetcode());
                m_dealer.OnDealerAnswer(m_request);
                //m_request2 = Globals.CIMTRequest;
                if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                {
                    returnModel.MTRetCode = m_manager.DealRequest(m_request.ResultDeal(), m_deal);
                    //returnModel.MTRetCode = m_manager.PositionGet(login, m_request.Symbol(), m_position);
                    returnModel.MTRetCode = m_manager.PositionGetByTicket(m_deal.PositionID(), m_position);

                    if (m_deal.Deal() == m_request.ResultDeal() && returnModel.MTRetCode == MTRetCode.MT_RET_OK && m_deal.PositionID() == m_position.Position())
                    {
                        returnModel.MTRetCode = m_position.PriceOpen(orderPrice);
                        returnModel.MTRetCode = m_position.PriceCurrent(orderPrice);
                        //returnModel.MTRetCode = m_manager.PositionUpdate(m_position);
                        if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                        {
                            returnModel.Message = string.Format("Order '{0}' is created with '{1}' price ", m_position.Position(), orderPrice);
                        }
                        else
                        {
                            returnModel.Message = "Order closed but price not  Update";
                        }
                    }
                    else
                    {
                        returnModel.Message = "Order closed but price not  Update";
                    }
                }
                else
                {
                    returnModel.Message = m_request.ResultRetcode().ToString();
                }
            }

            return returnModel;
        }
        public static ReturnModel DealerSendOrderCloseAndModify(ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP, ulong orderId)
        {
            return dealerService.DealerSendOrderCloseAndModify(m_dealer, login, orderPrice, volume, enTradeAction, enOrderType, symbol, priceSL, priceTP, orderId);

            ReturnModel returnModel = new ReturnModel();

            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
            CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
            m_request = m_manager.RequestCreate();
            m_order = m_manager.OrderCreate();
            m_deal = m_manager.DealCreate();

            switch (enTradeAction)
            {
                case 5:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_PENDING;
                    break;
                case 200:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
                case 201:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_PENDING;
                    break;
                case 202:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_MODIFY;
                    break;
                case 203:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_MODIFY;
                    break;
                case 204:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_REMOVE;
                    break;
                case 205:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_ORD_ACTIVATE;
                    break;
                default:
                    enTradeActions = CIMTRequest.EnTradeActions.TA_DEALER_POS_EXECUTE;
                    break;
            }
            switch (enOrderType)
            {
                case 0:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
                case 1:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_SELL;
                    break;
                case 2:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY_LIMIT;
                    break;
                case 3:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_SELL_LIMIT;
                    break;
                default:
                    enOrderType1 = CIMTOrder.EnOrderType.OP_BUY;
                    break;
            }
            {
                uint id = 0;
                //m_request.SourceLogin(login);
                //m_request.Action(enTradeActions);
                //m_request.SourceLogin(1117);
                //m_request.Login(login);
                ////m_request.Symbol("EURUSD");
                //m_request.Symbol(symbol);
                //m_request.Volume(volume);
                //m_request.Type(enOrderType1);
                //m_request.PriceOrder(orderPrice);
                m_request.TypeTime(CIMTOrder.EnOrderTime.ORDER_TIME_GTC);
                //m_request.TypeFill(CIMTOrder.EnOrderFilling.ORDER_FILL_FOK);
                m_request.Action(enTradeActions);
                m_request.Login(login);
                //m_request.SourceLogin(1003);
                m_request.Order(orderId);

                m_request.Symbol(symbol);
                m_request.Type(enOrderType1);
                m_request.TypeFill(CIMTOrder.EnOrderFilling.ORDER_FILL_FOK);
                m_request.Volume(volume);
                m_request.PriceOrder(orderPrice);

                m_request.PriceSL(priceSL);
                m_request.PriceTP(priceTP);

                //m_request.PriceTrigger(0);
                //m_request.TimeExpiration();
                //m_request.Digits(5);
                //m_request.Assign(m_request);
                returnModel.MTRetCode = m_manager.DealerSend(m_request, m_dealer, out id);
                Console.WriteLine(m_manager.DealerSend(m_request, m_dealer, out id));
                Thread.Sleep(3000);
                Console.WriteLine(m_request);
                Console.WriteLine(id);
                Console.WriteLine(m_request.ResultRetcode());
                m_dealer.OnDealerAnswer(m_request);
                //m_request2 = Globals.CIMTRequest;
                if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                {
                    returnModel.MTRetCode = m_manager.DealRequest(m_request.ResultDeal(), m_deal);
                    //returnModel.MTRetCode = m_manager.PositionGet(login, m_request.Symbol(), m_position);
                    returnModel.MTRetCode = m_manager.PositionGetByTicket(m_deal.PositionID(), m_position);

                    if (m_deal.Deal() == m_request.ResultDeal() && returnModel.MTRetCode == MTRetCode.MT_RET_OK && m_deal.PositionID() == m_position.Position())
                    {
                        returnModel.MTRetCode = m_position.PriceOpen(orderPrice);
                        returnModel.MTRetCode = m_position.PriceCurrent(orderPrice);
                        //returnModel.MTRetCode = m_manager.PositionUpdate(m_position);
                        if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                        {
                            returnModel.Message = string.Format("Order '{0}' is created with '{1}' price ", m_position.Position(), orderPrice);
                        }
                        else
                        {
                            returnModel.Message = "Order Closed";
                        }
                    }
                    else
                    {
                        returnModel.Message = "Order Closed";
                    }
                }
                else
                {
                    returnModel.Message = m_request.ResultRetcode().ToString();
                }
            }

            return returnModel;
        }
        public static void GetLastRequest(CIMTRequest request)
        {
            //string message = string.Format("OnDealerAnswer: retcode= {2}, deal = {0}, order = {1}", request.ResultDeal(), request.ResultOrder(), request.ResultRetcode());
            //if (request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
            //{

            //}
            //--- return request
            m_request.Assign(request);
        }
        public static MTRetCode GetDealerRequest(CIMTRequest request)
        {
            return dealerService.GetDealerRequest(request);

            MTRetCode mTRet = new MTRetCode();
            mTRet = m_manager.DealerStart();
            if (mTRet == MTRetCode.MT_RET_OK)
            {
                mTRet = m_manager.DealerGet(m_request);
            }
            return mTRet;
        }
        //static void DealerFunc()
        //{
        //    if(m_dealer.OnDealerAnswer(m_request)== )
        //}



        #endregion

    #region Book
        public static MTBook GetBook(string symbol)
        {
            //ReturnModel returnModel = new ReturnModel();
            //if (m_manager != null)
            //{
            MTBook res = new MTBook();
            //res.flags = MTBook.EnBookFlags.FLAG_ALL;
            //res.symbol = symbol;
            //CBooks bookObj = new CBooks();
            //bookObj.Initialize(m_manager);
            //MTRetCode result = MTRetCode.MT_RET_ERROR;
            string[] symbols = { "EURUSD", "USDCAD", "UKOIL.c", "CHFJPY", "USDCHF", "XAUUSD", "CADJPY", "USDJPY", "XAGUSD", "USOIL.c", "GBPUSD", "NZDUSD", "AUDUSD", "NACUSD.c", "DJCUSD.c", "SPCUSD.c", "GECEUR.c", "JPCJPY.c" };
            CBooks bookObj = new CBooks();
            bookObj.Initialize(m_manager);
            m_manager.BookSubscribeBatch(symbols, bookObj);
            bookData = m_manager.BookGet(symbol, out res);

            //if (result != MTRetCode.MT_RET_OK)
            //{
            //return null;
            //}
            //else
            //{
            return res;
            //}
            //}
            //else
            //{
            //MTBook res = new MTBook();
            //return res;
            //}
        }

        #endregion
        //charts
        //public static ReturnModel ChartRequest(string symbol, long from, long to)
        //{
        //ReturnModel returnModel = new ReturnModel();
        //returnModel.MTRetCode = m_manager.ChartRequest(symbol, from, to, )
        //}

    #region Group
        public static ReturnModel GetGroup(string group)
        {
            return groupService.GetGroup(group);
        }
        public static ReturnModel GetGroupTotal()
        {
            return groupService.GetGroupTotal();

        }
        public static ReturnModel GetAllGroup()
        {
            return groupService.GetAllGroups();
        }
        public static ReturnModel UpdateGroup(GroupModel group) {
            return groupService.UpdateGroup(group);
        }
    #endregion
    }
}
