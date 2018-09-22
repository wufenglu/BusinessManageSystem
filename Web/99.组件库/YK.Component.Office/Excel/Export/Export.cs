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
using YK.Component.Attribute;
using YK.Component.Attribute.Model;
using YK.Component.Office.Excel.Model;
using System.Web;
using YK.Component.Office.Excel.Common;
using NPOI.SS.Util;

namespace YK.Component.Office.Excel.Export
{
    /// <summary>
    /// 导出
    /// </summary>
    public class Export<TEntity> where TEntity : ExcelRowModel, new()
    {
        /// <summary>
        /// 基于导入执行导出
        /// </summary>
        public void Execute(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //设置区块
            SetAreaBlock(excelGlobalDTO);

            //设置头部颜色
            SetHeadColor(excelGlobalDTO);

            //设置锁定
            SetSheetLocked(excelGlobalDTO);

            //设置列隐藏
            SetSheetColumnHidden(excelGlobalDTO);

            //清空批注
            ClearComment(excelGlobalDTO);

            //设置批注
            SetComment(excelGlobalDTO);

            //设置列类型
            SetSheetColumnType(excelGlobalDTO);

            //删除行
            DeleteRow(excelGlobalDTO);

            //如果路径文件为空则不存储
            if (string.IsNullOrEmpty(excelGlobalDTO.FilePath) == true)
            {
                return;
            }

            //写入内容
            using (FileStream fs = new FileStream(excelGlobalDTO.FilePath, FileMode.Create))
            {
                excelGlobalDTO.Workbook.Write(fs);
                fs.Dispose();
                fs.Close();
            }
        }

        /// <summary>
        /// 根据模板Excel执行
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="excelVersionEnum"></param>
        /// <param name="excelGlobalDTO"></param>
        public virtual void ExecuteByEmptyBuffer(byte[] buffer, ExcelVersionEnum excelVersionEnum, ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            Stream stream = new MemoryStream(buffer);
            excelGlobalDTO.Workbook = ExcelHelper.GetWorkbook(stream, excelVersionEnum);
            this.ExecuteByData(excelGlobalDTO);
        }

        /// <summary>
        /// 基于数据执行导出：FilePath、SheetEntityList、StartRowIndex
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void ExecuteByData(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //初始化
            ExecuteByDataInit(excelGlobalDTO);

            //初始化Excel
            ExecuteByDataInitExcel(excelGlobalDTO);

            //设置区块
            SetAreaBlock(excelGlobalDTO);

            //设置头部颜色
            SetHeadColor(excelGlobalDTO);

            //设置锁定
            SetSheetLocked(excelGlobalDTO);

            //设置列隐藏
            SetSheetColumnHidden(excelGlobalDTO);

            //清空批注
            ClearComment(excelGlobalDTO);

            //设置批注
            SetComment(excelGlobalDTO);

            //设置列类型
            SetSheetColumnType(excelGlobalDTO);

            //如果路径文件为空则不存储
            if (string.IsNullOrEmpty(excelGlobalDTO.FilePath) == true)
            {
                return;
            }

            //写入内容
            using (FileStream fs = new FileStream(excelGlobalDTO.FilePath, FileMode.Create))
            {
                excelGlobalDTO.Workbook.Write(fs);
                fs.Dispose();
                fs.Close();
            }
        }

        /// <summary>
        /// 基于数据导出的初始化
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        private void ExecuteByDataInit(ExcelGlobalDTO<TEntity> excelGlobalDTO) {
            if (excelGlobalDTO.Workbook == null)
            {
                excelGlobalDTO.Workbook = Excel.Common.ExcelHelper.CreateWorkbook();
            }

            #region 如果Sheet为空则初始化Sheet
            if (excelGlobalDTO.Sheets == null)
            {
                //构建默认一个
                ExcelSheetModel<TEntity> sheetModel = new ExcelSheetModel<TEntity>();
                sheetModel.SheetIndex = 0;
                sheetModel.StartRowIndex = excelGlobalDTO.GlobalStartRowIndex;
                sheetModel.StartColumnIndex = excelGlobalDTO.GlobalStartColumnIndex;

                //设置一个默认
                excelGlobalDTO.Sheets = new List<ExcelSheetModel<TEntity>>();
                excelGlobalDTO.Sheets.Add(sheetModel);
            }
            else
            {
                //如果未设置起始行起始列，则以全局为准
                foreach (var item in excelGlobalDTO.Sheets)
                {
                    item.StartRowIndex = item.StartRowIndex ?? excelGlobalDTO.GlobalStartRowIndex;
                    item.StartColumnIndex = item.StartColumnIndex ?? excelGlobalDTO.GlobalStartColumnIndex;
                }
            }
            #endregion
        }

        /// <summary>
        /// 初始化Excel
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        private void ExecuteByDataInitExcel(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //遍历Sheet，创建头部、数据行
            foreach (var item in excelGlobalDTO.Sheets)
            {
                item.SheetHeadList = ExcelAttributeHelper<TEntity>.GetHeads();

                //创建Sheet
                ISheet sheet = null;
                #region 创建Sheet
                if (string.IsNullOrEmpty(item.SheetName))
                {
                    sheet = excelGlobalDTO.Workbook.CreateSheet();
                }
                else
                {
                    sheet = excelGlobalDTO.Workbook.CreateSheet(item.SheetName);
                }
                #endregion

                #region 创建头部
                IRow row = sheet.CreateRow(item.StartRowIndex.Value);
                foreach (var head in item.SheetHeadList)
                {
                    //设置列的值
                    ICell cell = row.CreateCell(head.ColumnIndex);
                    cell.SetCellValue(head.HeadName);

                    //设置列宽
                    if (head.ColumnWidth > 0)
                    {
                        head.ColumnWidth = head.ColumnWidth + 1;//多留一个字符的宽度
                        sheet.SetColumnWidth(head.ColumnIndex, head.ColumnWidth * 256);
                    }
                }
                #endregion

                //如果没有实体集合则跳出
                if (item.SheetEntityList == null)
                {
                    continue;
                }

                //设置行号
                int rowNumber = item.StartRowIndex.Value;
                foreach (var entity in item.SheetEntityList)
                {
                    //设置实体的行号
                    rowNumber++;
                    entity.RowNumber = rowNumber;
                }

                #region 创建行、列及设置值
                foreach (var entity in item.SheetEntityList)
                {
                    //创建行
                    IRow dataRow = sheet.CreateRow(entity.RowNumber);
                    //循环创建列
                    foreach (var head in item.SheetHeadList)
                    {
                        //设置值
                        ICell cell = dataRow.CreateCell(head.ColumnIndex);
                        object value = entity.GetType().GetProperty(head.PropertyName).GetValue(entity);
                        if (value != null)
                        {
                            if (head.ColumnType == Attribute.Enum.ColumnTypeEnum.Date && string.IsNullOrEmpty(head.Format) == false)
                            {
                                cell.SetCellValue(((DateTime)value).ToString(head.Format));
                            }
                            else if (head.ColumnType == Attribute.Enum.ColumnTypeEnum.Decimal && string.IsNullOrEmpty(head.Format) == false)
                            {
                                string cellValue = Math.Round((decimal)value, Convert.ToInt32(head.Format)).ToString();
                                cell.SetCellValue(cellValue);
                            }
                            else
                            {
                                cell.SetCellValue(value.ToString());
                            }
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 执行导出
        /// </summary>
        /// <param name="excelGlobalDTO"> Excel全局对象</param>
        public void ExportMemoryStream(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //当前请求上下文
            HttpResponse response = HttpContext.Current.Response;
            //生成名称
            string fileName = string.Format("attachment;filename={0}({1}).{2}",
                HttpUtility.UrlEncode(excelGlobalDTO.FileName.Split('.')[0],
                System.Text.Encoding.UTF8), DateTime.Now.ToString("yyyyMMdd"),
                excelGlobalDTO.FileName.Split('.')[1]
                );
            //生成文件流

            response.Clear();
            //表头采用utf-8编码，解决在ie情况下的文件名乱码问题
            response.HeaderEncoding = Encoding.GetEncoding("utf-8");//表头添加编码格式 
            response.AddHeader("content-disposition", fileName);
            response.Charset = "utf-8";
            //响应体采用gb2312编码
            response.ContentEncoding = Encoding.GetEncoding("utf-8");
            response.ContentType = "application/ms-excel";

            //填充文件流
            using (MemoryStream excelStream = new MemoryStream())
            {
                excelGlobalDTO.Workbook.Write(excelStream);
                response.BinaryWrite((excelStream).ToArray());
            }
            response.End();
        }

        /// <summary>
        /// 设置区块
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void SetAreaBlock(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //循环遍历设置区块
            foreach (var item in excelGlobalDTO.Sheets)
            {
                //单个Sheet设置区块
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);
                if (item.AreaBlock != null)
                {
                    CellRangeAddress cellRangeAddress = new CellRangeAddress(item.AreaBlock.StartRowIndex, item.AreaBlock.EndRowIndex, item.AreaBlock.StartColumnIndex, item.AreaBlock.EndColumnIndex);
                    sheet.AddMergedRegion(cellRangeAddress);

                    //创建行、列
                    IRow row = sheet.CreateRow(item.AreaBlock.StartRowIndex);
                    ICell cell = row.CreateCell(item.AreaBlock.StartColumnIndex);
                    cell.SetCellValue(item.AreaBlock.Content);

                    //设置列样式
                    ICellStyle cellStyle = excelGlobalDTO.Workbook.CreateCellStyle();
                    cellStyle.VerticalAlignment = VerticalAlignment.CENTER;
                    cellStyle.WrapText = true;
                    cell.CellStyle = cellStyle;

                    //设置高度
                    if (item.AreaBlock.Height != null) {
                        row.Height = item.AreaBlock.Height.Value;
                    }
                }
            }

        }

        /// <summary>
        /// 设置头部颜色
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void SetHeadColor(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            foreach (var item in excelGlobalDTO.Sheets)
            {
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);
                IRow row = sheet.GetRow(item.StartRowIndex.Value);

                //创建头部
                foreach (var head in item.SheetHeadList)
                {
                    if (head.IsSetHeadColor == false)
                    {
                        continue;
                    }

                    ICell cell = row.GetCell(head.ColumnIndex);

                    IFont font = excelGlobalDTO.Workbook.CreateFont();//创建字体样式
                    font.Color = HSSFColor.RED.index;//设置字体颜色

                    ICellStyle style = excelGlobalDTO.Workbook.CreateCellStyle();//创建单元格样
                    style.SetFont(font);
                    cell.CellStyle = style;
                }
            }
        }

        /// <summary>
        /// 设置批注
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void SetComment(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            foreach (var item in excelGlobalDTO.Sheets)
            {
                //值判断
                if (item.SheetEntityList == null)
                {
                    continue;
                }

                //获取Excel的Sheet，对Excel设置批注
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);
                foreach (var entity in item.SheetEntityList)
                {
                    if (entity.ColumnErrorMessage == null)
                    {
                        continue;
                    }

                    //获取行对象
                    IRow row = sheet.GetRow(entity.RowNumber);

                    //基于属性设置批注
                    var groupPropertyName = entity.ColumnErrorMessage.Where(w => w.PropertyName != null).GroupBy(g => g.PropertyName);
                    foreach (var groupItem in groupPropertyName)
                    {
                        //获取单元格，设置批注
                        ExcelHeadDTO headDto = item.SheetHeadList.Where(n => n.PropertyName == groupItem.Key).FirstOrDefault();
                        ICell cell = row.GetCell(headDto.ColumnIndex);
                        if (cell == null)
                        {
                            cell = row.CreateCell(headDto.ColumnIndex);
                        }

                        //设置批注
                        string errorMsg = string.Join(";", groupItem.Select(s => s.ErrorMessage).Distinct().ToArray());
                        SetCellComment(cell, errorMsg, excelGlobalDTO);
                    }

                    //基于列头设置批注，适用于动态列
                    var groupColumnName = entity.ColumnErrorMessage.Where(w => w.PropertyName == null).GroupBy(g => g.ColumnName);
                    foreach (var groupItem in groupColumnName)
                    {
                        //获取单元格，设置批注
                        ExcelHeadDTO headDto = item.SheetHeadList.Where(n => n.HeadName == groupItem.Key).FirstOrDefault();
                        ICell cell = row.GetCell(headDto.ColumnIndex);
                        if (cell == null)
                        {
                            cell = row.CreateCell(headDto.ColumnIndex);
                        }

                        //设置批注
                        string errorMsg = string.Join(";", groupItem.Select(s => s.ErrorMessage).Distinct().ToArray());
                        SetCellComment(cell, errorMsg, excelGlobalDTO);
                    }
                }
            }
        }

        /// <summary>
        /// 设置批注
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="errorMsg"></param>
        /// <param name="excelGlobalDTO"></param>
        public void SetCellComment(ICell cell, string errorMsg, ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            ICreationHelper facktory = cell.Row.Sheet.Workbook.GetCreationHelper();
            if (cell.CellComment == null)
            {
                //创建批注区域
                IDrawing patr = cell.Row.Sheet.CreateDrawingPatriarch();
                var anchor = facktory.CreateClientAnchor();
                //设置批注区间大小
                anchor.Col1 = cell.ColumnIndex;
                anchor.Col2 = cell.ColumnIndex + 2;
                //设置列
                anchor.Row1 = cell.RowIndex;
                anchor.Row2 = cell.RowIndex + 3;
                cell.CellComment = patr.CreateCellComment(anchor);
            }
            if (excelGlobalDTO.ExcelVersionEnum == ExcelVersionEnum.V2003)
            {
                cell.CellComment.String = new HSSFRichTextString(errorMsg);//2003批注方式
            }
            else
            {
                cell.CellComment.String = new XSSFRichTextString(errorMsg);//2007批准方式
            }
            cell.CellComment.Author = "yank";
        }

        /// <summary>
        /// 清空批注
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void ClearComment(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            foreach (var item in excelGlobalDTO.Sheets)
            {
                //获取Excel的Sheet，对Excel设置批注
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);

                //清空批注
                for (int i = (item.StartRowIndex.Value); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                    {
                        continue;
                    }
                    for (var j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell == null)
                        {
                            continue;
                        }
                        cell.CellComment = null;
                    }
                }
            }
        }

        /// <summary>
        /// 设置锁定
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void SetSheetLocked(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //遍历Sheet实体集合
            foreach (var item in excelGlobalDTO.Sheets)
            {
                //获取Excel的Sheet，对Excel设置批注
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);
                foreach (var head in item.SheetHeadList)
                {
                    //行是否锁定，此处设置的是否锁定只针对未输入行上的列
                    IRow row = sheet.GetRow(item.StartRowIndex.Value);
                    ICell cell = row.GetCell(head.ColumnIndex);
                    ICellStyle cellStyle = cell.CellStyle;
                    cellStyle.IsLocked = head.IsLocked;
                    cell.CellStyle = cellStyle;

                    //设置整列样式
                    //sheet.SetDefaultColumnStyle(head.ColumnIndex, cellStyle);

                    //锁定
                    if (head.IsLocked == true)
                    {
                        sheet.ProtectSheet("MD5");
                    }
                }

                //遍历行，设置锁定
                for (int j = (item.StartRowIndex.Value) + 1; j <= sheet.LastRowNum; j++)
                {
                    //获取行
                    IRow row = sheet.GetRow(j);

                    //不存在则跳出
                    if (row == null)
                    {
                        continue;
                    }

                    //遍历头部，设置列
                    foreach (var head in item.SheetHeadList)
                    {
                        //获取列
                        ICell cell = row.GetCell(head.ColumnIndex);

                        //判断单元格是否为空
                        if (cell == null)
                        {
                            continue;
                        }

                        //锁定列头
                        ICellStyle cellStyle = cell.CellStyle;
                        cellStyle.IsLocked = head.IsLocked;
                        cell.CellStyle = cellStyle;
                    }
                }
            }
        }

        /// <summary>
        /// 设置隐藏
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void SetSheetColumnHidden(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //遍历Sheet实体集合
            foreach (var item in excelGlobalDTO.Sheets)
            {
                //获取Excel的Sheet，对Excel设置批注
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);
                foreach (var head in item.SheetHeadList)
                {
                    if (head.IsHiddenColumn == true)
                    {
                        sheet.SetColumnHidden(head.ColumnIndex, head.IsHiddenColumn);
                    }
                }
            }
        }

        /// <summary>
        /// 设置列类型
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void SetSheetColumnType(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            //遍历Sheet实体集合
            foreach (var item in excelGlobalDTO.Sheets)
            {
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);

                //判断是否为空
                if (item.SheetEntityList == null)
                {
                    continue;
                }

                //遍历行
                foreach (var entity in item.SheetEntityList)
                {
                    IRow row = sheet.GetRow(entity.RowNumber);
                    foreach (var head in item.SheetHeadList)
                    {
                        //获取列
                        ICell cell = row.GetCell(head.ColumnIndex);

                        //设置生成下拉框的行和列
                        var cellRegions = new NPOI.SS.Util.CellRangeAddressList((item.StartRowIndex.Value) + 1, sheet.LastRowNum, head.ColumnIndex, head.ColumnIndex);

                        IDataValidationHelper dvHelper = sheet.GetDataValidationHelper();
                        IDataValidationConstraint dvConstraint = null;
                        IDataValidation dataValidate = null;
                        switch (head.ColumnType)
                        {
                            //列类型为文本
                            case Attribute.Enum.ColumnTypeEnum.Text:
                                break;
                            //列类型为日期
                            case Attribute.Enum.ColumnTypeEnum.Date:
                                head.Format = string.IsNullOrEmpty(head.Format) ? "yyyy-MM-dd" : head.Format;
                                dvConstraint = dvHelper.CreateDateConstraint(OperatorType.BETWEEN, DateTime.MinValue.ToString(head.Format), DateTime.MaxValue.ToString(head.Format), head.Format);
                                dataValidate = dvHelper.CreateValidation(dvConstraint, cellRegions);
                                dataValidate.CreateErrorBox("输入不合法", "必须为日期");
                                dataValidate.ShowPromptBox = true;
                                break;
                            //列类型为浮点
                            case Attribute.Enum.ColumnTypeEnum.Decimal:
                                dvConstraint = dvHelper.CreateDecimalConstraint(OperatorType.BETWEEN, "0", "9999999999");
                                dataValidate = dvHelper.CreateValidation(dvConstraint, cellRegions);
                                dataValidate.CreateErrorBox("输入不合法", "必须在0~9999999999之间。");
                                dataValidate.ShowPromptBox = true;
                                break;
                            //列类型为选项
                            case Attribute.Enum.ColumnTypeEnum.Option:
                                //不符合条件则跳出
                                if (entity.ColumnOptions == null
                                    || entity.ColumnOptions.Count == 0
                                    || entity.ColumnOptions.Keys.Contains(head.PropertyName) == false)
                                {
                                    continue;
                                }
                                dvConstraint = dvHelper.CreateExplicitListConstraint(entity.ColumnOptions[head.PropertyName].ToArray());
                                dataValidate = dvHelper.CreateValidation(dvConstraint, cellRegions);
                                dataValidate.CreateErrorBox("输入不合法", "请选择下拉列表中的值。");
                                dataValidate.ShowPromptBox = true;
                                break;
                        }

                        //类型在指定的范围内是才设置校验
                        if (dataValidate != null)
                        {
                            sheet.AddValidationData(dataValidate);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="excelGlobalDTO"></param>
        public void DeleteRow(ExcelGlobalDTO<TEntity> excelGlobalDTO)
        {
            foreach (var item in excelGlobalDTO.Sheets)
            {
                ISheet sheet = excelGlobalDTO.Workbook.GetSheetAt(item.SheetIndex);

                //判断是否为空
                if (item.SheetEntityList == null)
                {
                    continue;
                }

                /*
                 反向排序：目的是从下向上移动
                 避免从上乡下导致变更后的行号跟实体的行号对不上
                 */
                foreach (var entity in item.SheetEntityList.OrderByDescending(o => o.RowNumber))
                {
                    IRow row = sheet.GetRow(entity.RowNumber);
                    if (entity.IsDeleteRow == true && row != null)
                    {
                        /*
                         说明：startRow、endRow从1开始，从开始行至结束行整体向上移动一行
                         n：负数代表向上移动，正数代表向下移动
                         */
                        //sheet.ShiftRows(entity.RowNumber, sheet.LastRowNum, -1);//有bug，向上移动行后批注没有了
                        row.ZeroHeight = true;//隐藏行
                    }
                }
            }
        }
    }
}
