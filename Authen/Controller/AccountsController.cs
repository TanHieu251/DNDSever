using DNDServer.Authen.Repository;
using DNDServer.Authen.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace DNDServer.Authen.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;

        public AccountsController(IAccountRepository repo)
        {
            accountRepo = repo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            try
            {
                var result = await accountRepo.SignUpAsync(model);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Unauthorized(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while signing up: " + ex.Message);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var resul = await accountRepo.SignInAsync(model);
            if (string.IsNullOrEmpty(resul))
            {
                return Unauthorized();
            }
            return Ok(new { token = $" {resul}" });
        }
    }
}
