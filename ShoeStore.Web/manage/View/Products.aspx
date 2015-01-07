<%@ Page Title="" Language="C#" MasterPageFile="~/manage/View/common.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ShoeStore.Web.manage.View.Products" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">

      <script type="text/javascript">
          var targetUrl = "/manage/Action/Products.ashx";
          //当前正在被操作的 行dom对象
          var $nowRow = null;

          $(function () {
              //1.为新增按钮绑定方法
              $("#btnAdd").click(showAdd);
              //2.初始化 新增 和修改面板
              $("#dialogModify").dialog({
                  autoOpen: false,
                  width: 500,
                  modal: true,
                  
              });
          })

          function showAdd() {
              //清空 文本框
              $("#PName").val("");
              $("#PImgSrc").val("");
              $("#PSort").val("");
              $("#MId").val("");
              $("#PRemark").val("");
              $("#imgPre").attr("src","")
              $("#PPrice").val("");
              $("#PNum").val("");
              $("#type").val("a")


              $("#dialogModify").dialog('open');
          };



 

          
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
                      $("#type").val("m");
                      $("#PName").val(jsObj.data.PName);
                      $("#PImgSrc").val(""); 
                      $("#PSort").val(jsObj.data.PSort);
                      $("#MId").val(jsObj.data.PId);
                      $("#PCateid").val(jsObj.data.PCateid).attr("selected", "selected");
                      if (jsObj.data.PIsrec == true) {
                          $("#RecTrue").attr("selected", "selected")
                      }else
                      {
                          $("#RecFalse").attr("selected", "selected")
                      }
                      
                      $("#PRemark").val(jsObj.data.PRemark);
                      $("#PPrice").val(jsObj.data.PPrice);
                      $("#PNum").val(jsObj.data.PNum);
                      $("#imgPre").attr("src", jsObj.data.PSrc+"?i="+Math.random())
                      $("#Isrc").val(jsObj.data.PSrc);
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
    <tr id="trNav" ><th colspan="8">当前正在：【商品列表】管理</th><th><input type="button" id="btnAdd" value="新增" /></th></tr>
    <tr>
        <th>商品名</th>
        <th>商品分类</th>
        <th>商品图片</th>
        <th>商品价格</th>
        <th>商品数量</th>
        <th>商品介绍</th>
        <th>是否推荐</th>
        <th>商品排序</th>
        <th style="width:100px;">操作</th>
    </tr>
    <!--循环生成行-->
    <asp:Repeater runat="server" ID="rptList">
        <ItemTemplate>
            <tr>
            
            <td><%#Eval("PName")%></td>
            <td><%#Eval("PCateidMODEL.PName")%></td>
            <td><img name="images" height="80px" src="<%#Eval("PSrc") %>" /></td>
            <td><%#Eval("PPrice")%></td>
             <td><%#Eval("PNum")%></td>
             <td><%#Eval("PRemark")%></td>
             <td><%#Eval("PIsrec")%></td>
            <td><%#Eval("PSort")%></td>
            <td>
                <a href="javascript:void(0)" onclick="doDel(<%#Eval("PId")%>,this)">删</a> 
                <a href="javascript:void(0)" onclick="showEditPanel(<%#Eval("PId")%>,this)">改</a>
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
<!--01.窗口-->
<div id="dialogModify" title="商品操作" class="dialogPanel">
    <form id="fModify" action="../Action/Products.ashx" method="post" enctype="multipart/form-data">
    <input type="hidden" id="MId" name="MId" />
    <input type="hidden" id="Isrc" name="Isrc" />
        <input type="hidden" id="type" name="type" />
        <input type="hidden" name="PageIndex" value="<%=pageIndex %>" />
    <table id="tbModify">
        <tr>
            <td class="td">商品名</td>
            <td colspan="2"><input type="text" id="PName" name="PName" /></td>
        </tr>
        <tr>
            <td class="td">产品分类</td>
            <td colspan="2">
                <select id="PCateid" name="PCateid">
                  <%=sbhtml.ToString() %>
                </select>
            </td>
        </tr>
        <tr id="trImg">
            <td class="td">产品图片</td>
            <td colspan="2"><input type="file" id="PImgSrc" name="PImgSrc" /><img id="imgPre" /></td>
            
        </tr>
        <tr>
            <td class="td">排序</td>
            <td colspan="2"><input type="text" id="PSort" name="PSort" value="10001"/></td>
        </tr>
        <tr>
            <td class="td">描述</td>
            <td colspan="2">
                <textarea id="PRemark" name="PRemark"></textarea>
            </td>
        </tr>
                <tr>
            <td class="td">价格</td>
            <td colspan="2">
                <input type="text" id="PPrice" name="PPrice" />
            </td>
        </tr>
                 <tr>
            <td class="td">数量</td>
            <td colspan="2">
                <input type="text" id="PNum" name="PNum" />
            </td>
        </tr>
                 <tr>
            <td class="td">是否推荐</td>
            <td colspan="2">
                <select id="IsRec" name="IsRec">
                    <option value="false" id="RecFalse">否</option>
                    <option value="true" id="RecTrue">是</option>
                    
                </select>
            </td>
        </tr>
        <tr><td></td><td colspan="2"><input type="submit" value="保存" /></td></tr>
    </table>
    </form>
</div>
<!--模态窗口 End-->
</asp:Content>
