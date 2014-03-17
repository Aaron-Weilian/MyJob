using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
    public class FormatUtil
    {
        public static string StrNull(object str) {

            return str == null ? "" : str.ToString();
        }

        public static object ObjNull(object str)
        {

            return str == null ? "" : str;
        }

        public static int String2Int(string str)
        {
            try
            {
                return StrNull(str) == "" ? 0 : int.Parse(str);
            }
            catch (Exception e)
            {
                Log.LogHelper.Error("System", "Convert", "System", "System", "String2Int fail", e);
                return 0;

            }
        }

        public static long String2Long(string str)
        {
            try
            {
                return StrNull(str) == "" ? 0 : long.Parse(str);
            }
            catch (Exception e)
            {
                Log.LogHelper.Error("System", "Convert", "System", "System", "String2Int fail", e);
                return 0;

            }
        }

        public static double String2Double(string str)
        {
            try
            {
                return StrNull(str) == "" ? 0 : double.Parse(str);
            }
            catch (Exception e)
            {
                Log.LogHelper.Error("System", "Convert", "System", "System", "String2Int fail", e);
                return 0;

            }
        }

        public static string Datetime2Str(DateTime str)
        { 
        
         return str.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string Date2Str(DateTime str)
        {

            return str.ToString("yyyy-MM-dd");
        }

    }
}
