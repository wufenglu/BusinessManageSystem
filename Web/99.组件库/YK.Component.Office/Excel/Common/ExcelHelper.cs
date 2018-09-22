using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Office.Excel.Model;

namespace YK.Component.Office.Excel.Common
{
    /// <summary>
    /// Excel帮助
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// 跟进文件流获取工作簿
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="versionEnum">版本</param>
        public static IWorkbook GetWorkbook(Stream stream, ExcelVersionEnum versionEnum)
        {
            //2003版本
            if (versionEnum == ExcelVersionEnum.V2003)
            {
                return new HSSFWorkbook(stream);
            }

            //2007版本
            if (versionEnum == ExcelVersionEnum.V2007)
            {
                return new XSSFWorkbook(stream);
            }
            return null;
        }

        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <param name="versionEnum"></param>
        /// <returns></returns>
        public static IWorkbook CreateWorkbook(ExcelVersionEnum versionEnum = ExcelVersionEnum.V2007)
        {
            //2003版本
            if (versionEnum == ExcelVersionEnum.V2003)
            {
                return new HSSFWorkbook();
            }

            //2007版本
            if (versionEnum == ExcelVersionEnum.V2007)
            {
                return new XSSFWorkbook();
            }
            return null;
        }

        /// <summary>
        /// 跟进文件名获取版本
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ExcelVersionEnum GetExcelVersion(string fileName)
        {
            //2007版本
            if (fileName.ToLower().IndexOf(".xlsx") > 0)
            {
                return ExcelVersionEnum.V2007;
            }

            //2003版本
            if (fileName.ToLower().IndexOf(".xls") > 0)
            {
                return ExcelVersionEnum.V2003;
            }            
            return ExcelVersionEnum.No;
        }

        /// <summary>
        /// 获取列的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static string GetCellValue(ICell cell) {
            //为空直接返回数据
            if (cell == null)
            {
                return string.Empty;
            }
            string value = string.Empty;
            switch (cell.CellType)
            {
                case CellType.Unknown://未知类型
                    value = cell.ToString();
                    break;
                case CellType.NUMERIC://数据类型
                    value = cell.ToString();
                    break;
                case CellType.STRING://string类型
                    value = cell.StringCellValue;
                    break;
                case CellType.FORMULA://计算公式
                    value = string.Empty;
                    break;
                case CellType.BLANK://空文本
                    value = string.Empty;
                    break;
                case CellType.BOOLEAN://逻辑类型
                    value = cell.ToString();
                    break;
                case CellType.ERROR://错误内容
                    value = string.Empty;
                    break;
                default:
                    value = string.Empty;
                    break;
            }
            return value;
        }
    }
}
