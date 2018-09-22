using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Component.Attribute.Attributes
{
    /// <summary>
    /// 特性基类
    /// </summary>
    public abstract class BaseAttribute : System.Attribute
    {
        /// <summary>
        /// 提示文本
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
