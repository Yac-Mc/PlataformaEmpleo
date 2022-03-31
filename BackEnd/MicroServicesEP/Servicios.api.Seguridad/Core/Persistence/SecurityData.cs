using Microsoft.AspNetCore.Identity;
using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Persistence
{
    public class SecurityData
    {
        public static async Task InsertNewUser(SecurityContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    Name ="Yoe Andres",
                    Surnames="Cardenas",
                    UserName="Ycardenas",
                    Email="yoe.cardenas@cun.edu.co",
                    TypeUser= "Administrator"
                };
                await userManager.CreateAsync(user, "Yac123456789#");
            }
        }
    }
}
