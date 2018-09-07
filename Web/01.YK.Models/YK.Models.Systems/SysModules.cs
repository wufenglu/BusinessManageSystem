using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;
using System.ComponentModel;

namespace YK.Models.Systems
{
    /// <summary> 
    ///模块
    /// </summary>
    [Table(Name = "Sys_Modules")]
    [Description("模块")]
    public class SysModules : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public virtual int ID { get; set; }
        /// <summary> 
        ///父级
        /// </summary>
        public virtual int ParentID { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary> 
        ///编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary> 
        ///层级
        /// </summary>
        public virtual int Level { get; set; }
        /// <summary> 
        ///是否启用
        /// </summary>
        public virtual bool IsEnable { get; set; }
        /// <summary> 
        ///排序
        /// </summary>
        public virtual int OrderBy { get; set; }
    }
}
