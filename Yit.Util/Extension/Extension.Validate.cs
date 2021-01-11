using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yit.Util.Extension
{
    public static partial class Extensions
    {
        public static bool IsEmpty(this object value)
        {
            if (value != null && !string.IsNullOrEmpty(value.ParseToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNullOrZero(this object value)
        {
            if (value == null || value.ParseToString().Trim() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
