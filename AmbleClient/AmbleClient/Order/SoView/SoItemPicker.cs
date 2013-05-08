using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.SO
{
    public partial class SoItemPicker : Form
    {
        int soItemId;

        public List<int> SoItemsIdsForPo=new List<int>();
        
        public SoItemPicker(int soItemId)
        {
            InitializeComponent();
            this.soItemId = soItemId;
        }

        private void RfqItemPicker_Load(object sender, EventArgs e)
        {
            DataTable dt = AmbleClient.Order.SoMgr.SoMgr.BuyerGetSoItemsWithSameVendor(UserInfo.UserId, this.soItemId);
            if (dt==null||dt.Rows.Count <= 0)
                return;


            foreach (DataRow dr in dt.Rows)
            {
                dataGridView1.Rows.Add(true,
                  Convert.ToInt32(dr["soItemId"]),
                  dr["mpn"].ToString(),
                  dr["mfg"].ToString(),
                  dr["dc"].ToString(),
                  dr["vendorName"].ToString(),
                  dr["qty"].ToString());
            }

        }

        private void tsbOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                if ((bool)(dgvr.Cells[0].Value))
                {
                    SoItemsIdsForPo.Add(Convert.ToInt32(dgvr.Cells["SoItemId"].Value.ToString()));
                
                }
            }

            SoItemsIdsForPo.Insert(0, soItemId);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void tsbCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }







    }
}
