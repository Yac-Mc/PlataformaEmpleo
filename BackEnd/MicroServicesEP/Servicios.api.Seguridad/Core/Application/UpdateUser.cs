using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Servicios.api.Seguridad.Core.Dto;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.JwtLogic;
using Servicios.api.Seguridad.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class UpdateUser
    {
        public class UserUpdateCommand : IRequest<UserDto>
        {
            public string Name { get; set; }
            public string Surnames { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class UserUpdateValidation : AbstractValidator<UserUpdateCommand>
        {
            public UserUpdateValidation()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Surnames).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class UserUpdaterHandler : IRequestHandler<UserUpdateCommand, UserDto>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerate _jwtGenerate;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public UserUpdaterHandler(UserManager<User> userManager, IMapper mapper, IJwtGenerate jwtGenerate, IHttpContextAccessor httpContextAccessor)
            {
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerate = jwtGenerate;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var user = await _userManager.FindByIdAsync(httpContext.Request.RouteValues["id"].ToString());
                if (user != null)
                {
                    if (request.Password != "")
                    {
                        var validatePassword = await _userManager.PasswordValidators[0].ValidateAsync(_userManager, user, request.Password);
                        if (!validatePassword.Succeeded)
                        {
                            StringBuilder errorPassword = new StringBuilder();
                            errorPassword.Append("Errores en el password: ");
                            int countError = 0;
                            validatePassword.Errors.ToList().ForEach(x => { 
                                countError++; 
                                errorPassword.Append($"{countError}: {x.Description}"); 
                            });
                            throw new Exception($"La nueva contraseña no es valida.{errorPassword.ToString()}. Por favor valide!");
                        }
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
                    }
                    user.Name = request.Name;
                    user.Surnames = request.Surnames;
                    user.Email = request.Email;
                    user.UserName = request.UserName;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var userDto = _mapper.Map<User, UserDto>(user);
                        userDto.Token = _jwtGenerate.CreateToken(user);
                        return userDto;
                    }
                }

                throw new Exception("No se pudo actualizar el usuario!");
            }
        }
    }
}
