using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Servicios.api.Seguridad.Core.Dto;
using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class DeleteUser
    {
        public class DeleteUserCommand : IRequest<string> { }

        public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, string>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public DeleteUserHandler(UserManager<User> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _userManager = userManager;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var user = await _userManager.FindByIdAsync(httpContext.Request.RouteValues["id"].ToString());
                if (user != null)
                {
                    var userDto = _mapper.Map<User, UserDto>(user);
                    await _userManager.DeleteAsync(user);
                    return $"El usuario {userDto.UserName} con id:{userDto.Id} ha sido eliminado con éxito!";
                }

                return "No se encontró el usuario!";
            }
        }
    }
}
