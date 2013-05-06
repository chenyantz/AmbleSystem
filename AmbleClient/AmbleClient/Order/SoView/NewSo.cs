using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace AmbleClient.SO
{
    public partial class NewSo : Form
    {
        List<int> rfqIds = new List<int>();
        ILog logger = LogManager.GetLogger(typeof(NewSo));

        public NewSo(int rfqId)
        {
            InitializeComponent();
            this.Text = "New Create an SO for RFQ:" + rfqId;
        }


        public NewSo(List<int> ids)
        {
            InitializeComponent();
            this.Text = "New Create an SO for RFQs:";
            foreach (int id in ids)
            {
                this.Text += (Tool.Get6DigitalNumberAccordingToId(id)+",");
            }
            this.Text=this.Text.Remove(this.Text.Length - 2);
            rfqIds.AddRange(ids);
            soViewControl1.rfqList = rfqIds;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (false == this.soViewControl1.SoSave())
                    return;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                MessageBox.Show("Save So Error");
                return;
            }
            
            if (UserInfo.UserId == soViewControl1.GetAssignedSaleID())
            {
                foreach(int rfqId in rfqIds)
               AmbleClient.RfqGui.RfqManager.RfqMgr.AddRfqHistory(rfqId, UserInfo.UserId, "Created an SO");
            }
            else
            { 
                foreach(int rfqId in rfqIds)
                    AmbleClient.RfqGui.RfqManager.RfqMgr.AddRfqHistory(rfqId, UserInfo.UserId, "Created an SO for " + AmbleClient.Admin.AccountMgr.AccountMgr.GetNameById(soViewControl1.GetAssignedSaleID()));
            
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }


        private void NewSo_Load(object sender, EventArgs e)
        {
            this.soViewControl1.NewSOFill();
        }

    }
}
