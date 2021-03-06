﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmbleClient.BomOffer
{
    public partial class BomOfferNew : Form
    {
       // bool isOffer;
        BomOfferTypeEnum bomOfferType;
        int custVenId;

        public BomOfferNew(BomOfferTypeEnum bomOfferType, int custVenId)
        {
            InitializeComponent();
            this.bomOfferType = bomOfferType;
            this.custVenId = custVenId;
            if (this.bomOfferType==BomOfferTypeEnum.Excess)
            {
                this.Text = "Add a New Excess";

            }
            else if (this.bomOfferType == BomOfferTypeEnum.BOM)
            {
                this.Text = "Add a New BOM";
            }
            else
            {
                this.Text = "Add a New Company";
            }

        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ItemsCheck.CheckTextBoxEmpty(tbMfg))
            {
                MessageBox.Show("Please input the MFG");
                return;
            
            }

            if (!ItemsCheck.CheckTextBoxEmpty(tbMpn))
            {
                MessageBox.Show("Please input the MPN");
                return;
            }

            if (!ItemsCheck.CheckIntNumber(tbQty))
            {
                MessageBox.Show("The QTY should be an integer value");
                tbQty.Focus();
                return;
            }

            if (!ItemsCheck.CheckTextBoxEmpty(tbPrice))
            {
                MessageBox.Show("Please input the Price");
                return;

            }
            else
            {
                if (!ItemsCheck.CheckFloatNumber(tbPrice))
                {
                    MessageBox.Show("The Price should be a float number");
                    tbPrice.Focus();
                    return;
                
                }
            }

            
            
            var publicBomOff = new publicbomoffer
            {
                mfg = tbMfg.Text.Trim().ToUpper(),
                mpn = tbMpn.Text.Trim().ToUpper(), //upper
                qty = int.Parse(tbQty.Text.Trim()),
                price = float.Parse(tbPrice.Text.Trim()),
                cpn = tbCpn.Text.Trim().ToUpper(),
                userID = (short)UserInfo.UserId,
                BomCustVendId=this.custVenId,
                enerDay = DateTime.Now
            };
            using (BomOfferEntities entity = new BomOfferEntities())
            {
                entity.publicbomoffer.AddObject(publicBomOff);
                entity.SaveChanges();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbMfg_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tbMpn_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tbQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tbCpn_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }




    }
}
