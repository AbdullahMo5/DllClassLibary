//+------------------------------------------------------------------+
//|                                                         Dark bug |
//|                                                  2020, Art Corp. |
//|                                           http://www.artem.press |
//+------------------------------------------------------------------+
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//+------------------------------------------------------------------+
//|                                                                  |
//+------------------------------------------------------------------+
namespace Goorge
{
	//+--------------------------------------------------------------+
	//|                                                              |
	//+--------------------------------------------------------------+
    public class Date
    {
        private static readonly DateTime m_ZeroDate = new DateTime(1970,1,1,0,0,0,0);
        //+----------------------------------------------------------+
		//|  Convert from DateTime to Unix                           |
		//+----------------------------------------------------------+
        public static long ConvertToUnixTimestamp(DateTime date)
        {
          DateTime origin = new DateTime(1970,1,1,0,0,0,0);
          TimeSpan diff = date - origin;
          return (long)Math.Floor(diff.TotalSeconds);
        }
        //+----------------------------------------------------------+
		//|  From unix to DateTime format                            |
		//+----------------------------------------------------------+
        public static DateTime ConvertFromUnixTime(ulong seconds)
        {
          DateTime origin = new DateTime(1970,1,1,0,0,0,0);
          return origin.AddSeconds(seconds);
        }
        //+----------------------------------------------------------+
		//|  From unix to DateTime format                            |
		//+----------------------------------------------------------+
        public static DateTime ConvertFromUnixTime(long seconds)
        {
          DateTime origin = new DateTime(1970,1,1,0,0,0,0);
          return origin.AddSeconds((ulong)seconds);
        }
        //+----------------------------------------------------------+
		//|  Get zero date 1/1/1970 0:0:0                            |
		//+----------------------------------------------------------+
        public static DateTime ZeroDate
        { 
           get { return m_ZeroDate; }
        }
    }
}
//+------------------------------------------------------------------+