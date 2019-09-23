using System;
using CtrlBox.CrossCutting.Enums;

namespace CtrlBox.Domain.Entities.ValueObjects
{
    public class FlowStep
    {
        public EFlowStep EFlowStep { get; protected set; }

        private FlowStep()
        {
           this.EFlowStep = EFlowStep.Create;
        }

        public static FlowStep FactoryCreate()
        {
            return new FlowStep();
        }

        public void SetFlowDelivery(bool hasCrossDocking)
        {
            if (hasCrossDocking)
                this.EFlowStep = EFlowStep.CrossDocking;
            else
                this.EFlowStep = EFlowStep.Delivery;
        }
        public void SetFlowDelivered()
        {
            this.EFlowStep = EFlowStep.Delivery;
        }
        public void SetFlowOrder()
        {
            this.EFlowStep = EFlowStep.Order;
        }

        public void SetAvailable()
        {
            this.EFlowStep = EFlowStep.Available;
        }

        public void SetInBox()
        {
            this.EFlowStep = EFlowStep.InBox;
        }

        public void SetInStock()
        {
            this.EFlowStep = EFlowStep.InStock;
        }
    }
}
