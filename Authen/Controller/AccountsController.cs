using DNDServer.Authen.DTO;
using DNDServer.Authen.Repository;
using DNDServer.Authen.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace DNDServer.Authen.Controller
{

    // DANG KY 
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

        // DANG NHAP 
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var resul = await accountRepo.SignInAsync(model);
            if (string.IsNullOrEmpty(resul.Token))
            {
                return Unauthorized();
            }
            return Ok(resul);
        }


        // FORGOT PASSWORD
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] DTOForgotPassword dtoForgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var result = await accountRepo.ForgotPassword(dtoForgotPassword);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Password reset email has been sent successfully." });
            }

            return BadRequest(result.Errors.Select(e => e.Description).ToList());
        }

        // RESET PASSWORD
        [HttpPost("reset-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] DTOResetPassword dTOResetPassword)
        {
            // Kiểm tra tính hợp lệ của DTO
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            // Gọi repository để thực hiện logic reset password
            var result = await accountRepo.ResetPassword(dTOResetPassword);

            // Kiểm tra kết quả của reset password
            if (result.Succeeded)
            {
                return Ok(new { Message = "Password reset successful." });
            }

            // Nếu thất bại, trả về các lỗi chi tiết
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { Errors = errors });
        }

        // REFRESH TOKEN
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] DTOToken model)
        {
            if (ModelState.IsValid)
            {
                BadRequest("Invalid data");
            }

            var result=await accountRepo.Refresh(model);
            if (result.IsSuccess==true)
            {
                return Ok();
            }
            //return BadRequest(result.Errors.Select(e => e.Description).ToList());
            return BadRequest(new DTOAuthResponse
            {
                IsSuccess=false,
                Message="Error when refresh token"
            });
        }


        //REVOKE
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody]string?  userName)
        {
            if (ModelState.IsValid)
            {
                BadRequest("Invalid data");
            }

            var result = await accountRepo.Revoke(userName);
            if (result.IsSuccess == true)
            {
                return Ok();
            }
            return BadRequest(new DTOAuthResponse
            {
                IsSuccess = false,
                Message = "Error when refresh token"
            });
        }



    }

}
