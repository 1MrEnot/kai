namespace Aura.Server.Entities
{
    using System;

    public class BankCard {

        public BankCard()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public long Number { get; set; }

        public int Cvv { get; set; }

        public byte ExpirationMonth { get; set; }

        public byte ExpirationYear { get; set; }
    }
}