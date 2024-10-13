﻿using DNDServer.Authen.Request;
using DNDServer.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return string.Empty;
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return string.Empty;
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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

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
    }
}