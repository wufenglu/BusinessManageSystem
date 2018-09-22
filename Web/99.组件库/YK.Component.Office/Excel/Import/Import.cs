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

namespace YK.Component.Office.Excel.Import
{
    /// <summary>
    /// 导入
    /// </summary>
    public class Import<TEntity> where TEntity : ExcelRowModel, new()
    {
        /// <summary>
        /// 全局对象
        /// </summary>
        public ExcelGlobalDTO<TEntity> ExcelGlobalDTO = new ExcelGlobalDTO<TEntity>();

        /// <summary>
        /// 日期导入显示替换
        /// </summary>
        public Dictionary<string, string> DateImport = new Dictionary<string, string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public Import()
        {
            DateImport.Add("-1月-", "-一月-");
            DateImport.Add("-2月-", "-二月-");
            DateImport.Add("-3月-", "-三月-");
            DateImport.Add("-4月-", "-四月-");
            DateImport.Add("-5月-", "-五月-");
            DateImport.Add("-6月-", "-六月-");
            DateImport.Add("-7月-", "-七月-");
            DateImport.Add("-8月-", "-八月-");
            DateImport.Add("-9月-", "-九月-");
            DateImport.Add("-10月-", "-十月-");
            DateImport.Add("-11月-", "-十一月-");
            DateImport.Add("-12月-", "-十二月-");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="globalStartRowIndex">起始行</param>
        /// <param name="globalStartColumnIndex">起始列</param>
        public Import(int globalStartRowIndex, int globalStartColumnIndex = 0)
        {
            ExcelGlobalDTO.GlobalStartRowIndex = globalStartRowIndex;
            ExcelGlobalDTO.GlobalStartColumnIndex = globalStartColumnIndex;
        }


        /// <summary>
        /// 执行导入
        /// </summary>
        public virtual void Execute(string filePath)
        {
            //获取工作簿
            this.GetWorkbook(filePath);

            //获取Sheet
            this.GetSheets();

            //获取实体集合（头部、数据）对象及校验（头部、实体）
            this.GetEntityAndValidation();
        }

        /// <summary>
        /// 根据流执行
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="excelVersionEnum"></param>
        public virtual void ExecuteByStream(Stream stream, ExcelVersionEnum excelVersionEnum)
        {
            //获取工作簿
            this.GetWorkbook(stream, excelVersionEnum);

            //获取Sheet
            this.GetSheets();

            //获取实体集合（头部、数据）对象及校验（头部、实体）
            this.GetEntityAndValidation();
        }

        /// <summary>
        /// 根据流执行
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="excelVersionEnum"></param>
        public virtual void ExecuteByBuffer(byte[] buffer, ExcelVersionEnum excelVersionEnum)
        {
            Stream stream = new MemoryStream(buffer);
            //获取工作簿
            this.GetWorkbook(stream, excelVersionEnum);

            //获取Sheet
            this.GetSheets();

            //获取实体集合（头部、数据）对象及校验（头部、实体）
            this.GetEntityAndValidation();
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        /// <param name="filePath"></param>
        public void GetWorkbook(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            long fileLength = fileInfo.Length;
            byte[] buffers = new byte[fileLength];

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                fs.Read(buffers, 0, buffers.Length);
                fs.Dispose();
                fs.Close();
            }

            ExcelGlobalDTO.FilePath = filePath;
            ExcelGlobalDTO.FileBytes = buffers;

            using (Stream stream = new FileStream(filePath, FileMode.Open))
            {
                ExcelGlobalDTO.ExcelVersionEnum = ExcelHelper.GetExcelVersion(fileInfo.Name);
                ExcelGlobalDTO.Workbook = ExcelHelper.GetWorkbook(stream, ExcelGlobalDTO.ExcelVersionEnum);
            }
        }

        /// <summary>
        /// 获取工作簿
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="excelVersionEnum"></param>
        public void GetWorkbook(Stream stream, ExcelVersionEnum excelVersionEnum)
        {
            //获取字节
            byte[] buffers = new byte[stream.Length];
            stream.Read(buffers, 0, buffers.Length);
            stream.Seek(0, SeekOrigin.Begin);
            ExcelGlobalDTO.FileBytes = buffers;

            //版本、工作簿赋值
            ExcelGlobalDTO.ExcelVersionEnum = excelVersionEnum;
            ExcelGlobalDTO.Workbook = ExcelHelper.GetWorkbook(stream, excelVersionEnum);
        }

        /// <summary>
        /// 获取Sheet
        /// </summary>
        private void GetSheets()
        {
            //如果没有设置Sheet则基于Workbook初始化Sheet，起始行起始列去全局的
            if (ExcelGlobalDTO.Sheets == null || ExcelGlobalDTO.Sheets.Count == 0)
            {
                //文件流
                Stream stream = new MemoryStream(ExcelGlobalDTO.FileBytes);
                //获取Sheet并设置
                ExcelGlobalDTO.Sheets = this.GetSheets(stream, ExcelGlobalDTO.ExcelVersionEnum);
                foreach (var item in ExcelGlobalDTO.Sheets)
                {
                    item.StartRowIndex = ExcelGlobalDTO.GlobalStartRowIndex;
                    item.StartColumnIndex = ExcelGlobalDTO.GlobalStartColumnIndex;
                }
            }
            else
            {
                //如果未设置起始行起始列，则以全局为准
                foreach (var item in ExcelGlobalDTO.Sheets)
                {
                    item.StartRowIndex = item.StartRowIndex ?? ExcelGlobalDTO.GlobalStartRowIndex;
                    item.StartColumnIndex = item.StartColumnIndex ?? ExcelGlobalDTO.GlobalStartColumnIndex;
                }
            }
        }

        /// <summary>
        /// 获取Sheet
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="excelVersionEnum"></param>
        /// <returns></returns>
        public List<ExcelSheetModel<TEntity>> GetSheets(Stream stream, ExcelVersionEnum excelVersionEnum)
        {
            //返回结果
            List<ExcelSheetModel<TEntity>> sheets = new List<ExcelSheetModel<TEntity>>();

            IWorkbook workbook = ExcelHelper.GetWorkbook(stream, excelVersionEnum);
            //获取所有Sheet
            int sheetCount = workbook.NumberOfSheets;
            //遍历Sheet
            for (int i = 0; i < sheetCount; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);

                //构建默认一个
                ExcelSheetModel<TEntity> sheetModel = new ExcelSheetModel<TEntity>();
                sheetModel.SheetIndex = i;
                sheetModel.SheetName = sheet.SheetName;

                //设置一个默认
                sheets.Add(sheetModel);
            }
            return sheets;
        }

        /// <summary>
        /// 获取实体集合（头部、数据）对象及校验（头部、实体）
        /// </summary>
        private void GetEntityAndValidation()
        {
            //验证头部
            this.ValidationHead();

            //获取头部集合
            GetSheetHeadList();

            //获取实体集合
            GetSheetEntityList();

            //校验实体
            this.ValidationValue();
        }

        /// <summary>
        /// 获取Sheet头部信息
        /// </summary>
        /// <returns></returns>
        public void GetSheetHeadList()
        {
            //获取所有Sheet
            int sheetCount = ExcelGlobalDTO.Workbook.NumberOfSheets;
            //遍历Sheet
            for (int i = 0; i < sheetCount; i++)
            {
                //获取Sheet
                ISheet sheet = ExcelGlobalDTO.Workbook.GetSheetAt(i);
                ExcelSheetModel<TEntity> sheetModel = ExcelGlobalDTO.Sheets.Where(w => w.SheetIndex == i).FirstOrDefault();
                List<ExcelHeadDTO> headDtoList = ExcelAttributeHelper<TEntity>.GetHeads();

                #region 设置列序号

                //获取头部行
                IRow headRow = sheet.GetRow(sheetModel.StartRowIndex.Value);
                if (headRow == null)
                {
                    continue;
                }

                //遍历列，设置序号
                foreach (ICell cell in headRow.Cells)
                {
                    ExcelHeadDTO headDTO = headDtoList.Where(n => n.HeadName == ExcelHelper.GetCellValue(cell)).FirstOrDefault();
                    //如果列不在实体内，在添加头部信息
                    if (headDTO == null)
                    {
                        headDTO = new ExcelHeadDTO();
                        headDTO.HeadName = ExcelHelper.GetCellValue(cell);
                        headDTO.ColumnIndex = cell.ColumnIndex;
                        headDtoList.Add(headDTO);
                    }
                    else
                    {
                        headDTO.ColumnIndex = cell.ColumnIndex;
                    }
                }

                #endregion

                //添加头部
                sheetModel.SheetHeadList = headDtoList;
            }
        }

        /// <summary>
        /// 获取Sheet实体集合
        /// </summary>
        private void GetSheetEntityList()
        {
            //结果集
            Dictionary<string, List<TEntity>> result = new Dictionary<string, List<TEntity>>();

            //获取所有Sheet
            int sheetCount = ExcelGlobalDTO.Workbook.NumberOfSheets;
            //遍历Sheet，目的：获取Sheet实体集合
            for (int i = 0; i < sheetCount; i++)
            {
                //Sheet、实体集合                
                ISheet sheet = ExcelGlobalDTO.Workbook.GetSheetAt(i);
                ExcelSheetModel<TEntity> sheetModel = ExcelGlobalDTO.Sheets.Where(w => w.SheetIndex == i).FirstOrDefault();
                List<ExcelHeadDTO> sheetHead = sheetModel.SheetHeadList;

                //如果头部为空，则不处理当前Sheet
                if (sheetHead == null || sheetHead.Count == 0)
                {
                    continue;
                }

                //获取图片
                List<ColumnFile> files = FileHelper.GetFiles(sheet);

                //实体集合
                List<TEntity> entityList = new List<TEntity>();

                //遍历行                
                for (int rowNum = (sheetModel.StartRowIndex.Value) + 1; rowNum <= sheet.LastRowNum; rowNum++)
                {
                    #region 行转换实体并赋值

                    //获取行
                    IRow row = sheet.GetRow(rowNum);

                    #region 行判断及列是否有值判断
                    //当前行没有任何内容                    
                    if (row == null || row.Cells == null)
                    {
                        continue;
                    }

                    //判断是否有值，没有值则跳出
                    List<string> cellValues = row.Cells.Select(s => ExcelHelper.GetCellValue(s)).ToList();
                    if (cellValues.Exists(w => string.IsNullOrEmpty(w) == false) == false)
                    {
                        continue;
                    }
                    #endregion

                    //实体
                    TEntity entity = new TEntity();
                    entity.RowNumber = rowNum;

                    //遍历头部，设置属性值和其他列
                    foreach (ExcelHeadDTO headDto in sheetHead)
                    {
                        ICell cell = row.GetCell(headDto.ColumnIndex);
                        //获取单元格的值，设置属性值
                        string value = ExcelHelper.GetCellValue(cell);

                        #region 属性为空,添加其他列
                        //属性为空,添加其他列
                        if (string.IsNullOrEmpty(headDto.PropertyName) == true)
                        {
                            //其他不在属性内的列
                            ColumnModel column = new ColumnModel();
                            column.ColumnIndex = headDto.ColumnIndex;
                            column.ColumnName = headDto.HeadName;
                            column.ColumnValue = value;
                            entity.OtherColumns.Add(column);
                            continue;
                        }
                        #endregion

                        #region  属性不为空，设置属性值
                        //属性不为空，设置属性值
                        PropertyInfo prop = entity.GetType().GetProperty(headDto.PropertyName);
                        try
                        {
                            //列文件
                            if (prop.PropertyType == typeof(List<ColumnFile>))
                            {
                                List<ColumnFile> columnFiles = files.Where(n => n.MinRow == rowNum && n.MinCol == headDto.ColumnIndex).ToList();
                                if (columnFiles != null && columnFiles.Count > 0)
                                {
                                    prop.SetValue(entity, columnFiles, null);
                                }
                                continue;
                            }

                            //判断值
                            if (string.IsNullOrEmpty(value))
                            {
                                continue;
                            }

                            //日期
                            if (headDto.ColumnType == Attribute.Enum.ColumnTypeEnum.Date && cell.CellType == CellType.STRING)
                            {
                                prop.SetValue(entity, cell.DateCellValue, null);
                                continue;
                            }

                            //Decimal保留两位
                            if (headDto.ColumnType == Attribute.Enum.ColumnTypeEnum.Decimal && string.IsNullOrEmpty(headDto.Format) == false)
                            {
                                prop.SetValue(entity, Math.Round(Convert.ToDecimal(value), int.Parse(headDto.Format)), null);
                                continue;
                            }

                            //类型是泛型类型且为空类型
                            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                prop.SetValue(entity, Convert.ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType)), null);
                                continue;
                            }

                            //GUID
                            if (prop.PropertyType == typeof(Guid))
                            {
                                prop.SetValue(entity, new Guid(value), null);
                                continue;
                            }
                            //默认设置
                            prop.SetValue(entity, Convert.ChangeType(value, prop.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            #region 异常处理
                            //判断是否为空，为空则实例化
                            if (entity.ColumnErrorMessage == null)
                            {
                                entity.ColumnErrorMessage = new List<ColumnErrorMessage>();
                            }

                            //设置异常信息
                            ColumnErrorMessage errorMsg = new ColumnErrorMessage();
                            errorMsg.PropertyName = prop.Name;
                            errorMsg.ErrorMessage = ex.Message;
                            errorMsg.ColumnName = headDto.HeadName;
                            entity.ColumnErrorMessage.Add(errorMsg);
                            #endregion
                        }
                        #endregion
                    }

                    #endregion

                    //添加实体至集合
                    entityList.Add(entity);
                }
                //添加Sheet实体集合
                sheetModel.SheetEntityList = entityList;
            }
        }

        /// <summary>
        /// 校验头部
        /// </summary>
        public virtual void ValidationHead()
        {
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

                List<ExcelHeadDTO> headDtoList = ExcelAttributeHelper<TEntity>.GetHeads();
                foreach (ExcelHeadDTO dto in headDtoList)
                {
                    //校验必填的，判断表头是否在excel中存在
                    if (dto.IsValidationHead == true && cellValues.Contains(dto.HeadName) == false)
                    {
                        throw new Exception(ExcelGlobalDTO.ExcelValidationMessage.Clgyl_Common_Import_TempletError);
                    }
                }
            }

            //头部校验后处理，用于特殊处理
            ValidationHeaderAfter();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public virtual void ValidationHeaderAfter()
        {

        }

        /// <summary>
        /// 验证内容
        /// </summary>
        public virtual void ValidationValue()
        {
            //遍历Sheet实体集合
            foreach (var sheet in ExcelGlobalDTO.Sheets)
            {
                //获取Sheet头部实体集合
                var headDtoList = sheet.SheetHeadList;
                foreach (var item in sheet.SheetEntityList)
                {
                    //获取实体验证信息
                    var result = ValidationHelper.Exec<TEntity>(item);
                    foreach (var msg in result)
                    {
                        //判断列异常消息是否为空，为空则实例化
                        if (item.ColumnErrorMessage == null)
                        {
                            item.ColumnErrorMessage = new List<ColumnErrorMessage>();
                        }

                        //异常信息
                        ColumnErrorMessage errorMsg = new ColumnErrorMessage();
                        errorMsg.PropertyName = msg.PropertyName;
                        errorMsg.ErrorMessage = msg.ErrorMessage;

                        //设置列信息
                        var headDto = headDtoList.Where(w => w.PropertyName == msg.PropertyName).FirstOrDefault();
                        if (headDto != null)
                        {
                            errorMsg.ColumnName = headDto.HeadName;
                        }

                        //添加至集合
                        item.ColumnErrorMessage.Add(errorMsg);
                    }
                }
            }

            //验证值后
            this.ValidationValueAfter();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        public virtual void ValidationValueAfter()
        {
        }
    }
}
