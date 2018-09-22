using YK.Component.Attribute;
using YK.Component.Attribute.Attributes;
using YK.Component.Attribute.Enum;
using YK.Component.Attribute.Model;
using YK.Component.Office.Excel.Export;
using YK.Component.Office.Excel.Import;
using YK.Component.Office.Excel.Model;
using YK.Component.Office.Excel.Model.Column;
using YK.Demo.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt = new DateTime();
           DateTime.TryParse("12-9月-2018",out dt);
           
            DateTime dt1 = Convert.ToDateTime("12-九月-2018");

            string excelPath = Directory.GetCurrentDirectory() + "\\Excel\\合同材料导入模版.xls";

            //ColumnModel columnModel = new ColumnModel();
            //columnModel.ColumnName = "材料名称";
            //columnModel.ColumnValidations = new List<ColumnValidationModel>();
            //columnModel.ColumnValidations.Add(new ColumnValidationModel() {
            //    ValidationTypeEnum = ValidationTypeEnum.Required,
            //    ErrorMessage = "材料名称必填"
            //});

            //ColumnModel columnCountModel = new ColumnModel();
            //columnCountModel.ColumnName = "数量";
            //columnCountModel.ColumnValidations = new List<ColumnValidationModel>();
            //columnCountModel.ColumnValidations.Add(new ColumnValidationModel()
            //{
            //    ValidationTypeEnum = ValidationTypeEnum.Required,
            //    ErrorMessage = "数量必填"
            //});

            //ImportByConfig<ExcelRowModel> import = new ImportByConfig<ExcelRowModel>(1);
            //import.ExcelGlobalDTO.Sheets = new List<ExcelSheetModel<ExcelRowModel>>();
            //ExcelSheetModel<ExcelRowModel> sheetModel = new ExcelSheetModel<ExcelRowModel>();
            //sheetModel.SheetIndex = 0;
            //sheetModel.SheetName = "合同材料";
            //sheetModel.ColumnConfig = new List<ColumnModel>();
            //sheetModel.ColumnConfig.Add(columnModel);
            //sheetModel.ColumnConfig.Add(columnCountModel);
            //import.ExcelGlobalDTO.Sheets.Add(sheetModel);

            //import.Execute(excelPath);

            //Export<ExcelRowModel> export = new Export<ExcelRowModel>();
            //export.Execute(import.ExcelGlobalDTO);

            #region 基于实体的导入导出

            Import<ContractProductImportDTO> import = new Import<ContractProductImportDTO>(1);
            import.Execute(excelPath);

            foreach (var item in import.ExcelGlobalDTO.Sheets)
            {
                item.AreaBlock = new Component.Office.Excel.Model.AreaBlock();
                item.AreaBlock.StartRowIndex = 0;
                item.AreaBlock.EndRowIndex = 0;
                item.AreaBlock.StartColumnIndex = 0;
                item.AreaBlock.EndColumnIndex = 6;
                item.AreaBlock.Height = 256 * 3;

                StringBuilder noteString = new StringBuilder("相关数据字典：（★★请严格按照相关格式填写，以免导入错误★★）\n");
                noteString.Append("1.列名带有' * '是必填列;\n");
                noteString.Append("2.会员卡号：会员卡号长度为3~20位,且只能数字或者英文字母;\n");
                noteString.Append("3.性别：填写“男”或者“女”;\n");
                noteString.Append("4.手机号码：只能是11位数字的标准手机号码;\n");
                noteString.Append("5.固定电话：最好填写为“区号+电话号码”，例：075529755361;\n");
                noteString.Append("6.会员生日：填写格式“年-月-日”，例：1990-12-27，没有则不填;\n");

                item.AreaBlock.Content = noteString.ToString();
            }

            Export<ContractProductImportDTO> export = new Export<ContractProductImportDTO>();
            export.Execute(import.ExcelGlobalDTO);

            #endregion
        }
    }

    public class VEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        [Length(20, ErrorMessage = "长度不能超过20个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "请输入邮箱")]
        public string Email { get; set; }
    }
}
