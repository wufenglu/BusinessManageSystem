using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace YK.Core.Pager
{
    /// <summary>
    /// ��ҳ
    /// </summary>
    public interface IPager
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
        /// <param name="recordCount"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader GetPagerInfo(string tableName, string selectValue, int pageSize, int pageIndex, string where, string orderBy, ref int recordCount, List<SqlParameter> spr);
    }
}
