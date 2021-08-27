using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Records.Core.Domain.ValueObjects
{
    public class User
    {
        public Guid UserId { get; set; }

        public byte[] CryptoKey { get; set; }
    }
}
