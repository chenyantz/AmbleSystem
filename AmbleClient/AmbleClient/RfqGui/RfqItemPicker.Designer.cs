namespace AmbleClient.RfqGui
{
    partial class SoItemPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoItemPicker));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbOK = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Selecte = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RfqId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MFG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Resale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOK,
            this.tsbCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbOK
            // 
            this.tsbOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOK.Image = ((System.Drawing.Image)(resources.GetObject("tsbOK.Image")));
            this.tsbOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOK.Name = "tsbOK";
            this.tsbOK.Size = new System.Drawing.Size(29, 22);
            this.tsbOK.Text = "OK";
            this.tsbOK.Click += new System.EventHandler(this.tsbOK_Click);
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selecte,
            this.RfqId,
            this.MPN,
            this.MFG,
            this.DC,
            this.TargetPrice,
            this.Resale,
            this.Cost});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 312);
            this.dataGridView1.TabIndex = 1;
            // 
            // Selecte
            // 
            this.Selecte.FillWeight = 40F;
            this.Selecte.HeaderText = "Select";
            this.Selecte.Name = "Selecte";
            this.Selecte.Width = 40;
            // 
            // RfqId
            // 
            this.RfqId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.RfqId.HeaderText = "RFQ ID";
            this.RfqId.Name = "RfqId";
            this.RfqId.Width = 51;
            // 
            // MPN
            // 
            this.MPN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MPN.HeaderText = "MPN";
            this.MPN.Name = "MPN";
            this.MPN.Width = 48;
            // 
            // MFG
            // 
            this.MFG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MFG.HeaderText = "MFG";
            this.MFG.Name = "MFG";
            this.MFG.Width = 48;
            // 
            // DC
            // 
            this.DC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DC.HeaderText = "D/C";
            this.DC.Name = "DC";
            this.DC.Width = 48;
            // 
            // TargetPrice
            // 
            this.TargetPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TargetPrice.HeaderText = "Target Price";
            this.TargetPrice.Name = "TargetPrice";
            this.TargetPrice.Width = 67;
            // 
            // Resale
            // 
            this.Resale.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Resale.HeaderText = "Resale";
            this.Resale.Name = "Resale";
            this.Resale.Width = 66;
            // 
            // Cost
            // 
            this.Cost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Cost.HeaderText = "Cost";
            this.Cost.Name = "Cost";
            this.Cost.Width = 54;
            // 
            // SoItemPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 337);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoItemPicker";
            this.Text = "RFQs for SO";
            this.Load += new System.EventHandler(this.RfqItemPicker_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbOK;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selecte;
        private System.Windows.Forms.DataGridViewTextBoxColumn RfqId;
        private System.Windows.Forms.DataGridViewTextBoxColumn MPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MFG;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Resale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
    }
}