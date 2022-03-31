using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.Seguridad.Core.Application;
using Servicios.api.Seguridad.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUser.UserRegisterCommand parameters)
        {
            return await _mediator.Send(parameters);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginUser.UserLoginCommand parameters)
        {
            return await _mediator.Send(parameters);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get(bool currentUser)
        {
            if (currentUser)
            {
                return await _mediator.Send(new CurrentUser.CurrentUserCommand());
            }
            return await _mediator.Send(new CurrentUser.AllUsersCommand());
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete()
        {
            return await _mediator.Send(new DeleteUser.DeleteUserCommand());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Put(UpdateUser.UserUpdateCommand parameters)
        {
            return await _mediator.Send(parameters);
        }
    }
}
