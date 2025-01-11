<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurOrderListDemo.aspx.cs" Inherits="UI.buy.PurOrderListDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>


    <link href="../lib.1.3.1/Source/lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    
     <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib.1.3.1/Source/lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/core/base.js" type="text/javascript"></script>

    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>

    

    <script src="../lib.1.3.1/Source/demos/grid/ProductData.js"></script>
   
    
   

    <script type="text/javascript">

        //扩展一个 数字输入 的编辑器
        $.ligerDefaults.Grid.editors['numberbox'] = {
            create: function (container, editParm) {
                var column = editParm.column;
                var precision = column.editor.precision;
                var input = $("<input type='text' style='text-align:right' class='l-text' />");
                input.bind('keypress', function (e) {
                    var keyCode = window.event ? e.keyCode : e.which;
                    return keyCode >= 48 && keyCode <= 57 || keyCode == 46 || keyCode == 8;
                });
                input.bind('blur', function () {
                    var value = input.val();
                    input.val(parseFloat(value).toFixed(precision));
                });
                container.append(input);
                return input;
            },
            getValue: function (input, editParm) {
                return parseFloat(input.val());
            },
            setValue: function (input, value, editParm) {
                var column = editParm.column;
                var precision = column.editor.precision;
                input.val(value.toFixed(precision));
            },
            resize: function (input, width, height, editParm) {
                input.width(width).height(height);
            }
        };

        //扩展 numberbox 类型的格式化函数
        $.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
            var precision = column.editor.precision;
            return value.toFixed(precision);
        };

        $(function () {
            $("#maingridaaaa").ligerGrid({
                columns: [
                    {
                        display: '产品', columns:
                          [
                              { display: '主键', name: 'ProductID', type: 'int' },
                              { display: '产品名', name: 'ProductName', align: 'left', width: 100 },
                              {
                                  display: '单价', name: 'UnitPrice', align: 'right', type: 'numberbox',
                                  editor: { type: 'numberbox', precision: 3 }
                              }
                          ]
                    },

                    { display: '仓库数量', name: 'UnitsInStock', align: 'right', type: 'float', editor: { type: 'numberbox', precision: 0 } }

                ], data: ProductData, enabledEdit: true
            });


        });
    </script>

</head>
<body>
    <form id="form1" runat="server">

     <div id="maingridaaaa">
    </div>
    <div style="display: none;">
    </div>

    </form>
</body>
</html>
