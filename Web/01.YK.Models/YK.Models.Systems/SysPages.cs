using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;
using System.ComponentModel;

namespace YK.Models.Systems
{
    /// <summary> 
    ///页面
    /// </summary>
    [Table(Name = "Sys_Pages")]
    [Description("页面")]
    public class SysPages : CommonProperty
    {
        /// <summary> 
        ///模块ID
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public virtual int ID { get; set; }
        /// <summary> 
        ///模块ID
        /// </summary>
        public virtual int ModuleID { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary> 
        ///编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary> 
        ///路径
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary> 
        ///排序
        /// </summary>
        public virtual int OrderBy { get; set; }
    }
}
