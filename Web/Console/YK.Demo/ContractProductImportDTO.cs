using YK.Component.Attribute.Attributes;
using YK.Component.Office.Excel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Demo.DTO
{

    /// <summary>
    /// 合同明细详细
    /// </summary>
    [Serializable]
    public class ContractProductImportDTO : ExcelRowModel
    {

        /// <summary>
        /// 材料名称
        /// </summary>
        [ExcelHead("材料名称", IsLocked = true, IsHiddenColumn = false, ColumnWidth = 8)]
        [Required(ErrorMessage = "材料名称必填")]
        [Length(100, ErrorMessage = "长度不能超过100")]
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [ExcelHead("数量")]
        [Required(ErrorMessage = "材料名称必填")]
        [Range(0, 10000, ErrorMessage = "范围必须在1-10000之间")]
        public decimal? Count { get; set; }

        ///<summary>
        /// 单价（含税）
        ///</summary>
        [ExcelHead("单价（含税）")]
        [Required(ErrorMessage = "单价（含税）必填")]
        [Range(0, 10000, ErrorMessage = "范围必须在1-10000之间")]
        public decimal? Price { get; set; }
    }
}
