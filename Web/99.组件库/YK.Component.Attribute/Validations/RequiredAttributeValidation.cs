using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Attribute.Attributes;
using YK.Component.Attribute.Model;

namespace YK.Component.Attribute.Validations
{
    /// <summary>
    /// 必填校验
    /// </summary>
    public class RequiredAttributeValidation<TEntity> : BaseAttributeValication<TEntity>
        where TEntity : new()
    {
        /// <summary>
        /// 属性键
        /// </summary>
        /// <returns></returns>
        protected override string GetAttributeKey()
        {
            return "Required";
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
                RequiredAttribute requiredAttribute = prop.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttribute == null)
                {
                    continue;
                }

                //如果是禁用特性则跳出
                if (disableAttributes != null && disableAttributes.Contains(prop.Name))
                {
                    continue;
                }

                //必填校验
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    result.Add(new ValidationResult()
                    {
                        PropertyName = prop.Name,
                        ErrorMessage = requiredAttribute.ErrorMessage
                    });
                }
            }

            return result;
        }
    }
}
