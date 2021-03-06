﻿namespace AmbleClient.Order.PoView
{
    partial class PoMaterials
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoMaterials));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDownload = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUploadDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUpload,
            this.tsbDownload,
            this.tsbDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(494, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbUpload
            // 
            this.tsbUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpload.Image")));
            this.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Size = new System.Drawing.Size(54, 22);
            this.tsbUpload.Text = "Upload";
            this.tsbUpload.Click += new System.EventHandler(this.tsbUpload_Click);
            // 
            // tsbDownload
            // 
            this.tsbDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDownload.Image = ((System.Drawing.Image)(resources.GetObject("tsbDownload.Image")));
            this.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownload.Name = "tsbDownload";
            this.tsbDownload.Size = new System.Drawing.Size(70, 22);
            this.tsbDownload.Text = "Download";
            this.tsbDownload.Click += new System.EventHandler(this.tsbDownload_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(48, 22);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileName,
            this.chFileSize,
            this.chUploadDate,
            this.chFileType});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(494, 331);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // chFileName
            // 
            this.chFileName.Text = "File Name";
            this.chFileName.Width = 150;
            // 
            // chFileSize
            // 
            this.chFileSize.Text = "Size";
            this.chFileSize.Width = 71;
            // 
            // chUploadDate
            // 
            this.chUploadDate.Text = "Upload Date";
            this.chUploadDate.Width = 95;
            // 
            // chFileType
            // 
            this.chFileType.Text = "Type";
            // 
            // PoMaterials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 356);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PoMaterials";
            this.Text = "PoMaterials";
            this.Load += new System.EventHandler(this.PoMaterials_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbUpload;
        private System.Windows.Forms.ToolStripButton tsbDownload;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.ColumnHeader chFileSize;
        private System.Windows.Forms.ColumnHeader chUploadDate;
        private System.Windows.Forms.ColumnHeader chFileType;
    }
}