using Goorge.Model;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Services
{
    public class PositionService : IService
    {
        private CIMTManagerAPI managerAPI = null;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            if(m_manager != null)
            {
                managerAPI = m_manager;
            }
        }
        public ReturnModel PositionRequest(ulong login)
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();
            using (CIMTPositionArray positions = managerAPI.PositionCreateArray())
            {
                returnModel.MTRetCode = managerAPI.PositionRequest(login, positions);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in positions.ToArray())
                    {
                        positionList.Add(new PositionModel(item));
                    }
                    returnModel.Data = positionList;
                }
                returnModel.TotalCount = Convert.ToInt32(positions.Total());
            }

            return returnModel;
        }
        public ReturnModel PositionRequest(ulong login, int pageNumber, int pageSize)
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();
            List<PositionModel> paginatedPositions = new List<PositionModel>();
            using (CIMTPositionArray positions = managerAPI.PositionCreateArray())
            {
                returnModel.MTRetCode = managerAPI.PositionRequest(login, positions);
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in positions.ToArray())
                    {
                        positionList.Add(new PositionModel(item));
                    }
                    int skip = (pageNumber - 1) * pageSize;
                    paginatedPositions = positionList.Skip(skip).Take(pageSize).ToList();
                    returnModel.Data = paginatedPositions;
                }
                returnModel.TotalCount = paginatedPositions.Count;
            }
            return returnModel;
        }
        public ReturnModel PositionsRequestByGroupAll(string mask)
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();

            var positionsArray = managerAPI.PositionCreateArray();
            var result = managerAPI.PositionGetByGroup(mask, positionsArray);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK)
            {
                foreach (var item in positionsArray.ToArray())
                {
                    positionList.Add(new PositionModel(item));
                }
                returnModel.Data= positionList;
            }
            returnModel.TotalCount = Convert.ToInt32(positionsArray.Total());
            return returnModel;

        }
        public ReturnModel PositionsRequestByGroupAll()
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();

            var positionsArray = managerAPI.PositionCreateArray();
            var result = managerAPI.PositionGetByGroup("*", positionsArray);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK)
            {
                foreach (var item in positionsArray.ToArray())
                {
                    positionList.Add(new PositionModel(item));
                }
                returnModel.Data= positionList;
            }
            returnModel.TotalCount = Convert.ToInt32(positionsArray.Total());
            return returnModel;

        }
        public ReturnModel PositionsRequestBySymbol(string group, string symbol)
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();

            var positionsArray = managerAPI.PositionCreateArray();
            var result = managerAPI.PositionGetBySymbol(group,symbol, positionsArray);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK)
            {
                foreach (var item in positionsArray.ToArray())
                {
                    positionList.Add(new PositionModel(item));
                }
                returnModel.Data= positionList;
            }
            returnModel.TotalCount = Convert.ToInt32(positionsArray.Total());
            return returnModel;

        }
        public ReturnModel PositionsRequestByGroup( string group)
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();

            var positionsArray = managerAPI.PositionCreateArray();
            var result = managerAPI.PositionGetByGroup(group, positionsArray);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK)
            {
                foreach (var item in positionsArray.ToArray())
                {
                    positionList.Add(new PositionModel(item));
                }
                returnModel.Data= positionList;
            }
            returnModel.TotalCount = Convert.ToInt32(positionsArray.Total());
            return returnModel;

        }
        public ReturnModel PositionsRequestByLogins( ulong[] logins)
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();

            var positionsArray = managerAPI.PositionCreateArray();
            var result = managerAPI.PositionGetByLogins(logins, positionsArray);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK)
            {
                foreach (var item in positionsArray.ToArray())
                {
                    positionList.Add(new PositionModel(item));
                }
                returnModel.Data= positionList;
            }
            returnModel.TotalCount = Convert.ToInt32(positionsArray.Total());
            return returnModel;

        }
        public ReturnModel PositionsRequestByLogin( ulong login)
        {
            ReturnModel returnModel = new ReturnModel();
            List<PositionModel> positionList = new List<PositionModel>();

            var positionsArray = managerAPI.PositionCreateArray();
            var result = managerAPI.PositionRequest(login, positionsArray);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK)
            {
                foreach (var item in positionsArray.ToArray())
                {
                    positionList.Add(new PositionModel(item));
                }
                returnModel.Data= positionList;
            }
            returnModel.TotalCount = Convert.ToInt32(positionsArray.Total());
            return returnModel;

        }
        public ReturnModel PositionRequestByTicket( ulong ticket)
        {
            ReturnModel returnModel = new ReturnModel();
            var position = managerAPI.PositionCreate();
            var result = managerAPI.PositionGetByTicket(ticket, position);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK){
                returnModel.Data= new PositionModel(position);
            }
            return returnModel;

        }
        public ReturnModel PositionRequestByTickets( ulong[] ticket)
        {
            ReturnModel returnModel = new ReturnModel();
            var positionList = new List<PositionModel>();
            var positions = managerAPI.PositionCreateArray();
            var result = managerAPI.PositionGetByTickets(ticket, positions);
            returnModel.MTRetCode = result;
            if (result == MTRetCode.MT_RET_OK){
                foreach(var item in positions.ToArray()){
                    positionList.Add(new PositionModel(item));
                }
                returnModel.TotalCount= Convert.ToInt32(positions.Total());
                returnModel.Data= positionList;
            }
            return returnModel;

        }
        public ReturnModel PositionsGetAll(ulong login, long from, long to)
        {
            ReturnModel returnModel = new ReturnModel();
            using (CIMTOrderArray Orders = managerAPI.OrderCreateArray())
            {
                var orderList = new List<ulong>();
                var orderRes = managerAPI.HistoryRequest(login, from, to, Orders);
                if (orderRes == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in Orders.ToArray())
                    {
                        var order = new OrderModel(item);
                        orderList.Add(order.PositionID);
                    }

                    var positionList = new List<PositionModel>();
                    var positions = managerAPI.PositionCreateArray();
                    var result = managerAPI.PositionGetByTickets(orderList.ToArray(), positions);
                    returnModel.MTRetCode = result;
                    if (result == MTRetCode.MT_RET_OK)
                    {
                        foreach (var item in positions.ToArray())
                        {
                            positionList.Add(new PositionModel(item));
                        }
                    }
                    returnModel.TotalCount = Convert.ToInt32(positions.Total());
                    returnModel.Data = positionList;
                }
            }
            return returnModel;
        }
    }
}
