<%@ Page Title="" Language="C#" MasterPageFile="~/foreground/View/Common.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ShoeStore.Web.foreground.View.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
   <script type="text/javascript">
       $(function () {
           $(".addtocart").click(function () {
               $label = $(this).siblings("label");
               $label.html(" ");
               $.post("/foreground/Action/ShoppingCart.ashx", { type: "add", Id:$($(this).siblings()[0]).val() }, function (jsObj) {
                
                       
                           $label.html(jsObj.msg);
                           setTimeout("$label.html('')", 1500);
                   
               }, "json");
           });
       })
      
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
 
                
        	<div id="slider-wrapper">
                <div id="slider" class="nivoSlider">
                    <img src="/images/slider/02.jpg" alt="" />
                    <a href="#"><img src="/images/slider/01.jpg" alt="" title="This is an example of a caption" /></a>
                    <img src="/images/slider/03.jpg" alt="" />
                    <img src="/images/slider/04.jpg" alt="" title="#htmlcaption" />
                </div>
                <div id="htmlcaption" class="nivo-html-caption">
                    <strong>This</strong> is an example of a <em>HTML</em> caption with <a href="#">a link</a>.
                </div>
            </div>
            <script type="text/javascript" src="/Js/jquery-1.4.3.min.js"></script>
            <script type="text/javascript" src="/Js/jquery.nivo.slider.pack.js"></script>
    <script src="/Js/jquery-ui-1.8.23.custom.min.js" type="text/javascript" ></script>
            <script type="text/javascript">
                $(window).load(function () {
                    $('#slider').nivoSlider();
                });
            </script>
        	<h1>推荐产品</h1>
    <asp:Repeater runat="server" id="rptRec">
        
        <ItemTemplate>
            <div class="product_box">
                <input type="hidden" id="getId" value="<%#Eval("pId")%>"/>
                 <h3><%#Eval("pName") %></h3>
            	<a href="ProductDetail.aspx?Id=<%#Eval("pId") %>"><img src="<%#Eval("pSrc")%>" alt="<%#Eval("pName") %>" /></a>
                
                <p class="product_price">$<%#Eval("pPrice") %></p>
                <a href="javascript:void(0)"  class="addtocart"></a>
                <a href="ProductDetail.aspx?Id=<%#Eval("pId") %>" class="detail"></a>
                <label style="color:red"  ></label>
            </div>       
        </ItemTemplate>
    </asp:Repeater>
            

</asp:Content>
