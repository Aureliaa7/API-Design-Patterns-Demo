using API_DesignPatterns.Core.Models;
using System.Threading.Tasks;

namespace API_DesignPatterns.Core.Interfaces.Services
{
    public interface IJwtService
    {
        Task<string> GetJwtAsync(LoginModel loginModel);
    }
}
