using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.Order
{
     public partial class OrderListView : Form
    {

        protected string filterColumn=string.Empty;
        protected string filterString=string.Empty;

        protected List<int> intStateList = new List<int>();

        protected Dictionary<string, string> filterColumnDict = new Dictionary<string, string>();

         private int selectedRow=0;


         protected bool externalSearch = false;
         protected bool externalSearchByMpn;
         protected string externalSearchString;


        
        public OrderListView()
        {
            InitializeComponent();
        }

        public void SetExternalSearch(bool isMpnSearch, string searchString)
        {
            externalSearch = true;
            externalSearchByMpn = isMpnSearch;
            externalSearchString = searchString;
        
        }




        private void OrderListView_Load(object sender, EventArgs e)
        {

            ViewStart();

            FillTheFilterColumnDict();
            foreach (string filterColumn in filterColumnDict.Keys)
            {
                tscbFilterColumn.Items.Add(filterColumn);
            }
            tscbList.SelectedIndexChanged -= tscbList_SelectedIndexChanged;
            tscbList.SelectedIndex = 0;
            tscbList.SelectedIndexChanged += tscbList_SelectedIndexChanged;

            FillTheStateCombox();
            GetTheStateList();
            if (!externalSearch)
            {
                FillTheDataGrid();
            }
            else
            {
                int selectedIndex = GetFilterIndexWhenExternalSearch(this.externalSearchByMpn);
                tscbFilterColumn.SelectedIndex = selectedIndex;
                tstbFilterString.Text = externalSearchString;
                tsbApply_Click(this, null);
            }
            
        }

        protected virtual int GetFilterIndexWhenExternalSearch(bool isMpn)
        {
            return 0;
        }

        protected virtual void FillTheFilterColumnDict()
        { 
        }
        protected virtual void FillTheStateCombox()
        { 
        
        }

        protected virtual void GetTheStateList()
        { 
        }

        protected virtual void ViewStart()
        { 
         //add items in comboBox and Datagridview
        
        }

        protected virtual void FillTheDataGrid()
        { 
        
        }


        protected void OrderStatesCheckedChanged(object sender, EventArgs e)
        {
            FillTheDataGrid();
        }

        private void tscbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTheDataGrid();
        }

        private void tsbApply_Click(object sender, EventArgs e)
        {
            if (this.tscbFilterColumn.SelectedIndex != -1)
            {
                this.filterColumn = filterColumnDict[(string)tscbFilterColumn.SelectedItem];
            }
            else
            {
                this.filterColumn = string.Empty;
            }
            this.filterString = tstbFilterString.Text.Trim();
            FillTheDataGrid();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            this.filterColumn = string.Empty;
            this.filterString = string.Empty;
            this.tscbFilterColumn.SelectedIndex = -1;
            this.tstbFilterString.Text = string.Empty;
           
            FillTheDataGrid();
        }

        protected virtual void OpenOrderDetails(int rowIndex)
        { 
        
        }


        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            FillTheDataGrid();
        }

        private void OrderListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                FillTheDataGrid();
            }


        }

        private void tscbListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.StateChanged(sender, e);
        }

        protected virtual void StateChanged(object sender, EventArgs e)
        { 
        
        
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                OpenOrderDetails(e.RowIndex);
            }
            RestoreSelectedRow();


        }

        private void RestoreSelectedRow()
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


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
        }




    }
}
