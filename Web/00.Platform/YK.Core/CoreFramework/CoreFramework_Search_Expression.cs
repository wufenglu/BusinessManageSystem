using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using YK.Core.Model;
using YK.Core.Pager;
using YK.Core.SqlHelper;

namespace YK.Core.CoreFramework
{
    /// <summary>
    /// 公共操作类，搜索模块（Search）
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    internal partial class CoreFramework<TEntity> : ICoreFramework<TEntity> 
    {
        /// <summary>
        /// 分页查询，基础方法，参数：页面大小，页码，主键，查询字段，表达式，排序，数据总条数
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="selectFields">查询字段</param>
        /// <param name="express">表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="recordCount">数据总条数</param>        
        /// <returns></returns>
        public List<TEntity> ExpressionSearch(int pageSize, int pageIndex, string selectFields, List<Expression> express, string orderBy, ref int recordCount)
        {
            //获取参数和条件
            CoreFrameworkEntity CoreFrameworkEntity = GetParaListAndWhere(express);
            //条件
            string where = CoreFrameworkEntity.Where;
            //参数列表
            List<SqlParameter> listPara = CoreFrameworkEntity.ParaList;

            selectFields = string.IsNullOrEmpty(selectFields) ? "*" : selectFields;//查询字段
            orderBy = string.IsNullOrEmpty(orderBy) ? PrimaryKey : orderBy;

            IPager page = Pager.Pager.getInstance();
            IDataReader sdr = page.GetPagerInfo(TableName, selectFields, pageSize, pageIndex, where, orderBy, ref recordCount, listPara);

            return DynamicBuilder<TEntity>.GetList(sdr, columnAttrList);

        }

        /// <summary>
        /// 不分页查询,基础方法
        /// </summary>
        /// <param name="express">表达式</param>
        /// <param name="count">显示总数，空则全部显示</param>
        /// <param name="selectFields">查询字段</param>
        /// <param name="orderBy">排序,为空则不排序</param>
        /// <returns></returns>
        public List<TEntity> ExpressionSearch(List<Expression> express, int? count = null, string selectFields = null, string orderBy = null)
        {
            //获取参数和条件
            CoreFrameworkEntity CoreFrameworkEntity = GetParaListAndWhere(express);
            //条件
            return this.CommonSearch(CoreFrameworkEntity, count, selectFields, orderBy);
        }
    }
}
