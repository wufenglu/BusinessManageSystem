using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;
using System.ComponentModel;

namespace YK.Models.Systems
{
    /// <summary> 
    ///组织
    /// </summary>
    [Table(Name = "Sys_Organizations")]
    [Description("组织")]
    public class SysOrganizations : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public virtual int ID { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary> 
        ///编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary> 
        ///是否启用
        /// </summary>
        public virtual bool IsEnable { get; set; }
    }
}
