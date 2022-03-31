using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Servicios.api.Seguridad.Core.Dto;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.JwtLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class CurrentUser
    {
        public class CurrentUserCommand : IRequest<List<UserDto>> { }

        public class AllUsersCommand : IRequest<List<UserDto>> { }

        public class CurrentUserHandler : IRequestHandler<CurrentUserCommand, List<UserDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IUserSession _userSession;
            private readonly IJwtGenerate _jwtGenerate;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CurrentUserHandler(UserManager<User> userManager, IUserSession userSession, IJwtGenerate jwtGenerate, IMapper mapper
                    , IHttpContextAccessor httpContextAccessor
                )
            {
                _userManager = userManager;
                _userSession = userSession;
                _jwtGenerate = jwtGenerate;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<List<UserDto>> Handle(CurrentUserCommand request, CancellationToken cancellationToken)
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];
                string token = authorizationHeader.Single().Split(" ").Last();
                if (await _jwtGenerate.IsActiveToken(token))
                {
                    await _jwtGenerate.CancelToken(token);
                    var user = await _userManager.FindByNameAsync(_userSession.GetUserSession());
                    if (user != null)
                    {
                        var userDto = _mapper.Map<User, UserDto>(user);
                        userDto.Token = _jwtGenerate.CreateToken(user);
                        return new List<UserDto> { userDto };
                    }

                    throw new Exception("No se encontró el usuario!");
                }

                throw new Exception($"El token {token} ya expiro!");
            }
        }

        public class AllUsersHandler : IRequestHandler<AllUsersCommand, List<UserDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public AllUsersHandler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }
            public async Task<List<UserDto>> Handle(AllUsersCommand request, CancellationToken cancellationToken)
            {
                var users = await _userManager.Users.ToListAsync();
                if (users.Any())
                {
                    List<UserDto> listUserDto = new List<UserDto>();
                    users.ForEach(x => listUserDto.Add(_mapper.Map<User, UserDto>(x)));
                    return listUserDto;
                }

                throw new Exception("No hay usuarios registrados!");
            }
        }
    }
}
