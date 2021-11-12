using API_DesignPatterns.Core.DTOs;
using API_DesignPatterns.Core.Interfaces.Services;
using API_DesignPatterns.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_DesignPatterns.API.Controllers
{
    public class AccountsController : APIDesignPatternsController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public AccountsController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await userService.LoginAsync(mapper.Map<LoginModel>(loginDto));
            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await userService.RegisterAsync(mapper.Map<RegisterModel>(registerDto));
            return Ok();
        }
    }
}
