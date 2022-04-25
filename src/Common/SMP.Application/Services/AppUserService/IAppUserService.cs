using Microsoft.AspNetCore.Identity;
using SMP.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Application.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task LogOut();

        Task UpdateUser(UpdateProfilDTO model);

        Task<UpdateProfilDTO> GetById(string id);

    }
}
