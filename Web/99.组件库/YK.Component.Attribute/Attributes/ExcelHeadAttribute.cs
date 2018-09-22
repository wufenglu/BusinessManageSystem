using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Attribute.Enum;

namespace YK.Component.Attribute.Attributes
{
    /// <summary>
    /// 类型验证属性
    /// </summary>
    public sealed class ExcelHeadAttribute : System.Attribute
    {
        /// <summary>
        /// 构造函数，
        /// </summary>
        public ExcelHeadAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="headName">头名称</param>
        /// <param name="isValidationHead">是否验证头部</param>
        /// <param name="isLocked">是否锁定</param>
        public ExcelHeadAttribute(string headName, bool isValidationHead = false, bool isLocked = false)
        {
            HeadName = headName;
            IsValidationHead = isValidationHead;
            IsLocked = isLocked;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string HeadName { get; set; }

        /// <summary>
        /// 是否验证
        /// </summary>
        public bool IsValidationHead { get; set; }

        /// <summary>
        /// 是否设置头部颜色：必填情况下才启用
        /// </summary>
        public bool IsSetHeadColor { get; set; }

        /// <summary>
        /// 是否锁定列
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHiddenColumn { get; set; }

        /// <summary>
        /// 列序号
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int ColumnWidth { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        public ColumnTypeEnum ColumnType { get; set; }

        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
    }
}
