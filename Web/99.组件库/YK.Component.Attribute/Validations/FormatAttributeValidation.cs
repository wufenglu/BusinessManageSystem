using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YK.Component.Attribute.Attributes;
using YK.Component.Attribute.Model;

namespace YK.Component.Attribute.Validations
{
    /// <summary>
    /// 格式校验属性
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class FormatAttributeValidation<TEntity> : BaseAttributeValication<TEntity>
        where TEntity : new()
    {
        /// <summary>
        /// 属性键
        /// </summary>
        /// <returns></returns>
        protected override string GetAttributeKey()
        {
            return "Format";
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<ValidationResult> Validation(TEntity entity)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            List<string> disableAttributes = GetDisableAttributes();

            //遍历属性
            foreach (PropertyInfo prop in entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = prop.GetValue(entity);

                //获取属性
                FormatAttribute formatAttribute = prop.GetCustomAttribute<FormatAttribute>();
                if (formatAttribute == null)
                {
                    continue;
                }

                //如果是禁用特性则跳出
                if (disableAttributes != null && disableAttributes.Contains(prop.Name))
                {
                    continue;
                }

                if (value == null)
                {
                    result.Add(new ValidationResult()
                    {
                        PropertyName = prop.Name,
                        ErrorMessage = formatAttribute.ErrorMessage
                    });
                    continue;
                }

                //正则表达式
                string regex = null;
                switch (formatAttribute.FormatEnum)
                {
                    case FormatEnum.Email:
                        regex = "^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$";
                        break;
                    case FormatEnum.Phone:
                        regex = @"^(\d{3,4}-)?\d{6,8}$";
                        break;
                    case FormatEnum.MobilePhone:
                        regex = @"^[1]+[3,5,8]+\d{9}";
                        break;
                    case FormatEnum.ID:
                        regex = @"(^\d{18}$)|(^\d{15}$)";
                        break;
                }

                //如果用户配置了正则表达式，以用户配置为准
                if (string.IsNullOrEmpty(formatAttribute.Regex) == false)
                {
                    regex = formatAttribute.Regex;
                }

                //如果正则为空则跳出
                if (regex == null)
                {
                    continue;
                }

                Regex r = new Regex(regex);
                //如果匹配不上则添加异常
                if (r.IsMatch(value.ToString()) == false)
                {
                    result.Add(new ValidationResult()
                    {
                        PropertyName = prop.Name,
                        ErrorMessage = formatAttribute.ErrorMessage
                    });
                }
            }
            return result;
        }
    }
}
