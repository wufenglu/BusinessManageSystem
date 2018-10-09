using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using YK.Core;
using YK.Interfaces.Systems;
using YK.Models.Systems;

namespace YK.Services.Systems
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public class SysModulesService: ISysModules
    {
        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public List<SysModules> GetAllModules() {
            return Framework<SysModules>.Instance().FindAll();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public void Save(SysModules entity)
        {
            Framework<SysModules>.Instance().Insert(entity);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public SysModules GetById(int id)
        {
            return Framework<SysModules>.Instance().Get(id);
        }
    }
}
