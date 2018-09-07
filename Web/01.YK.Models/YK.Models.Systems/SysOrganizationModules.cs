using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;
using System.ComponentModel;

namespace YK.Models.Systems
{
    /// <summary> 
    ///组织模块
    /// </summary>
    [Table(Name = "Sys_OrganizationModules")]
    [Description("组织")]
    public class SysOrganizationModules : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public virtual int ID { get; set; }
        /// <summary> 
        ///父级
        /// </summary>
        public virtual int ModuleID { get; set; }
        /// <summary> 
        ///租户ID
        /// </summary>
        public virtual int OrganizationID { get; set; }
    }
}
