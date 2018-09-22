using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data .SqlClient;
using log4net;

namespace YK.Core.SqlHelper
{
    /// <summary>
    /// ���ݿ���������ӿ�
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// ����Ӱ�������
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        int ExecuteNonQuery(CommandType commandType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        int ExecuteNonQueryPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// ���������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        int ExecuteNonQueryPro(string cmdText);

        /// <summary>
        /// �������Ĳ������
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// ���������Ĳ������
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string cmdText);

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        DataSet GetDataSet(CommandType cmdType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        DataSet GetDataSetPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// ���������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        DataSet GetDataSetPro(string cmdText);

        /// <summary>
        /// ���������ı���ѯ
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        DataSet GetDataSet(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �����������ı���ѯ
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        DataSet GetDataSet(string cmdText);

        /// <summary>
        /// �����Ķ���
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader ExecuteReaderPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �����������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        IDataReader ExecuteReaderPro(string cmdText);


        /// <summary>
        /// ���������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �����������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string cmdText);

        /// <summary>
        /// ���ص�һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        string ExecuteScalar(CommandType cmdType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        string ExecuteScalarPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// ���������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        string ExecuteScalarPro(string cmdText);

        /// <summary>
        /// ���������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        string ExecuteScalar(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// �����������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        string ExecuteScalar(string cmdText);

        /// <summary>
        /// ���������ݲ���
        /// </summary>
        /// <param name="tableNamelist">��������</param>
        /// <param name="dtlist">���ݱ�����</param>
        /// <param name="isPkIdentitylist">�Ƿ�����־Դ</param>
        bool BatchCopyInsert(string tableName, DataTable dt, bool isPkIdentity);

       /// <summary>
       /// ���������ݲ���
       /// </summary>
       /// <param name="tableNamelist">��������</param>
       /// <param name="dtlist">���ݱ�����</param>
       /// <param name="isPkIdentitylist">�Ƿ�����־Դ</param>
        bool BatchCopyInsert(List<string> tableNameList, List<DataTable> dtList, List<bool> isPkIdentitylist);
    }
}
