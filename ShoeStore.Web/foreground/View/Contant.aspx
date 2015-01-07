<%@ Page Title="" Language="C#" MasterPageFile="~/foreground/View/Common.Master" AutoEventWireup="true" CodeBehind="Contant.aspx.cs" Inherits="ShoeStore.Web.foreground.View.Contant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#submit").click(submit);
            $("#phone").keypress(keypress);
        })
        
        function keypress()
        {
            
            if (event.keyCode < 48 || event.keyCode > 57) {
                
                return false;
            }
           
        }
        function submit()
        {
            
            
            if ($("#author").val() == "")
            {
                
                msgBox.showMsgErr("请输入名字");
                $("#author").focus();
                
                return false;
            }
            if ($("#email").val() == "") {
                msgBox.showMsgErr("请输入邮箱");
                
                $("#email").focus();
                return false;
            }
            if ($("#phone").val() == "") {
                msgBox.showMsgErr("请输入手机号码");
                
                $("#phone").focus();
                return false;
            }
            if ($("#text").val() == "") {
                msgBox.showMsgErr("请输入留言内容");
                
                $("#text").focus();
                return false;
            }
            
            var data = $("#contact").serialize();
       
            $.post("/foreground/Action/ContantUs.ashx", data, function (jsObj) {
                alert(jsObj.msg);
                
                $("#author").val("");
                $("#email").val("");
                $("#phone").val("");
                $("#text").val("");
            }, "json");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeRight" runat="server">
    <div id="content" class="float_r">
        	<h1>联系我们</h1>
            <div class="content_half float_l">
                <p>Etiam eget leo nisl. Morbi magna enim, lobortis vitae condimentum eu, ultrices a lacus.</p>
                <div id="contact_form">
                   <form method="post" name="contact" action="#" id="contact" >
                        <input type="hidden" name="IsPostBack" value="true" />
                        <label for="author">Name:</label> <input type="text" id="author" name="author" class="required input_field" />
                        <div class="cleaner h10"></div>
                        <label for="email">Email:</label> <input type="text" id="email" name="email" class="validate-email required input_field" />
                        <div class="cleaner h10"></div>
                        
                        <label for="phone">Phone:</label><input type="text" name="phone" id="phone" class="input_field"    />
                        <div class="cleaner h10"></div>
        
                        <label for="text">Message:</label> <textarea id="text" name="text" rows="0" cols="0" class="required"></textarea>
                        <div class="cleaner h10"></div>
                        
                        <input type="button" class="submit_btn" name="submit" id="submit"  value="Send" />
                        
                    </form>
                </div>
            </div>
        <div class="content_half float_r">
        	<h5>Primary Office</h5>
			660-110 Quisque diam at ligula, <br />
			Etiam dictum lectus quis, 11220<br />
			Sed mattis mi at sapien<br /><br />
						
			Phone: 010-010-6600<br />
			Email: <a href="#">info@yourcompany.com</a><br/>
			
            <div class="cleaner h40"></div>
			
            <h5>Secondary Office</h5>
			120-360 Cras ac nunc laoreet,<br />
			Nulla vitae leo ac dui, 10680<br />
			Cras id sem nulla<br /><br />
			
			Phone: 030-030-0220<br />
			Email: <a href="#">contact@yourcompany.com</a><br/>
			<br />
            Validate <a href="#" rel="nofollow">XHTML</a> &amp; <a href="#" rel="nofollow">CSS</a>
        </div>
        
        <div class="cleaner h40"></div>
        
            
        </div> 
</asp:Content>
