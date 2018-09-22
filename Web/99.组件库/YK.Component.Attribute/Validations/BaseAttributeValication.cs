using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YK.Component.Attribute.Model;

namespace YK.Component.Attribute.Validations
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class BaseAttributeValication<TEntity>: IAttributeValidation<TEntity> where TEntity : new()
    {
        /// <summary>
        /// 禁用的特性
        /// </summary>
        public static Dictionary<string, List<string>> EntityDisableAttributes = new Dictionary<string, List<string>>();

        /// <summary>
        /// 获取key
        /// </summary>
        /// <returns></returns>
        protected abstract string GetAttributeKey();

        /// <summary>
        /// 获取实体属性键
        /// </summary>
        /// <returns></returns>
        protected string GetEntityAttributeKey()
        {
            Type entity = typeof(TEntity);
            return GetAttributeKey() + "_" + entity.GetType().FullName;
        }

        /// <summary>
        /// 获取禁用属性
        /// </summary>
        /// <returns></returns>
        protected List<string> GetDisableAttributes() {
            string entityKey = GetEntityAttributeKey();
            if (EntityDisableAttributes.Keys.Contains(entityKey)) {
                return EntityDisableAttributes[entityKey];
            }
            return null;
        }

        /// <summary>
        /// 禁用属性
        /// </summary>
        /// <param name="attributeNames"></param>
        public void DisableAttributes(List<string> attributeNames)
        {
            string entityKey = GetEntityAttributeKey();
            EntityDisableAttributes.Add(entityKey, attributeNames);
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract List<ValidationResult> Validation(TEntity entity);
    }
}
