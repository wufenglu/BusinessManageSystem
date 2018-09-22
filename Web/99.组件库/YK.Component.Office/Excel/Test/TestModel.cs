using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Attribute.Attributes;
using YK.Component.Office.Excel.Model;

namespace YK.Component.Office.Excel.Test
{
    /// <summary>
    /// 测试实体
    /// </summary>
    public class TestModel : ExcelRowModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [ExcelHead("名称", true, IsLocked = false)]
        [Required(ErrorMessage = "名称必填")]
        [Length(10, ErrorMessage = "不能超过10个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [ExcelHead("编码", true, IsLocked = true)]
        [Required(ErrorMessage = "编码必填")]
        public string Code { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [ExcelHead("序号", true, IsLocked = true)]
        [Range(1, 10, ErrorMessage = "必须大于或等于1，小于或等于10")]
        public int Num { get; set; }
    }
}
