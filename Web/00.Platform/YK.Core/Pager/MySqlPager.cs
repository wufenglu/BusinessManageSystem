using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using YK.Core.SqlHelper;

namespace YK.Core.Pager
{
    /// <summary>
    /// ��ҳ
    /// </summary>
    internal class MySqlPager : PagerBase, IPager
    {
        /// <summary>
        /// ���ݲ����ֱ��ǣ�������������ҳ���С����ҳ�룬��������ѯ����,����
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="selectValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public override string GetPagerSql(string tableName, string selectValue, int pageSize, int pageIndex, string where, string orderBy)
        {
            string cmdText = "select " + selectValue + " from " + tableName + " where " + where + orderBy + " limit " + pageSize * (pageIndex - 1) + "," + pageSize;
            return cmdText;
        }
    }
}
