using System;
using CtrlBox.CrossCutting.Enums;

namespace CtrlBox.Domain.Entities
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
                this.EFlowStep = EFlowStep.Delivered;
        }

        public void SetFlowDelivered()
        {
            this.EFlowStep = EFlowStep.Delivered;
        }

        public void SetFlowExpedition()
        {
            this.EFlowStep = EFlowStep.Expedition;
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

        internal void SetFlowCrossDocking()
        {
            this.EFlowStep = EFlowStep.CrossDocking;
        }
    }
}
