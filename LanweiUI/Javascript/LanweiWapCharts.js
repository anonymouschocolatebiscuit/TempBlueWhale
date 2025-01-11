

function DrawPie(data,id,name, ishuan) {

    var option = ECharts.ChartOptionTemplates.Pie(data,name,ishuan);
    Draw(option, id);

}


function DrawBars(data, id, name, isstack, showArea) {

    var option = ECharts.ChartOptionTemplates.Bars(data, name, isstack);
    if (showArea) {
        option = ECharts.ChartOptionTemplates.Bars(data, name, isstack);
    }
    else {
        option = ECharts.ChartOptionTemplates.Bars(data, name, isstack, {});
    }
    Draw(option, id);

}


/* data：点的数据集合， id：chart的容器，也就是div的ID， name：chart标题， isstack：是否竖直， showArea:是点图还是区域度*/
function DrawLines(data, id, name, isstack, showArea) {
    if (showArea) {
        option = ECharts.ChartOptionTemplates.Lines(data, name,isstack );
    }
    else {
        option = ECharts.ChartOptionTemplates.Lines(data, name,isstack,{});
    }
     Draw(option, id);
 }



function Draw(_option,id) { 
    var option = _option;
    var container = document.getElementById(id);
    opt = ECharts.ChartConfig(container, option);
    ECharts.Charts.RenderChart(opt);
}