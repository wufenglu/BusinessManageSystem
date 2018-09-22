using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Attribute.Attributes;
using YK.Component.Attribute.Model;

namespace YK.Component.Attribute
{
    /// <summary>
    /// Excel特性帮助类
    /// </summary>
    public class ExcelAttributeHelper<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 获取头部集合
        /// </summary>
        /// <returns></returns>
        public static List<ExcelHeadDTO> GetHeads()
        {
            //结果集
            List<ExcelHeadDTO> result = new List<ExcelHeadDTO>();

            int columnIndex = 0;
            //遍历属性
            foreach (PropertyInfo info in typeof(TEntity).GetProperties())
            {
                ExcelHeadAttribute attribute = info.GetCustomAttribute<ExcelHeadAttribute>();
                if (attribute != null)
                {
                    //头部实体赋值
                    ExcelHeadDTO dto = new ExcelHeadDTO();
                    dto.PropertyName = info.Name;
                    dto.HeadName = attribute.HeadName;
                    dto.IsValidationHead = attribute.IsValidationHead;
                    dto.IsSetHeadColor = attribute.IsSetHeadColor;
                    dto.IsLocked = attribute.IsLocked;
                    dto.ColumnIndex = columnIndex;
                    dto.ColumnType = attribute.ColumnType;
                    dto.IsHiddenColumn = attribute.IsHiddenColumn;
                    dto.ColumnWidth = attribute.ColumnWidth;
                    dto.Format = attribute.Format;

                    //获取属性
                    RequiredAttribute requiredAttribute = info.GetCustomAttribute<RequiredAttribute>();
                    if (requiredAttribute != null)
                    {
                        dto.IsValidationHead = true;
                    }

                    //添加至结果集
                    result.Add(dto);
                }
                columnIndex++;
            }

            //返回结果集
            return result;
        }

        /// <summary>
        /// 获取头部对象
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static ExcelHeadDTO GetHead(string propertyName)
        {
            //获取属性对应的头部实体
            List<ExcelHeadDTO> list = GetHeads();
            return list.Where(w => w.PropertyName == propertyName).First();
        }
    }
}
