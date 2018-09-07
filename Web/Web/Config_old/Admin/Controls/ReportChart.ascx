<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportChart.ascx.cs" Inherits="Admin_Controls_ReportChart" %>
<%@ Import Namespace="YK.Common" %>
<link class="include" rel="stylesheet" type="text/css" href="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/jquery.jqplot.min.css" />
<!--[if lt IE 9]><script language="javascript" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/excanvas.js"></script><![endif]-->

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/jquery.min.js"></script>

<%--<link rel="stylesheet" type="text/css" href="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/jquery.jqplot.css" />--%>
<div id="chart1" style="height: <%=count*50%>px; width: <%=count*50%>px; margin-top: 20px;"></div>
<div id="chart2" style="height: <%=count*50%>px; width: <%=count*50%>px; margin: 20px;"></div>
<div id="chart3" style="height: <%=count*50%>px; width: <%=count*50%>px; margin: 20px;clear: both;"></div>

<script class="code" type="text/javascript">
    $(document).ready(function () {
        var data = <%=data %>;
        var plot1 = jQuery.jqplot(
        'chart1',
        [data],
        {
            title: '<%=title %>饼图',
            cursor: {
                style: 'crosshair', //当鼠标移动到图片上时，鼠标的显示样式，该属性值为css类 
                show: true, //是否显示光标 
                showTooltip: true // 是否显示提示信息栏 
            },
            seriesColors: ["#579575", "#d8b83f", "#ff5800", "#0085cc", "#4bb2c5", "#c5b47f", "#EAA228"],
            seriesDefaults: {
                renderer: jQuery.jqplot.PieRenderer,
                rendererOptions: {
                    showDataLabels: true,
                    diameter: undefined, // 设置饼的直径 
                    fill: true, // 设置饼的每部分被填充的状态 
                    shadow: true //为饼的每个部分的边框设置阴影，以突出其立体效果 
                }
            },
            legend: { show: true, location: 'e' }
        }
      );
    });
</script>

<script class="code" type="text/javascript">
    var thisData=<%=data %>;
    var xTitle='<%=xTitle %>';
    var yTitle='<%=yTitle %>';
     $(document).ready(function(){   
        var data = <%=data %>;
        $.jqplot.config.enablePlugins = true;
        var plot3 = $.jqplot('chart2', [data], {
            title: '<%=title %>条形图',
            series:[{renderer:$.jqplot.BarRenderer}],
            axes: {
                xaxis: {
                  renderer: $.jqplot.CategoryAxisRenderer,
                  label: '<%=xTitle %>',
                  labelRenderer: $.jqplot.CanvasAxisLabelRenderer,
                  tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                  tickOptions: {
                      angle: -30,
                      //fontFamily: 'Courier New',
                      fontSize: '9pt'
                  }          
                },
                yaxis: {
                  label: '<%=yTitle %>',
                  labelRenderer: $.jqplot.CanvasAxisLabelRenderer
                }
          }
        });
    });
</script>

<script language="javascript" type="text/javascript">
//                    var lineNumbers = [];
//                    $(document).ready(function () {
//                        var plot1 = $.jqplot('chart3', lineNumbers,
//                        {
//                            title: '日访问量走势图',
//                            axes: {
//                                xaxis: {
//                                    tickOptions:{angle:90}
//                                }
//                            }
//                        });
//                    });     
    $(document).ready(function(){       
    var data = <%=data %>;
    var plot2 = $.jqplot('chart3', [data], {
            title: '<%=title %>走势图',  
            cursor: {            
                show: true,            
                zoom: true,            
                looseZoom: true,            
                showTooltip: false        
            },
            axes: {        
                    xaxis: {          
                     renderer: $.jqplot.CategoryAxisRenderer,//类型：DateAxisRenderer 日期类型，CategoryAxisRenderer类别类型       
                     label: '<%=xTitle %>',         
                     labelRenderer: $.jqplot.CanvasAxisLabelRenderer,          
                     tickRenderer: $.jqplot.CanvasAxisTickRenderer,          
                     tickOptions: {             
                              // labelPosition: 'middle',              
                              angle: -30         
                          }                   
                      },        
                      yaxis: {          
                            label: '<%=yTitle %>',          
                            labelRenderer: $.jqplot.CanvasAxisLabelRenderer        
                        }      
                    },
            //焦点信息显示参数设置
            highlighter: {            
                //show: true,             
                //showLabel: true,             
                //tooltipAxes: 'xy',//鼠标移动上去显示的坐标值，x代表x轴的序号，y代表y轴的值      
                //sizeAdjust: 7.5 ,//鼠标移动上去扩大的倍数
                //tooltipLocation : 'ne',
                //lineWidth: 1,
                //tooltipSeparator: ', ',//鼠标移动上显示值的间隔符号
                //useAxesFormatters: false,
                //tooltipFormatString: '<span mce_style="">%s</span> '      
            }    
        }); 
    });   
</script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/jquery.jqplot.min.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.logAxisRenderer.min.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.canvasTextRenderer.min.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.canvasAxisLabelRenderer.min.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.canvasAxisTickRenderer.min.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.dateAxisRenderer.min.js"></script>

<script class="include" language="javascript" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.pieRenderer.min.js"></script>

<script class="include" language="javascript" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.donutRenderer.min.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.barRenderer.min.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.categoryAxisRenderer.min.js"></script>

<%--控制鼠标移动到图表上面显示的坐标信息 start--%>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.highlighter.js"></script>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.cursor.min.js"></script>

<%--控制鼠标移动到图表上面显示的坐标信息 end--%>

<script class="include" type="text/javascript" src="<%=CommonClass.AppPath %>Admin/js/jquery.jqplot/plugins/jqplot.pointLabels.min.js"></script>

