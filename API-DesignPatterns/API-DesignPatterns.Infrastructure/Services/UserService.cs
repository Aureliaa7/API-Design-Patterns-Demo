using API_DesignPatterns.Core.Exceptions;
using API_DesignPatterns.Core.Interfaces.Services;
using API_DesignPatterns.Core.Models;
using API_DesignPatterns.Infrastructure.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace API_DesignPatterns.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtService jwtService;

        public UserService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            return await jwtService.GetJwtAsync(loginModel);
        }

        public async Task RegisterAsync(RegisterModel registerModel)
        {
            ApplicationUser user = new()
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            };
            var result = await userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(x => x.Description).ToList();
                throw new FailedRegistrationException(string.Join(", ", errorMessages));
            } 
        }
    }
}
