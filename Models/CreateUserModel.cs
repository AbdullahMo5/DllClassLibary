using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Model
{
    public class CreateUserModel
    {
        public string comment;
        public int enable;
        public ulong login { get; set; }
        public string group { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string zipcode { get; set; }
        public uint leverage { get; set; }
        public int agent_account { get; set; }
        public bool success { get; set; }
        public bool demo { get; set; }
    }
}
