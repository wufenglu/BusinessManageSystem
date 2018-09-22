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
    internal class OraclPager : PagerBase, IPager
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
                        SELECT * FROM ( 
                            SELECT t.*,ROWNUM as rowindex FROM (
	                            SELECT {0} FROM {1} WHERE {2} {5}
                            )_t WHERE ROWNUM<={4}
                        ) tt WHERE rowindex >= {3}
                            ";
            int StartIndex = pageSize * (pageIndex - 1);
            int EndIndex = pageSize * pageIndex;
            string cmdText = string.Format(pageCmdText, selectValue, tableName, where, StartIndex, EndIndex, orderBy);
            return cmdText;
        }
    }
}
