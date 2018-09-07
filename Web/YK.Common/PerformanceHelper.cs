using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Common
{
    /// <summary>
    /// 性能测试帮助类
    /// </summary>
    public class PerformanceHelper
    {        
        /// <summary>
        /// 秒表对象
        /// </summary>
        public System.Diagnostics.Stopwatch stopwatch { get; set; }
        /// <summary>
        /// 性能明细列表
        /// </summary>
        public List<PerformanceDtlEntity> entityList { get; set; }
        /// <summary>
        /// 性能明细
        /// </summary>
        public PerformanceDtlEntity dtlEntity { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PerformanceHelper()
        {
            //列表
            entityList = new List<PerformanceDtlEntity>();            
        }
        /// <summary>
        /// 开始计算
        /// </summary>
        /// <param name="title">标题</param>
        public void Start(string title)
        {
            //明细
            dtlEntity= new PerformanceDtlEntity();
            dtlEntity.Title = title;//标题
            dtlEntity.StartTime = DateTime.Now;//开始时间   

            //秒表
            stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();//秒表开始
        }
        /// <summary>
        /// 结束计算
        /// </summary>
        public void Stop()
        {
            stopwatch.Stop();//秒表结束

            dtlEntity.StopTime = DateTime.Now;//结束时间
            dtlEntity.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;//总运行时间            
            entityList.Add(dtlEntity);//添加明细
        }
    }
    /// <summary>
    /// 性能明细
    /// </summary>
    public class PerformanceDtlEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime StopTime { get; set; }
        /// <summary>
        /// 总运行时间（一毫秒为单位）
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
    }
}
