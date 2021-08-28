using System;

namespace Records.Core.Domain.ValueObjects
{
    public class User
    {
        public Guid UserId { get => Guid.Parse("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"); }

        public byte[] CryptoKey { get; set; }
    }
}
