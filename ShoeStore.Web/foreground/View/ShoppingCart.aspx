<%@ Page Title="" Language="C#" MasterPageFile="~/foreground/View/Common.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="ShoeStore.Web.foreground.View.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <script type="text/javascript">
        $ProductName = "";
        $(function () {
           
            Settotale();
            //设置当数量的值发生变化时触发的时间
            $(".num").change(function () {
                $all = 0;
                
                if ($(this).val() == "" || $(this).val() =="0")
                {
                    alert("请输入正确的数量");
                    $(this).val("1")
                    return false;
                }
                //设置购物车最下面的总价的值
                $($(this).parent().siblings()[5]).html($(this).val() * parseInt($($(this).parent().siblings()[4]).html()))
                $(".total").each(function () {
                    if ($($(this).siblings()[1]).children().attr("checked")) {
                        $all = $all + parseInt($(this).html());
                    }
                    
                });
                $("#all").html( $all);
                
                //当值改变时提交到服务器
                $.post("/foreground/Action/ShoppingCart.ashx", { type: "update", itemId: $($(this).parent().siblings()[0]).val(), count: $(this).val() }, function (jsObj) {
                    msgBox.showMsgInfo(jsObj.msg);
                }, "json");
            });
            $("#all").html( $all);

             
          //设置CheckBox点击事件
            $(".Check").click(function () {
              $i=0
                if ($(this).attr("checked")) {
                    //如果点击时是勾选
                    $("#all").html(parseInt($("#all").html()) + parseInt($($(this).parent().siblings()[5]).html()));
                    //遍历所有CheckBox 如果全都勾上了 那么全选的勾也会勾上
                    $(".Check").each(function () {
                        if ($(this).attr("checked"))
                        {
                            $i = $i + 1;

                        }
                    });
                    if ($i == $(".Check").length)
                    {
                        $("#CheckAll").attr("checked", "checked");
                    }
                }
                    //如果有一个CheckBox没勾上 那么全选CheckBox就会取消
                else {

                    $("#all").html(parseInt($("#all").html()) - parseInt($($(this).parent().siblings()[5]).html()));
                   
                        $("#CheckAll").attr("checked", "");
                  
                }
            });

            //设置数量只能输入数字
            $(".num").keypress(function (event) {
                if (event.keyCode < 48|| event.keyCode > 57) {

                    return false;
                }
            });

            //模态窗口"提示是否清空购物车"
            $("#dClear").dialog({
                autoOpen: false,
                width: 350,
                modal: true,
                buttons: {
                    "确 定": clear,
                    "取 消": function () {
                        $(this).dialog("close");
                    }
                }
            });

            //模态窗口"提示是否支付"
            $("#dCheckOut").dialog({
                autoOpen: false,
                width: 350,
                modal: true,
                buttons: {
                    "确 定 支 付": CheckOut,
                    "取 消": function () {
                        $(this).dialog("close");
                        $("#dCheckOut").html("");
                        $ProductName = "";
                    }
                }
            });
            
            //勾上全选CheckBox后 全部CheckBox 都会勾上 或者全取消
            $("#CheckAll").click(function () {
                if ($(this).attr("checked")) {
                    $(".Check").attr("checked", "checked");
                    $all = 0;
                    $(".total").each(function () {
                        
                            $all = $all + parseInt($(this).html());
                            $("#all").html($all)

                    });

                }
                else {
                    $(".Check").attr("checked", "");
                    $("#all").html(0)
                }

                
            });
            //点击支付后弹出模态窗口
            $("#CheckOut").click(function () {
                $a = 0;
                $(".Check").each(function () {
                    
                    if ($(this).attr("checked"))
                    {
                        $a = $a + 1;
                        $ProductName = $ProductName+"|" + $($(this).parent().siblings()[2]).html();
                        
                    }
                });
                if ($a == 0)
                {
                    alert("您没有选中任何商品")
                    return false;
                }
                $("#dCheckOut").html("您购买的商品是" + $ProductName + "<br />需要支付" + $("#all").html() + "元");
                $("#dCheckOut").dialog("open")
            });
            $("#clearAll").click(function () {
                $("#dClear").dialog("open");
            })
        });
        function CheckOut()
        {
            $("#dCheckOut").dialog("close");
            $num="";
            $product = "";
            $itemsid = "";
            $(".Check").each(function(){
                if($(this).attr("checked"))
                {
                    
                    $itemsid = $itemsid + "," + $($(this).parent().siblings()[0]).val();
                    $num=$num+ ","+$($(this).parent().siblings()[3]).children().val();
                    $product =$product + "," + $($(this).parent().siblings()[2]).html();
                    
                }
            });
            $.post("/foreground/Action/CheckOut.ashx",{num:$num,product:$product,itemsid:$itemsid,almost:$("#all").html()},function(jsObj){
                switch(jsObj.state)
                {
                    case "ok":
                        alert(jsObj.msg);
                        msgBox.showMsgOk(jsObj.msg);
                        window.location.reload();
                        break;
                    case "err":
                        alert(jsObj.msg);
                        msgBox.showMsgErr(jsObj.msg);
                        break;
                
            }
             },"json");
        }

        //设置每一条购物车数据的总价
        function Settotale()
        {
            $all = 0;
            $(".total").each(function () {
               
                $(this).html(parseInt($($(this).siblings()[4]).children().val()) * parseInt($($(this).siblings()[5]).html()));
                if ($($(this).parent().siblings()[1]).children().attr("checked")) {
                    $all = $all + parseInt($(this).html());
                }
                
                
                
            });
            $("#all").html($all);
            
        }

        function clear()
        {
            $.post("/foreground/Action/ShoppingCart.ashx?type=clearAll", null, function (jsObj) {
                switch (jsObj.state) {
                    case "ok":
                        msgBox.showMsgOk(jsObj.msg);
                        window.location.reload();
                        break;
                    case "err":
                        msgBox.showMsgErr(jsObj.msg);
                        break;
                }
            }, "json");
        }

        function clearOne(obj)
        {
           
            $row = $(obj).parent().parent();
            
            $.post("/foreground/Action/ShoppingCart.ashx?type=clearOne&itemId=" + obj.id, null, function (jsObj) {
                
                switch (jsObj.state)
                {
                    case "ok":
                        msgBox.showMsgOk(jsObj.msg);
                        $row.remove();
                        Settotale();
                        break;
                    case "err":
                        msgBox.showMsgErr(jsObj.err);
                        break;
                }
            }, "json");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
    <h1>购 物 车</h1>
        	<table style="width:700px" cellspacing="0" cellpadding="5">
                   	  <tr bgcolor="#ddd">
                             <th><input type="checkbox" id="CheckAll"  /></th>
                        	<th style="width:220px" align="left">图片 </th> 
                        	<th style="width:180px" align="left">商品名 </th> 
                       	  	<th style="width:100px" align="center">数量 </th> 
                        	<th style="width:40px" align="right">价钱 </th> 
                             
                        	<th style="width:60px" align="right">总价 </th> 
                             <th style="width:100px" align="center">时间</th>
                        	<th style="width:90px"> </th>
                            
                      </tr>
                <asp:Repeater runat="server" ID="rptCar">
                    <ItemTemplate>
                        
                        <tr id="<%#Eval("CPIdMODEL.pId") %>">
                            <input type="hidden" id="getId" value="<%#Eval("CItemId") %>"/>
                            <td><input type="checkbox" class="Check" /></td>
                        	<td><a href="ProductDetail.aspx?Id=<%#Eval("CPIdMODEL.pId") %>"><img src="<%#Eval("CPIdMODEL.pSrc") %>" alt="image 1" /></a></td> 
                        	<td><%#Eval("CPIdMODEL.PName") %> </td> 
                            <td align="center"><input type="text" value="<%#Eval("cCount")%>" id="num" class="num" style="width: 30px; text-align: right" /> </td>
                            <td align="right" id="price" class="price"><%#Eval("CPIdMODEL.pPrice") %> </td> 
                            <td align="right" id="total" class="total"><%#Eval("CPIdMODEL.pPrice")%></td>
                            <td align="center" ><%#Eval("CTime") %></td>
                            <td align="center"> <a href="javascript:void(0)" id="<%#Eval("CItemId") %>" onclick="clearOne(this)"><img src="/images/remove_x.gif" alt="remove" /><br />删除</a> </td>
						</tr>
                    </ItemTemplate>
                </asp:Repeater>
                    
                        <tr>
                        	<td colspan="3" align="right"  height="30px">Have you modified your basket? Please click here to <a href="#"><strong>Update</strong></a>&nbsp;&nbsp;</td>
                            <td align="right" style="background:#ddd; font-weight:bold"> 总 价 </td>
                            <td align="right" style="background:#ddd; font-weight:bold" id="all">$0 </td>
                            <td style="background:#ddd; font-weight:bold"> </td>
						</tr>
					</table>
                    <div style="float:right; width: 215px; margin-top: 20px;">
                    
					<p><a href="javascript:void(0)" id="CheckOut"><img src="/images/buy.png" width="150"/></a></p>
                        <p><a href="javascript:void(0)" id="clearAll">清空购物车</a></p>
                    <p><a href="Products.aspx">继续购物</a></p>
                    	<div id="dClear">
                            是否清空购物车(请慎重)
                    	</div>
                        <div id="dCheckOut">
                           
                    	</div>
                    </div>


</asp:Content>
