using Goorge.Model;
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Services
{
    public class Services
    {
        private CIMTManagerAPI _cIMTManager;
        public Services(CIMTManagerAPI cIMTManager)
        {
            _cIMTManager = cIMTManager;
        }
        public ReturnModel CreateUser(string account, CIMTUser user)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
                MTRetCode res = _cIMTManager.UserExternalRequest(account, user);
                returnModel.MTRetCode = res;
            }
            catch (Exception exception)
            {
                //returnModel.Error = exception.InnerException.Message;
            }
            return returnModel;
        }
        public ReturnModel UpdateUser(CIMTUser user)
        {
            ReturnModel returnModel = new ReturnModel();

            try
            {
                MTRetCode res = _cIMTManager.UserUpdate(user);
                returnModel.MTRetCode = res;
            }
            catch (Exception exception)
            {
                //returnModel.Error = exception.InnerException.Message;

            }
            return returnModel;
        }
        public ReturnModel ChangePassword(uint type , string password)
        {
            ReturnModel returnModel = new ReturnModel();
            try
            {
            MTRetCode res = _cIMTManager.PasswordChange(type, password);
                returnModel.MTRetCode = res;
            }
            catch (Exception exception)
            {
                //returnModel.Error = exception.InnerException.Message;
            }
            return returnModel;
        }
        //public ReturnModel ChangeLeverage(uint type, string password)
        //{
        //    ReturnModel returnModel = new ReturnModel();
        //    try
        //    {
        //        MTRetCode res = CIMTUser.leve(type, password);
        //        returnModel.MTRetCode = res;
        //    }
        //    catch (Exception exception)
        //    {
        //        returnModel.Error = exception.InnerException.Message;
        //    }
        //    return returnModel;
        //}
    }
}
