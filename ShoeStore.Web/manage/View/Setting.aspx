<%@ Page Title="" Language="C#" MasterPageFile="~/manage/View/common.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="ShoeStore.Web.manage.View.Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <script type="text/javascript">
        var targetUrl = "/manage/Action/Setting.ashx";
        var $nowRow = null;
        $(function () {
            
            $("#dialogModify").dialog({
                autoOpen: false,
                width: 400,
                modal: true,
                buttons: {
                    //修改面板的 确定按钮 方法，提交 新数据到 服务器，并处理返回值
                    "确定": doAdd,
                    "取消": function () {
                        $(this).dialog("close");
                    }
                }
            });
            
           
            $("#btnAdd").click(showAdd);
            
            $(".Name").change(function () {
               
                if ($(this).val() == "") {
                    alert($($(this).parent().siblings()[0]).html() + "不能为空");
                    
                    msgBox.showMsgErr($($(this).parent().siblings()[0]).html() + "不能为空");
                    return false;
                }
              
                $.post(targetUrl, { type: "save", name: $($(this).parent().siblings()[0]).html(), value: $(this).val(), id: $(this).siblings().val() }, function (jsObj) {
                    processData(jsObj, function () {
                        
                        msgBox.showMsgOk(jsObj.msg);
                    },
                    function () {
                        
                        msgBox.showMsgOk(jsObj.msg);
                        window.location.reload();
                    });
                }, "json");
           
            });
            

        });
 
    
        function showAdd() {
            //清空 文本框
            $("#Name").val("");
            $("#PassWord").val("");
            $("#MId").val("");

            $("#dialogModify").dialog('open');
        };


        function doAdd() {
            //新增操作
           
                //2.1获取要提交的数据并验证
                var data = $("#fModify").serialize();
                //2.2将数据提交到服务器的数据库里 保存
                $.post(targetUrl, data + "&type=a", function (jsObj) {
                    processData(jsObj, function () {
                        msgBox.showMsgOk(jsObj.msg, function () {
                            window.location.reload();//刷新
                        });
                    });
                }, "json");
        
            $(this).dialog("close");
        }



        //删除菜单
        function doDel(mid, btn) {
            $nowRow = $(btn).parent().parent();
            $.post(targetUrl, { type: "del", "mid": mid }, function (jsObj) {
                processData(jsObj, function () {
                    msgBox.showMsgOk(jsObj.msg);
                    $nowRow.remove();
                });
            }, "json");
        }


        /***统一处理 返回的 json 数据************/
        function processData(jsObj, okFunc, errFunc) {
            //根据返回的 数据 状态 执行相应的操作
            switch (jsObj.state) {
                case "ok"://如果ok则执行 ok回调函数
                    if (okFunc) okFunc();
                    break;
                case "err"://如果err的话，则 执行 err 回调函数
                    if (errFunc) errFunc();
                    msgBox.showMsgErr(jsObj.msg);
                    break;
                case "np"://没有权限，则直接跳转到指定页面
                    msgBox.showMsgErr(jsObj.msg, function () {
                        window.location = jsObj.nextUrl;
                    });
                    break;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
          
    <form id="fInfo" action="#" name="fInfo" method="post">
    <table id="tbList">
        <tr id="trNav" ><th colspan="2">当前正在：【系统信息】管理</th><th style="text-align:center"><input type="button" id="btnAdd" value="新增"  /></th></tr>
        <asp:Repeater runat="server" ID="rptlist">
            <ItemTemplate>
         <tr>
             
            <td><%#Eval("Name") %></td>
            <td><input type="text" id="Name" name="Name" class="Name" value="<%#Eval("Value") %>" /><input type="hidden" name="id" value="<%#Eval("Id")%>"/></td>
             <td><a href="javascript:void(0)" onclick="doDel(<%#Eval("Id")%>,this)">删除</a></td>
        </tr
            </ItemTemplate>
        </asp:Repeater>
>
     

      
    </table>
     </form>

            <!--模态窗口 Begin-->
<!--01.修改窗口-->
<div id="dialogModify" title="系统信息管理" clas="dialogPanel">
    <form id="fModify">
 
    <table id="tbModify">
        <tr>
            <td>系统信息名:</td>
            <td><input type="text" id="Name" name="Name" /></td>
        </tr>
        <tr>
            <td>系统信息值：</td>
            <td><input type="text" id="value" name="Value" /></td>
        </tr>
       
    </table>
    </form>
</div>

</asp:Content>
