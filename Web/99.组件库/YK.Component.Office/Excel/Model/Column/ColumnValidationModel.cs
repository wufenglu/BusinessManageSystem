using YK.Component.Attribute.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Component.Office.Excel.Model.Column
{
    /// <summary>
    /// 列验证信息
    /// </summary>
    public class ColumnValidationModel
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        public ValidationTypeEnum ValidationTypeEnum { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
