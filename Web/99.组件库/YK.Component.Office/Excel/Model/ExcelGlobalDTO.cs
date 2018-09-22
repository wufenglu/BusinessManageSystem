using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Attribute.Model;
using System.IO;
using YK.Component.Office.Excel.Model.Const;

namespace YK.Component.Office.Excel.Model
{
    /// <summary>
    /// Excel全局对象
    /// </summary>
    public class ExcelGlobalDTO<TEntity> where TEntity : class
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExcelGlobalDTO() {
            ExcelValidationMessage = new ExcelValidationMessage();
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径：以文件路径处理时使用
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件字节数组
        /// </summary>
        public byte[] FileBytes { get; set; }

        /// <summary>
        /// Excel版本
        /// </summary>
        public ExcelVersionEnum ExcelVersionEnum { get; set; }

        /// <summary>
        /// 工作簿对象
        /// </summary>
        public IWorkbook Workbook { get; set; }

        /// <summary>
        /// 全局起始行：不设置Sheet的默认按照全局来
        /// </summary>
        public int GlobalStartRowIndex { get; set; }

        /// <summary>
        /// 全局起始列：不设置Sheet的默认按照全局来
        /// </summary>
        public int GlobalStartColumnIndex { get; set; }

        /// <summary>
        /// Sheet集合
        /// </summary>
        public List<ExcelSheetModel<TEntity>> Sheets { get; set; }

        /// <summary>
        /// Excel验证消息
        /// </summary>
        public ExcelValidationMessage ExcelValidationMessage { get; set; }

        /// <summary>
        /// 设置默认的Sheet，适用于基于数据的导出
        /// </summary>
        public void SetDefaultSheet() {
            //Sheet设置
            Sheets = new List<ExcelSheetModel<TEntity>>();
            ExcelSheetModel<TEntity> sheetModel = new ExcelSheetModel<TEntity>();
            Sheets.Add(sheetModel);
        }
    }
}
