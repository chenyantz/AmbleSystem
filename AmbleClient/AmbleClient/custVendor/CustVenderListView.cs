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

       

        public CustVenderListView()
        {
            InitializeComponent();
           
        }

        private void CustVenderListView_Load(object sender, EventArgs e)
        {

            SetFilterByDict();
            SetTheDataGridViewColumn();
            FillTheDataGrid();



        }

        protected virtual void SetFilterByDict()
        { }

        protected virtual void SetTheDataGridViewColumn()
        { }

        protected virtual void FillTheDataGrid()
        { }
        protected virtual void NewCustVen()
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
              CustVenView cvv = new CustVenView(custVenInfoList[e.RowIndex]);
              if (DialogResult.Yes == cvv.ShowDialog())
              { 
              
              }
             


           
           }



        }



    }
}
