using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Services
{
    public interface IService
    {
       void Initialize(CIMTManagerAPI m_manager);
    }
}
