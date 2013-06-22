using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.OfferGui.OfferMgr;
using AmbleClient.custVendor.CustVendorManager;

namespace AmbleClient.OfferGui
{
   public class BuyerOfferItems:OfferItems
    {
       public BuyerOfferItems()
       {
       }

       public void AutoFill(string mpn, string mfg)
       {
           tbMpn.Text = mpn;
           tbMfg.Text = mfg;
           VendorAutoComplete();
           tbVendorName.Leave+=new EventHandler(tbVendorName_Leave);
       }

       private void VendorAutoComplete()
       {
           List<string> vendorNames = CustVenInfoManager.GetAllCustomerVendorNameICanSee(1, UserInfo.UserId);
               
           this.tbVendorName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           tbVendorName.AutoCompleteSource = AutoCompleteSource.CustomSource;

           AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
           foreach (string vendorName in vendorNames)
           {
               autoSource.Add(vendorName);
           }
           tbVendorName.AutoCompleteCustomSource = autoSource;
       }

       private void tbVendorName_Leave(object sender, EventArgs e)
       {
           //自动填充contact,phone,fax
           Dictionary<string, string> contactInfo = CustVenInfoManager.GetContactInfo(1, UserInfo.UserId, tbVendorName.Text.Trim());
           //contact   
           AutoCompleteStringCollection contactSource = new AutoCompleteStringCollection();
           if (contactInfo.Keys.Contains("contact1"))
           {
               tbContact.Text = contactInfo["contact1"];
               contactSource.Add(contactInfo["contact1"]);
           }
           if (contactInfo.Keys.Contains("contact2"))
           {
               contactSource.Add(contactInfo["contact2"]);
           }
           tbContact.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           tbContact.AutoCompleteSource = AutoCompleteSource.CustomSource;
           tbContact.AutoCompleteCustomSource = contactSource;
           //phone
           AutoCompleteStringCollection phoneSource = new AutoCompleteStringCollection();
           if (contactInfo.Keys.Contains("phone1"))
           {
               tbPhone.Text = contactInfo["phone1"];
               phoneSource.Add(contactInfo["phone1"]);
           }
           if (contactInfo.Keys.Contains("phone2"))
           {
               phoneSource.Add(contactInfo["phone2"]);
           }
           if (contactInfo.Keys.Contains("cellphone"))
           {
               phoneSource.Add(contactInfo["cellphone"]);
           }
           tbPhone.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           tbPhone.AutoCompleteSource = AutoCompleteSource.CustomSource;
           tbPhone.AutoCompleteCustomSource = phoneSource;

           AutoCompleteStringCollection faxSource = new AutoCompleteStringCollection();
           if (contactInfo.Keys.Contains("fax"))
           {
               tbFax.Text = contactInfo["fax"];
               faxSource.Add(contactInfo["fax"]);
           }
           tbFax.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           tbFax.AutoCompleteSource = AutoCompleteSource.CustomSource;
           tbFax.AutoCompleteCustomSource = faxSource;

           AutoCompleteStringCollection emailSource = new AutoCompleteStringCollection();
           if (contactInfo.Keys.Contains("email1"))
           {
               tbEmail.Text = contactInfo["email1"];
               emailSource.Add(contactInfo["email1"]);
           }
           if (contactInfo.Keys.Contains("email2"))
           {
               emailSource.Add(contactInfo["email2"]);
           }
           tbEmail.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           tbEmail.AutoCompleteSource = AutoCompleteSource.CustomSource;
           tbEmail.AutoCompleteCustomSource = emailSource;



       }

       public override void FillTheTable(AmbleClient.OfferGui.OfferMgr.Offer offer)
       {
           offerId = offer.offerId;

           base.FillTheTable(offer);
           tbVendorName.Text = offer.vendorName;
           tbContact.Text = offer.contact;
           tbPhone.Text = offer.phone;
           tbFax.Text = offer.fax;
           tbEmail.Text = offer.email;
       }


       public Offer GetValue()
       {
           Offer offer = new Offer();
           offer.mpn = tbMpn.Text.Trim().ToUpper();
           offer.mfg = tbMfg.Text.Trim().ToUpper();
           offer.vendorName = tbVendorName.Text.Trim();
           offer.contact = tbContact.Text.Trim();
           offer.phone = tbPhone.Text.Trim();
           offer.fax = tbFax.Text.Trim();
           offer.email = tbEmail.Text.Trim();
           offer.packing = tbPacking.Text.Trim();
           offer.quantity = int.Parse(tbQuantity.Text.Trim());
           offer.price = float.Parse(tbPrice.Text.Trim());
            offer.LT = tbDeliverTime.Text.Trim();

           offer.buyerId = UserInfo.UserId;

           offer.offerDate = DateTime.Now;
           offer.offerStates = 0;//new 
           offer.notes = tbNotes.Text.Trim();

           return offer;
       
       
       }

       public bool SaveItems(int rfqId)
       {
           if (CheckItems() == false)
           {
               return false;
           }
           var offer = GetValue();
           offer.rfqNo = rfqId;
           return OfferMgr.OfferMgr.SaveOffer(offer);

       }

       public int GetTheSavedOfferId()
       {

           return OfferMgr.OfferMgr.GetNewSavedOfferId(UserInfo.UserId);
       
       }

       public void UpdateItems()
       {
           if (CheckItems() == false)
           {
               return;
           }
           var offer = GetValue();
           offer.offerId = offerId;
           OfferMgr.OfferMgr.UpdateOffer(offer);
    
       }


       public void UpdateOfferState(int state)
       {
           OfferMgr.OfferMgr.ChangeOfferState(state, offerId);
       
       }

       public int GetOfferId()
       {
           return offerId;
       }


    }
}
