using DNDServer.Authen.DTO;
using DNDServer.Authen.Request;
using DNDServer.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DNDServer.Authen.Repository
{
    public class AcccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public IConfiguration configuration { get; }


        public AcccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<DTOAuthResponse> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new DTOAuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid Email",
                };
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return new DTOAuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid Email",
                };
            }

            var authClaims = new List<Claim>
             {
            new Claim(ClaimTypes.Email, model.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var userRole = await userManager.GetRolesAsync(user);
            if (userRole != null)
            {
                foreach (var role in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpriyTime = DateTime.UtcNow.AddMinutes(1);
            await userManager.UpdateAsync(user);
            return new DTOAuthResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }


        // dang ky
        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.UserName,

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //kiem tra role customer da co chua 
                if (!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }

                await userManager.AddToRoleAsync(user, AppRole.Customer);
            }
            return result;
        }


        // FORGOT PASS WORD
        public async Task<IdentityResult> ForgotPassword(DTOForgotPassword dtoForgotPassword)
        {
            var user = await userManager.FindByEmailAsync(dtoForgotPassword.Email);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User with this email does not exist." });
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"http://localhost:4200/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";

            // Set up the RestSharp client and request to send the email using Mailtrap
            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            var request = new RestRequest
            {
                Method = Method.Post,
                RequestFormat = DataFormat.Json,
            };

            request.AddHeader("Authorization", "Bearer 6d5c1b9d8aa0262fffaa3a7c17737cc6");
            request.AddJsonBody(new
            {
                from = new { email = "hello@demomailtrap.com" },
                to = new[] { new { email = user.Email } },
                template_uuid = "1773ed64-4839-4603-8c07-47120bbe07dd",
                template_variables = new { user_email = user.Email, pass_reset_link = resetLink }
            });

            // Send the email
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "Failed to send the password reset email. Please try again." });
            }
        }

        //RESET PASSWORD
        public async Task<IdentityResult> ResetPassword(DTOResetPassword dtoResetPassWord)
        {
            var user = await userManager.FindByEmailAsync(dtoResetPassWord.Email);
            dtoResetPassWord.Token = WebUtility.UrlDecode(dtoResetPassWord.Token);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User with this email does not exist." });
            }

            var result = await userManager.ResetPasswordAsync(user, dtoResetPassWord.Token, dtoResetPassWord.NewPassword);

            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(result.Errors.ToArray());
        }

        // REFRESH TOKEN genaration

        private JwtSecurityToken GenerateJwt(string username)
        {
            var authClaims = new List<Claim>
    {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret valid configuration")));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );


            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        // handle refresh
        public async Task<DTOAuthResponse> Refresh(DTOToken model)
        {
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            if (principal?.Identity?.Name is null)
            {
                return new DTOAuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid token",
                };
            }
            var user = await userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpriyTime < DateTime.UtcNow)
            {
                return new DTOAuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid ",
                };
            }

            var token = GenerateJwt(principal.Identity.Name);
            return new DTOAuthResponse
            {
                IsSuccess = true,
                Message = "Refresh token successfully",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<DTOAuthResponse> Revoke(string? userName)
        {
            if (userName is null)
            {
                return new DTOAuthResponse
                {
                    IsSuccess = false,
                    Message = "No UserName ",
                };
            }

            var user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return new DTOAuthResponse
                {
                    IsSuccess = false,
                    Message = "No find user ",
                };
            }
            user.RefreshToken = null;
            await userManager.UpdateAsync(user);
            return new DTOAuthResponse
            {
                IsSuccess = true,
                Message = "Rekove Successed",
            };
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var authenKey = configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenKey)),
                ValidateLifetime = false, // Disable lifetime validation to allow expired tokens
                ValidIssuer = configuration["JWT:ValidIssuer"],
                ValidAudience = configuration["JWT:ValidAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return principal;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }

    }
}
