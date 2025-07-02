<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="saleCharts.aspx.cs" Inherits="BlueWhale.UI.saleCharts" %>
<!DOCTYPE html
  PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
  <title>Sales Chart</title>
  <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
  <script src="build\dist\esl.js" type="text/javascript"></script>
  <script src="build\dist\WapCharts.js" type="text/javascript"></script>
</head>

<body>
  <form id="form1" runat="server">
    <table id="tb" border="0" cellpadding="0" cellspacing="0" style="width:100%; text-align:center; line-height:30px;">
      <tr>
        <td colspan="2">
          <div id="chartsBar" style="height:300px; width:98%;"></div>
          <script type="text/javascript">
              var dataNameBar;
              var dataValueBar;
              $(document).ready(
                  function () {

                      $.ajax({
                          url: "HandlerSaleEChart.ashx",
                          data: { cmd: "bar" },
                          cache: false,
                          async: false,
                          dataType: 'json',
                          success: function (data) {
                              if (data) {
                                  dataNameBar = makeData(data).category;
                                  dataValueBar = makeData(data).data;
                              }
                              else {
                                  alert("Network Error!");
                              }
                          },
                          error: function (msg) {
                              alert("System Error");
                          }
                      });
                  });
              require.config({
                  paths: {
                      echarts: 'build/dist'
                  }
              });
              require(
                  [
                      'echarts',
                      'echarts/chart/bar'
                  ],
                  function (ec) {
                      var myChart = ec.init(document.getElementById('chartsBar'));
                      option = {
                          title: {
                              text: 'Monthly sales summary for this year',
                              subtext: 'Unit: RM'
                          },
                          tooltip: {
                              trigger: 'axis'
                          },

                          toolbox: {
                              show: true,
                              feature: {
                                  mark: { show: true },
                                  dataView: { show: true, readOnly: false },
                                  magicType: { show: true, type: ['line', 'bar'] },
                                  restore: { show: true },
                                  saveAsImage: { show: true }
                              }
                          },
                          calculable: true,
                          xAxis: [
                              {
                                  type: 'category',
                                  data: dataNameBar
                              }
                          ],
                          yAxis: [
                              {
                                  type: 'value'
                              }
                          ],
                          series: [
                              {
                                  itemStyle: {
                                      normal: {
                                          label: {
                                              show: true,
                                              formatter: '{c}'
                                          },
                                          labelLine: { show: true }
                                      }
                                  },

                                  name: 'Sale Amount',
                                  type: 'bar',
                                  data: dataValueBar, //[2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3],
                                  markPoint: {
                                      data: [
                                      ]
                                  },
                                  markLine: {
                                      data: [
                                          { type: 'average', name: 'Average' }
                                      ]
                                  }
                              }
                          ]
                      };
                      myChart.setOption(option);
                  }
              );
          </script>
        </td>
      </tr>
      <tr>
        <td style="width:50%;">
          <div id="chartsPie" style="height:300px; width:90%;"></div>
          <script type="text/javascript">
              function makeData(data) {
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
                          url: "HandlerSaleEChart.ashx",
                          data: { cmd: "pie" },
                          cache: false,
                          async: false,
                          dataType: 'json',
                          success: function (data) {
                              if (data) {
                                  dataNamePie = makeData(data).category;
                                  dataValuePie = makeData(data).data;
                              }
                              else {
                                  alert("Network Error!");
                              }
                          },
                          error: function (msg) {
                              alert("System Error");
                          }
                      });
                  });
              require.config({
                  paths: {
                      echarts: 'build/dist'
                  }
              });
              require(
                  [
                      'echarts',
                      'echarts/chart/pie'
                  ],
                  function (ec) {
                      var myChart = ec.init(document.getElementById('chartsPie'));
                      option = {
                          title: {
                              text: 'Sales proporion this year',
                              subtext: '',
                              x: 'center'
                          },
                          tooltip: {
                              trigger: 'item',
                              formatter: "{a} <br/>{b} : {c} ({d}%)"
                          },
                          legend: {
                              orient: 'vertical',
                              x: 'left',
                              data: dataNamePie
                          },
                          toolbox: {
                              show: true,
                              feature: {
                                  mark: { show: true },
                                  dataView: { show: true, readOnly: false },
                                  magicType: {
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
                                  restore: { show: true },
                                  saveAsImage: { show: true }
                              }
                          },
                          calculable: true,
                          series: [
                              {
                                  itemStyle: {
                                      normal: {
                                          label: {
                                              show: true,
                                              formatter: '{b} :({d}%)'
                                          },
                                          labelLine: { show: true }
                                      }
                                  },
                                  name: 'Visit Source',
                                  type: 'pie',
                                  radius: '55%',
                                  center: ['50%', '60%'],
                                  data: dataValuePie
                              }
                          ]
                      };
                      myChart.setOption(option);
                  }
              );
          </script>
        </td>
        <td style="width:50%;">
          <div id="chartsLine" style="height:300px; width:100%;"></div>
          <script src="build/dist/echarts.js" type="text/javascript"></script>
          <script type="text/javascript">
              var dataNameLine;
              var dataValueLine;
              $(document).ready(
                  function () {
                      $.ajax({
                          url: "HandlerSaleEChart.ashx",
                          data: { cmd: "line" },
                          cache: false,
                          async: false,
                          dataType: 'json',
                          success: function (data) {
                              if (data) {
                                  dataNameLine = makeData(data).category;
                                  dataValueLine = makeData(data).data;
                              }
                              else {
                                  alert("Network Error!");
                              }
                          },
                          error: function (msg) {
                              alert("System Error");
                          }
                      });
                  });
              require.config({
                  paths: {
                      echarts: 'build/dist'
                  }
              });
              require(
                  [
                      'echarts',
                      'echarts/chart/line'
                  ],
                  function (ec) {
                      var myChart = ec.init(document.getElementById('chartsLine'));

                      var option = {
                          tooltip: {
                              show: true
                          },
                          legend: {
                              data: ['Total Sales']
                          },
                          xAxis: [
                              {
                                  type: 'category',
                                  data: dataNameLine
                              }
                          ],
                          yAxis: [
                              {
                                  type: 'value'
                              }
                          ],
                          series: [
                              {
                                  itemStyle: {
                                      normal: {
                                          label: {
                                              show: true,
                                              formatter: '{c}'
                                          },
                                          labelLine: { show: true }
                                      }
                                  },
                                  "name": "Total Sales",
                                  "type": "line",
                                  "data": dataValueLine //[5, 20, 40, 10, 10, 20]
                              }
                          ]
                      };
                      myChart.setOption(option);
                  }
              );
          </script>
        </td>
      </tr>
      <tr>
        <td colspan="2">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="2">&nbsp;</td>
      </tr>
    </table>
  </form>
</body>

</html>