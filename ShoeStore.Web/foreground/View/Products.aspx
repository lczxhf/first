<%@ Page Title="" Language="C#" MasterPageFile="~/foreground/View/Common.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ShoeStore.Web.foreground.View.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <style type="text/css">
        .page_list{text-align:right;padding-top:10px}
.page_list a{border:#ddd 1px solid;color:#15428b;padding:2px 5px;margin-right:2px}
.page_list a:hover,.page_list a:active{border:#e1e6ed 1px solid;color:#000;background-color:#D3E1F6}
.page_list span.current{border:#ddd 1px solid;padding:2px 5px;font-weight:bold;margin-right:2px;color:#FFF;background-color:#15428b}
.page_list span.disabled{border:#f3f3f3 1px solid;padding:2px 5px;margin-right:2px;color:#CCC}
    </style>
    <script type="text/javascript">

        $(function () {
            $(".addtocart").click(function () {
                
                $label = $(this).siblings("label");
                $label.html(" ");
                $.post("/foreground/Action/ShoppingCart.ashx", { type: "add", Id: $($(this).siblings()[0]).val() }, function (jsObj) {

                    $label.html(jsObj.msg);
                    setTimeout("$label.html('')", 1500);


                }, "json");
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
    <div id="content" class="float_r">
        <h1><%=title %></h1>
        <asp:Repeater runat="server" ID="rptProduct">
            <ItemTemplate>
                <div class="product_box">
                    <input type="hidden" id="getId" value="<%#Eval("pId")%>"/>
                    <h3><%#Eval("pName") %></h3>
                    <a href="ProductDetail.aspx?Id=<%#Eval("pId") %>">
                        <img src="<%#Eval("pSrc")%>" alt="<%#Eval("pName") %>" /></a>

                    <p class="product_price">$<%#Eval("pPrice") %></p>
                    <a href="javascript:void(0)" class="addtocart"></a>
                    <a href="ProductDetail.aspx?Id=<%#Eval("pId") %>" class="detail"></a>
                    <label style="color:red"  ></label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
       
    </div>
    <div class="page_list">
            <div class="list_info">共<%=num %>个/<%=PageCount %>页 <%=page %> </div>
        </div>
</asp:Content>
