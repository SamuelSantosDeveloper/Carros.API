using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Carros.Api.Services;
using Carros.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Carros.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authentication;

        public AccountController(IConfiguration configuration, IAuthenticate authentication)
        {
            _configuration = configuration ??
            throw new ArgumentNullException(nameof(configuration));

            _authentication = authentication ??
             throw new ArgumentNullException(nameof(authentication));
        }

        [HttpPost("CreateUser")]

        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterViewModel model){
            if(model.Password != model.ConfirmPassword){
                ModelState.AddModelError("ConfirmPassword", "As senhas não conferem");
                return BadRequest(ModelState);
            }

            var result = await _authentication.RegistrarUser(model.Email, model.Password);

            if (result == true){
                return Ok($"Usuário {model.Email} criado com sucesso!");
            }else{
                ModelState.AddModelError("CreateUser", "Cadastro inválido");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]

        public async Task<ActionResult<UserToken>> Login([FromBody] LoginViewModel userInfor){
           var result = await _authentication.Authenticate(userInfor.Email, userInfor.Password);

           if(result == true){
            return GenerateToken(userInfor);
           }
           else{
            ModelState.AddModelError("LoginUser", "Login inválido!");
            return BadRequest(ModelState);
           }

        }

        private ActionResult<UserToken> GenerateToken(LoginViewModel userInfor)
        {
            var claims = new []
            {
                new Claim("email", userInfor.Email),
                new Claim("meuToken", "token do samuel"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken(){
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}