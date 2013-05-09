namespace AmbleClient.SO
{
    partial class SoItemView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoItemView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGeneratePo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbSoItemState = new System.Windows.Forms.ToolStripComboBox();
            this.soItemsControl1 = new AmbleClient.SO.SoItemsControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOp,
            this.toolStripSeparator1,
            this.tsbGeneratePo,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tscbSoItemState});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(694, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOp
            // 
            this.tsbOp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOp.Image = ((System.Drawing.Image)(resources.GetObject("tsbOp.Image")));
            this.tsbOp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOp.Name = "tsbOp";
            this.tsbOp.Size = new System.Drawing.Size(54, 22);
            this.tsbOp.Text = "Update";
            this.tsbOp.Click += new System.EventHandler(this.tsbOp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbGeneratePo
            // 
            this.tsbGeneratePo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbGeneratePo.Image = ((System.Drawing.Image)(resources.GetObject("tsbGeneratePo.Image")));
            this.tsbGeneratePo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGeneratePo.Name = "tsbGeneratePo";
            this.tsbGeneratePo.Size = new System.Drawing.Size(85, 22);
            this.tsbGeneratePo.Text = "Generate PO";
            this.tsbGeneratePo.Click += new System.EventHandler(this.tsbGeneratePo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 22);
            this.toolStripLabel1.Text = "Change State To:";
            // 
            // tscbSoItemState
            // 
            this.tscbSoItemState.Name = "tscbSoItemState";
            this.tscbSoItemState.Size = new System.Drawing.Size(160, 25);
            this.tscbSoItemState.SelectedIndexChanged += new System.EventHandler(this.tscbSoItemState_SelectedIndexChanged);
            // 
            // soItemsControl1
            // 
            this.soItemsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soItemsControl1.Location = new System.Drawing.Point(0, 25);
            this.soItemsControl1.Name = "soItemsControl1";
            this.soItemsControl1.Size = new System.Drawing.Size(694, 477);
            this.soItemsControl1.TabIndex = 1;
            // 
            // SoItemView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 502);
            this.Controls.Add(this.soItemsControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoItemView";
            this.Text = "SoItemView";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOp;
        private SoItemsControl soItemsControl1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox tscbSoItemState;
        private System.Windows.Forms.ToolStripButton tsbGeneratePo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

    }
}