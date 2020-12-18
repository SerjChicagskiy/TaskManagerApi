using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApiTaskManager.BLL.Comunication;
using WebApiTaskManager.DAL.Context;
using System.Security.Claims;
using WebApiTaskManager.BLL.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using WebApiTaskManager.DAL.Repositories;
using System.Linq;
using WebApiTaskManager.BLL.DTO;
using AutoMapper;

namespace WebApiTaskManager.BLL.Sevices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings appSettings;
        UnitOfWork unitOfWork;
        IMapper mapper;
        public AuthenticationService(IOptions<AppSettings> appSettings,UnitOfWork unitOfWork, IMapper mapper)
        {
            this.appSettings = appSettings.Value;
            this.unitOfWork=unitOfWork;
            this.mapper=mapper;
        }
        public async Task<UserResponse> AuthenticateAsync(string login, string password)
        {
            var user=(await unitOfWork.UserRepository.GetAllAsync())
                        .SingleOrDefault(usr=>usr.Login==login&&usr.Password==password);
            
            if(user==null)
                return new UserResponse("Invalid login or password!");
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var claims=new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.AddRange(user.UserRoles.Select(x=>new Claim(ClaimTypes.Role,x.Role.Name)));
                
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(appSettings.TokenExpires),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                var userDTO= mapper.Map<User,UserDTO>(user);
                userDTO.Password = null;

                return new UserResponse(userDTO);
            }
            catch(Exception ex)
            {
                return new UserResponse($"An error occured when authentication user: {ex.Message}");
            }
        }
    }
}