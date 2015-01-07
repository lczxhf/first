using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Common
{
    /// <summary>
    /// 数据验证类
    /// </summary>
    public static class ValidateHelper
    {
        static Regex regIsNum = new Regex("^[0-9]+$");
        /// <summary>
        /// 验证 参数是否为 整型数值
        /// </summary>
        /// <param name="strNum"></param>
        /// <returns></returns>
        public static bool IsNum(string strNum)
        {
            return regIsNum.IsMatch(strNum);
        }
    }
}
