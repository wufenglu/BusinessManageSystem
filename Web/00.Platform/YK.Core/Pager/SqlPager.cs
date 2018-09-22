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
    internal class SqlPager : PagerBase, IPager
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
            string pageCmdText = @"
                            SELECT {0} FROM (
	                            SELECT ROW_NUMBER() OVER ( {5} ) rowindex,{0}
                                    FROM {1} 
                                      WHERE {2} 
                            )_t WHERE rowindex BETWEEN {3} AND {4} ORDER BY rowindex
                            ";
            int StartIndex = pageSize * (pageIndex - 1);
            int EndIndex = pageSize * pageIndex;
            string cmdText = string.Format(pageCmdText, selectValue, tableName, where, StartIndex, EndIndex, orderBy);

            return cmdText;
        }
    }
}
