     
        var manager;
        $(function() {
        
        
          $("#txtUserList").ligerComboBox({ 
              url: "../baseSet/UsersList.aspx?Action=GetDDLList", 
              isMultiSelect: true, 
              isShowCheckBox: true,
              textField:"names",
              valueFieldID: 'bizId'  //自定义值的控件ID，用于获取选择的值
          });



        var form = $("#form").ligerForm();

        var txtUserList = $.ligerui.get("txtUserList");
        txtUserList.set("Width", 250);

        var txtDateStart = $.ligerui.get("txtDateStart");
        txtDateStart.set("Width", 110);

        var txtDateEnd = $.ligerui.get("txtDateEnd");
        txtDateEnd.set("Width", 110);

        var ddlTypeList = $.ligerui.get("ddlTypeList");
        ddlTypeList.set("Width", 100);

        var ddlState = $.ligerui.get("ddlState");
        ddlState.set("Width", 100);

      
            manager = $("#maingrid").ligerGrid({
            checkbox: false,
            columns: [

                { display: '打卡规则', name: 'groupname', width: 80, align: 'center' },

                { display: '员工姓名', name: 'username', width: 100, align: 'center' },

                { display: '打卡类别', name: 'checkin_type', width: 80, align: 'center' },

                { display: '打卡日期', name: 'checkin_date', width: 100, align: 'center', valign: 'center' },

                { display: '打卡时间', name: 'checkin_time', width: 100, align: 'center', valign: 'center' },

                 { display: '打卡状态', name: 'exception_type', width: 100, align: 'center', valign: 'center' },

                

                 { display: '打卡地点', name: 'location_title', width: 100, align: 'left' },
                 
                 { display: '打卡地点详情', name: 'location_detail', width: 250, align: 'left' },
                
                  { display: '打卡wifi名称', name: 'wifiname', width: 100, align: 'left' },
                
                
                { display: '打卡备注', name: 'notes', width: 100, align: 'center' },
                
                
                { display: 'MAC地址', name: 'wifimac', width: 120, align: 'center' },
                
            
                { display: '附件', name: 'mediaids', width: 150, align: 'center' }
                
            
                ],
               
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                  dataAction: 'local', //本地排序
                usePager: false,
                //url: "ReceivableList.aspx?Action=GetDataList", 
                alternatingRow: false,
                
                rownumbers:true//显示序号
                 
                
                
            }
            );

        });
        

      


        function search() {

           
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;
            
            
             var bizId = $("#bizId").val();
            
         
    
             var userIdString = bizId.split(";");

             var type = $("#ddlTypeList").val();
             var state = $("#ddlState").val();
          
            
 
             if(userIdString!="")
            {
                bizId="";
                for(var i=0;i<userIdString.length;i++)
                {
                   bizId+=userIdString[i]+",";
                } 
                bizId=bizId.substring(0,bizId.length-1);
                  
             }

           
          
            manager.changePage("first");
            manager._setUrl("kaoqinList.aspx?Action=GetDataList&bizId=" + bizId + "&start=" + start + "&end=" + end + "&type=" + type + "&state=" + state);

           


        }


  
        
        
        function reload() {
            manager.reload();
        }


   