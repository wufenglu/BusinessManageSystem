﻿using System;
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
    /// 长度校验
    /// </summary>
    public class LengthAttributeValidation<TEntity> : BaseAttributeValication<TEntity>
        where TEntity : new()
    {
        /// <summary>
        /// 属性键
        /// </summary>
        /// <returns></returns>
        protected override string GetAttributeKey()
        {
            return "Length";
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
                LengthAttribute lengthAttribute = prop.GetCustomAttribute<LengthAttribute>();
                if (lengthAttribute == null || value == null)
                {
                    continue;
                }

                //如果是禁用特性则跳出
                if (disableAttributes != null && disableAttributes.Contains(prop.Name))
                {
                    continue;
                }

                //长度校验
                if (lengthAttribute.Length < value.ToString().Length)
                {
                    result.Add(new ValidationResult()
                    {
                        PropertyName = prop.Name,
                        ErrorMessage = lengthAttribute.ErrorMessage
                    });
                }
            }

            return result;
        }
    }
}
