using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;


namespace Lanwei.Weixin.UI.baseSet
{
    public partial class UsersListRight : BasePage
    {
        public UserDAL dal = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //if (CheckPower("UserListPowerSet"))
                //{
                //    MessageBox.Show(this, "无此操作权限！");
                //    return;
                //}


                this.xmlDS.DataFile = "../xml/Menu_Admin.xml";

                this.Bind();


            }
        }

        public void Bind()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            this.hf.Value = id.ToString();


            this.showTreeFromDatabase();
            


            //自动打钩


            this.SetNodeSelected(null);




        }


        

        #region-----------所有权限树--------绑定数据库-------在右边------------



        protected void showTreeFromDatabase()
        {
            this.TreeViewRight.Nodes.Clear();//清除节点
            TreeNode node = new TreeNode();//实例化树节点对象
            node.Text = "蓝微·云ERP系统";//添加节点文本
            node.Value = "1";//添加节点值
            node.NavigateUrl = "Lanwei";
            this.TreeViewRight.Nodes.Add(node);//将节点对象的属性添加到TreeView控件
            showTreeViewFromDatabase(node);

        }

        protected void showTreeViewFromDatabase(TreeNode node)//添加根节点
        {

            string Id = node.Value.ToString();//调用节点的值
            List<PageInfo> list = dal.GetAllPages(Id);//通过父节点的值查出相应的子节点名称（类别名称）
            foreach (PageInfo ti in list)
            {
                TreeNode tn = new TreeNode();//实例化树节点对象
                tn.Text = ti.Title;// +ti.Url;//设置节点文本
                tn.Value = ti.Id.ToString();//设置节点值
                tn.NavigateUrl = ti.Url;

                node.ChildNodes.Add(tn);//将上一级节点作为父节点
                showTreeViewFromDatabase(tn);//递归调用
            }
        }


        #endregion


        #region 设置现有的权限为---------选中状态
        /// <summary>
        /// 设置现有的权限为---------选中状态？
        /// </summary>
        /// <param name="pNode"></param>
        private void SetNodeSelected(TreeNode pNode)
        {
            int userId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            if (pNode == null)
            {
                foreach (TreeNode node in this.TreeViewRight.Nodes)
                {

                    bool has = dal.CheckPowerByUserIdAndUrl(userId, node.NavigateUrl.ToString());

                    if (has == true)
                    {
                        node.Checked = true;

                    }
                    else
                    {
                        node.Checked = false;
                    }

                    SetNodeSelected(node);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.ChildNodes)
                {

                    bool has = dal.CheckPowerByUserIdAndUrl(userId, node.NavigateUrl.ToString());

                    if (has == true)
                    {
                        node.Checked = true;

                    }
                    else
                    {
                        node.Checked = false;
                    }

                    SetNodeSelected(node);
                }

            }

        }
        #endregion


        #region 判断是否有选择权限？
        /// <summary>
        /// 判断是否有选择权限？
        /// </summary>
        /// <param name="pNode"></param>
        private void SelectedNode(TreeNode pNode)
        {
            int userId = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());

            if (pNode == null)
            {
                foreach (TreeNode node in this.TreeViewRight.Nodes)
                {


                    bool test = dal.CheckPowerByUserIdAndUrl(userId, node.NavigateUrl.ToString());


                    if (node.Checked == true)
                    {
                        selected = true;

                    }


                    SelectedNode(node);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.ChildNodes)
                {

                    bool test = dal.CheckPowerByUserIdAndUrl(userId, node.NavigateUrl.ToString());

                    if (node.Checked == true)
                    {
                        selected = true;
                    }


                    SelectedNode(node);
                }

            }

        }
        #endregion



        #region 遍历----插入权限到数据库

        bool selected = false;
        private void GetSelectedNode(TreeNode pNode)
        {
            if (pNode == null)
            {
                foreach (TreeNode node in this.TreeViewRight.Nodes)
                {
                    if (node.Checked == true)
                    {


                        int pageId = ConvertTo.ConvertInt(node.Value.ToString());
                        string url = node.NavigateUrl.ToString();
                        string title = node.Text.ToString();

                        int seq = ConvertTo.ConvertInt(node.ToolTip.ToString());

                        int parentId = 0;
                        int temp = dal.AddUsersPageList(ConvertTo.ConvertInt(this.hf.Value.ToString()), pageId, url, title, parentId, seq);

                    }
                    GetSelectedNode(node);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.ChildNodes)
                {
                    if (node.Checked == true)
                    {
                        int pageId = ConvertTo.ConvertInt(node.Value.ToString());
                        string url = node.NavigateUrl.ToString();
                        string title = node.Text.ToString();

                        int parentId = ConvertTo.ConvertInt(node.Parent.Value);
                        int seq = ConvertTo.ConvertInt(node.ToolTip.ToString());

                        int temp = dal.AddUsersPageList(ConvertTo.ConvertInt(this.hf.Value.ToString()), pageId, url, title, parentId, seq);
                    }
                    GetSelectedNode(node);
                }

            }
        }

        #endregion



        protected void btnSave_Click(object sender, EventArgs e)
        {

            //if (!CheckPower("UserListPowerSet"))
            //{
            //    MessageBox.Show(this, "无此操作权限！");
            //    return;
            //}



            this.SelectedNode(null);

            if (selected == false)
            {
                MessageBox.Show(this, "请选择操作权限！");
                return;
            }
            else
            {
                int temp = dal.DeletePageListByUserId(ConvertTo.ConvertInt(this.hf.Value.ToString()));
                this.GetSelectedNode(null);

                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId;
                logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "设置用户权限";
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                MessageBox.Show(this, "操作成功！");


            }

        }

        protected void btnTongbu_Click(object sender, EventArgs e)
        {


            dal.TruncateTablePageList();

            this.InsertIntoPageList(null);

            LogsDAL logs = new LogsDAL();
            logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
            logs.Events = "同步权限菜单";
            logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            logs.Add();

            this.Bind();



        }

        #region 遍历----插入数据库-------------所有的xml 节点同步到数据库


        /// <summary>
        /// 遍历----插入数据库-------------所有的xml 节点同步到数据库
        /// </summary>
        /// <param name="pNode"></param>
        private void InsertIntoPageList(TreeNode pNode)
        {
            if (pNode == null)
            {
                foreach (TreeNode node in this.tvListXML.Nodes)
                {
                    int pageId = ConvertTo.ConvertInt(node.Value.ToString());
                    string url = node.NavigateUrl.ToString();
                    string title = node.Text.ToString();

                    int seq = ConvertTo.ConvertInt(node.ToolTip.ToString());

                    int parentId = 0;
                    int temp = dal.AddPageList(pageId, url, title, parentId, seq);

                    InsertIntoPageList(node);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.ChildNodes)
                {
                    int pageId = ConvertTo.ConvertInt(node.Value.ToString());
                    string url = node.NavigateUrl.ToString();
                    string title = node.Text.ToString();

                    int parentId = ConvertTo.ConvertInt(node.Parent.Value);
                    int seq = ConvertTo.ConvertInt(node.ToolTip.ToString());

                    int temp = dal.AddPageList(pageId, url, title, parentId, seq);

                    InsertIntoPageList(node);
                }

            }
        }

        #endregion
    }
}
