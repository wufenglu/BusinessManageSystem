using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Component.Office.Excel.Model.Column
{
    /// <summary>
    /// 列实体
    /// </summary>
    public class ColumnModel
    {
        /// <summary>
        /// 列序号
        /// </summary>
        public int ColumnIndex { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列值
        /// </summary>
        public string ColumnValue { get; set; }

        /// <summary>
        /// 列验证集合
        /// </summary>
        public List<ColumnValidationModel> ColumnValidations { get; set; }
    }
}
