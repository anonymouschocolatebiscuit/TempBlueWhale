using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace BlueWhale.UI.baseSet
{
    public partial class UsersListRight : BasePage
    {
        public UserDAL dal = new UserDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.xmlDS.DataFile = "../xml/Menu_Admin.xml";
                this.Bind();
            }
        }

        public void Bind()
        {
            int id = ConvertTo.ConvertInt(Request.QueryString["id"].ToString());
            this.hf.Value = id.ToString();
            this.showTreeFromDatabase();

            // Automatically check the boxes
            this.SetNodeSelected(null);
        }

        #region

        protected void showTreeFromDatabase()
        {
            this.TreeViewRight.Nodes.Clear(); // Clear nodes
            TreeNode node = new TreeNode(); // Instantiate tree node object
            node.Text = "Blue Whale ERP"; // Add node text
            node.Value = "1"; // Add node value
            node.NavigateUrl = "Lanwei";
            this.TreeViewRight.Nodes.Add(node); // Add node object to TreeView control
            showTreeViewFromDatabase(node);
        }

        protected void showTreeViewFromDatabase(TreeNode node) // Add root node
        {
            string Id = node.Value.ToString(); // Call node value
            List<PageInfo> list = dal.GetAllPages(Id); // Get child nodes by parent node value
            foreach (PageInfo ti in list)
            {
                TreeNode tn = new TreeNode(); // Instantiate tree node object
                tn.Text = ti.Title; // Set node text
                tn.Value = ti.Id.ToString(); // Set node value
                tn.NavigateUrl = ti.Url;

                node.ChildNodes.Add(tn); // Add the parent node to child node
                showTreeViewFromDatabase(tn); // Recursive call
            }
        }

        #endregion

        #region Set current permissions to selected status
        /// <summary>
        /// Set current permissions to selected status?
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

        #region Check if permissions are selected?
        /// <summary>
        /// Check if permissions are selected?
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

        #region Traverse and insert permissions to the database

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
            this.SelectedNode(null);

            if (selected == false)
            {
                MessageBox.Show(this, "Please select the operation permissions!");
                return;
            }
            else
            {
                int temp = dal.DeletePageListByUserId(ConvertTo.ConvertInt(this.hf.Value.ToString()));
                this.GetSelectedNode(null);

                LogsDAL logs = new LogsDAL();
                logs.ShopId = LoginUser.ShopId;
                logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                logs.Events = "Set User Permissions";
                logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                MessageBox.Show(this, "Execution successful!");
            }
        }

        protected void btnTongbu_Click(object sender, EventArgs e)
        {
            dal.TruncateTablePageList();
            this.InsertIntoPageList(null);

            LogsDAL logs = new LogsDAL();
            logs.ShopId = LoginUser.ShopId;
            logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
            logs.Events = "Sync Permission Menu";
            logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            logs.Add();

            this.Bind();
        }

        #region Traverse and insert database -- sync all XML nodes to database

        /// <summary>
        /// Traverse and insert into database -- sync all XML nodes to database
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
