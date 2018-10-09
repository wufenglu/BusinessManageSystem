using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;
using System.ComponentModel;

namespace YK.Models.Systems
{
    /// <summary> 
    ///系统级用户
    /// </summary>
    [Table(Name = "Sys_User")]
    [Description("系统级用户")]
    public class SysUser : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public virtual string ID { get; set; }
        /// <summary> 
        ///用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary> 
        ///用户名
        /// </summary>
        public virtual string UserCode { get; set; }
        /// <summary> 
        ///密码
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary> 
        ///状态
        /// </summary>
        public virtual bool IsEnable { get; set; }
    }
}
