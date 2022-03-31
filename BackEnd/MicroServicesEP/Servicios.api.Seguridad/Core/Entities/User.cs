using Microsoft.AspNetCore.Identity;

namespace Servicios.api.Seguridad.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string TypeUser { get; set; }
        public string PathImage { get; set; }
    }
}
