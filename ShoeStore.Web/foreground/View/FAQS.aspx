<%@ Page Title="" Language="C#" MasterPageFile="~/foreground/View/Common.Master" AutoEventWireup="true" CodeBehind="FAQS.aspx.cs" Inherits="ShoeStore.Web.foreground.View.FAQS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
            <style type="text/css">
        .page_list{text-align:right;padding-top:10px}
.page_list a{border:#ddd 1px solid;color:#15428b;padding:2px 5px;margin-right:2px}
.page_list a:hover,.page_list a:active{border:#e1e6ed 1px solid;color:#000;background-color:#D3E1F6}
.page_list span.current{border:#ddd 1px solid;padding:2px 5px;font-weight:bold;margin-right:2px;color:#FFF;background-color:#15428b}
.page_list span.disabled{border:#f3f3f3 1px solid;padding:2px 5px;margin-right:2px;color:#CCC}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
    <div id="content" class="float_r faqs">
        	<h1>常见问题</h1>
        <asp:Repeater runat="server" ID="rptQuestion">
            <ItemTemplate>
                <h5><%#Eval("Question") %></h5>
            <p><%#Eval("Answer") %></p>
            </ItemTemplate>
        </asp:Repeater>
        </div> 
      <div class="page_list">
            <div class="list_info">共<%=num %>个/<%=PageCount %>页 <%=page %> 

            </div>
        </div>
</asp:Content>



