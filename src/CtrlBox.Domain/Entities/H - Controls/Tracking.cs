﻿using CtrlBox.Domain.Common;
using System;
using System.Collections.Generic;

namespace CtrlBox.Domain.Entities
{
    public class Tracking : EntityBase
    {
        public Guid TrackingTypeID { get; set; }
        public TrackingType TrackingType { get; set; }

        public bool IsLastTrack { get; set; }

        public ICollection<TrackingBox> TrackingsBoxes { get; set; }
        public ICollection<TrackingClient> TrackingsClients { get; set; }

        private Tracking()
            : base()
        {
            this.TrackingsClients = new HashSet<TrackingClient>();
        }

        public void Init()
        {
            if (this.Id == null || this.Id == Guid.Empty)
            {
                base.InitBase();
            }
        }

        public void AddClient(Guid clientID)
        {
            this.TrackingsClients.Add(TrackingClient.FactoryCreate(this.Id, clientID));
        }

        internal static Tracking FactoryCreate(Guid trackingTypeID, Guid? boxID)
        {
            return new Tracking() { TrackingTypeID = trackingTypeID, IsLastTrack = true };
        }
    }
}
