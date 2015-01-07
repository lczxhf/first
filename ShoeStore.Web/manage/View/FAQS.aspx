<%@ Page Title="" Language="C#" MasterPageFile="~/manage/View/common.Master" AutoEventWireup="true" CodeBehind="FAQS.aspx.cs" Inherits="ShoeStore.Web.manage.View.FAQS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="placeHead" runat="server">
      <script type="text/javascript">
          var targetUrl = "/manage/Action/FAQS.ashx";
          //当前正在被操作的 行dom对象
          var $nowRow = null;

          $(function () {
              //1.为新增按钮绑定方法
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
          })

          function showAdd() {
              //清空 文本框
              $("#Question").val("");
              $("#Answer").val("");
              $("#MId").val("");
              $("#Sort").val("");
              $("#dialogModify").dialog('open');
          };


          //2.确定
          function doAdd() {
              //新增操作
              if ($("#MId").val() == "") {
                  //2.1获取要提交的数据并验证
                  var data = $("#fModify").serialize();
                  //2.2将数据提交到服务器的数据库里 保存
                  $.post(targetUrl, data + "&type=a", function (jsObj) {
                      processData(jsObj, function () {
                          msgBox.showMsgOk(jsObj.msg);
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
                          $tds[1].innerHTML = jsObj.data.Question;
                          $tds[2].innerHTML = jsObj.data.Answer;
                          $tds[3].innerHTML = jsObj.data.Sort;
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

                      $("#Question").val(jsObj.data.Question);
                      $("#Answer").val(jsObj.data.Answer);
                      $("#MId").val(jsObj.data.PId);
                      $("#Sort").val(jsObj.data.fSort);
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
    <tr id="trNav"><th colspan="4">当前正在：【常见问题】管理</th><th><input type="button" id="btnAdd" value="新增" /></th></tr>
    <tr>
        <th>Id</th>
        <th>问 题</th>
        <th>回 答</th>
        <th>排 序</th>
        <th style="width:100px;">操作</th>
    </tr>
    <!--循环生成行-->
    <asp:Repeater runat="server" ID="rptQuestion">
        <ItemTemplate>
            <tr>
                <th>Id</th>
            <td><%#Eval("Question")%></td>
            <td><%#Eval("Answer")%></td>
            <td><%#Eval("fSort")%></td>
          
            
            <td>
                <a href="javascript:void(0)" onclick="doDel(<%#Eval("PId")%>,this)">删</a> 
                <a href="javascript:void(0)" onclick="showEditPanel(<%#Eval("PId")%>,this)">改</a>
            </td>
        </tr>
        </ItemTemplate>
    </asp:Repeater>
    <!--循环生成行-->
</table>

    <!--模态窗口 Begin-->
<!--01.修改窗口-->
<div id="dialogModify" title="常见问题管理" class="dialogPanel">
    <form id="fModify">
    <input type="hidden" id="MId" name="MId" />
    <table id="tbModify">
        <tr>
            <td>问 题:</td>
            <td><input type="text" id="Question" name="Question" /></td>
        </tr>
        <tr>
            <td>回 答：</td>
            <td><input type="text" id="Answer" name="Answer" /></td>
        </tr>
        <tr>
            <td>排 序：</td>
            <td><input type="text" id="Sort" name="Sort" /></td>
        </tr>
       
    </table>
    </form>
</div>
</asp:Content>
