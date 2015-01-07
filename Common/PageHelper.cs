using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace ShoeStore.Common
{
    public class PageHelper
    {
        #region 1.0 读取文件字符串 +string ReadFile(string strPath)
        /// <summary>
        /// 读取文件字符串
        /// </summary>
        /// <param name="strPath">文件路径-物理路径</param>
        /// <returns></returns>
        public static string ReadFile(string strPath)
        {
            return System.IO.File.ReadAllText(strPath);
        } 
        #endregion

        #region 2.0 获取提示和跳转 js 代码字符串 +void WriteJsMsg(string strMsg, string strBackUrl)
        /// <summary>
        /// 获取提示和跳转 js 代码字符串
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strBackUrl"></param>
        /// <returns></returns>
        public static void WriteJsMsg(string strMsg, string strBackUrl)
        {
            string strBack = "<script>alert('" + strMsg + "');window.location='" + strBackUrl + "';</script>";
            HttpContext.Current.Response.Write(strBack);
        } 
        #endregion

        #region 3.0 获取提示字符串 +void WriteJsMsg(string strMsg)
        /// <summary>
        /// 获取提示字符串
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strBackUrl"></param>
        /// <returns></returns>
        public static void WriteJsMsg(string strMsg)
        {
            string strBack = "<script>alert('" + strMsg + "');</script>";
            HttpContext.Current.Response.Write(strBack);
        } 
        #endregion

        #region 4.0 将bool值转成 是 / 否 +string Bool2CnStr(string isDelStr)
        /// <summary>
        /// 将bool值转成 是 / 否
        /// </summary>
        /// <param name="isDelStr"></param>
        /// <returns></returns>
        public static string Bool2CnStr(string isDelStr)
        {
            return isDelStr == "true" ? "是" : "否";
        } 
        #endregion

        #region 5.0 根据 集合 生成 下拉框 选项 + string MakeSelectOpts<T>
        /// <summary>
        /// 根据 集合 生成 下拉框 选项 +
        /// </summary>
        /// <typeparam name="T">集合元素的 类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="valueProertyName">作为选项 的value的属性名</param>
        /// <param name="textPropertyName">作为选项 的text的属性名</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string MakeSelectOpts<T>(IList<T> list, string valueProertyName, string textPropertyName, object defaultValue)
        {
            System.Text.StringBuilder sbOpts = new System.Text.StringBuilder(200);
            //获取集合 内部 元素 的类型
            Type tType = typeof(T);
            //获取 指定 value 的 属性对象
            PropertyInfo tValueProperty = tType.GetProperty(valueProertyName);
            //获取 指定 text 的 属性对象
            PropertyInfo tTextProperty = tType.GetProperty(textPropertyName);

            foreach (T m in list)
            {
                //获取 元素的 指定value属性的 值
                object value = tValueProperty.GetValue(m, null);
                //获取 元素的 指定text属性的 值
                object text = tTextProperty.GetValue(m, null);

                if (value.Equals(defaultValue))//使用Equals方法的原因，是 提供给用户重写比较方法
                    sbOpts.AppendLine("<option selected value=\"" + value + "\">" + text + "</option>");
                else
                    sbOpts.AppendLine("<option value=\"" + value + "\">" + text + "</option>");
            }
            return sbOpts.ToString();
        } 
        #endregion
    }
}