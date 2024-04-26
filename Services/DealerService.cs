using Goorge.Model;
using Goorge.Models;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using TradeRateSell;

namespace Goorge.Services
{
    public class DealerService: IService
    {
        CIMTManagerAPI managerAPI;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            managerAPI = m_manager;
        }
       public ReturnModel DealRequestByTicket(ulong ticket) { 
            ReturnModel model = new ReturnModel();
            using(var deal = managerAPI.DealCreate())
            {
                var res = managerAPI.DealRequest(ticket,deal);
                model.MTRetCode = res;
                if(res == MTRetCode.MT_RET_OK)
                {
                    model.Data = new DealModel(deal);
                }

            }            
            return model;        
        }
       public ReturnModel DealsRequest(ulong login, long from, long to) { 
            ReturnModel model = new ReturnModel();
            List<DealModel> dealsList = new List<DealModel>();  
            using(var deals = managerAPI.DealCreateArray())
            {
                var res = managerAPI.DealRequest(login, from, to, deals);
                model.MTRetCode = res;
                if(res == MTRetCode.MT_RET_OK)
                {
                    foreach(var deal in deals.ToArray())
                    {
                        dealsList.Add(new DealModel(deal));
                    }
                    model.Data = dealsList;
                    model.TotalCount = dealsList.Count;
                }

            }
            
            return model;        
        }
       public void DealAdd() { }
       public ReturnModel DealUpdate(ulong ticket) {
            var model = new ReturnModel();
            var resp = managerAPI.DealDelete(ticket);
            model.MTRetCode = resp;
            if(resp == MTRetCode.MT_RET_OK)
            {
                model.Message = "OK";
            }           

            return model;
        }
       public void DealDelete() { }
       public ReturnModel DealRequestByGroup(string mask, long from, long to) {
            ReturnModel model = new ReturnModel();
            using (var deals = managerAPI.DealCreateArray())
            {
                var res = managerAPI.DealRequestByGroup(mask,from,to, deals);
                List<DealModel> dealsList = new List<DealModel>();
                model.MTRetCode = res;
                if (res == MTRetCode.MT_RET_OK)
                {
                    foreach (var deal in deals.ToArray()){
                        dealsList.Add(new DealModel(deal));
                    }
                    model.Data = dealsList;
                    model.TotalCount = dealsList.Count;
                }
            }
            return model;
        }
       public ReturnModel DealRequestByGroupSymbol(string mask,string symbol, long from, long to) {
            ReturnModel model = new ReturnModel();
            List<DealModel> dealsList = new List<DealModel>();
            using (var deals = managerAPI.DealCreateArray())
            {
                var res = managerAPI.DealRequestByGroupSymbol(mask,symbol,from,to, deals);
                model.MTRetCode = res;
                if (res == MTRetCode.MT_RET_OK)
                {
                    foreach (var deal in deals.ToArray()) {
                        dealsList.Add(new DealModel(deal));
                    }
                    model.TotalCount = dealsList.Count;
                    model.Data = dealsList;
                }
            }
            return model;
        }
       public ReturnModel DealRequestByLogins(ulong[] logins, long from , long to) {
            ReturnModel model = new ReturnModel();
            var dealsList = new List<DealModel>();  
            using(var deals = managerAPI.DealCreateArray())
            {
                var res = managerAPI.DealRequestByLogins(logins, from,to,deals);
                model.MTRetCode= res;
                if(res == MTRetCode.MT_RET_OK)
                {
                    foreach(var item in deals.ToArray())
                    {
                        dealsList.Add(new DealModel(item));
                    }
                    model.Data= dealsList;
                    model.TotalCount= dealsList.Count;
                }

            }

            return model;
        }
       public ReturnModel DealRequestByTickets(ulong[] tickets) { 
            var model = new ReturnModel();  
            var dealsList = new List<DealModel>();
            using(var deals =managerAPI.DealCreateArray())
            {
                var res = managerAPI.DealRequestByTickets(tickets, deals);
                model.MTRetCode= res;
                if(res == MTRetCode.MT_RET_OK)
                {
                    foreach(var item in deals.ToArray())
                    {
                        dealsList.Add (new DealModel(item));
                    }
                    model.Data= dealsList;
                    model.TotalCount = dealsList.Count;
                }

            }

            return model;
        }
       public ReturnModel DealRequestPage(ulong login, long from, long to, uint current_page, uint total_pages) {
            var model = new ReturnModel();
            var dealsList =new List<DealModel>();   
            using(var deals = managerAPI.DealCreateArray())
            {
                var res = managerAPI.DealRequestPage(login, from, to, current_page, total_pages, deals);
                model.MTRetCode = res;
                foreach(var item in deals.ToArray())
                {
                    dealsList.Add(new DealModel(item));
                }
                model.Data= dealsList;
                model.TotalCount= dealsList.Count;
            }
            return model;
        }
       public void DealAddBatch() { }
       public void DealUpdateBatch() { }
       public void DealUpdateBatchArray() { }
       public ReturnModel DealDeleteBatch(ulong[] tickets) { 
            var model = new ReturnModel();
            var responseList = new List<DealResponseModel>();
            MTRetCode[] retCodes = new MTRetCode[tickets.Length];
            var resp = managerAPI.DealDeleteBatch(tickets, retCodes);
            for(int i = 0; i < retCodes.Length;i++){
                responseList.Add(new DealResponseModel(tickets[i], retCodes[i]));
            }
            model.Data= responseList;
            model.TotalCount= retCodes.Length;


            return model;
        
        }
       public void DealPerform() { }
       public  ReturnModel DealCommissionUpdate(ulong ticket, double commission)
        {
            ReturnModel returnModel = new ReturnModel();
            using (CIMTDeal deal = managerAPI.DealCreate())
            {
                returnModel.MTRetCode = managerAPI.DealRequest(ticket, deal);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    deal.Commission(commission);
                    returnModel.MTRetCode = managerAPI.DealUpdate(deal);
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
       public  ReturnModel DealerBalance(UInt64 login, double amount, uint type, string comment, bool deposit)
       {
            ReturnModel returnModel = new ReturnModel();
            ulong deal_id;
            returnModel.MTRetCode = managerAPI.DealerBalanceRaw(login, deposit ? amount : -amount, type, comment, out deal_id);

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
       public ReturnModel DealerSend(CDealer m_dealer,ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP)
        {
            ReturnModel returnModel = new ReturnModel();

            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
            CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
            var m_request = managerAPI.RequestCreate();
            var m_position = managerAPI.PositionCreate();
            var m_deal = managerAPI.DealCreate();
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
                returnModel.MTRetCode = managerAPI.DealerSend(m_request, m_dealer, out id);
                Thread.Sleep(3000);
                //m_dealer.OnDealerAnswer(m_request);
                //m_request2 = Globals.CIMTRequest;
                if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                {
                    returnModel.MTRetCode = managerAPI.DealRequest(m_request.ResultDeal(), m_deal);
                    //returnModel.MTRetCode = m_manager.PositionGet(login, m_request.Symbol(), m_position);
                    returnModel.MTRetCode = managerAPI.PositionGetByTicket(m_deal.PositionID(), m_position);

                    if (m_deal.Deal() == m_request.ResultDeal() && returnModel.MTRetCode == MTRetCode.MT_RET_OK && m_deal.PositionID() == m_position.Position())
                    {
                        returnModel.MTRetCode = m_position.PriceOpen(orderPrice);
                        returnModel.MTRetCode = m_position.PriceCurrent(orderPrice);
                        returnModel.MTRetCode = managerAPI.PositionUpdate(m_position);
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
       public ReturnModel DealerSendOrder(CDealer m_dealer, ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP)
        {
            ReturnModel returnModel = new ReturnModel();
            var m_position = managerAPI.PositionCreate();
            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
                CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
                var m_request = managerAPI.RequestCreate();
                var m_order = managerAPI.OrderCreate();
                var m_deal = managerAPI.DealCreate();

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
                    returnModel.MTRetCode = managerAPI.DealerSend(m_request, m_dealer, out id);
                    Console.WriteLine(managerAPI.DealerSend(m_request, m_dealer, out id));
                    Thread.Sleep(3000);
                    Console.WriteLine(m_request);
                    Console.WriteLine(id);
                    Console.WriteLine(m_request.ResultRetcode());
                    m_dealer.OnDealerAnswer(m_request);
                    //m_request2 = Globals.CIMTRequest;
                    if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                    {
                        returnModel.MTRetCode = managerAPI.DealRequest(m_request.ResultDeal(), m_deal);
                        //returnModel.MTRetCode = managerAPI.PositionGet(login, m_request.Symbol(), m_position);
                        returnModel.MTRetCode = managerAPI.PositionGetByTicket(m_deal.PositionID(), m_position);

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
       public ReturnModel DealerSendCloseAndModifyPosition(CDealer m_dealer, ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP, ulong positionId)
        {
            ReturnModel returnModel = new ReturnModel();

            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
            CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
            var m_request = managerAPI.RequestCreate();
            var m_position = managerAPI.PositionCreate();
            var m_deal = managerAPI.DealCreate();

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
                returnModel.MTRetCode = managerAPI.DealerSend(m_request, m_dealer, out id);
                Thread.Sleep(3000);
                //m_dealer.OnDealerAnswer(m_request);
                //m_request2 = Globals.CIMTRequest;
                if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                {
                    returnModel.MTRetCode = managerAPI.DealRequest(m_request.ResultDeal(), m_deal);
                    //returnModel.MTRetCode = m_manager.PositionGet(login, m_request.Symbol(), m_position);
                    returnModel.MTRetCode = managerAPI.PositionGetByTicket(m_deal.PositionID(), m_position);

                    if (m_deal.Deal() == m_request.ResultDeal() && returnModel.MTRetCode == MTRetCode.MT_RET_OK && m_deal.PositionID() == m_position.Position())
                    {
                        returnModel.MTRetCode = m_position.PriceOpen(orderPrice);
                        returnModel.MTRetCode = m_position.PriceCurrent(orderPrice);
                        returnModel.MTRetCode = managerAPI.PositionUpdate(m_position);
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
        public  ReturnModel DealerSendOrderCloseAndModify(CDealer m_dealer, ulong login, double orderPrice, ulong volume, int enTradeAction, int enOrderType, string symbol, double priceSL, double priceTP, ulong orderId)
        {
            ReturnModel returnModel = new ReturnModel();

            CIMTRequest.EnTradeActions enTradeActions = new CIMTRequest.EnTradeActions();
            CIMTOrder.EnOrderType enOrderType1 = new CIMTOrder.EnOrderType();
            var m_request = managerAPI.RequestCreate();
            var m_order = managerAPI.OrderCreate();
            var m_deal = managerAPI.DealCreate();
            var m_position = managerAPI.PositionCreate();

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
                returnModel.MTRetCode = managerAPI.DealerSend(m_request, m_dealer, out id);
                Console.WriteLine(managerAPI.DealerSend(m_request, m_dealer, out id));
                Thread.Sleep(3000);
                Console.WriteLine(m_request);
                Console.WriteLine(id);
                Console.WriteLine(m_request.ResultRetcode());
                m_dealer.OnDealerAnswer(m_request);
                //m_request2 = Globals.CIMTRequest;
                if (m_request.ResultRetcode() == MTRetCode.MT_RET_REQUEST_DONE)
                {
                    returnModel.MTRetCode = managerAPI.DealRequest(m_request.ResultDeal(), m_deal);
                    //returnModel.MTRetCode = m_manager.PositionGet(login, m_request.Symbol(), m_position);
                    returnModel.MTRetCode = managerAPI.PositionGetByTicket(m_deal.PositionID(), m_position);

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
        public MTRetCode GetDealerRequest(CIMTRequest request)
        {
            MTRetCode mTRet = new MTRetCode();
            mTRet = managerAPI.DealerStart();
            if (mTRet == MTRetCode.MT_RET_OK)
            {
                // mTRet = managerAPI.DealerGet(m_request);
                mTRet = managerAPI.DealerGet(request);
            }
            return mTRet;
        }
       

    }
}

