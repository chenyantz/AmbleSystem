namespace AmbleClient.SO
{
    partial class SoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsbViewPo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbApprove = new System.Windows.Forms.ToolStripButton();
            this.tsbReject = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbForceClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbToExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbToDocx = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUpdate,
            this.tsbViewPo,
            this.toolStripSeparator1,
            this.tsbApprove,
            this.tsbReject,
            this.tsbCancel,
            this.tsbForceClose,
            this.toolStripSeparator2,
            this.tsbToExcel,
            this.tsbToDocx});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(959, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbUpdate
            // 
            this.tsbUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpdate.Image")));
            this.tsbUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdate.Name = "tsbUpdate";
            this.tsbUpdate.Size = new System.Drawing.Size(54, 22);
            this.tsbUpdate.Text = "Update";
            this.tsbUpdate.Click += new System.EventHandler(this.tsbUpdate_Click);
            // 
            // tsbViewPo
            // 
            this.tsbViewPo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbViewPo.Image = ((System.Drawing.Image)(resources.GetObject("tsbViewPo.Image")));
            this.tsbViewPo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewPo.Name = "tsbViewPo";
            this.tsbViewPo.Size = new System.Drawing.Size(59, 22);
            this.tsbViewPo.Text = "View PO";
            this.tsbViewPo.Click += new System.EventHandler(this.tsbViewPo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbApprove
            // 
            this.tsbApprove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbApprove.Image = ((System.Drawing.Image)(resources.GetObject("tsbApprove.Image")));
            this.tsbApprove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbApprove.Name = "tsbApprove";
            this.tsbApprove.Size = new System.Drawing.Size(61, 22);
            this.tsbApprove.Text = "Approve";
            this.tsbApprove.Click += new System.EventHandler(this.tsbApprove_Click);
            // 
            // tsbReject
            // 
            this.tsbReject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbReject.Image = ((System.Drawing.Image)(resources.GetObject("tsbReject.Image")));
            this.tsbReject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReject.Name = "tsbReject";
            this.tsbReject.Size = new System.Drawing.Size(46, 22);
            this.tsbReject.Text = "Reject";
            this.tsbReject.Click += new System.EventHandler(this.tsbReject_Click);
            // 
            // tsbCancel
            // 
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(49, 22);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // tsbForceClose
            // 
            this.tsbForceClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbForceClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbForceClose.Image")));
            this.tsbForceClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForceClose.Name = "tsbForceClose";
            this.tsbForceClose.Size = new System.Drawing.Size(79, 22);
            this.tsbForceClose.Text = "Force Close";
            this.tsbForceClose.Click += new System.EventHandler(this.tsbForceClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbToExcel
            // 
            this.tsbToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbToExcel.Image = ((System.Drawing.Image)(resources.GetObject("tsbToExcel.Image")));
            this.tsbToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToExcel.Name = "tsbToExcel";
            this.tsbToExcel.Size = new System.Drawing.Size(93, 22);
            this.tsbToExcel.Text = "Save All As xls";
            this.tsbToExcel.Click += new System.EventHandler(this.tsbToExcel_Click);
            // 
            // tsbToDocx
            // 
            this.tsbToDocx.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbToDocx.Image = ((System.Drawing.Image)(resources.GetObject("tsbToDocx.Image")));
            this.tsbToDocx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbToDocx.Name = "tsbToDocx";
            this.tsbToDocx.Size = new System.Drawing.Size(135, 22);
            this.tsbToDocx.Text = "Save Current As docx";
            this.tsbToDocx.Click += new System.EventHandler(this.tsbToDocx_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(959, 595);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // SoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 620);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SoView";
            this.Text = "SoView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SoView_FormClosed);
            this.Load += new System.EventHandler(this.SoView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbUpdate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbViewPo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbToExcel;
        private System.Windows.Forms.ToolStripButton tsbApprove;
        private System.Windows.Forms.ToolStripButton tsbReject;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripButton tsbForceClose;
        private System.Windows.Forms.ToolStripButton tsbToDocx;
       // private System.Windows.Forms.TabPage tabPage1;
       //private System.Windows.Forms.TabPage tabPage2;
    }
}