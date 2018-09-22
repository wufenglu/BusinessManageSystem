using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using YK.Component.Attribute.Validations;
using YK.Component.Attribute.Model;
using YK.Component.Attribute.Enum;

namespace YK.Component.Attribute
{
    /// <summary>
    /// 属性验证
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<ValidationResult> Exec<TEntity>(TEntity entity) where TEntity : new()
        {
            List<ValidationResult> result = new List<ValidationResult>();

            result.AddRange(AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Required).Validation(entity));
            result.AddRange(AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Length).Validation(entity));
            result.AddRange(AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Range).Validation(entity));
            result.AddRange(AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Format).Validation(entity));

            return result;
        }

        /// <summary>
        /// 设置禁用特性验证的属性
        /// </summary>
        /// <param name="attributeNames"></param>
        public static void DisableAttributes<TEntity>(List<string> attributeNames) where TEntity : new()
        {
            AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Required).DisableAttributes(attributeNames);
            AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Length).DisableAttributes(attributeNames);
            AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Range).DisableAttributes(attributeNames);
            AttributeFactory<TEntity>.GetValication(ValidationTypeEnum.Format).DisableAttributes(attributeNames);
        }
    }    
}
