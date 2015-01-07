<%@ Page Title="" Language="C#" MasterPageFile="~/manage/View/common.Master" AutoEventWireup="true" CodeBehind="ProductCate.aspx.cs" Inherits="ShoeStore.Web.manage.View.ProductCate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <style type="text/css">
#divRight table{
    margin:20px;
    padding:0px;
    list-style-type:none;
    width:967px;
}
        .del
        {
            margin-right:10px;
        }
        #divRight table a
        {
            cursor:pointer;
        }
        #divRight table a:hover
        {
            
            color:black;
        }
#divRight .claTitle {
    cursor:pointer;
    background-color:#0094ff;
    color:black;
    padding:5px;
    font-size:18px;
    border:1px solid #000;
}
        #divRight .claTitle th
        {
            width:300px;
        }
        #divRight .tr td
        {
            text-align:center;
            width:300px;
        }
    </style>
    <script type="text/javascript">
        var targetUrl = "/manage/Action/ProductCate.ashx";
        //当前正在被操作的 行dom对象
        var $nowRow = null;

        $(function () {
            $("#divRight .claTitle").click(function () {
                $(this).siblings("tr").slideToggle();
            });

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
        });

        function showAdd() {
            //清空 文本框
            $("#Sort").val("");
            $("#Cate").val("");
            $("#MId").val("");

            $("#dialogModify").dialog('open');
        };

        function doAdd() {
            //新增操作
            if ($("#MId").val() == "") {
                //2.1获取要提交的数据并验证
                var data = $("#fModify").serialize();
                //2.2将数据提交到服务器的数据库里 保存
                $.post(targetUrl, data + "&type=a", function (jsObj) {
                    processData(jsObj, function () {
                        msgBox.showMsgOk(jsObj.msg, function () {
                            window.location.reload();
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


        function doEdit() {
            var data = $("#fModify").serialize();
            //1.提交修改数据
            $.post(targetUrl, data + "&type=m", function (jsObj) {
                //调用pd方法 统一处理 返回的数据
                processData(jsObj, function () {
                    //1.1如果修改成功，则 将数据更新到 dom的行对象中
                    if ($nowRow != null) {
                        //不再直接修改表格行 的内容 因为 修改面板 里的 下拉框也需要重新加载
                        var $tds = $nowRow.children("td");
                        $tds[0].innerHTML = jsObj.data.PName;
                        $tds[1].innerHTML = jsObj.data.PcSort;
                        //清空选中行
                        $nowRow = null;
                    }
                });
            }, "json");
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

                    $("#Cate").val(jsObj.data.PName);
                    $("#Sort").val(jsObj.data.PcSort);
                    $("#MId").val(jsObj.data.Id);
                    $("#Parent").val(jsObj.data.PId).attr("selected", "selected");
                
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
            <table id="tbList">
    <tr id="trNav"><th colspan="3">当前正在：【商品类别】管理</th><th><input type="button" id="btnAdd" value="新增" /></th></tr>

</table>
    <%=sbHtml.ToString() %>

        <!--模态窗口 Begin-->
<!--01.修改窗口-->
<div id="dialogModify" title="用户管理操作" class="dialogPanel">
    <form id="fModify">
    <input type="hidden" id="MId" name="MId" />
    <table id="tbModify">
        <tr>
            <td colspan="2" style="text-align:center">
                <select style="width:100px" id="Parent" name="Parent">
                    <asp:Repeater runat="server" ID="rptlist">
                        <ItemTemplate>
                            <option value="<%#Eval("Id") %>"><%#Eval("PName") %></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </select>

            </td>
            
        </tr>
        <tr>
            <td>分类:</td>
            <td><input type="text" id="Cate" name="Cate" /></td>
        </tr>
        <tr>
            <td>排序号：</td>
            <td><input type="text" id="Sort" name="Sort" /></td>
        </tr>
       
    </table>
    </form>
</div>
</asp:Content>
