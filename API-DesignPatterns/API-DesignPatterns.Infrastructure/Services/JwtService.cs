using API_DesignPatterns.Core.Exceptions;
using API_DesignPatterns.Core.Interfaces.Services;
using API_DesignPatterns.Core.Models;
using API_DesignPatterns.Infrastructure.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace API_DesignPatterns.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public JwtService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<string> GetJwtAsync(LoginModel loginModel)
        {
            var user = await userManager.FindByNameAsync(loginModel.Email); // bc. the email is also set as username

            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                byte[] jwtBytes = Encoding.UTF8.GetBytes(configuration.GetSection("JWT").Value);
                var secretKey = new SymmetricSecurityKey(jwtBytes);
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return token;
            }
            else
            {
                throw new InvalidCredentialsException("Invalid Credentials!");
            }
        }
    }
}
