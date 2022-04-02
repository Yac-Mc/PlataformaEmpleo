using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string TypeUser { get; set; }
        public string Token { get; set; }
        public string PathImg { get; set; }
    }
}
