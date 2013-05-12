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
        bool isItemListAlready;

        public List<int> SoItemsIdsForPo=new List<int>();

        private List<int> SoItemsForSelect = new List<int>();

        public SoItemPicker(int soItemId)
        {
            InitializeComponent();
            this.soItemId = soItemId;
            isItemListAlready = false;
        }

        public SoItemPicker(List<int> soItemIds)
        {
            InitializeComponent();
            SoItemsForSelect.AddRange(soItemIds);
            isItemListAlready = true;
        }


        private void RfqItemPicker_Load(object sender, EventArgs e)
        {

            if (isItemListAlready)
            {
                foreach (int id in SoItemsForSelect)
                {
                    var soItemInfo = AmbleClient.Order.SoMgr.SoMgr.GetSoItemInfoAccordingToSoItemId(id);
                    List<OfferGui.OfferMgr.Offer> offerList = OfferGui.OfferMgr.OfferMgr.GetOffersByRfqId(soItemInfo.rfqId);
                    foreach (OfferGui.OfferMgr.Offer o in offerList)
                    {
                        dataGridView1.Rows.Add(true,
                     soItemInfo.soItemsId,
                     soItemInfo.partNo,
                     soItemInfo.mfg,
                     soItemInfo.dc,
                    o.vendorName,
                    soItemInfo.qty,
                    o.price
                       );
                    }
                }
            }
            else
            {
                DataTable dt = AmbleClient.Order.SoMgr.SoMgr.BuyerGetSoItemsWithSameVendor(UserInfo.UserId, this.soItemId);
                if (dt == null || dt.Rows.Count <= 0)
                    return;


                foreach (DataRow dr in dt.Rows)
                {
                    dataGridView1.Rows.Add(true,
                      Convert.ToInt32(dr["soItemId"]),
                      dr["mpn"].ToString(),
                      dr["mfg"].ToString(),
                      dr["dc"].ToString(),
                      dr["vendorName"].ToString(),
                      dr["qty"].ToString(),
                      dr["price"].ToString());
                      
                }

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }







    }
}
