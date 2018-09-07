using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;

namespace YK.Models.Organizations
{
    /// <summary> 
    ///组织角色
    /// </summary>
    public class TB_Org_Roles : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string ID { get; set; }
        /// <summary> 
        ///组织ID
        /// </summary>
        public string OrganizationId { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public string Name { get; set; }
        /// <summary> 
        ///密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary> 
        ///状态
        /// </summary>
        public int State { get; set; }
        /// <summary> 
        ///排序
        /// </summary>
        public int OrderBy { get; set; }
    }
}
