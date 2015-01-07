<%@ Page Title="" Language="C#" MasterPageFile="~/foreground/View/Common.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="ShoeStore.Web.foreground.View.ProductDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
        <script type="text/javascript">

            $(function () {
                $(".addtocart").click(function () {
                    $label = $(this).siblings("label");
                    $label.html(" ");
                    
                    $.post("/foreground/Action/ShoppingCart.ashx", { type: "add", Id: $(this).next().val(),count:$("#num").val() }, function (jsObj) {

                        $label.html(jsObj.msg);
                        setTimeout("$label.html('')", 1500);
                        $("#num").val("1");


                    }, "json");
                });

                $("#num").keypress(function () {
                    if (event.keyCode < 49 || event.keyCode > 57) {

                        return false;
                    }
                });
            })
           
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
            	<h1><%=product.PName %>详细介绍</h1>
            <div class="content_half float_l">
        	<a  rel="lightbox[portfolio]" href="/images/product/10_l.jpg"><img src="<%=product.PSrc %>" alt="image" /></a>
            </div>
            <div class="content_half float_r">
                <table>
                    <tr>
                        <td width="160">价格:</td>
                        <td>$<%=product.PPrice %></td>
                    </tr>
                    <tr>
                        <td>库存:</td>
                        <td><%=product.PNum %></td>
                    </tr>
                    <tr>
                        <td>分类:</td>
                        <td><%=product.PCateidMODEL.PName %></td>
                    </tr>
                    <tr>
                        <td>是否推荐:</td>
                        <td><%=product.PIsrec %></td>
                    </tr>
                    <tr>
                    	<td>购买数量</td>
                        <td><input type="text" id="num" value="1" style="width: 40px; text-align: right" /></td>
                    </tr>
                </table>
                <div class="cleaner h20"></div>
                
                <a href="javascript:void(0)" class="addtocart" style="margin-right:30px"></a>
                <input type="hidden" id="getId" value="<%=product.PId%>"/>
                <label style="color:red"  ></label>

			</div>
            <div class="cleaner h30"></div>
            
            <h5>商 品 介 绍</h5>
            <p><%=product.PRemark %></p>	
            
          <div class="cleaner h50"></div>
            
            <h3>相 关 商 品</h3>
    <asp:Repeater runat="server" ID="rptRelated">
        <ItemTemplate>
            <div class="product_box">
            	<a href="productdetail.html"><img src="<%#Eval("pSrc") %>" alt="" /></a>
                <h3><%#Eval("pName") %></h3>
                <p class="product_price">$ <%#Eval("pPrice") %></p>
                <a href="shoppingcart.html" class="addtocart"></a>
                <a href="productdetail.html" class="detail"></a>
            </div> 
        </ItemTemplate>
    </asp:Repeater>
        	
</asp:Content>
