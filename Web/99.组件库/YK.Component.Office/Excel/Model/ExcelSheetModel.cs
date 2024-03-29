﻿using YK.Component.Attribute.Model;
using YK.Component.Office.Excel.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YK.Component.Office.Excel.Model
{
    /// <summary>
    /// Excel实体
    /// </summary>
    public class ExcelSheetModel<TEntity> where TEntity : class
    {
        /// <summary>
        /// Sheet索引序号
        /// </summary>
        public int SheetIndex { get; set; }

        /// <summary>
        /// Sheet名称
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 起始行：从0开始
        /// </summary>
        public int? StartRowIndex { get; set; }

        /// <summary>
        /// 其实列：从0开始
        /// </summary>
        public int? StartColumnIndex { get; set; }

        /// <summary>
        /// 设置区块
        /// </summary>
        public AreaBlock AreaBlock { get; set; }

        /// <summary>
        /// Sheet实体集合
        /// </summary>
        public List<TEntity> SheetEntityList { get; set; }

        /// <summary>
        /// Sheet头部集合
        /// </summary>
        public List<ExcelHeadDTO> SheetHeadList { get; set; } 

        /// <summary>
        /// 行的列配置
        /// </summary>
        public List<ColumnModel> ColumnConfig { get; set; }
    }
}
