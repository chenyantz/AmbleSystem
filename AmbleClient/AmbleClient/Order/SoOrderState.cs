using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmbleClient.Order.SoMgr;

namespace AmbleClient.Order
{

    public enum OrderItemsState
    { 
     Normal,
     New,
     Modified
    };

    public class Operation
{
     public  List<JobDescription> jobs;
     public  string operationName;
     public  delegate void OperationMethod(int soId);
     public OperationMethod operationMethod;
    }
    
    
    
    public abstract class SoItemState
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
          listJobDes.Add(JobDescription.SalesManager);
          listJobDes.Add(JobDescription.Boss);
          listJobDes.Add(JobDescription.Admin);
          return listJobDes;
     }
      public void UpdateState(int soItemId, int soItemState)
      {
          SoMgr.SoMgr.UpdateSoItemState(soItemId,soItemState);

      }
      public List<Operation> GetOperationList()
      {
          return operationList;
      }

    }


  public class SoItemNew : SoItemState
  {
      public SoItemNew()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.SalesManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);

          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Reject SO",
            operationMethod=this.RejectSo
          };

          var operation1 = new Operation
          {
              jobs = opJobs,
              operationName = "Approve SO",
              operationMethod = this.ApproveSo

          };

          operationList.Add(operation);
          operationList.Add(operation1);
      
      }

      public override List<JobDescription> WhoCanUpdate()
      {
          var listJobDes = new List<JobDescription>();
          listJobDes.Add(JobDescription.Sales);
          listJobDes.Add(JobDescription.SalesManager);
          listJobDes.Add(JobDescription.Boss);
          listJobDes.Add(JobDescription.Admin);
          return listJobDes;

      }

      public override int GetStateValue()
      {
          return 0;
      }

      public override string GetStateString()
      {
          return "New";
      }

      public void RejectSo(int soId)
      {
          UpdateState(soId,new SoItemRejected().GetStateValue());
      }
      public void ApproveSo(int soId)
      {
          UpdateState(soId,new SoItemApprove().GetStateValue());
      }

  }


  public class SoItemRejected : SoItemState
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

  public class SoItemApprove : SoItemState
  {
      public SoItemApprove()
      {
          var opJobs=new List<JobDescription>();
          opJobs.Add(JobDescription.SalesManager);
          opJobs.Add(JobDescription.Boss);
          opJobs.Add(JobDescription.Admin);
          var operation = new Operation
          {
            jobs=opJobs,
            operationName="Cancel SO",
            operationMethod=this.CancelSo
          };
       }
      public override int GetStateValue()
      {
          return 2;
      }
      public override string GetStateString()
      {
          return "Approved";
      }
      public void CancelSo(int soId)
      {
          UpdateState(soId, new SoItemCancelled().GetStateValue());
      
      }
  }


 public class SoItemCancelled:SoItemState
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

 public class SoItemWaitingForShip : SoItemState
    {
        public SoItemWaitingForShip()
        {
        
        var opJobs1=new List<JobDescription>();
          opJobs1.Add(JobDescription.Financial);
          opJobs1.Add(JobDescription.FinancialManager);
          opJobs1.Add(JobDescription.Boss);
          opJobs1.Add(JobDescription.Admin);

          var operation = new Operation
          {
              jobs = opJobs1,
              operationName = "Payment Received Before Ship",
              operationMethod =this.SetStatePaymentRecvBeforeShip
          };

         var opJobs2=new List<JobDescription>();
          opJobs2.Add(JobDescription.Logistics);
          opJobs2.Add(JobDescription.LogisticsManager);
          opJobs2.Add(JobDescription.Boss);
          opJobs2.Add(JobDescription.Admin);

          var operation1 = new Operation
          {
              jobs = opJobs2,
              operationName = "Shipment Completed Before Pay",
              operationMethod = this.SetStateShipmentCompleteBeforePay

          };
          var operation2 = new Operation
          {
              jobs=opJobs2,
              operationName="Partial Shipment Before Pay",
              operationMethod=this.SetStatePartialShipmentBeforePay
          };
          operationList.Add(operation);
          operationList.Add(operation1);
          operationList.Add(operation2);

        }

        public void SetStatePaymentRecvBeforeShip(int soid)
        {
            UpdateState(soid, new SoItemPayMentRecvBeforeShip().GetStateValue());
        
        }

        public void SetStateShipmentCompleteBeforePay(int soid)
        {
            UpdateState(soid, new SoItemShipCompletedBeforePay().GetStateValue());
        }

        public void SetStatePartialShipmentBeforePay(int soid)
        {
            UpdateState(soid, new SoItemPartialShipBeforePay().GetStateValue());
        
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

    public class SoItemPayMentRecvBeforeShip : SoItemState
    {
        public SoItemPayMentRecvBeforeShip()
        {
            var opJobs = new List<JobDescription>();
            opJobs.Add(JobDescription.Logistics);
            opJobs.Add(JobDescription.LogisticsManager);
            opJobs.Add(JobDescription.Boss);
            opJobs.Add(JobDescription.Admin);

            var operation1 = new Operation
            {
                jobs = opJobs,
                operationName = "Shipment Completed After Pay",
                operationMethod = this.SetStateShipmentCompleteAfterPay

            };
            var operation2 = new Operation
            {
                jobs = opJobs,
                operationName = "Partial Shipment After Pay",
                operationMethod = this.SetStatePartialShipmentAfterPay
            };
            operationList.Add(operation1);
            operationList.Add(operation2);
        }
         
     private void SetStateShipmentCompleteAfterPay(int soid)
     {
         UpdateState(soid, new SoItemShipCompletedAfterPay().GetStateValue());
      }

     private void SetStatePartialShipmentAfterPay(int soid)
     {
         UpdateState(soid, new SoItemPartialShipAfterPay().GetStateValue());
     }

     public override int GetStateValue()
        {
            return 5;
        }
        public override string GetStateString()
        {
            return "Payment Received Before Ship";
        }
    
    }

    public class SoItemShipCompletedAfterPay : SoItemState
    {
        public SoItemShipCompletedAfterPay()
        {
            var opJobs = new List<JobDescription>();
            opJobs.Add(JobDescription.FinancialManager);
            opJobs.Add(JobDescription.Boss);
            opJobs.Add(JobDescription.Admin);

            var operation1 = new Operation
            {
                jobs = opJobs,
                operationName = "Close SO item",
                operationMethod = this.CloseSo

            };
            operationList.Add(operation1);

        }

        private void CloseSo(int soid)
        {
            UpdateState(soid, new SoItemClose().GetStateValue());
        
        }

        public override int GetStateValue()
        {
            return 6;
        }
        public override string GetStateString()
        {
            return "Shipment Complete after Pay";
        }
    
    }

    public class SoItemPartialShipAfterPay : SoItemState
    {
        public SoItemPartialShipAfterPay()
        {
            var opJobs = new List<JobDescription>();
            opJobs.Add(JobDescription.Logistics);
            opJobs.Add(JobDescription.LogisticsManager);
            opJobs.Add(JobDescription.Boss);
            opJobs.Add(JobDescription.Admin);

            var operation1 = new Operation
            {
                jobs = opJobs,
                operationName = "Shipment Completed After Pay",
                operationMethod = this.SetStateShipmentCompleteAfterPay

            };

            operationList.Add(operation1);

        }

        private void SetStateShipmentCompleteAfterPay(int soid)
        {
            UpdateState(soid, new SoItemShipCompletedAfterPay().GetStateValue());
        }

        public override int GetStateValue()
        {
            return 7;
        }
        public override string GetStateString()
        {
            return "Partial Shipment after Pay";
        }
    
    }
    public class SoItemShipCompletedBeforePay : SoItemState
    {
        public SoItemShipCompletedBeforePay()
        {
            var opJobs = new List<JobDescription>();
            opJobs.Add(JobDescription.Financial);
            opJobs.Add(JobDescription.FinancialManager);
            opJobs.Add(JobDescription.Boss);
            opJobs.Add(JobDescription.Admin);

            var operation1 = new Operation
            {
                jobs = opJobs,
                operationName = "Payment Received After Ship",
                operationMethod = this.SetStatePaymentReceviedAfterShip

            };

            operationList.Add(operation1);
       
        
        }

        private void SetStatePaymentReceviedAfterShip(int soid)
        {
            UpdateState(soid, new SoItemPayMentRecvAfterShip().GetStateValue());
        }

        public override int GetStateValue()
        {
            return 8;
        }
        public override string GetStateString()
        {
            return "Shipment Complete Before Pay";
        }

    }

    public class SoItemPartialShipBeforePay : SoItemState
    {
        public SoItemPartialShipBeforePay()
        {
            var opJobs = new List<JobDescription>();
            opJobs.Add(JobDescription.Logistics);
            opJobs.Add(JobDescription.LogisticsManager);
            opJobs.Add(JobDescription.Boss);
            opJobs.Add(JobDescription.Admin);

            var operation1 = new Operation
            {
                jobs = opJobs,
                operationName = "Shipment Completed Before Pay",
                operationMethod = this.SetStateShipmentCompleteBeforePay

            };

            operationList.Add(operation1);

        }

        private void SetStateShipmentCompleteBeforePay(int soid)
        {
            UpdateState(soid, new SoItemShipCompletedBeforePay().GetStateValue());
        }




        public override int GetStateValue()
        {
            return 9;
        }
        public override string GetStateString()
        {
            return "Partial Shipment Before Pay";
        }

    }

    public class SoItemPayMentRecvAfterShip : SoItemState
    {
        public SoItemPayMentRecvAfterShip()
        {
            var opJobs = new List<JobDescription>();
            opJobs.Add(JobDescription.FinancialManager);
            opJobs.Add(JobDescription.Boss);
            opJobs.Add(JobDescription.Admin);

            var operation1 = new Operation
            {
                jobs = opJobs,
                operationName = "Close SO",
                operationMethod = this.CloseSo

            };
            operationList.Add(operation1);

        }

        private void CloseSo(int soid)
        {
            UpdateState(soid, new SoItemClose().GetStateValue());

        }

        public override int GetStateValue()
        {
            return 10;
        }
        public override string GetStateString()
        {
            return "PayMent Received After Ship";
        }

    }

    public class SoItemClose : SoItemState
    {
        public override int GetStateValue()
        {
            return 11;
        }
        public override string GetStateString()
        {
            return "Closed";
        }
    
     }



  public  class SoItemOrderStateList
  {
     List<SoItemState> soItemStateList=new List<SoItemState>();

     public SoItemOrderStateList()
      { 

       soItemStateList.Add(new SoItemNew());
       soItemStateList.Add(new SoItemRejected());
       soItemStateList.Add(new SoItemApprove());
       soItemStateList.Add(new SoItemCancelled());
       soItemStateList.Add(new SoItemWaitingForShip());
       soItemStateList.Add(new SoItemPayMentRecvBeforeShip());
       soItemStateList.Add(new SoItemShipCompletedAfterPay());
       soItemStateList.Add(new SoItemPartialShipAfterPay());
       soItemStateList.Add(new SoItemShipCompletedBeforePay());
       soItemStateList.Add(new SoItemPartialShipBeforePay());
       soItemStateList.Add(new SoItemPayMentRecvAfterShip());
       soItemStateList.Add(new SoItemClose());
     
      }

      
      public List<SoItemState> GetWholeSoStateList()
      {
          return soItemStateList;     
      }

      public SoItemState GetSoStateAccordingToValue(int dbValue)
      {
          foreach (SoItemState state in soItemStateList)
          {
              if (state.GetStateValue() == dbValue)
              {
                  return state;
              }
          
          
          }
          return null;
      }

      public string GetSoStateStringAccordingToValue(int dbValue)
      {
          foreach (SoItemState state in soItemStateList)
          {
              if (state.GetStateValue() == dbValue)
              {

                  return state.GetStateString();
              }
          
          }
          return "";
      
      }
  
  
  
  
  }


}
