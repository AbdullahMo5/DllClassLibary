//+----------------------------------------------------------------------+
//|                            MetaTrader 5 API Manager for .NET Example |
//|                       Copyright 2001-2019, MetaQuotes Software Corp. |
//|                                            http://www.metaquotes.net |
//+----------------------------------------------------------------------+
using MetaQuotes.MT5CommonAPI;
using MetaQuotes.MT5ManagerAPI;
using System;
//+----------------------------------------------------------------------+
//|                                                                      |
//+----------------------------------------------------------------------+
namespace Goorge
{
    public class CUsers : CIMTUserSink
    {
        private CIMTManagerAPI m_manager;
        CIMTUser _user;
        //+---------------------------------------------------------------+
        //| Init native implementation                                    |
        //+---------------------------------------------------------------+
        public MTRetCode Initialize(CIMTManagerAPI manager)
        {
            //--- checking
            if (manager == null) return (MTRetCode.MT_RET_ERR_PARAMS);
            //---
            Console.WriteLine("CIMTUserSink initialize success");
            //--- 
            m_manager = manager;
            //---
            return (RegisterSink());
        }

        //+---------------------------------------------------------------+
        //|                                                               |
        //+---------------------------------------------------------------+
        public override void OnUserLogin(string ip, CIMTUser user, CIMTUser.EnUsersConnectionTypes type)
        {
            Console.WriteLine("OnUserLogin login = {0}, ip = {1}, type = {2}", user.Login(), ip, type);
        }
        //+---------------------------------------------------------------+
        //|                                                               |
        //+---------------------------------------------------------------+
        public override void OnUserUpdate(CIMTUser user)
        {
            Console.WriteLine("OnUserUpdate login = {0}, leverage = {1}", user.Login(), user.Leverage());
        }
        public MTRetCode ChangeLeverage(ulong login, uint leverage)
        {
            MTRetCode res = new MTRetCode();
            res = MTRetCode.MT_RET_OK_NONE;
            if (m_manager == null)
            {
                CManager manager_class = new CManager();
                manager_class.Initialize(m_manager);
            }
            using (CIMTUser user = m_manager.UserCreate())
            {
                res = user.Leverage(400);
                res = m_manager.UserUpdate(user);
            }








            //CIMTUser user;
            //    CSomeClientClass managerInit = new CSomeClientClass();
            //m_manager = managerInit.Initialize();
            //CIMTUser user = m_manager.UserCreate();
            //res = user.Leverage(400);
            //res = m_manager.UserUpdate(user);
            ////MTRetCode res = m_manager.UserGet(login, user);
            ////if (res == MTRetCode.MT_RET_OK)
            ////{
            ////    UserUpdate = user.Leverage(leverage);
            ////}
            return res;
        }

        //public static CIMTManagerAPI Manager
        //{
        //    get
        //    {
        //        if (IsLoggedIn)
        //            return (CIMTManagerAPI)HttpContext.Current.Session["manager"];
        //        //---
        //        return null;
        //    }
        //    set
        //    {
        //        //--- 
        //        if (HttpContext.Current != null && HttpContext.Current.Session != null)
        //            HttpContext.Current.Session["manager"] = value;
        //    }
        //}
        //public static bool IsLoggedIn
        //{
        //    get
        //    {
        //        return ((HttpContext.Current != null) && HttpContext.Current.Session != null) && (HttpContext.Current.Session["manager"] != null);
        //    }
        //}
    }
}
//+----------------------------------------------------------------------+