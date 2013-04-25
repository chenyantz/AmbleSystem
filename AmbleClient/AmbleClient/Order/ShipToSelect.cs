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
    public partial class ShipToSelect : Form
    {
        private List<string> shipToList;

        private string selectedString;

        public ShipToSelect(List<string> shipToList)
        {
            this.shipToList = shipToList;
            InitializeComponent();
        }

        public string SelectionString
        {
          get{
           return selectedString;
          }
        
        }


        private void ShipToSelect_Load(object sender, EventArgs e)
        {

           if (shipToList.Count > 1)
           {
               textBox1.Text=shipToList[0];

               for (int i = 1; i < shipToList.Count; i++)
               {
                   System.Windows.Forms.TextBox ShipTo = new TextBox();
                   ShipTo.Dock = System.Windows.Forms.DockStyle.Fill;
                   ShipTo.Location = new System.Drawing.Point(3, 3);
                   ShipTo.Multiline = true;
                   ShipTo.Name = "ShipTo";
                   ShipTo.Size = new System.Drawing.Size(570, 76);
                   ShipTo.TabIndex = 0;
                   ShipTo.Text = shipToList[i];

                   System.Windows.Forms.TabPage tabPage = new TabPage();

                   tabPage.Controls.Add(ShipTo);
                   tabPage.Location = new System.Drawing.Point(4, 25);
                   tabPage.Name = "tabPage1";
                   tabPage.Padding = new System.Windows.Forms.Padding(3);
                   tabPage.Size = new System.Drawing.Size(576, 82);
                   tabPage.TabIndex = i;
                   tabPage.Text = "Ship To "+(i+1).ToString();
                   tabPage.UseVisualStyleBackColor = true;
                   tabControl1.TabPages.Add(tabPage);
               }


        }





    }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            TabPage tp = tabControl1.SelectedTab;
            foreach (Control control in tp.Controls)
            {
                if (control is TextBox)
                {
                    selectedString = control.Text.Trim();

                }
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

    }
}
