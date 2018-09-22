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
    /// ���ݿ����������
    /// </summary>
    internal class SqlHelper : ISqlHelper
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SqlHelper));

        /// <summary>
        /// �����ַ���
        /// </summary>
        public string ConnectionString;

        /// <summary>
        /// �⻧����
        /// </summary>
        public string OrgCode { get; set; }

        public SqlHelper()
        {
        }

        /// <summary>
        /// ��ʼ�����������ַ���
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlHelper(string orgCode,string connectionString)
        {
            OrgCode = orgCode;
            ConnectionString = connectionString;
        }

        /// <summary>
        /// �������ݿ����Ӷ���
        /// </summary>
        /// <returns></returns>
        public  SqlConnection GetConnection(bool isMaster = true)
        {
           SqlConnection conn = new SqlConnection();
           if(conn.State==ConnectionState.Open||conn.State==ConnectionState.Broken)
           {
               conn.Close();
           }
           if (!string.IsNullOrEmpty(ConnectionString))
           {
               return new SqlConnection(ConnectionString);
           }
           else
           {
               return new SqlConnection(new ConnectionHelper().GetConnectionString(isMaster));
           }
       }

        /// <summary>
        /// ����Ӱ�������
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public int ExecuteNonQuery(CommandType commandType,string cmdText,List<SqlParameter> spr)
       {
           int i = 0;
           using (SqlConnection conn = GetConnection())
           {
               conn.Open();
               SqlCommand cmd = new SqlCommand(cmdText, conn);
               //����
               SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);               
               try
               {
                   cmd.Parameters.Clear();
                   cmd.CommandType = commandType;
                   cmd.CommandTimeout = 300;
                   cmd.Transaction = trans;
                   if (spr != null)
                   {
                       foreach (SqlParameter s in spr)
                       {
                           cmd.Parameters.Add(s);
                       };
                   }
                   i = cmd.ExecuteNonQuery();
                   trans.Commit();
               }
               catch(Exception ex)
               {
                   trans.Rollback();
                   _logger.Error(ex.Message,ex);
               }
               finally
               {                   
                   conn.Close();
                   cmd.Parameters.Clear();
               }
               return i;    
           }                
       }

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public int ExecuteNonQueryPro(string cmdText,List<SqlParameter> spr)
       {
           return ExecuteNonQuery(CommandType.StoredProcedure, cmdText, spr);
       }

        /// <summary>
        /// ���������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public int ExecuteNonQueryPro(string cmdText)
       {
           return ExecuteNonQueryPro(cmdText, null);
       }

        /// <summary>
        /// �������Ĳ������
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public int ExecuteNonQuery(string cmdText, List<SqlParameter> spr)
       {
           return ExecuteNonQuery(CommandType.Text, cmdText, spr);
       }

        /// <summary>
        /// ���������Ĳ������
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public int ExecuteNonQuery(string cmdText)
       {
           return ExecuteNonQuery(cmdText, null);
       }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public DataSet GetDataSet(CommandType cmdType,string cmdText,List<SqlParameter> spr)
       {
           DataSet ds = new DataSet();
           using (SqlConnection conn = GetConnection(false))
           {
               conn.Open();
               SqlDataAdapter oda = new SqlDataAdapter(cmdText, conn);
               try
               {                   
                   oda.SelectCommand.CommandType = cmdType;
                   oda.SelectCommand.CommandTimeout = 300;
                   if (spr != null)
                   {
                       foreach (SqlParameter s in spr)
                       {
                           oda.SelectCommand.Parameters.Add(s);
                       }
                   }
                   oda.Fill(ds);
               }
               catch (Exception ex)
               {
                   _logger.Error(ex.Message, ex);
               }
               finally
               {
                   oda.SelectCommand.Parameters.Clear();
                   conn.Close();
               }
               return ds;
           } 
       }

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public DataSet GetDataSetPro(string cmdText, List<SqlParameter> spr)
       {
          return  GetDataSet(CommandType.StoredProcedure,cmdText,spr);
       }
       
        /// <summary>
        /// ���������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public DataSet GetDataSetPro(string cmdText)
       {
          return  GetDataSetPro(cmdText,null);
       }

        /// <summary>
        /// ���������ı���ѯ
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public DataSet GetDataSet(string cmdText, List<SqlParameter> spr)
       {
          return GetDataSet(CommandType.Text, cmdText, spr);
       }

        /// <summary>
        /// �����������ı���ѯ
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public DataSet GetDataSet(string cmdText)
       {
           return GetDataSet(CommandType.Text, cmdText, null);
       }

        /// <summary>
        /// �����Ķ���
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public IDataReader ExecuteReader(CommandType cmdType, string cmdText, List<SqlParameter> spr)
       {
           SqlConnection conn = GetConnection(false);
           conn.Open();
           SqlCommand cmd = new SqlCommand(cmdText, conn);
           try
           {
               cmd.CommandType = cmdType;
               cmd.CommandTimeout = 300;
               if (spr != null)
               {
                   foreach (SqlParameter s in spr)
                   {
                       cmd.Parameters.Add(s);
                   }
               }
               SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               return dr;
           }
           catch (Exception ex)
           {
               _logger.Error(ex.Message, ex);
           }
           finally
           {
               cmd.Parameters.Clear();
           }
           return null;
       }

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public IDataReader ExecuteReaderPro(string cmdText, List<SqlParameter> spr)
       {
           return ExecuteReader(CommandType.StoredProcedure, cmdText, spr);
       }

        /// <summary>
        /// �����������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public IDataReader ExecuteReaderPro(string cmdText)
       {
           return ExecuteReaderPro(cmdText,null);
       }


        /// <summary>
        /// ���������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public IDataReader ExecuteReader(string cmdText, List<SqlParameter> spr)
       {
           return ExecuteReader(CommandType.Text, cmdText, spr);
       }

        /// <summary>
        /// �����������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public IDataReader ExecuteReader(string cmdText)
       {
           return ExecuteReader(cmdText, null);
       }
       
        /// <summary>
        /// ���ص�һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public string ExecuteScalar(CommandType cmdType, string cmdText, List<SqlParameter> spr)
       {
           using (SqlConnection conn = GetConnection(false))
           {
               conn.Open();
               SqlCommand cmd = new SqlCommand(cmdText, conn);
               try
               {
                   cmd.CommandType = cmdType;
                   cmd.CommandTimeout = 300;
                   if (spr != null)
                   {
                       foreach (SqlParameter s in spr)
                       {
                           cmd.Parameters.Add(s);
                       }
                   }
                   if (cmd.ExecuteScalar() != null)
                   {
                       return cmd.ExecuteScalar().ToString();
                   }
               }
               catch (Exception ex)
               {
                   _logger.Error(ex.Message, ex);
               }
               finally
               {
                   cmd.Parameters.Clear();
                   conn.Close();
               }
               return null;
           }
       }

        /// <summary>
        /// �������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public string ExecuteScalarPro(string cmdText, List<SqlParameter> spr)
       {
           return ExecuteScalar(CommandType.StoredProcedure, cmdText, spr);
       }

        /// <summary>
        /// ���������Ĵ洢����
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public string ExecuteScalarPro(string cmdText)
       {
           return ExecuteScalarPro(cmdText, null);
       }

        /// <summary>
        /// ���������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
       public string ExecuteScalar(string cmdText, List<SqlParameter> spr)
       {
           return ExecuteScalar(CommandType.Text, cmdText, spr);
       }

        /// <summary>
        /// �����������ı�
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
       public string ExecuteScalar(string cmdText)
       {
           return ExecuteScalar(cmdText, null);
       }

        /// <summary>
       /// ���������ݲ���
       /// </summary>
       /// <param name="tableNamelist">��������</param>
       /// <param name="dtlist">���ݱ�����</param>
       /// <param name="isPkIdentitylist">�Ƿ�����־Դ</param>
       public bool BatchCopyInsert(string tableName, DataTable dt, bool isPkIdentity)
       {
           List<string> tableNameList = new List<string>() { tableName };
           List<DataTable> dtList = new List<DataTable>() { dt };
           List<bool> isPkIdentitylist = new List<bool>() { isPkIdentity };
           return BatchCopyInsert(tableNameList, dtList, isPkIdentitylist);
       }
       /// <summary>
       /// ���������ݲ���
       /// </summary>
       /// <param name="tableNamelist">��������</param>
       /// <param name="dtlist">���ݱ�����</param>
       /// <param name="isPkIdentitylist">�Ƿ�����־Դ</param>
       public bool BatchCopyInsert(List<string> tableNameList, List<DataTable> dtList, List<bool> isPkIdentitylist)
       {
           using (SqlConnection conn = GetConnection())
           {
               SqlTransaction trans = null;
               conn.Open();
               trans = conn.BeginTransaction();
               SqlBulkCopyOptions ops = SqlBulkCopyOptions.TableLock;
               try
               {
                   for (int i = 0; i < dtList.Count; i++)
                   {
                       if (isPkIdentitylist[i])
                       {
                           ops = ops | SqlBulkCopyOptions.KeepIdentity;
                       }
                       using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, ops, trans))
                       {
                           bulkCopy.DestinationTableName = tableNameList[i];
                           bulkCopy.BatchSize = dtList[i].Rows.Count;
                           bulkCopy.WriteToServer(dtList[i]);
                       }
                   }
                   trans.Commit();
                   return true;
               }
               catch (Exception ex)
               {
                   _logger.Error(ex.Message,ex);
                   if (trans != null)
                       trans.Rollback();                   
               }
               return false;
           }
       }
    }
}
