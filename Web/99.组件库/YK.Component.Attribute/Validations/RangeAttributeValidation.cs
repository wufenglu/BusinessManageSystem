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
    /// 长度校验
    /// </summary>
    public class RangeAttributeValidation<TEntity> : BaseAttributeValication<TEntity>
        where TEntity : new()
    {
        /// <summary>
        /// 属性键
        /// </summary>
        /// <returns></returns>
        protected override string GetAttributeKey()
        {
            return "Range";
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override List<ValidationResult> Validation(TEntity entity)
        {
            //变量定义
            List<ValidationResult> result = new List<ValidationResult>();
            List<string> disableAttributes = GetDisableAttributes();

            //遍历属性
            foreach (PropertyInfo prop in entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                //获取属性值
                object value = prop.GetValue(entity);

                //获取特性
                RangeAttribute rangeAttribute = prop.GetCustomAttribute<RangeAttribute>();
                if (rangeAttribute == null)
                {
                    continue;
                }

                //如果是禁用特性则跳出
                if (disableAttributes != null && disableAttributes.Contains(prop.Name))
                {
                    continue;
                }

                //如果是可为空类型，且值为空则跳出
                if (prop.PropertyType.IsGenericType
                    && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    && value == null)
                {
                    continue;
                }

                //判断值是否为空
                if (value == null)
                {
                    //添加异常
                    result.Add(new ValidationResult()
                    {
                        PropertyName = prop.Name,
                        ErrorMessage = rangeAttribute.ErrorMessage
                    });
                    continue;
                }

                //是否有错误
                bool isError = false;
                switch (rangeAttribute.OperandType.Name)
                {
                    //Int32类型
                    case "Int32":
                        //比较大小
                        int min = Convert.ToInt32(rangeAttribute.Minimum);
                        int max = Convert.ToInt32(rangeAttribute.Maximum);
                        int propValue = Convert.ToInt32(value);
                        if (propValue < min || propValue > max)
                        {
                            isError = true;
                        }
                        break;
                    //Double类型
                    case "Double":
                        //比较大小
                        double doubleMin = Convert.ToDouble(rangeAttribute.Minimum);
                        double doubleMax = Convert.ToDouble(rangeAttribute.Maximum);
                        double doublePropValue = Convert.ToDouble(value);
                        if (doublePropValue < doubleMin || doublePropValue > doubleMax)
                        {
                            isError = true;
                        }
                        break;
                    //Decimal类型
                    case "Decimal":
                        //比较大小
                        decimal decimalMin = Convert.ToDecimal(rangeAttribute.Minimum);
                        decimal decimalMax = Convert.ToDecimal(rangeAttribute.Maximum);
                        decimal decimalPropValue = Convert.ToDecimal(value);
                        if (decimalPropValue < decimalMin || decimalPropValue > decimalMax)
                        {
                            isError = true;
                        }
                        break;
                    //默认类型：字符比较
                    default:
                        //比较大小
                        string strMin = rangeAttribute.Minimum.ToString();
                        string strMax = rangeAttribute.Maximum.ToString();
                        string strPropValue = value.ToString();
                        if (string.Compare(strPropValue, strMin) == 0 || string.Compare(strPropValue, strMax) == 1)
                        {
                            isError = true;
                        }
                        break;
                }
                //是否有异常
                if (isError)
                {
                    result.Add(new ValidationResult()
                    {
                        PropertyName = prop.Name,
                        ErrorMessage = rangeAttribute.ErrorMessage
                    });
                }
            }
            return result;
        }
    }
}
