using DNDServer.Authen.DTO;
using DNDServer.Authen.Request;
using Microsoft.AspNetCore.Identity;

namespace DNDServer.Authen.Repository
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<DTOAuthResponse> SignInAsync(SignInModel model);
        public Task<IdentityResult> ForgotPassword(DTOForgotPassword dTOForgotPassword);
        public Task<IdentityResult> ResetPassword(DTOResetPassword dTOResetPassword);
        public Task<DTOAuthResponse> Refresh(DTOToken dTOToken);
        public Task<DTOAuthResponse> Revoke(string? userName);
    }
}
