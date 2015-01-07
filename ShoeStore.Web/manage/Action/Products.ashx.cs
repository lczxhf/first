using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// Products 的摘要说明
    /// </summary>
    public class Products : BaseHandler
    {
        BLL.Product bllproduct = new BLL.Product();
        HttpPostedFile File = null;
        public override void SonLoad()
        {
            string strType = context.Request["type"];
            switch (strType)
            {
                case "get":
                    GetMenu();
                    break;
                case "a":
                    DoAdd();
                    break;
                case "m":
                    DoModify();
                    break;
                case "del":
                    DoDel();
                    break;
            }
        }
        #region 1.0 新增菜单 -void DoAdd()
        /// <summary>
        /// 新增菜单
        /// </summary>
        private void DoAdd()
        {
            check();
            File = context.Request.Files["PImgSrc"];
            if (!File.ContentType.Contains("image/"))
            {
                Common.PageHelper.WriteJsMsg("只能上传图片", "/manage/View/Products.aspx");
                context.Response.End();
            }
            if (bllproduct.ProductNameIsExist(context.Request["PName"]) == true)
            {
                Common.PageHelper.WriteJsMsg("此商品名已存在", "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
            }
            //1.2 生成 新的图片名
            string strNewImgName =Common. FileHelper.NewFileName(File.FileName);
            Common.FileHelper.MakeThumbImg(File, strNewImgName, "/images/product", 200, 150);
            Common.FileHelper.MakeThumbImg(File, strNewImgName, "/images/product/rough", 66, 66);
            MODEL.Product product = new MODEL.Product()
            {
                PCateid = Convert.ToInt32(context.Request["PCateid"]),
                PIsrec = Boolean.Parse(context.Request["IsRec"]),
                  PName=context.Request["PName"],
                   PNum=Convert.ToInt32(context.Request["PNum"]),
                    PPrice=decimal.Parse(context.Request["PPrice"]),
                     PRemark=context.Request["PRemark"],
                      PSort=Convert.ToInt32(context.Request["PSort"]),
                       PSrc="/images/product/"+strNewImgName

            };
            try
            {
                //2.调用业务层对象 提交
                 bllproduct. Add(product);

                 Common.PageHelper.WriteJsMsg("新增成功", "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
            }
            catch (Exception ex)
            {
                Common.PageHelper.WriteJsMsg("异常:" + ex.Message, "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
            }
        }
        #endregion

        #region 2.0 根据id获取 菜单 -void GetMenu()
        /// <summary>
        /// 2.0 根据id获取 菜单
        /// </summary>
        private void GetMenu()
        {
            string strId = context.Request["mid"];
            if (!Common.ValidateHelper.IsNum(strId))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "参数异常~~");
                context.Response.End();
            }

            try
            {
                MODEL.Product product = bllproduct.GetModel(Convert.ToInt32(strId));
                int s = product.PSrc.LastIndexOf('/');
                product.PSrc= product.PSrc.Insert(s+1, "rough/");
                //在服务端 将查询到的对象 转成 json字符串
                string strJson = Common.DataHelper.Obj2Json(product);
                //组成 规定格式 json数据 返回浏览器
                Common.AjaxMsgHelper.AjaxMsg("ok", "加载成功~", strJson);
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        private void DoModify()
        {
            check();
            File = context.Request.Files["PImgSrc"];

            if (!File.ContentType.Contains("image/")&&File.ContentLength!=0)
            {
                Common.PageHelper.WriteJsMsg("只能上传图片", "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
                context.Response.End();
            }
            //验证图片大小
            if ((File.ContentLength <= 2 || File.ContentLength > 4 * 1024 * 1024)&&File.ContentLength!=0)
            {
                Common.PageHelper.WriteJsMsg("图片太大或太小(只能小于4M)", "/manage/View/Products.aspx?pageIndex="+context.Request["pageIndex"]);
                context.Response.End();
            }
            
            string strOldSrc = "";
            if (context.Request.Files["PImgSrc"]!=null)
            {
            File = context.Request.Files["PImgSrc"];


            if (File.ContentLength > 0)
            {
                //*2.1获取原来的图片路径
                //*2.1.1去数据库查询要删除的原来的图片
                strOldSrc = bllproduct.GetModel(int.Parse(context.Request["MId"])).PSrc;
                Common.FileHelper.MakeThumbImg(File, strOldSrc, "", 200, 150);
                Common.FileHelper.MakeThumbImg(File, strOldSrc, "", 66, 66);

            }
            else
            {
                int a = context.Request["Isrc"].ToString().LastIndexOf('/');
                strOldSrc = context.Request["Isrc"].ToString().Remove(a - 6, 6);

            }
            }
            
            BLL.ProductCate bllcate = new BLL.ProductCate();
            string cateName = bllcate.GetCateName(Convert.ToInt32(context.Request["PCateid"]));
            MODEL.ProductCate cate = new MODEL.ProductCate() { PName = cateName };
            
                MODEL.Product product = new MODEL.Product()
              {   PCateidMODEL=cate,
                  PCateid = Convert.ToInt32(context.Request["PCateid"]),
                  PIsrec = Boolean.Parse(context.Request["IsRec"]),
                  PName = context.Request["PName"],
                  PNum = Convert.ToInt32(context.Request["PNum"]),
                  PPrice = decimal.Parse(context.Request["PPrice"]),
                  PRemark = context.Request["PRemark"],
                  PSort = Convert.ToInt32(context.Request["PSort"]),
                  PSrc = strOldSrc,
                   PId=Convert.ToInt32(context.Request["MId"])
              };

                try
                {
                    bllproduct.Update(product);
                  
                    Common.PageHelper.WriteJsMsg("修改成功", "/manage/View/Products.aspx");
                }
                catch (Exception ex)
                {
                    Common.PageHelper.WriteJsMsg("异常"+ex.Message, "/manage/View/Products.aspx");
                }
            }
        
        #endregion


        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        private void DoDel()
        {
            if (bllproduct.Del(context.Request["mid"]) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "删除成功~");
            }
        }
        #endregion

        void check()
        {
            if (string.IsNullOrEmpty(context.Request["Pname"]))
            {
                Common.PageHelper.WriteJsMsg("商品名字不能为空","/manage/View/Products.aspx?pageIndex="+context.Request["pageIndex"]);
                context.Response.End();
            }
            if (!Common.ValidateHelper.IsNum(context.Request["PSort"]))
            {
                Common.PageHelper.WriteJsMsg("序列号只能为数字", "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
                context.Response.End();
            }
            if (string.IsNullOrEmpty(context.Request["PRemark"]))
            {
                Common.PageHelper.WriteJsMsg("商品描述不能为空", "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
                context.Response.End();
            }
            if (!Common.ValidateHelper.IsNum(context.Request["PPrice"]))
            {
                Common.PageHelper.WriteJsMsg("商品价格只能为数字", "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
                context.Response.End();
            }
            if (!Common.ValidateHelper.IsNum(context.Request["PNum"]))
            {
                Common.PageHelper.WriteJsMsg("商品数量只能为数字", "/manage/View/Products.aspx?pageIndex=" + context.Request["pageIndex"]);
                context.Response.End();
            }
            


        }
    }
}