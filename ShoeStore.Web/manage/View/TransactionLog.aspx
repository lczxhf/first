<%@ Page Title="" Language="C#" MasterPageFile="~/manage/View/common.Master" AutoEventWireup="true" CodeBehind="TransactionLog.aspx.cs" Inherits="ShoeStore.Web.manage.View.TransactionLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <script type="text/javascript">
        var targetUrl = "/manage/Action/TransactionLog.ashx";
        //当前正在被操作的 行dom对象
        var $nowRow = null;
        $(function () {
            //1.为新增按钮绑定方法
            $("#btnAdd").click(showAdd);
            //2.初始化 新增 和修改面板
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
        })

        function showAdd() {
            //清空 文本框
            $("#pName").val("");
            $("#Time").val("");
            $("#UserName").val("");
            $("#Count").val("");
            $("#MId").val("");
            $("#dialogModify").dialog('open');
        }
        //2.确定
        function doAdd() {
            //新增操作
         
            if ($("#MId").val() == "") {
                
                //2.1获取要提交的数据并验证
                if (check() == false)
                {
                    return false;
                }
               
                var data = $("#fModify").serialize();
                
                //2.2将数据提交到服务器的数据库里 保存
                $.post(targetUrl, data + "&type=a", function (jsObj) {
                    //processData(jsObj, okFunc, errFunc)
                    processData(jsObj, function () {
                        msgBox.showMsgOk(jsObj.msg, function () {
                            window.location.reload();//刷新
                        });
                    });

                }, "json");
            }
                //修改操作
            else {
                doEdit();
            }
            //2.3关闭当前窗体
            $(this).dialog("close");
        }

        //3.显示修改面板
        function showEditPanel(mid, btn) {
            //将被修的行 存入 全局变量
            $nowRow = $(btn).parent().parent();

            msgBox.showMsgWait("加载中...");

            //2.向服务器 发送 请求（根据菜单id查询 菜单数据）
            $.get(targetUrl, { type: "get", "mid": mid, s: Math.random() }, function (jsObj) {
                //获取 响应报文 后 立即 隐藏 消息框

                msgBox.hidBox();
                //调用pd方法 统一处理 返回的数据
                processData(jsObj, function () {
                    //2.1将数据 显示到 修改面板 的控件中
                    $("#pName").val(jsObj.data.PIdMODEL.PName);
                    $("#Count").val(jsObj.data.PCount);
                    $("#Time").val(jsonDateFormat( jsObj.data.PTime));
                    $("#UserName").val(jsObj.data.UserMODEL.ULoginName);
                    $("#MId").val(jsObj.data.Id);
                    //3.展开 修改面板

                    $('#dialogModify').dialog('open');
                }, function () {
                    msgBox.showMsgErr(jsObj.msg);
                });
            }, "json");
            //显示修改面板
            $("#dialogModify").dialog("open");
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

        function doEdit() {
            if (check() == false)
            {
                return false;
            }
            var data = $("#fModify").serialize();
            //1.提交修改数据
            $.post(targetUrl, data + "&type=m", function (jsObj) {
                //调用pd方法 统一处理 返回的数据
                processData(jsObj, function () {
                    //1.1如果修改成功，则 将数据更新到 dom的行对象中
                    if ($nowRow != null) {
                        //不再直接修改表格行 的内容 因为 修改面板 里的 下拉框也需要重新加载
                        var $tds = $nowRow.children("td");
                        $tds[1].innerHTML = jsObj.data.PIdMODEL.PName;
                        $tds[2].innerHTML = jsObj.data.PCount;
                        $tds[3].innerHTML = jsonDateFormat(jsObj.data.PTime);
                        $tds[4].innerHTML = jsObj.data.UserMODEL.ULoginName;
                        //清空选中行
                        $nowRow = null;
                    }
                });
            }, "json");
        }

        function check()    
        {
            
            if ($("#pName").val() == "")
            {
                
                msgBox.showMsgErr("商品名不能为空");
                $("#pName").focus();
                return false;
            }
            if ($("#Count").val() == "") {
               
                msgBox.showMsgErr("数量不能为空");
                $("#Count").focus();
                return false;
            }
            if ($("#Time").val() == "") {
                
                msgBox.showMsgErr("购买时间不能为空");
                $("#Time").focus();
                return false;
            }
            if ($("#UserName").val() == "") {
                
                msgBox.showMsgErr("用户不能为空");
                $("#UserName").focus();
                return false;
            }
            return true;
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
        //将序列化成json格式后日期(毫秒数)转成日期格式
        function jsonDateFormat(cellval) {
            var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            var hour = date.getHours();
            var minute = date.getMinutes() < 10 ? "0"+date.getMinutes():date.getMinutes();
            return date.getFullYear() + "-" + month + "-" + currentDate+" "+hour+":"+minute;
        }
    </script>
    <style type="text/css">
        .page_list{text-align:right;padding-top:10px}
.page_list a{border:#ddd 1px solid;color:#15428b;padding:2px 5px;margin-right:2px}
.page_list a:hover,.page_list a:active{border:#e1e6ed 1px solid;color:#000;background-color:#D3E1F6}
.page_list span.current{border:#ddd 1px solid;padding:2px 5px;font-weight:bold;margin-right:2px;color:#FFF;background-color:#15428b}
.page_list span.disabled{border:#f3f3f3 1px solid;padding:2px 5px;margin-right:2px;color:#CCC}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
            <table id="tbList">
    <tr id="trNav" ><th colspan="5">当前正在：【购买记录】管理</th><th><input type="button" id="btnAdd" value="新增" /></th></tr>
    <tr>
        <th>Id</th>
        <th>商品名</th>
        <th>数量</th>
        <th>购买时间</th>
        <th>用户名</th>
        
        <th style="width:100px;">操作</th>
    </tr>
    <!--循环生成行-->
    <asp:Repeater runat="server" ID="rptList">
        <ItemTemplate>
            <tr>
            <td><%#Eval("Id")%></td>
            <td><%#Eval("PIdMODEL.pName")%></td>
            <td><%#Eval("PCount")%></td>
            <td><%#Eval("PTime")%></td>
            <td><%#Eval("UserMODEL.ULoginName")%></td>
            <td>
                <a href="javascript:void(0)" onclick="doDel(<%#Eval("Id")%>,this)">删</a> 
                <a href="javascript:void(0)" onclick="showEditPanel(<%#Eval("Id")%>,this)">改</a>
            </td>
        </tr>
        </ItemTemplate>
    </asp:Repeater>
    <!--循环生成行-->
</table>
    <div class="page_list">
            <div class="list_info">共<%=num %>个/<%=PageCount %>页 <%=page %> </div>
        </div>
    <!--模态窗口 Begin-->
<!--01.修改窗口-->
<div id="dialogModify" title="用户菜单操作" class="dialogPanel">
    <form id="fModify">
    <input type="hidden" id="MId" name="MId" />
    <table id="tbModify">
        <tr>
            <td>商品名:</td>
            <td><input type="text" id="pName" name="pName" /></td>
        </tr>
        <tr>
            <td>数量：</td>
            <td><input type="text" id="Count" name="Count" /></td>
        </tr>
        <tr>
            <td>购买时间</td>
            <td><input type="text" id="Time" name="Time" /></td>
        </tr>
         <tr>
            <td>用户名</td>
            <td><input type="text" id="UserName" name="UserName" /></td>
        </tr>
    </table>
    </form>
</div>
</asp:Content>
