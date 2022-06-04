using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PchmiLab2.Win
{
    using System.Linq;
    using Core;

    public partial class MainForm : Form
    {
        private readonly HashSet<TreeNode> _filledNodes;
        private readonly LabApplication _labApplication;

        public MainForm()
        {
            InitializeComponent();
            _filledNodes = new HashSet<TreeNode>();

            InitTreeView(SourceTreeView);
            InitTreeView(DestTreeView);

            _labApplication = new LabApplication();
        }

        private void InitTreeView(TreeView treeView)
        {
            treeView.Nodes.Clear();
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var driveNode = new TreeNode(drive);
                FillNode(driveNode, true);
                treeView.Nodes.Add(driveNode);
                _filledNodes.Add(driveNode);
            }
        }

        private static void FillNode(TreeNode node, bool fillChildFolder=false)
        {
            try
            {
                node.Nodes.Clear();
                var existingChild = node.Nodes
                    .Cast<TreeNode>()
                    .Select(n => n.Name)
                    .ToHashSet();
                var directoryInfo = new DirectoryInfo(GetNodeFullPath(node));

                foreach (var directoryName in directoryInfo
                    .GetDirectories()
                    .Select(d => d.Name)
                    .Where(n => !existingChild.Contains(n)))
                {
                    var newNode = new TreeNode(directoryName);
                    node.Nodes.Add(newNode);
                    if (fillChildFolder)
                    {
                        FillNode(newNode);
                    }
                }

                foreach (var fileName in directoryInfo
                    .GetFiles()
                    .Select(d => d.Name)
                    .Where(n => !existingChild.Contains(n)))
                {
                    node.Nodes.Add(new TreeNode(fileName));
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static void FillChildFolderNodes(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                var nodeFullPath = GetNodeFullPath(node);
                if (!Directory.Exists(nodeFullPath))
                    continue;
                FillNode(child);
            }
        }

        private static string GetNodeFullPath(TreeNode node)
        {
            return node.Parent is null
                ? node.Text
                : Path.Join(GetNodeFullPath(node.Parent), node.Text);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            try
            {
                var foldersToCopy = SourceTreeView.Nodes
                    .Cast<TreeNode>()
                    .SelectMany(GetCheckedNodes)
                    .Select(GetNodeFullPath);

                var destNode = DestTreeView.SelectedNode;
                var destFolder = GetNodeFullPath(destNode);

                _labApplication.CopyDirs(destFolder, foldersToCopy);
                FillNode(destNode);

                var sourceNode = GetTreeNodeByPath(SourceTreeView, destFolder);
                FillNode(sourceNode);
            }
            catch (Exception)
            {

            }
        }

        private static IEnumerable<TreeNode> GetCheckedNodes(TreeNode node)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                if (subNode.Checked)
                    yield return subNode;

                var checkedChildren = GetCheckedNodes(subNode);
                foreach (var checkedNode in checkedChildren)
                    yield return checkedNode;
            }
        }

        private static TreeNode GetTreeNodeByPath(TreeView treeView, string path)
        {
            TreeNode curNode = null;
            var curOptions = treeView.Nodes.Cast<TreeNode>();

            foreach (var curNodeName in path.Split(Path.DirectorySeparatorChar))
            {
                curNode = curOptions.Single(n => n.Text.Replace('\\', ' ').Trim() == curNodeName);
                curOptions = curNode.Nodes.Cast<TreeNode>();
            }

            return curNode;
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            FillChildFolderNodes(e.Node);
        }

        private void DestTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            FillChildFolderNodes(e.Node);
        }
    }
}