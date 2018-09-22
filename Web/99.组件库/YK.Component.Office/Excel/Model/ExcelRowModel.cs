using YK.Component.Attribute.Enum;
using YK.Component.Office.Excel.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Component.Office.Excel.Model
{
    /// <summary>
    /// 基类实体
    /// </summary>
    public class ExcelRowModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExcelRowModel() {
            OtherColumns = new List<ColumnModel>();
            ColumnErrorMessage = new List<Model.ColumnErrorMessage>();
            ColumnOptions = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// 列错误信息
        /// </summary>
        public List<ColumnErrorMessage> ColumnErrorMessage { get; set; }

        /// <summary>
        /// 列选项值:键为列名，值为集合（例如：Type，工程合同、采购合同）
        /// </summary>
        public Dictionary<string, List<string>> ColumnOptions { get; set; }

        /// <summary>
        /// 是否删除行
        /// </summary>
        public bool IsDeleteRow { get; set; }

        /// <summary>
        /// 其他属性
        /// </summary>
        public List<ColumnModel> OtherColumns { get; set; }
    }
}
