using System.Collections.Generic;

namespace Common.Authentication.Identity.Common.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public long ProfileId { get; set; }
        public List<string> Roles { get; set; }
    }
}
