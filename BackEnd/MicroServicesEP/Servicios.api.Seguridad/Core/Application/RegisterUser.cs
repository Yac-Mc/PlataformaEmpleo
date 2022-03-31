using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Servicios.api.Seguridad.Core.Dto;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.JwtLogic;
using Servicios.api.Seguridad.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class RegisterUser
    {
        public class UserRegisterCommand : IRequest<UserDto>
        {
            public string Name { get; set; }
            public string Surnames { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string TypeUser { get; set; }
        }

        public class UserRegisterValidation : AbstractValidator<UserRegisterCommand>
        {
            public UserRegisterValidation()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Surnames).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.TypeUser).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserDto>
        {
            private readonly SecurityContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly IJwtGenerate _jwtGenerate;
            public UserRegisterHandler(SecurityContext context, UserManager<User> userManager, IMapper mapper, IJwtGenerate jwtGenerate)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
                _jwtGenerate = jwtGenerate;
            }

            public async Task<UserDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
            {
                bool exists = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (exists)
                {
                    throw new Exception("El email del usuario ya existe!");
                }

                exists = await _context.Users.Where(x => x.UserName == request.UserName).AnyAsync();
                if (exists)
                {
                    throw new Exception("El UserName del usuario ya existe!");
                }

                var user = new User
                {
                    Name = request.Name,
                    Surnames = request.Surnames,
                    Email = request.Email,
                    UserName = request.UserName,
                    TypeUser = request.TypeUser
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<User, UserDto>(user);
                    userDto.Token = _jwtGenerate.CreateToken(user);
                    return userDto;
                }

                throw new Exception("No se pudo registrar el usuario!");
            }
        }
    }
}
