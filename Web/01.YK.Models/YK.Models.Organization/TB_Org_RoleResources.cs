using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using YK.Model;

namespace YK.Models.Organizations
{
    /// <summary> 
    ///组织角色资源
    /// </summary>
    public class TB_Org_RoleResources : CommonProperty
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string ID { get; set; }
        /// <summary> 
        ///角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary> 
        ///资源ID
        /// </summary>
        public string ResourceId { get; set; }
    }
}
