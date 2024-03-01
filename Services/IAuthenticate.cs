using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carros.Api.Services
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> RegistrarUser(string email, string password);

        Task Logout();
    }
}