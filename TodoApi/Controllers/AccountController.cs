using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Entities.Identity;
using TodoApi.DTOs;
using Todo.Domain.Services;
using Microsoft.Win32;

namespace TodoApi.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Appuser> _signInManager;

        public AuthController(UserManager<Appuser> userManager, ITokenService tokenService, SignInManager<Appuser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }


       
        //Register
        [HttpPost("register")]
        // Data will be with json format 
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return BadRequest("Email already in use");

            var user = new Appuser
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.UserName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var token = await _tokenService.CreateTokenAsync(user, _userManager);

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                UserName = user.UserName,
                Token = token
            };
        }



        //Login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid email or password");

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
        }
















    }





}

