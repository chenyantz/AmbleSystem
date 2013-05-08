namespace AmbleClient.Order.PoView
{
    partial class PoItemsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoItemsView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscbOp = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tscbPoState = new System.Windows.Forms.ToolStripComboBox();
            this.poItemsControl1 = new AmbleClient.PO.PoItemsControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbOp,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tscbPoState});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(699, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscbOp
            // 
            this.tscbOp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tscbOp.Image = ((System.Drawing.Image)(resources.GetObject("tscbOp.Image")));
            this.tscbOp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tscbOp.Name = "tscbOp";
            this.tscbOp.Size = new System.Drawing.Size(71, 22);
            this.tscbOp.Text = "Op&&Close";
            this.tscbOp.Click += new System.EventHandler(this.tscbOp_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 22);
            this.toolStripLabel1.Text = "Change State To:";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tscbPoState
            // 
            this.tscbPoState.Name = "tscbPoState";
            this.tscbPoState.Size = new System.Drawing.Size(180, 25);
            this.tscbPoState.SelectedIndexChanged += new System.EventHandler(this.tscbPoState_SelectedIndexChanged);
            // 
            // poItemsControl1
            // 
            this.poItemsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.poItemsControl1.Location = new System.Drawing.Point(0, 25);
            this.poItemsControl1.Name = "poItemsControl1";
            this.poItemsControl1.Size = new System.Drawing.Size(699, 425);
            this.poItemsControl1.TabIndex = 1;
            // 
            // PoItemsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 450);
            this.Controls.Add(this.poItemsControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PoItemsView";
            this.Text = "PoItemsViewAdd";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tscbOp;
        private PO.PoItemsControl poItemsControl1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbPoState;
    }
}