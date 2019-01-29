using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using YK.Utility.Extensions;
using YK.Utility.Views.Models;

namespace YK.Utility.Views
{
    /// <summary>
    /// Grid帮助类
    /// </summary>
    public class GridHelper
    {
        /// <summary>
        /// 获取Grid
        /// </summary>
        /// <returns></returns>
        public static Grid GetData()
        {
            Grid grid = new Grid();

            //xml读取
            string path = CommonClass.PhysicalApplicationPath + "/Metadata/AppGrid/1.metadata.config";
            XmlDocument xmlDoc = new XmlDocument();
            System.Web.HttpContext.Current.Server.MapPath("~/");
            xmlDoc.Load(path);

            //toolbars
            grid.Toolbars = new List<Toolbar>();
            XmlNodeList toolbars = xmlDoc.SelectSingleNode("grid").SelectSingleNode("layout").SelectSingleNode("toolbars").ChildNodes;
            foreach (XmlNode item in toolbars)
            {
                Toolbar toolbar = new Toolbar();
                toolbar.ToolbarId = item.Attributes["toolbarId"].Value.ToInt();
                ToolbarTypeEnum toolType;
                Enum.TryParse<ToolbarTypeEnum>(item.Attributes["type"].Value,out toolType) ;
                toolbar.Type = toolType;
                toolbar.TemplateStyle = item.Attributes["templateStyle"].Value;

                grid.Toolbars.Add(toolbar);
            }
            
            return grid;
        }
    }
}
