using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;
using System.ComponentModel;

namespace YK.Models.Systems
{
    /// <summary> 
    ///动作
    /// </summary>
    [Table(Name = "Sys_Acions")]
    [Description("动作")]
    public class SysAcions : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public virtual int ID { get; set; }
        /// <summary> 
        ///页面ID
        /// </summary>
        public virtual int PageID { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary> 
        ///编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary> 
        ///排序
        /// </summary>
        public virtual int OrderBy { get; set; }
    }
}
