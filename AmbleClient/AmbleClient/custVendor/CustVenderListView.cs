using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.custVendor.CustVendorManager;

namespace AmbleClient.custVendor
{
    public partial class CustVenderListView : Form
    {
        protected Dictionary<string,string> filterByDict=new Dictionary<string,string>();
        protected string filterColumn=string.Empty;
        protected List<custvendorinfo> custVenInfoList=new List<custvendorinfo>();

        protected int selectedRow = 0;

        public CustVenderListView()
        {
            InitializeComponent();
           
        }

        private void CustVenderListView_Load(object sender, EventArgs e)
        {

            SetFilterByDict();

            tscbFilterBy.Items.AddRange(filterByDict.Keys.ToArray());

            SetTheDataGridViewColumn();
            FillTheDataGrid();



        }

        protected void RestoreSelectedRow()
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            dataGridView1.Rows[0].Selected = false;
            if (selectedRow > dataGridView1.Rows.Count - 1)
            {
                selectedRow = dataGridView1.Rows.Count - 1;
            }
            else if (selectedRow < 0)
            {
                selectedRow = 0;
            }
            dataGridView1.Rows[selectedRow].Selected = true;

        }


        protected virtual void SetFilterByDict()
        { }

        protected virtual void SetTheDataGridViewColumn()
        { }

        protected virtual void FillTheDataGrid()
        { }
        protected virtual void NewCustVen()
        { }
        protected virtual void DeleteCustVen()
        { }

        private void tscbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                filterColumn = filterByDict[tscbFilterBy.Text.Trim()];
            }
            catch (Exception ex)
            {
                Logger.Debug(ex.Message);
                Logger.Debug(ex.StackTrace);
                filterColumn = string.Empty;
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {

            this.dataGridView1.Rows.Clear();
            FillTheDataGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewCustVen();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.RowIndex>=0&&e.ColumnIndex>=0)
           {
              if(e.RowIndex>dataGridView1.ColumnCount-1)
                  return;

               //get the real index.
              int realIndex = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);


              CustVenView cvv = new CustVenView(custVenInfoList[realIndex]);
              if (DialogResult.Yes == cvv.ShowDialog())
              {
                  tsbRefresh_Click(this, null);
              }
              RestoreSelectedRow();
        
           }
        }

        private void tsbApply_Click(object sender, EventArgs e)
        {
            tsbRefresh_Click(this, null);
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            tscbFilterBy.Text = string.Empty;
            tstbFilterString.Text = string.Empty;
            tsbRefresh_Click(this, null);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            DeleteCustVen();
        }

        protected void tscbAllOrMine_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsbRefresh_Click(this, null);
        }


    }
}
