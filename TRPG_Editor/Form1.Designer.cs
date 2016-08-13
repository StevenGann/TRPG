namespace TRPG_Editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewItems = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeViewItems
            // 
            this.treeViewItems.LabelEdit = true;
            this.treeViewItems.Location = new System.Drawing.Point(12, 12);
            this.treeViewItems.Name = "treeViewItems";
            this.treeViewItems.Size = new System.Drawing.Size(704, 504);
            this.treeViewItems.TabIndex = 0;
            this.treeViewItems.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeViewItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 528);
            this.Controls.Add(this.treeViewItems);
            this.Name = "Form1";
            this.Text = "TRPG Data Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewItems;
    }
}

