using Common.Authentication.Identity.Common.Models;

namespace Common.Authentication.Identity.Interfaces
{
    public interface ICurrentUserService
    {
         UserModel User { get; }
    }
}
