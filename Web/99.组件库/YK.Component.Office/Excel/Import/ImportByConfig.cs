using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Attribute;
using YK.Component.Attribute.Model;
using YK.Component.Office.Excel.Common;
using YK.Component.Office.Excel.Model;
using YK.Component.Office.Excel.Model.Column;
using YK.Component.Attribute.Enum;

namespace YK.Component.Office.Excel.Import
{
    /// <summary>
    /// 导入
    /// </summary>
    public class ImportByConfig<TEntity>  : Import<TEntity> where TEntity : ExcelRowModel, new()
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="globalStartRowIndex">起始行</param>
        /// <param name="globalStartColumnIndex">起始列</param>
        public ImportByConfig(int globalStartRowIndex, int globalStartColumnIndex = 0) : base(globalStartRowIndex,globalStartColumnIndex)
        {
        }

        /// <summary>
        /// 校验头部
        /// </summary>
        public override void ValidationHead()
        {
            base.ValidationHead();

            int sheetCount = ExcelGlobalDTO.Workbook.NumberOfSheets;
            for (int i = 0; i < sheetCount; i++)
            {
                ISheet sheet = ExcelGlobalDTO.Workbook.GetSheetAt(i);
                ExcelSheetModel<TEntity> sheetModel = ExcelGlobalDTO.Sheets.Where(w => w.SheetIndex == i).FirstOrDefault();
                IRow row = sheet.GetRow(sheetModel.StartRowIndex.Value);
                if (row == null)
                {
                    continue;
                }

                //获取表头信息
                List<string> cellValues = row.Cells.Select(s => ExcelHelper.GetCellValue(s)).ToList();
                if (cellValues == null)
                {
                    continue;
                }

                //配置如果为空则跳过
                if (sheetModel.ColumnConfig == null)
                {
                    continue;
                }

                foreach (ColumnModel columnModel in sheetModel.ColumnConfig)
                {
                    if (columnModel.ColumnValidations == null) {
                        continue;
                    }
                    foreach (var validation in columnModel.ColumnValidations) {
                        //校验必填的，判断表头是否在excel中存在
                        if (validation.ValidationTypeEnum == ValidationTypeEnum.Required && cellValues.Contains(columnModel.ColumnName) == false)
                        {
                            throw new Exception(ExcelGlobalDTO.ExcelValidationMessage.Clgyl_Common_Import_TempletError);
                        }
                    }                    
                }
            }

            //头部校验后处理，用于特殊处理
            ValidationHeaderAfter();
        }

        /// <summary>
        /// 验证内容
        /// </summary>
        public override void ValidationValue()
        {
            base.ValidationValue();

            //遍历Sheet实体集合
            foreach (var sheet in ExcelGlobalDTO.Sheets)
            {
                //获取Sheet头部实体集合
                var headDtoList = sheet.SheetHeadList;
                foreach (var item in sheet.SheetEntityList)
                {
                    foreach (var config in sheet.ColumnConfig)
                    {
                        //判断列异常消息是否为空，为空则实例化
                        if (item.ColumnErrorMessage == null)
                        {
                            item.ColumnErrorMessage = new List<ColumnErrorMessage>();
                        }

                        if (config.ColumnValidations == null) {
                            continue;
                        }

                        foreach (ColumnValidationModel validation in config.ColumnValidations) {
                            ColumnModel columnModel = item.OtherColumns.Where(n => n.ColumnName == config.ColumnName).FirstOrDefault();
                            if (validation.ValidationTypeEnum == ValidationTypeEnum.Required
                                && string.IsNullOrEmpty(columnModel.ColumnValue) == true)
                            {
                                //异常信息
                                ColumnErrorMessage errorMsg = new ColumnErrorMessage();
                                errorMsg.ColumnName = columnModel.ColumnName;
                                errorMsg.ErrorMessage = validation.ErrorMessage;
                                //添加至集合
                                item.ColumnErrorMessage.Add(errorMsg);
                            }
                        }
                    }
                }
            }

            //验证值后
            this.ValidationValueAfter();
        }
    }
}
