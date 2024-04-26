using Goorge.Model;
using Goorge.Models;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Goorge.Services
{
    class UserService:IService
    {
        private CIMTManagerAPI managerAPI = null;
        public void Initialize(CIMTManagerAPI m_manager)
        {
            if (m_manager != null)
            {
                managerAPI = m_manager;
            }
        }

        public ReturnModel CreateUser(CreateUserModel userModel)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                using (CIMTUser user = managerAPI.UserCreate())
                {
                    user.Login(userModel.login);
                    user.EMail(userModel.email);
                    user.Group(userModel.group);
                    user.Leverage(userModel.leverage);
                    user.EMail(userModel.email);
                    user.Name(userModel.name);
                    user.Phone(userModel.phone);
                    user.City(userModel.city);
                    user.Comment(userModel.comment);
                    user.Country(userModel.country);
                    user.State(userModel.state);
                    user.Rights(CIMTUser.EnUsersRights.USER_RIGHT_ENABLED);
                    returnModel.MTRetCode = managerAPI.UserAdd(user, userModel.password, userModel.password);
                    if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                    {
                        returnModel.Data = userModel;
                    }
                    else
                    {
                        returnModel.Message = returnModel.MTRetCode.ToString();
                    }
                }
                return returnModel;
;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);   
                returnModel.Message = exception.InnerException.Message;
            }
            return returnModel;
        }
        public ReturnModel UpdateUser(CreateUserModel user)
        {
            ReturnModel returnModel = new ReturnModel();
            try{
                using (CIMTUser c_user = managerAPI.UserCreate())
                {
                    returnModel.MTRetCode = managerAPI.UserGet(user.login, c_user);
                    if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                    {
                        c_user.Login(user.login);
                        c_user.EMail(user.email);
                        c_user.Group(user.group);
                        c_user.Leverage(user.leverage);
                        c_user.EMail(user.email);
                        c_user.Name(user.name);
                        c_user.Phone(user.phone);
                        c_user.City(user.city);
                        c_user.Comment(user.comment);
                        c_user.Country(user.country);
                        c_user.State(user.state);
                        returnModel.MTRetCode = managerAPI.UserUpdate(c_user);
                        if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                        {
                            returnModel.Data = user;
                        }
                        else
                        {
                            returnModel.Message = returnModel.MTRetCode.ToString();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                returnModel.Message = exception.InnerException.Message;
                //returnModel.Error = exception.InnerException.Message;

            }
            return returnModel;
        }
        public  MTRetCode DeleteUser(ulong login)
        {
            MTRetCode res = MTRetCode.MT_RET_OK_NONE;
            using (CIMTUser user = managerAPI.UserCreate())
            {
                res = managerAPI.UserDelete(login);
            }
            return res;
        }
        public ReturnModel GetUserRights(ulong login)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                using (var user = managerAPI.UserCreate())
                {
                    var request = managerAPI.UserGet(login, user);
                    returnModel.MTRetCode = request;
                    if (request == MTRetCode.MT_RET_OK)
                    {
                        returnModel.Data = Enum.GetName(typeof(CIMTUser.EnUsersRights) ,user.Rights());
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnModel.Message = ex.InnerException.Message;
            }

            return returnModel;
        }
        public ReturnModel ChangePassword(uint type, ulong login, string password)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                MTRetCode res = managerAPI.UserPasswordChange((CIMTUser.EnUsersPasswords)type, login, password);
                returnModel.MTRetCode = res;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                returnModel.Message = exception.InnerException.Message;
                //returnModel.Error = exception.InnerException.Message;
            }
            return returnModel;
        }
        public ReturnModel UserPasswordCheck(int type, ulong login, string password)
        {
            ReturnModel returnModel = new ReturnModel();
            CIMTUser.EnUsersPasswords passwordType = new CIMTUser.EnUsersPasswords();
            switch (type)
            {
                case 0:
                    passwordType = CIMTUser.EnUsersPasswords.USER_PASS_MAIN;
                    break;
                case 1:
                    passwordType = CIMTUser.EnUsersPasswords.USER_PASS_INVESTOR;
                    break;
                case 2:
                    passwordType = CIMTUser.EnUsersPasswords.USER_PASS_API;
                    break;
                default:
                    passwordType = CIMTUser.EnUsersPasswords.USER_PASS_MAIN;
                    break;
            }
            var resp = managerAPI.UserPasswordCheck(passwordType, login, password);

            return returnModel;
        }
        public ReturnModel GetUser(ulong login)
        {
            ReturnModel returnModel = new ReturnModel();
            try {
                using (var user = managerAPI.UserCreate())
                {
                    var request = managerAPI.UserGet(login, user);
                    returnModel.MTRetCode = request;                
                    if(request == MTRetCode.MT_RET_OK)
                    {
                        returnModel.Data = new UserModel(user);
                    }
                }
            
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                returnModel.Message = ex.InnerException.Message;    
            }

            return returnModel;
        }
        public ReturnModel GetUserBalance(ulong login, bool fixflag) { 
            ReturnModel returnModel = new ReturnModel();
            double balance_user = 0;
            double balance_history = 0;
            double credit_user = 0;
            double credit_history = 0;
            object model = null;
            //var data = new List<double>();
            using (CIMTUser user = managerAPI.UserCreate())
            {
                returnModel.MTRetCode = managerAPI.UserBalanceCheck(login, fixflag, out balance_user, out balance_history, out credit_user, out credit_history);
            }
            if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
            {
                //data.Add(balance_user);
                //data.Add(balance_history);
                //data.Add(credit_user);
                //data.Add(credit_history);
                model = new
                {
                    balance_user = balance_user,
                    balance_history = balance_history,
                    credit_user = credit_user,
                    credit_history = credit_history,
                };
            }
            returnModel.Data = model;
            return returnModel;

        }
        public ReturnModel GetUserBalance(ulong[] logins)
        {
            ReturnModel returnModel = new ReturnModel();
            Dictionary<ulong, object> models = new Dictionary<ulong, object>();
            for (int i = 0; i < logins.Length; i++)
            {
                double balance_user = 0;
                double balance_history = 0;
                double credit_user = 0;
                double credit_history = 0;
                object model = null;

                using (CIMTUser user = managerAPI.UserCreate())
                {
                    returnModel.MTRetCode = managerAPI.UserBalanceCheck(logins[i], false, out balance_user, out balance_history, out credit_user, out credit_history);
                }
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    model = new
                    {
                        balance_user,
                        balance_history,
                        credit_user,
                        credit_history,
                    };
                    models.Add(logins[i], model);
                }
            }

            returnModel.Data = models;
            return returnModel;
        }
        public ReturnModel GetUserByLogins(ulong[] logins) 
        {
            ReturnModel returnModel = new ReturnModel();
            try { 
                List<UserModel> userList = new List<UserModel>();
                using (CIMTUserArray users = managerAPI.UserCreateArray())
                {
                    returnModel.MTRetCode = managerAPI.UserRequestByLogins(logins, users);
                    if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                    {
                        foreach (var item in users.ToArray())
                        {
                            userList.Add(new UserModel(item));
                        }
                        returnModel.TotalCount = Convert.ToInt32(users.Total());
                    }
                    returnModel.Data = userList;
                }                                
            }catch(Exception ex){
                Console.WriteLine(ex.ToString()); 
                returnModel.Message = ex.InnerException.Message;
            }
            return returnModel; 
        }
        public ReturnModel GetUserTotal()
        {
            ReturnModel returnModel = new ReturnModel();
            var resp = managerAPI.UserTotal();
            returnModel.Data= resp;

            return returnModel;
        }
        public ReturnModel GetUserLoginsByGroup(string group) { 
        
            ReturnModel returnModel = new ReturnModel();
            MTRetCode statusCode = new MTRetCode();
            var logins = managerAPI.UserLogins(group, out statusCode);
            returnModel.MTRetCode= statusCode;
            if(statusCode == MTRetCode.MT_RET_OK)
            {
                returnModel.Data = logins;
            }
            
            return returnModel;
        }
        public ReturnModel GetUsersByGroup(string group) {

            ReturnModel returnModel = new ReturnModel();
            MTRetCode res = new MTRetCode();
            using (CIMTUserArray users = managerAPI.UserCreateArray())
            {
                returnModel.MTRetCode = managerAPI.UserRequestArray(group, users);
                List<UserModel> userList = new List<UserModel>();
                if (returnModel.MTRetCode == MTRetCode.MT_RET_OK)
                {
                    foreach (var item in users.ToArray())
                    {
                        userList.Add(new UserModel(item));
                    }
                    returnModel.TotalCount = userList.Count();
                    returnModel.Data = userList;
                }
            }
            return returnModel;
        }
        public MTRetCode UserUpdateLeverage(ulong login, uint leverage)
        {
            MTRetCode res = new MTRetCode();
            res = MTRetCode.MT_RET_OK_NONE;
            using (CIMTUser user = managerAPI.UserCreate())
            {
                res = managerAPI.UserGet(login, user);
                res = user.Leverage(leverage);
                res = managerAPI.UserUpdate(user);
            }
            return res;
        }
    }
}
