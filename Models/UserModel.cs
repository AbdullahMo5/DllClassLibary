using MetaQuotes.MT5CommonAPI;

namespace Goorge.Model
{
    public class UserModel
    {
        public string Account { get; set; }
        public string Address { get; set; }
        public double Balance { get; set; }
        public string City { get; set; }
        public ulong ClientId { get; set; }
        public uint Color { get; set; }
        public string Comment { get; set; }
        public double CommissionAgentDaily { get; set; }
        public double CommissionAgentMonthly { get; set; }
        public double CommissionDaily { get; set; }
        public double CommissionMonthly { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public double Credit { get; set; }
        public string FirstName { get; set; }
        public string Group { get; set; }
        public string Email { get; set; }
        public string ID { get; set; }
        public long LastAccess { get; set; }
        public long Registration { get; set; }
        public string LastIP { get; set; }
        public string LastName { get; set; }
        public long LastPassChange { get; set; }
        public uint Leverage { get; set; }
        public ulong Login { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }

        public UserModel(string account, string address, double balance, string city, ulong clientId, uint color, string comment, double commissionAgentDaily, double commissionAgentMonthly, double commissionDaily, double commissionMonthly, string company, string country, double credit, string firstName, string group, string eMail, string iD, long lastAccess, string lastIP, string lastName, long lastPassChange, uint leverage, string name, string phone)
        {
            Account = account;
            Address = address;
            Balance = balance;
            City = city;
            ClientId = clientId;
            Color = color;
            Comment = comment;
            CommissionAgentDaily = commissionAgentDaily;
            CommissionAgentMonthly = commissionAgentMonthly;
            CommissionDaily = commissionDaily;
            CommissionMonthly = commissionMonthly;
            Company = company;
            Country = country;
            Credit = credit;
            FirstName = firstName;
            Group = group;
            Email = eMail;
            ID = iD;
            LastAccess = lastAccess;
            LastIP = lastIP;
            LastName = lastName;
            LastPassChange = lastPassChange;
            Leverage = leverage;
            Name = name;
            Phone = phone;
        }
        public UserModel(CIMTUser user)
        {
            Account = user.Account();
            Address = user.Address();
            Balance = user.Balance();
            City = user.City();
            ClientId = user.ClientID();
            Color = user.Color();
            Comment = user.Comment();
            CommissionAgentDaily = user.CommissionAgentDaily();
            CommissionAgentMonthly = user.CommissionAgentMonthly();
            CommissionDaily = user.CommissionDaily();
            CommissionMonthly = user.CommissionMonthly();
            Company = user.Company();
            Country = user.Country();
            Credit = user.Credit();
            FirstName = user.FirstName();
            Group = user.Group();
            Email = user.EMail();
            ID = user.ID();
            LastAccess = user.LastAccess();
            LastIP = user.LastIP();
            LastName = user.LastName();
            LastPassChange = user.LastPassChange();
            Login = user.Login();
            Leverage = user.Leverage();
            Name = user.Name();
            Phone = user.Phone();
            State = user.State();
            Registration = user.Registration();
        }
        public UserModel()
        {

        }



    }
}
