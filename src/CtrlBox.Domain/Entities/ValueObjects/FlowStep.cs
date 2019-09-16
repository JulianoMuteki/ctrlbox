using System;
using CtrlBox.CrossCutting.Enums;

namespace CtrlBox.Domain.Entities.ValueObjects
{
    public class FlowStep
    {
        public EFlowStep EFlowStep { get; set; }

        private FlowStep()
        {
           this.EFlowStep = EFlowStep.Create;
        }

        public static FlowStep FactoryCreate()
        {
            return new FlowStep();
        }

        internal void SetFlowDelivery(bool hasCrossDocking)
        {
            if (hasCrossDocking)
                this.EFlowStep = EFlowStep.CrossDocking;
            else
                this.EFlowStep = EFlowStep.Delivery;
        }
        internal void SetFlowDelivered()
        {
            this.EFlowStep = EFlowStep.Delivery;
        }
        internal void SetFlowOrder()
        {
            this.EFlowStep = EFlowStep.Order;
        }

        internal void SetAvailable()
        {
            this.EFlowStep = EFlowStep.Available;
        }

        internal void SetInBox()
        {
            this.EFlowStep = EFlowStep.InBox;
        }

        internal void SetInStock()
        {
            this.EFlowStep = EFlowStep.InStock;
        }
    }
}
