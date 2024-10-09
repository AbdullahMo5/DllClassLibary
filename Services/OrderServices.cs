using Goorge.Model;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TradeRateSell;

namespace Goorge.Services
{
    public class OrderServices : IService
    {
        private CIMTManagerAPI managerAPI = null;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            if (m_manager != null)
            {
                managerAPI = m_manager;
            }
        }
        public ReturnModel HistoryRequest(ulong login, long from, long to)
        {
            ReturnModel returnModel = new ReturnModel();
            using (CIMTOrderArray Orders = managerAPI.OrderCreateArray())
            {
                List<OrderModel> orderList = new List<OrderModel>();
                returnModel.MTRetCode = managerAPI.HistoryRequest(login, from, to, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in Orders.ToArray())
                    {
                        orderList.Add(new OrderModel(item));
                    }
                }
                returnModel.Data = orderList;
                returnModel.TotalCount = orderList.Count();
            }

            return returnModel;
        }
        public ReturnModel HistoryRequestByLogin(ulong[] logins, long from, long to)
        {
            ReturnModel returnModel = new ReturnModel();
            List<OrderModel> orderList = new List<OrderModel>();
            using (CIMTOrderArray Orders = managerAPI.OrderCreateArray())
            {
                returnModel.MTRetCode = managerAPI.HistoryRequestByLogins(logins, from, to, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK){
                    foreach (var item in Orders.ToArray()){
                        orderList.Add(new OrderModel(item));
                    }
                }
                returnModel.Data = orderList;
                returnModel.TotalCount = orderList.Count();
            }

            return returnModel;
        }
        public ReturnModel PendingHistoryRequestByLogin(ulong[] logins, long from, long to)
        {
            ReturnModel returnModel = new ReturnModel();
            List<OrderModel> orderList = new List<OrderModel>();
            using (CIMTOrderArray Orders = managerAPI.OrderCreateArray())
            {
                returnModel.MTRetCode = managerAPI.HistoryRequestByLogins(logins, from, to, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in Orders.ToArray())
                    {
                        if(item.ActivationMode() == 1)
                            orderList.Add(new OrderModel(item));
                    }
                }
                returnModel.Data = orderList;
                returnModel.TotalCount = orderList.Count();
            }

            return returnModel;
        }
        public ReturnModel OrderRequestByLogin(ulong[] logins)
        {
            ReturnModel returnModel = new ReturnModel();
            using (CIMTOrderArray Orders = managerAPI.OrderCreateArray())
            {
                returnModel.MTRetCode = managerAPI.OrderGetByLogins(logins, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    List<OrderModel>ordersList = new List<OrderModel>();
                    foreach (var item in Orders.ToArray())
                    {
                        ordersList.Add(new OrderModel(item));
                    }
                    returnModel.Data = ordersList;
                    returnModel.TotalCount=ordersList.Count();
                }
            }

            return returnModel;
        }
        public ReturnModel OrderRequestByTickets(ulong[] tickets)
        {
            ReturnModel returnModel = new ReturnModel();
            using (CIMTOrderArray Orders = managerAPI.OrderCreateArray())
            {
                var ordersList = new List<OrderModel>();
                returnModel.MTRetCode = managerAPI.OrderRequestByLogins(tickets, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in Orders.ToArray())
                    {
                        ordersList.Add(new OrderModel(item));
                    }
                    returnModel.Data = ordersList;
                }
            }

            return returnModel;
        }
        public ReturnModel OrderRequestByTicket(ulong ticket)
        {
            ReturnModel returnModel = new ReturnModel();
            using (CIMTOrder Order = managerAPI.OrderCreate())
            {
                returnModel.MTRetCode = managerAPI.OrderRequest(ticket, Order);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    returnModel.Data = new OrderModel(Order);
                }
            }

            return returnModel;
        }
        public ReturnModel OrderRequest(string groupMask, ulong ticket)
        {
            ReturnModel returnModel = new ReturnModel();
            List<OrderModel> ordersList = new List<OrderModel>();
            using (var Orders = managerAPI.OrderCreateArray())
            {
                returnModel.MTRetCode = managerAPI.OrderRequestByGroup(groupMask, Orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach(var item in Orders.ToArray())
                    {
                        ordersList.Add(new OrderModel(item));
                    }
                    returnModel.Data = ordersList;
                }
            }

            return returnModel;
        }
        public ReturnModel DeleteOrder(ulong ticket)
        {
            ReturnModel returnModel = new ReturnModel();

            returnModel.MTRetCode = managerAPI.OrderDelete(ticket);

            return returnModel;
        }
        public ReturnModel GetAllOpenOrders(ulong[] login)
        {
            ReturnModel returnModel = new ReturnModel();
            using (CIMTOrderArray orders = managerAPI.OrderCreateArray())
            {
                List<OrderModel> orderList = new List<OrderModel>();
                returnModel.MTRetCode = managerAPI.OrderGetByLogins(login, orders);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {

                    foreach (var order in orders.ToArray())
                    {
                        orderList.Add(new OrderModel(order));
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
    }
}
