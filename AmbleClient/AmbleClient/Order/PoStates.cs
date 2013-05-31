using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbleClient.Order.SoMgr;
using System.Windows.Forms;

namespace AmbleClient.Order
{

    public abstract class PoItemState
    {
        protected List<Operation> operationList = new List<Operation>();
        public virtual int GetStateValue()
        {
            return -1;
        }
        public virtual string GetStateString()
        {
            return "empty";
        }

        public virtual List<JobDescription> WhoCanUpdate()
        {
            var listJobDes = new List<JobDescription>();
            listJobDes.Add(JobDescription.PurchasersManager);
            listJobDes.Add(JobDescription.Boss);
            listJobDes.Add(JobDescription.Admin);
            return listJobDes; ;
        }
        public void UpdateState(int poItemId, int poItemState)
        {
            PoMgr.PoMgr.UpdatePoItemState(poItemId, poItemState);

        }
        public List<Operation> GetOperationList()
        {
            return operationList;
        }

    }

    public class PoItemNew : PoItemState
    {
      public override List<JobDescription> WhoCanUpdate()
      {
          var listJobDes = new List<JobDescription>();
          listJobDes.Add(JobDescription.Purchaser);
          listJobDes.Add(JobDescription.PurchasersManager);
          listJobDes.Add(JobDescription.Boss);
          listJobDes.Add(JobDescription.Admin);
          return listJobDes; ;
      }
        
        public override int GetStateValue()
        {
            return 0;
        }
        public override string GetStateString()
        {
            return "New";
        }    
    
    }
    public class PoItemRejected : PoItemState
    {
        public override int GetStateValue()
        {
            return 1;
        }
        public override string GetStateString()
        {
            return "Rejected";
        }    
    
    }
    public class PoItemApproved : PoItemState
    {
      public PoItemApproved()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.PurchasersManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Cancel PO",
            operationMethod=this.CancelPo
          };

          var opJobs1 = new List<JobDescription>();
          opJobs1.Add(JobDescription.Purchaser);
          opJobs1.Add(JobDescription.PurchasersManager);
          opJobs1.Add(JobDescription.Boss);
          opJobs1.Add(JobDescription.Admin);
          
          var operation1 = new Operation
          {
              jobs = opJobs1,
              operationName = "Waiting for Ship",
              operationMethod = this.SetPoStateWatingForShip

          };

          operationList.Add(operation);
          operationList.Add(operation1);
      
      }

      public void CancelPo(int poItemId)
      {
          UpdateState(poItemId, new PoItemCancelled().GetStateValue());

        int poId=PoMgr.PoMgr.GetPoIdAccordingToPoItemId(poItemId);
        List<sbyte> poStateList=PoMgr.PoMgr.GetPoItemStateListAccordingToPoId(poId);
        if (poStateList.Count == 1 || poStateList[0] == new PoItemCancelled().GetStateValue())
        {
            MessageBox.Show("Please be noted that all states of PO Items are changed to state CANCEL.The state of PO:" + Tool.Get6DigitalNumberAccordingToId(poId) + " will be changed to CANCEL! ");
            PoMgr.PoMgr.UpdatePoState(poId, (int)PoStatesEnum.Cancel);    
        
        }




      }
      public void SetPoStateWatingForShip(int poItemId)
      {
          UpdateState(poItemId, new PoItemWaitingForShip().GetStateValue());
          int soItemId= PoMgr.PoMgr.GetSoItemIdAccordingToPoItemId(poItemId);
          SoMgr.SoMgr.UpdateSoItemState(soItemId, new SoItemWaitingForShip().GetStateValue());
          //change the so, po state to under process
          int poId = PoMgr.PoMgr.GetPoIdAccordingToPoItemId(poItemId);
          int soId = SoMgr.SoMgr.GetSoIdAccordingToSoItemId(soItemId);
          PoMgr.PoMgr.UpdatePoState(poId, (int)PoStatesEnum.UnderProcess);
          SoMgr.SoMgr.UpdateSoState(soId, (int)SoStatesEnum.UnderProcess);

      }

        public override int GetStateValue()
        {
            return 2;
        }
        public override string GetStateString()
        {
            return "Approved";
        }    
    
    }
    public class PoItemCancelled : PoItemState
    {
        public override int GetStateValue()
        {
            return 3;
        }
        public override string GetStateString()
        {
            return "Cancelled";
        }    
    
    }
    public class PoItemWaitingForShip : PoItemState
    {

        public PoItemWaitingForShip()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.FinancialManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Full Payment Before Recv",
            operationMethod=this.SetStateFullPaymentBeforeRecv
          };
          var operation1 = new Operation
          {
              jobs = opJobs,
              operationName = "Deposit",
              operationMethod = this.SetStateDeposit

          };

          var opJobs1 = new List<JobDescription>();

          opJobs1.Add(JobDescription.LogisticsManager);
          opJobs1.Add(JobDescription.Boss);
          opJobs1.Add(JobDescription.Admin);
          
          var operation2 = new Operation
          {
              jobs = opJobs1,
              operationName = "Full Received Before Pay",
              operationMethod = this.SetStateFullRecivedBeforePay

          };
          var operation3 = new Operation
          {
              jobs = opJobs1,
              operationName = "Partial Received Before Pay",
              operationMethod = this.SetStatePartialRecivedBeforePay

          };


          operationList.Add(operation);
          operationList.Add(operation1);
          operationList.Add(operation2);
          operationList.Add(operation3);
      
      }

        public void SetStateFullPaymentBeforeRecv(int poId)
      {
          UpdateState(poId, new PoItemFullPaymentBeforeReceived().GetStateValue());

      }
        public void SetStateDeposit(int poId)
      {
          UpdateState(poId, new PoItemDeposit().GetStateValue());
      }

        public void SetStateFullRecivedBeforePay(int poId)
        {
            UpdateState(poId, new PoItemFullReceivedBeforePay().GetStateValue());

        }
        public void SetStatePartialRecivedBeforePay(int poId)
        {
            UpdateState(poId, new PoItemPartialReceivedBeforePay().GetStateValue());
        }
        
        
        public override int GetStateValue()
        {
            return 4;
        }
        public override string GetStateString()
        {
            return "Waiting For Ship";
        }  
    }
    public class PoItemFullPaymentBeforeReceived : PoItemState
    {
       public PoItemFullPaymentBeforeReceived()
       {
        var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.LogisticsManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Full Received After Pay",
            operationMethod=this.SetStateFullReceivedAfterPay
          };
          
          var operation1 = new Operation
          {
              jobs = opJobs,
              operationName = "Partial Received After Pay",
              operationMethod = this.SetStatePartialReceivedAfterPay

          };

          operationList.Add(operation);
          operationList.Add(operation1);
      
      }

       public void SetStateFullReceivedAfterPay(int poId)
      {
          UpdateState(poId, new PoItemFullReceivedAfterPay().GetStateValue());

      }
       public void SetStatePartialReceivedAfterPay(int poId)
      {
          UpdateState(poId, new PoItemPartialReceivedAfterPay().GetStateValue());
      }

    
        
        public override int GetStateValue()
        {
            return 5;
        }
        public override string GetStateString()
        {
            return "Full Payment Before Recv";
        }  
    
    }
    public class PoItemDeposit : PoItemState
    {
     public PoItemDeposit()
       {
        var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.FinancialManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Balance",
            operationMethod=this.Balance
          };
          
          operationList.Add(operation);
      
      }

      public void Balance(int poId)
      {
          UpdateState(poId, new PoItemBalance().GetStateValue());

      }
        public override int GetStateValue()
        {
            return 6;
        }
        public override string GetStateString()
        {
            return "Deposit";
        }  
    }
    public class PoItemBalance : PoItemState
    {
      public PoItemBalance()
       {
        var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.LogisticsManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Full Received After Pay",
            operationMethod=this.SetStateFullReceivedAfterPay
          };
          
          var operation1 = new Operation
          {
              jobs = opJobs,
              operationName = "Partial Received After Pay",
              operationMethod = this.SetStatePartialReceivedAfterPay

          };

          operationList.Add(operation);
          operationList.Add(operation1);
      
      }

       public void SetStateFullReceivedAfterPay(int poId)
      {
          UpdateState(poId, new PoItemFullReceivedAfterPay().GetStateValue());

      }
       public void SetStatePartialReceivedAfterPay(int poId)
      {
          UpdateState(poId, new PoItemPartialReceivedAfterPay().GetStateValue());
      }



        public override int GetStateValue()
        {
            return 7;
        }
        public override string GetStateString()
        {
            return "Balance";
        }  
    
    }
    public class PoItemFullReceivedAfterPay : PoItemState
    {

      public PoItemFullReceivedAfterPay()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.LogisticsManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Close PO",
            operationMethod=this.ClosePo
          };

          operationList.Add(operation);
      
      }

      public void ClosePo(int poItemId)
      {
          UpdateState(poItemId, new PoItemClosed().GetStateValue());
          int poId = PoMgr.PoMgr.GetPoIdAccordingToPoItemId(poItemId);
          List<sbyte> poStateList = PoMgr.PoMgr.GetPoItemStateListAccordingToPoId(poId);
          if (poStateList.Count == 1 || poStateList[0] == new PoItemClosed().GetStateValue())
          {
              MessageBox.Show("Please be noted that all states of PO Items are changed to state CLOSED.The state of PO:" + Tool.Get6DigitalNumberAccordingToId(poId) + " will be changed to CLOSED! ");
              PoMgr.PoMgr.UpdatePoState(poId, (int)PoStatesEnum.Closed);

          }


      }



        public override int GetStateValue()
        {
            return 8;
        }
        public override string GetStateString()
        {
            return "Full Received After Pay";
        }  
    
    }
    public class PoItemPartialReceivedAfterPay : PoItemState
    {
        public PoItemPartialReceivedAfterPay()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.LogisticsManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Full Received After Pay",
            operationMethod=this.SetStateFullReceivedAfterPay
          };

          operationList.Add(operation);
      
      }

        public void SetStateFullReceivedAfterPay(int poId)
      {
          UpdateState(poId, new PoItemFullReceivedAfterPay().GetStateValue());

      }
        
        public override int GetStateValue()
        {
            return 9;
        }
        public override string GetStateString()
        {
            return "Partial Received After Pay";
        }  
    
    }
    public class PoItemFullReceivedBeforePay : PoItemState
    {
      public PoItemFullReceivedBeforePay()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.FinancialManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Full Payment After Recv",
            operationMethod=this.SetStateFullPaymentAfterRecv
          };

          operationList.Add(operation);
      
      }

      public void SetStateFullPaymentAfterRecv(int poId)
      {
          UpdateState(poId, new PoItemFullPaymentAfterReceived().GetStateValue());

      }
        
        public override int GetStateValue()
        {
            return 10;
        }
        public override string GetStateString()
        {
            return "Full Received Before Pay";
        }  
    
    
    }
    public class PoItemPartialReceivedBeforePay : PoItemState
    {
        public PoItemPartialReceivedBeforePay()
      {
          var opJobs1 = new List<JobDescription>();
          opJobs1.Add(JobDescription.LogisticsManager);
          opJobs1.Add(JobDescription.Boss);
          opJobs1.Add(JobDescription.Admin);
          
          var operation2 = new Operation
          {
              jobs = opJobs1,
              operationName = "Full Received Before Pay",
              operationMethod = this.SetStateFullRecivedBeforePay

          };

          operationList.Add(operation2);
      
      }


        public void SetStateFullRecivedBeforePay(int poId)
        {
            UpdateState(poId, new PoItemFullReceivedBeforePay().GetStateValue());

        }

        
        public override int GetStateValue()
        {
            return 11;
        }
        public override string GetStateString()
        {
            return "Partial Received Before Pay";
        }  
    
    }
    public class PoItemFullPaymentAfterReceived : PoItemState
    {

      public PoItemFullPaymentAfterReceived()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.LogisticsManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Close",
            operationMethod=this.ClosePo
          };

          operationList.Add(operation);
      
      }

      public void ClosePo(int poItemId)
      {
          UpdateState(poItemId, new PoItemClosed().GetStateValue());
          int poId = PoMgr.PoMgr.GetPoIdAccordingToPoItemId(poItemId);
          List<sbyte> poStateList = PoMgr.PoMgr.GetPoItemStateListAccordingToPoId(poId);
          if (poStateList.Count == 1 || poStateList[0] == new PoItemClosed().GetStateValue())
          {
              MessageBox.Show("Please be noted that all states of PO Items are changed to state CLOSED.The state of PO:" + Tool.Get6DigitalNumberAccordingToId(poId) + " will be changed to CLOSED! ");
              PoMgr.PoMgr.UpdatePoState(poId, (int)PoStatesEnum.Closed);

          }


      } 
        
        public override int GetStateValue()
        {
            return 12;
        }
        public override string GetStateString()
        {
            return "Full Payment After Recv";
        }  
    }
    public class PoItemClosed : PoItemState
    {
        public override int GetStateValue()
        {
            return 13;
        }
        public override string GetStateString()
        {
            return "Closed";
        }  
    }



    public class PoItemStateList
    {
        List<PoItemState> poStateList = new List<PoItemState>();

        public PoItemStateList()
        {
            poStateList.Add(new PoItemNew());
            poStateList.Add(new PoItemRejected());
            poStateList.Add(new PoItemApproved());
            poStateList.Add(new PoItemCancelled());
            poStateList.Add(new PoItemWaitingForShip());
            poStateList.Add(new PoItemFullPaymentBeforeReceived());
            poStateList.Add(new PoItemDeposit());
            poStateList.Add(new PoItemBalance());
            poStateList.Add(new PoItemFullReceivedAfterPay());
            poStateList.Add(new PoItemPartialReceivedAfterPay());
            poStateList.Add(new PoItemFullReceivedBeforePay());
            poStateList.Add(new PoItemPartialReceivedBeforePay());
            poStateList.Add(new PoItemFullPaymentAfterReceived());
            poStateList.Add(new PoItemClosed());
        }


        public List<PoItemState> GetWholeSoStateList()
        {
            return poStateList;
        }

        public PoItemState GetPoStateAccordingToValue(int dbValue)
        {
            foreach (PoItemState state in poStateList)
            {
                if (state.GetStateValue() == dbValue)
                {
                    return state;
                }
            }
            return null;
        }

        public string GetPoStateStringAccordingToValue(int dbValue)
        {
            foreach (PoItemState state in poStateList)
            {
                if (state.GetStateValue() == dbValue)
                {

                    return state.GetStateString();
                }

            }
            return "";

        }

    }

    public enum PoStatesEnum
    {
        New = 0,
        Approved = 1,
        UnderProcess = 2,
        Closed = 3,
        Rejected = 4,
        Cancel = 5

    };



}
