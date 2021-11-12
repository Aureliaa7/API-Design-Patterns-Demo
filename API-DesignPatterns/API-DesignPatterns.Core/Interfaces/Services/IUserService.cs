using API_DesignPatterns.Core.Models;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> LoginAsync(LoginModel loginModel);

        Task RegisterAsync(RegisterModel registerModel);
    }
}
