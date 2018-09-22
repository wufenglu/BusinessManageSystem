using YK.Component.Office.Excel.Model;
using YK.Component.Office.Excel.Model.Column;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Component.Office.Excel.Common
{
    /// <summary>
    /// 获取文件
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>

        public static List<ColumnFile> GetFiles(ISheet sheet)
        {
            return PictureHelper.GetAllPictureInfos(sheet, new WorkSpace());
        }
    }
}
