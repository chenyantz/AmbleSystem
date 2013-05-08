using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.PoMgr;


namespace AmbleClient.Order.PoView
{
    public partial class NewPo : Form
    {

        int soId;

        List<int> soItemsId = new List<int>();
        
        public NewPo()
        {
            InitializeComponent();
        }

        public NewPo(int soId)
        {
            this.soId = soId;
            InitializeComponent();
            this.Text = "New Create a PO for SO:" + soId;
        
        }


        public NewPo(List<int> soItemId)
        {

            InitializeComponent();
            this.Text = "New Create a PO for SO Items:";
            foreach (int id in soItemId)
            {
                this.Text += id.ToString() + ",";
            
            }
            this.Text = this.Text.Remove(this.Text.Length - 1);
            soItemsId.AddRange(soItemId);
            poViewControl1.soItemsIdList = soItemsId;
            poViewControl1.NewPoFill();


        
        }



        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!poViewControl1.CheckValues())
                return;
            po poMain = poViewControl1.GetValues();
            poMain.poStates =(sbyte) new PoItemNew().GetStateValue();
            poMain.poDate = DateTime.Now;
            PoMgr.PoMgr.SavePoMain(poMain);
            int poId = PoMgr.PoMgr.GetTheInsertId(UserInfo.UserId);
            PoMgr.PoMgr.SetPoNumber(poId);
            
            List<PoItemContentAndState> items = poViewControl1.GetPoItemContentAndState();
            foreach (PoItemContentAndState pics in items)
            {
                pics.poItem.poId = poId;
            
            }
            PoMgr.PoMgr.UpDatePoItems(items);
            this.Close();

        }

    }
}
