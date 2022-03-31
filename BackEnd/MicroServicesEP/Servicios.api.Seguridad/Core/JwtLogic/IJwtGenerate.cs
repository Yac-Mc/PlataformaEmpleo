using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.JwtLogic
{
    public interface IJwtGenerate
    {
        string CreateToken(User user);
        Task CancelToken(string token);
        Task<bool> IsActiveToken(string token);
    }
}
