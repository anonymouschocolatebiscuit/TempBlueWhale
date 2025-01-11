<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baiduCharts.aspx.cs" Inherits="Lanwei.Weixin.UI.baiduCharts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>管理驾驶舱</title>



    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>


    <script src="Javascript/echarts/esl.js" type="text/javascript"></script>
    <script src="Javascript/MyEcharts.js" type="text/javascript"></script>

    <script src="Javascript/WapCharts.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    
    
    
  
  
   <table id="tb" border="0" cellpadding="0" cellspacing="0" style="width:100%; text-align:center; line-height:30px;">
                
        <tr>
        <td colspan="2">
       
       
        <!-- 为ECharts准备一个具备大小（宽高）的Dom -->
    <div id="chartsBar" style="height:300px; width:98%;"></div>
    
    <!-- ECharts单文件引入 -->
  
  <script type="text/javascript">
  
  
  
   var dataNameBar;
    var dataValueBar;
       
        $(document).ready( 
          function () {
              
              $.ajax({
                  url: "HandlerBaiduECharts.ashx",
                  data:{cmd:"bar"},
                  cache: false,
                  async: false,
                  dataType: 'json',
                  success: function (data) {
                      if (data) {

                          dataNameBar = makeData(data).category;
                         dataValueBar=makeData(data).data;
                         
                    
                      }
                      else
                      {
                         alert("网络异常！");
                      }
                  },

                  error: function (msg) {
                      alert("系统发生错误");
                  }

              });


          });
          
     
   //  alert(dataName);
     
  
  
  
        // 3、路径配置
        require.config({
            paths: {
                echarts: 'build/dist'
            }
        });
        
        // 4、使用
        require(
            [
                'echarts',
                'echarts/chart/bar' // 使用柱状图就加载bar模块，按需加载
            ],
            function (ec) {
                // 基于准备好的dom，初始化echarts图表
             var myChart = ec.init(document.getElementById('chartsBar')); 
                
              option = {
                            title : {
                                text: '本年月度销售汇总',
                                subtext: '单位：元'
                            },
                            tooltip : {
                                trigger: 'axis'
                            },
                            
                            toolbox: {
                                show : true,
                                feature : {
                                    mark : {show: true},
                                    dataView : {show: true, readOnly: false},
                                    magicType : {show: true, type: ['line', 'bar']},
                                    restore : {show: true},
                                    saveAsImage : {show: true}
                                }
                            },
                            calculable : true,
                            xAxis : [
                                {
                                    type : 'category',
                                    data :dataNameBar //['1月','2月','3月','4月','5月','6月','7月','8月','9月','10月','11月','12月']
                                }
                            ],
                            yAxis : [
                                {
                                    type : 'value'
                                }
                            ],
                            series : [
                               
                                {
                                
                                
                                    itemStyle:{ 
                                    normal:{ 
                                          label:{ 
                                            show: true, 
                                            formatter: '{c}' 
                                          }, 
                                          labelLine :{show:true} 
                                        } 
                                    },
                            
                        //以上显示数值
                                
                                    name:'销售额',
                                    type:'bar',
                                    data:dataValueBar, //[2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
                                    markPoint : {
                                        data : [
                                            //{name : '年最高', value : 182.2, xAxis: 7, yAxis: 183, symbolSize:18},
                                            //{name : '年最低', value : 2.3, xAxis: 11, yAxis: 3}
                                        ]
                                    },
                                    markLine : {
                                        data : [
                                            {type : 'average', name : '平均值'}
                                        ]
                                    }
                                }
                            ]
                        };
                    
                
                
        
                // 为echarts对象加载数据 
                myChart.setOption(option); 
            }
        );
    </script>
  
    
 
   
    
    
  
       
          
          
          </td>
  
        </tr>
        
        <tr>
        <td style="width:50%;">
        
     
   <!-- 为ECharts准备一个具备大小（宽高）的Dom -->
    <div id="chartsPie" style="height:300px; width:90%;"></div>
    
    
     
    
    <!-- ECharts单文件引入 -->
    
    <script type="text/javascript">
    
    //处理返回的json数据
    function makeData(data)
    {
        var categories = [];
            var datas = [];
            for (var i = 0; i < data.length; i++) {
                categories.push(data[i].name || "");
                datas.push({ name: data[i].name, value: data[i].value || 0 });
            }
            return { category: categories, data: datas };
    }
    var dataNamePie;
    var dataValuePie;
       
        $(document).ready( 
          function () {
              
              $.ajax({
                  url: "HandlerBaiduECharts.ashx",
                  data:{cmd:"pie"},
                  cache: false,
                  async: false,
                  dataType: 'json',
                  success: function (data) {
                      if (data) {
                         dataNamePie =makeData(data).category;
                         dataValuePie=makeData(data).data;
                    
                      }
                      else
                      {
                         alert("网络异常！");
                      }
                  },

                  error: function (msg) {
                      alert("系统发生错误");
                  }

              });


          });
          
      
    
        // 3、路径配置
        require.config({
            paths: {
                echarts: 'build/dist'
            }
        });
        
        // 4、使用
        require(
            [
                'echarts',
                'echarts/chart/pie' // 使用柱状图就加载bar模块，按需加载
            ],
            function (ec) {
                // 基于准备好的dom，初始化echarts图表
                var myChart = ec.init(document.getElementById('chartsPie')); 
                
               option = {
                            title : {
                                text: '本年销售额占比',
                                subtext: '',
                                x:'center'
                            },
                            tooltip : {
                                trigger: 'item',
                                formatter: "{a} <br/>{b} : {c} ({d}%)"
                            },
                            legend: {
                                orient : 'vertical',
                                x : 'left',
                                data:dataNamePie //['直接访问','邮件营销','联盟广告','视频广告','搜索引擎']
                            },
                            toolbox: {
                                show : true,
                                feature : {
                                    mark : {show: true},
                                    dataView : {show: true, readOnly: false},
                                    magicType : {
                                        show: true, 
                                        type: ['pie', 'funnel'],
                                        option: {
                                            funnel: {
                                                x: '25%',
                                                width: '50%',
                                                funnelAlign: 'left',
                                                max: 1548
                                            }
                                        }
                                    },
                                    restore : {show: true},
                                    saveAsImage : {show: true}
                                }
                            },
                            calculable : true,
                            series : [
                                {
                                
                                    itemStyle:{ 
                            normal:{ 
                                  label:{ 
                                    show: true, 
                                    formatter: '{b} :({d}%)' 
                                  }, 
                                  labelLine :{show:true} 
                                } 
                            },
                        //以上显示数值
                                   
                                    name:'访问来源',
                                    type:'pie',
                                    radius : '55%',
                                    center: ['50%', '60%'],
                                    data:dataValuePie
                                }
                            ]
                        };
                    
                
                
        
                // 为echarts对象加载数据 
                myChart.setOption(option); 
            }
        );
    </script>
    
    
 
      

    
    
  
  
    
        
           </td>
  
        <td style="width:50%;">
        
      
       <!-- 1、为ECharts准备一个具备大小（宽高）的Dom -->
    <div id="chartsLine" style="height:300px; width:90%;"></div>
    
       <!-- 2、ECharts单文件引入 -->
    
    <script src="build/dist/echarts.js" type="text/javascript"></script>
    
    
     <script type="text/javascript">
     
     
      var dataNameLine;
    var dataValueLine;
       
        $(document).ready( 
          function () {
              
              $.ajax({
                  url: "HandlerBaiduECharts.ashx",
                  data:{cmd:"line"},
                  cache: false,
                  async: false,
                  dataType: 'json',
                  success: function (data) {
                      if (data) {
                         dataNameLine =makeData(data).category;
                         dataValueLine=makeData(data).data;
                         
                    
                      }
                      else
                      {
                         alert("网络异常！");
                      }
                  },

                  error: function (msg) {
                      alert("系统发生错误");
                  }

              });


          });
     
     
     
        // 3、路径配置
        require.config({
            paths: {
                echarts: 'build/dist'
            }
        });
        
        // 4、使用
        require(
            [
                'echarts',
                'echarts/chart/line' // 使用柱状图就加载bar模块，按需加载
            ],
            function (ec) {
                // 基于准备好的dom，初始化echarts图表
                var myChart = ec.init(document.getElementById('chartsLine')); 
                
                var option = {
                    tooltip: {
                        show: true
                    },
                    legend: {
                        data:['销售量']
                    },
                    xAxis : [
                        {
                            type : 'category',
                            data :dataNameLine //["衬衫","羊毛衫","雪纺衫","裤子","高跟鞋","袜子"]
                        }
                    ],
                    yAxis : [
                        {
                            type : 'value'
                        }
                    ],
                    series : [
                        {
                           
                            itemStyle:{ 
                            normal:{ 
                                  label:{ 
                                    show: true, 
                                    formatter: '{c}' 
                                  }, 
                                  labelLine :{show:true} 
                                } 
                            },
                            
                        //以上显示数值
                            "name":"销售量",
                            "type":"line",
                            "data":dataValueLine //[5, 20, 40, 10, 10, 20]
                        }
                    ]
                };
        
                // 为echarts对象加载数据 
                myChart.setOption(option); 
            }
        );
    </script>
    
    
    
       

    
    
  
      
        
            </td>
  
        </tr>
        
        <tr>
        <td colspan="2">
        
            &nbsp;</td>
  
        </tr>
        
        <tr>
        <td colspan="2">
        
            &nbsp;</td>
  
        </tr>
        
        </table>
    
    
    </form>
</body>
</html>
