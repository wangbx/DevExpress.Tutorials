namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class TreeViewAdapter
    {
        private TreeView navigationTreeView;

        public TreeViewAdapter(TreeView navigationTreeView)
        {
            this.navigationTreeView = navigationTreeView;
        }

        private TreeNode AddNode(int id, int parentId, string nodeText)
        {
            TreeNode node = this.LocateTreeNodeById(this.navigationTreeView.Nodes, parentId);
            TreeNode node2 = new TreeNode(nodeText) {
                Tag = id
            };
            if (node == null)
            {
                this.navigationTreeView.Nodes.Add(node2);
                return node2;
            }
            node.Nodes.Add(node2);
            return node2;
        }

        public void AddNode(int id, int parentId, string nodeText, object nodeImage)
        {
            this.SetNodeImage(this.AddNode(id, parentId, nodeText), nodeImage);
        }

        private Image GetImageByString(string nodeImage)
        {
            if (nodeImage != string.Empty)
            {
                string filename = FilePathUtils.FindFilePath(nodeImage, true);
                if (filename != string.Empty)
                {
                    return Image.FromFile(filename);
                }
            }
            return null;
        }

        public int GetSelectedModuleId()
        {
            if (this.navigationTreeView.SelectedNode == null)
            {
                return -1;
            }
            return Convert.ToInt32(this.navigationTreeView.SelectedNode.Tag);
        }

        private TreeNode LocateTreeNodeById(TreeNodeCollection nodes, int nodeId)
        {
            foreach (TreeNode node in nodes)
            {
                if (Convert.ToInt32(node.Tag) == nodeId)
                {
                    return node;
                }
                TreeNode node2 = this.LocateTreeNodeById(node.Nodes, nodeId);
                if (node2 != null)
                {
                    return node2;
                }
            }
            return null;
        }

        private void SetNodeImage(TreeNode node, object nodeImage)
        {
            Image imageByString = null;
            if (nodeImage is string)
            {
                imageByString = this.GetImageByString(nodeImage.ToString());
            }
            if (nodeImage is Image)
            {
                imageByString = nodeImage as Image;
            }
            if (imageByString != null)
            {
                int num = this.navigationTreeView.ImageList.Images.Add(imageByString, Color.Transparent);
                node.ImageIndex = num;
                node.SelectedImageIndex = num;
            }
            else
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }
        }
    }
}

