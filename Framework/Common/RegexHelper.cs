using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Common
{
    public static class RegexHelper
    {
        /// <summary>
        /// 正则判断是否为整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d+$");
        }

        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        public static bool IsUnsign(this string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }
    }
}
