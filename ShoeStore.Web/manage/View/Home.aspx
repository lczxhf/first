<%@ Page Title="" Language="C#" MasterPageFile="~/manage/View/common.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ShoeStore.Web.manage.View.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <style type="text/css">
        #dialogModify
        {
            display:none;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#dialogModify").dialog({
                autoOpen: false,
                width: 400,
                modal: true,

            });
            
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
    <center><h3>欢迎来到后台管理系统~~别看我简陋!我可是功能强大</h3></center><img src="/images/xiaohuangren.png" />
    
    <img src="/images/xiaohuangren2.png" style="margin-left:730px"/>
    <div style="margin-left:300px;margin-top:50px"><img src="/images/xiaohuangren3.gif"style="width:400px;"  /></div>

</asp:Content>
