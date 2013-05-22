using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.RfqGui.RfqManager;

namespace AmbleClient.RfqGui
{
    public partial class SoItemPicker : Form
    {
        string customerName;
        int rfqId;
        RfqStatesEnum rfqState;

        public List<int> RfqIdsForSo=new List<int>();
        
        public SoItemPicker(string customerName,int rfqId,RfqStatesEnum rfqState)
        {
            InitializeComponent();
            this.customerName = customerName;
            this.rfqId = rfqId;
            this.rfqState = rfqState;
        }

        private void RfqItemPicker_Load(object sender, EventArgs e)
        {

           if(rfqState==RfqStatesEnum.Quoted)
           {
               DataTable dt = AmbleClient.RfqGui.RfqManager.RfqMgr.GetRfqForSo(customerName, UserInfo.UserId);

               foreach (DataRow dr in dt.Rows)
               {
                   if (Convert.ToInt32(dr["rfqNo"]) == this.rfqId)
                   {
                       dataGridView1.Rows.Insert(0,
                           true,
                       Tool.Get6DigitalNumberAccordingToId(Convert.ToInt32(dr["rfqNo"])),
                       dr["partNo"].ToString(),
                       dr["mfg"].ToString(),
                       dr["dc"].ToString(),
                       dr["targetPrice"].ToString(),
                       dr["resale"].ToString(),
                       dr["cost"].ToString());



                   }
                   else
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

           }
           else
          {
              Rfq rfq = AmbleClient.RfqGui.RfqManager.RfqMgr.GetRfqAccordingToRfqId(this.rfqId);
              dataGridView1.Rows.Add(true,
                   Tool.Get6DigitalNumberAccordingToId(this.rfqId),
                   rfq.partNo,
                   rfq.mfg,
                   rfq.dc,
                   rfq.targetPrice,
                   rfq.resale,
                   rfq.cost);

           }



            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Cells[0].ReadOnly = true;
            }
        }

        private void tsbOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                if ((bool)(dgvr.Cells[0].Value))
                {
                    RfqIdsForSo.Add(Tool.GetIdAccordingTo6DigitalNumber(dgvr.Cells["RfqId"].Value.ToString()));
                
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }







    }
}
