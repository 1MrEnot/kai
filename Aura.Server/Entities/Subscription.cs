namespace Aura.Server.Entities
{
    using System;

    public class Subscription {

        public Guid Id { get; set; }

        public SubscriptionType SubscriptionType { get; set; }

        public DateTime StartDate { get; set; }
    }
}