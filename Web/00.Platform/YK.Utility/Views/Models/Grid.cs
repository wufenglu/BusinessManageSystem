using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Utility.Views.Models
{
    /// <summary>
    /// gird组件
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// 主键
        /// </summary>
       public int GridId { get; set; }

        /// <summary>
        /// 页面id
        /// </summary>
        public int FunctionPageId { get; set; }

        /// <summary>
        /// grid名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 元数据版本
        /// </summary>
        public string MetadataVersion { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// 工具栏
        /// </summary>
        public List<Toolbar> Toolbars { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public List<Event> Events { get; set; }

        /// <summary>
        /// 视图集合
        /// </summary>
        public List<View> Views { get; set; }
    }

    /// <summary>
    /// 视图
    /// </summary>
    public class View {
        /// <summary>
        /// 主键
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public int Name { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 模板样式：default
        /// </summary>
        public string TemplateStyle { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public DataSource DataSource { get; set; }
    }

    /// <summary>
    /// 数据源
    /// </summary>
    public class DataSource {
        /// <summary>
        /// 主键名称
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; set; }
        /// <summary>
        /// 模式
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// 不锁定
        /// </summary>
        public bool WithNoLock { get; set; }

        /// <summary>
        /// 命令
        /// </summary>
        public Command Command { get; set; }

        /// <summary>
        /// 列集合
        /// </summary>
        public List<Column> Columns { get; set; }
    }

    /// <summary>
    /// 列
    /// </summary>
    public class Column
    { 
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 实体
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor { get; set; }

        /// <summary>
        /// 数据类型：date
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 对其方式
        /// </summary>
        public HorizontalAlignmentEnum Align { get; set; }

        /// <summary>
        /// 数据类型：field
        /// </summary>
        public string DataSourceType { get; set; }

        /// <summary>
        /// 自动排序
        /// </summary>
        public bool AllowSort { get; set; }

        /// <summary>
        /// 是否统计列
        /// </summary>
        public bool IsSummaryColumn { get; set; }

        /// <summary>
        /// 是否加粗
        /// </summary>
        public bool IsBold { get; set; }
    }

    /// <summary>
    /// 命令
    /// </summary>
    public class Command {
        /// <summary>
        /// 类型：sql
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 工具栏
    /// </summary>
    public class Toolbar {
        /// <summary>
        /// 主键
        /// </summary>
        public int ToolbarId { get; set; }

        /// <summary>
        /// 类型：全局（global），行按钮（row）
        /// </summary>
        public ToolbarTypeEnum Type { get; set; }

        /// <summary>
        /// 模板样式：default\row
        /// </summary>
        public string TemplateStyle { get; set; }

        /// <summary>
        /// 分组集合
        /// </summary>
        public List<Group> Groups { get; set; }
    }

    /// <summary>
    /// 分组
    /// </summary>
    public class Group {
        /// <summary>
        /// 对齐方式
        /// </summary>
        public HorizontalAlignmentEnum Align { get; set; }

        /// <summary>
        /// 按钮集合
        /// </summary>
        public List<Item> Items { get; set; }
    }

    /// <summary>
    /// 项
    /// </summary>
    public class Item {
        /// <summary>
        /// 主键
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否高度
        /// </summary>
        public bool IsHighlight { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ButtonTypeEnum Type { get; set; }

        /// <summary>
        /// icon样式地址
        /// </summary>
        public string IconClassUrl { get; set; }

        /// <summary>
        /// icon样式
        /// </summary>
        public string IconClass { get; set; }
        /// <summary>
        /// 是否菜单按钮
        /// </summary>
        public bool IsMenuButton { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 唯一标识，按钮标记如：btnPrint
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 按钮集合，当按钮为菜单按钮时存在
        /// </summary>
        public List<Item> Items { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public List<Event> Events { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public Behavior Behavior { get; set; }
    }

    /// <summary>
    /// 行为
    /// </summary>
    public class Behavior {

        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 目标：self
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 类型：page
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 事件
    /// </summary>
    public class Event {
        /// <summary>
        /// 时间名称，如onlick
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 调用函数
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }

    /// <summary>
    /// 水平对其
    /// </summary>
    public enum HorizontalAlignmentEnum
    {
        left = 1,
        center = 2,
        right = 3
    }

    /// <summary>
    /// 垂直对其
    /// </summary>
    public enum VerticalAlignmentEnum
    {
        top = 1,
        middle = 2,
        bottom = 3
    }

    /// <summary>
    /// 按钮类型
    /// </summary>
    public enum ButtonTypeEnum
    {
        button = 1,
        menu = 2
    }

    /// <summary>
    /// 工具类型
    /// </summary>
    public enum ToolbarTypeEnum
    {
        global,
        row
    }
}