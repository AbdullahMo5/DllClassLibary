using Goorge.Model;
using Goorge.Models;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System.Collections.Generic;

namespace Goorge.Services
{
    public class SymbolService : IService
    {
        CIMTManagerAPI managerAPI = null;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            managerAPI = m_manager;
        }

        public ReturnModel GetAllSymbols()
        {
            var returnModel = new ReturnModel();
            using (var symbol = managerAPI.SymbolCreate())
            {
                var symbolsArray = new List<SymbolModel>();
                var total = managerAPI.SymbolTotal();
                for (uint i = 0; i < total; i++)
                {
                    symbol.Clear();
                    var resp = managerAPI.SymbolNext(i, symbol);
                    if (resp == MTRetCode.MT_RET_OK)
                    {
                        symbolsArray.Add(new SymbolModel(symbol));
                    }
                }
                returnModel.Data = symbolsArray;
                returnModel.TotalCount = symbolsArray.Count;

            }

            return returnModel;

        }

    }
}
