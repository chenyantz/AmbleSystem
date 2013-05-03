using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.RfqGui
{
    public partial class RfqItemPicker : Form
    {
        string customerName;
        int rfqId;

        public List<int> RfqIdsForSo=new List<int>();
        
        public RfqItemPicker(string customerName,int rfqId)
        {
            InitializeComponent();
            this.customerName = customerName;
            this.rfqId = rfqId;
        }

        private void RfqItemPicker_Load(object sender, EventArgs e)
        {
            DataTable dt = new AmbleClient.RfqGui.RfqManager.RfqMgr().GetRfqForSo(customerName, UserInfo.UserId, this.rfqId);

            foreach (DataRow dr in dt.Rows)
            {
                dataGridView1.Rows.Add(true,
                  Tool.Get6DigitalNumberAccordingToId(Convert.ToInt32(dr["rfqNo"])),
                  dr["partNo"].ToString(),
                  dr["mfg"].ToString(),
                  dr["dc"].ToString(),
                  dr["targetPrice"].ToString(),
                  dr["resale"].ToString(),
                  dr["cost"].ToString());
            }

        }

        private void tsbOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                if (dgvr.Cells[0].Selected == true)
                {
                    RfqIdsForSo.Add(Tool.GetIdAccordingTo6DigitalNumber(dgvr.Cells["RfqId"].Value.ToString()));
                
                }
            }

            RfqIdsForSo.Insert(0, rfqId);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }







    }
}
