using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Web;
using System.Data;
using YK.Core.Enums;

namespace YK.Core.Model
{
    //public enum Join { like,in,or,(,) }
    /// <summary>
    /// 表达式
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Expression()
        { 
        
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="condition">条件</param>
        /// <param name="value">值</param>
        public Expression(string fieldName, ConditionEnum condition,object value)
        {
            FieldName = fieldName;
            Condition = condition;
            Value = value.ToString();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="join">连接符: (、)、and 、 or</param>
        /// <param name="value">值</param>
        public Expression(string join)
        {
            Join = join;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public ConditionEnum Condition { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 连接符: (、)、and 、 or
        /// </summary>
        public string Join { get; set; }
    }
}
