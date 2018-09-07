using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;

namespace YK.Models.Organizations
{
    /// <summary> 
    ///组织用户角色
    /// </summary>
    public class TB_Org_UserRoles : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string ID { get; set; }
        /// <summary> 
        ///用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary> 
        ///角色ID
        /// </summary>
        public string RoleId { get; set; }
    }
}
