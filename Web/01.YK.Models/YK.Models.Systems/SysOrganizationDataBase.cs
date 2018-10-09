using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;
using System.ComponentModel;

namespace YK.Models.Systems
{
    /// <summary> 
    ///组织数据库
    /// </summary>
    [Table(Name = "Sys_OrganizationDataBase")]
    [Description("组织数据库")]
    public class SysOrganizationDataBase : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }
        /// <summary> 
        ///租户ID
        /// </summary>
        public int OrganizationID { get; set; }
        /// <summary> 
        ///数据库类型
        /// </summary>
        public string DbType { get; set; }
        /// <summary> 
        ///数据库地址
        /// </summary>
        public string Server { get; set; }
        /// <summary> 
        ///数据库名称
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary> 
        ///用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary> 
        ///密码
        /// </summary>
        public string Password { get; set; }
        /// <summary> 
        ///端口
        /// </summary>
        public string Port { get; set; }
        /// <summary> 
        ///是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary> 
        ///是否主库
        /// </summary>
        public bool IsMaster { get; set; }
        /// <summary> 
        ///权重
        /// </summary>
        public bool Weight { get; set; }
    }
}
