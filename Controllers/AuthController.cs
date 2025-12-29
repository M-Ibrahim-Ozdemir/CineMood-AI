using CineMoodAI.Aplication.DTO;
using CineMoodAI.Aplication.interfaces;
using CineMoodAI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace CineMoodAI.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   

    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;   //bunları incekte ettik
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        //bir tane de metoy yazcaz en temelde kullancagımz yonetm kullanıcı yaratmamız lazım
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var user = new AppUser { UserName = dto.Username, DisplayName = dto.DisplayName }; //register oldugumuzda  dene app user yaratıyoz username ve diplay bu olan bir user yarattık
            var result = await _userManager.CreateAsync(user, dto.Password); //ıdentity kullanarak _usermanager bana bir user yarat deik. user bu passwordda bu. tüm gerekli guvenlik seylerini o yapıyor. Ver taanında kayıt yaratıyor

            if (!result.Succeeded) return BadRequest(result.Errors);

            return new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user), DisplayName = user.DisplayName };
       //register oldoktan hemen sonrada bir token yaratıyor
        }



        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username); //logınde de bu username yi arıyo
            if (user == null) return Unauthorized(); //yoksa uno der

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false); //varsa signInManager den CheckPasswordSignIn(yani şifreyi kontrol eder)
            if (!result.Succeeded) return Unauthorized(); //resulta bakıyo basarılı ise okey ise token yartır token doner  degise ano

            return new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user), DisplayName = user.DisplayName };
            //basarılı ise bi token yaratır

            //bu da tamam ama bizim controllerimiz hala herkse açık bunu enghellemenin tek yolu RecommendationControllere gidip [Authorize] yadık
        }
    }


    
}
