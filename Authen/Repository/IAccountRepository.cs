using DNDServer.Authen.Request;
using Microsoft.AspNetCore.Identity;

namespace DNDServer.Authen.Repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
