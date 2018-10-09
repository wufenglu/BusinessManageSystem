﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
 
namespace YK.Model
{
    /// <summary> 
    ///公共属性
    /// </summary>
    public class CommonProperty
    {
        /// <summary>
        /// 变更的属性
        /// </summary>
        public Dictionary<string, object> ChanageProperty = new Dictionary<string, object>();

        /// <summary> 
        ///创建人ID
        /// </summary>
        public int CreaterID { get; set; }
        /// <summary> 
        ///创建人
        /// </summary>
        public string Creater { get; set; }
        /// <summary> 
        ///创建日期
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        /// <summary> 
        ///修改人ID
        /// </summary>
        public int ModifierID { get; set; }
        /// <summary> 
        ///修改人
        /// </summary>
        public string Modifier { get; set; }
        /// <summary> 
        ///修改日期
        /// </summary>
        public DateTime? ModifyOn { get; set; }
    }
}
