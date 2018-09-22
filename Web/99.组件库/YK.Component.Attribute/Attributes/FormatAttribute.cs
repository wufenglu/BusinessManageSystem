using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Component.Attribute.Attributes
{
    /// <summary>
    /// 类型验证属性
    /// </summary>
    public sealed class FormatAttribute : BaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rormatEnum"></param>
        public FormatAttribute(FormatEnum rormatEnum)
        {
            FormatEnum = rormatEnum;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regex">正则表达式</param>
        public FormatAttribute(string regex)
        {
            Regex = regex;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        public FormatEnum FormatEnum { get; set; }
    }

    /// <summary>
    /// 验证类型枚举
    /// </summary>
    public enum FormatEnum
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        Email,
        /// <summary>
        /// 电话
        /// </summary>
        Phone,
        /// <summary>
        /// 移动电话
        /// </summary>
        MobilePhone,
        /// <summary>
        /// 身份证号
        /// </summary>
        ID
    }
}
