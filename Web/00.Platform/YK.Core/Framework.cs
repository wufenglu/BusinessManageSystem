using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YK.Core.CoreFramework;
using YK.Model;

namespace YK.Core
{
    /// <summary>
    /// 核心
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Framework<TEntity> where TEntity : CommonProperty, new()
    {
        /// <summary>
        /// 核心
        /// </summary>
        public static ICoreFramework<TEntity> Instance(string orgCode=null)
        {
             return new CoreFramework<TEntity>(orgCode); 
        }
    }
}
