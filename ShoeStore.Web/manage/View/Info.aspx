<%@ Page Title="" Language="C#" MasterPageFile="~/manage/View/common.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="ShoeStore.Web.manage.View.Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <script type="text/javascript">
        var targetUrl = "/manage/Action/Info.ashx";
        $(function () {
            
            $("#Phone").keypress(function (event) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    return false;
                }
            });
          
            $("#PostCode").keypress(function (event) {
                if (event.keyCode < 48 || event.keyCode > 57) {

                    return false;
                }
            });
           
            $("#Age").keypress(function (event) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    return false;
                }
            });
          
            $("#Money").keypress(function (event) {
                if (event.keyCode < 48 || event.keyCode > 57) {

                    return false;
                }
            })
            
        
            $age = $("#Age").val();
            $phone = $("#Phone").val();
            $email = $("#Email").val();
            $("#Age").change(function () {
                if (parseInt($(this).val()) > 120 || parseInt($(this).val()) < 1) {
                    alert("请输入真实年龄");
                    msgBox.showMsgErr("请输入真实年龄");
                    $("#Age").val($age);

                }
            });
            $("#Phone").change(function () {

                if ($(this).val().length != 11) {
                    alert("请输入11位手机号码");
                    msgBox.showMsgErr("请输入11位手机号码");
                    $("#Phone").val($phone);

                }
            });
            $("#Email").change(function () {

                if ($(this).val().indexOf("@") < 0) {
                    alert("请输入正确的邮箱");
                    msgBox.showMsgErr("请输入正确的邮箱");
                    $email = $("#Email").val($email);
                }

            });

            $("#svae").click(save);
            $("#cancel").click(cancel)
        });
        function save()
        {
            var data = $("#fInfo").serialize();
            
            $.post(targetUrl, data, function (jsObj) {
                switch (jsObj.state) {
                    case "ok":
                        alert("保存成功");
                        msgBox.showMsgOk(jsObj.msg);
                        
                        break;
                    case "err":
                        alert("保存失败");
                        msgBox.showMsgErr(jsObj.msg);
                        break;
                }
            }, "json");
        }
        function cancel()
        {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
        
    <form id="fInfo" action="#" name="fInfo" method="post">
        <input type="hidden" name="iId" value="<%=userInfo.IId%>" />
        <input type="hidden" name="iuserId" value="<%=userId %>"/>
    <table id="tbList">
        <tr><th colspan="2">用 户 信 息</th></tr>
        <tr>
            <td>用户名</td>
            <td><input type="text" id="Name" name="Name" value="<%=userName %>" /></td>
        </tr>
        <tr>
            <td>真实姓名</td>
            <td><input type="text" id="RealName" name="RealName" value="<%=userInfo.IName %>" /></td>
        </tr>
         <tr>
            <td>余额</td>
            <td><input type="text" id="Money" name="Money" value="<%=userInfo.IMoney %>"  /></td>
        </tr>
        <tr>
            <td>年龄</td>
            <td><input type="text" id="Age" name="Age" value="<%=userInfo.IAge%>" /></td>
        </tr>
        <tr>
            <td>地址</td>
            <td><input type="text" id="Address" name="Address" value="<%=userInfo.IAddress %>" /></td>
        </tr>
        <tr>
            <td>邮政编码</td>
            <td><input type="text" id="PostCode" name="PostCode" value="<%=userInfo.IPostCode %>" /></td>
        </tr>
        <tr>
            <td>手机号码</td>
            <td><input type="text" id="Phone" name="Phone" value="<%=userInfo.Phone %>" /></td>
        </tr>
        <tr>
            <td>邮箱</td>
            <td><input type="text" id="Email" name="Email" value="<%=userInfo.IEmail %>"/></td>
        </tr>
        <tr><td colspan="2"><input type="button" value="保 存" id="save" style="margin-right:50px" /> <input type="button" value="返 回" id="cancel" />   </td></tr>
      
    </table>
     </form>
 
</asp:Content>
