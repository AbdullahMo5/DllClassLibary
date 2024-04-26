using MetaQuotes.MT5CommonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Model
{
    public class ReturnModel
    {
        public MTRetCode MTRetCode { get; set; }
        //public UserModel UserModel { get; set; }
        public object Data { get; set; }
        public int TotalCount { get; set; }
        public string Message { get; set; }
    }
}
