using System;
using System.IO;
using System.Windows.Forms;
using TRPG;

namespace TRPG_Editor
{
    public partial class Form1 : Form
    {
        private DataPack datapack;
        private TreeNode mySelectedNode;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            datapack = new DataPack(Directory.GetCurrentDirectory());
            datapack.Load();
            buildItemsTree();
        }

        private void buildItemsTree()
        {
            foreach (Item i in datapack.ItemsMaster)
            {
                TreeNode n = new TreeNode();
                n.Text = i.Name;
                n.Nodes.Add(new TreeNode("Lore: " + i.Lore));
                n.Nodes.Add(new TreeNode("Weight: " + Convert.ToString(i.Weight)));
                n.Nodes.Add(new TreeNode("Value: " + Convert.ToString(i.Value)));
                n.Nodes.Add(new TreeNode("Damage: " + Convert.ToString(i.Damage)));
                n.Nodes.Add(new TreeNode("Defense: " + Convert.ToString(i.Defense)));
                n.Nodes.Add(new TreeNode("Accuracy: " + Convert.ToString(i.Accuracy)));
                n.Nodes.Add(new TreeNode("Health: " + Convert.ToString(i.Health)));
                n.Nodes.Add(new TreeNode("Uses: " + Convert.ToString(i.Uses)));
                n.Nodes.Add(new TreeNode("Experience: " + Convert.ToString(i.Experience)));

                treeViewItems.Nodes.Add(n);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            mySelectedNode = treeViewItems.GetNodeAt(e.X, e.Y);
            if (mySelectedNode != null)
            {
                treeViewItems.SelectedNode = mySelectedNode;
                treeViewItems.LabelEdit = true;
                if (!mySelectedNode.IsEditing)
                {
                    mySelectedNode.BeginEdit();
                }
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        // Stop editing without canceling the label change.
                        e.Node.EndEdit(false);
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        MessageBox.Show("Invalid tree node label.\n" +
                           "The invalid characters are: '@','.', ',', '!'",
                           "Node Label Edit");
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and
                       place the node in edit mode again. */
                    e.CancelEdit = true;
                    MessageBox.Show("Invalid tree node label.\nThe label cannot be blank",
                       "Node Label Edit");
                    e.Node.BeginEdit();
                }
            }
        }
    }
}