using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using SMP.Domain.Models.Entities;
using SMP.Domain.UoW;

namespace SMP.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        //extansion metotlarına bakk !! 

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<GetAppUserVM> GetById(string id)
        {
            var user = await _unitOfWork.UserRepository.GetFilteredFirstOrDefault(

                selector: x => new GetAppUserVM
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Location = x.Location,
                    ImagePath = x.ImagePath,
                    Biyography = x.Biyography,
                    User_Score = x.User_Score,
                    Follower_Count = x.Follower_Count,
                    Following_Count = x.Following_Count,
                },

            expression: x => x.Id == id && x.Status != Status.Passive);

            var model = _mapper.Map<GetAppUserVM>(user);

            return model;

        }

        public async Task<List<GetAppUserVM>> GetUsers()
        {
            var categories = await _unitOfWork.UserRepository.GetFilteredList(
               selector: x => new GetAppUserVM
               {
                   Id = x.Id,
                   UserName = x.UserName,
                   Password = x.PasswordHash,
                   Email = x.Email,
               },
               expression: x => x.Status != Domain.Enums.Status.Passive,
               orderBy: x => x.OrderBy(x => x.UserName));

            return categories;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;

        }

        public async Task UpdateUser(UpdateProfilDTO model)
        {
            var user = await _unitOfWork.UserRepository.GetDefault(x => x.Id == model.Id);

            if (user != null)
            {
                if (model.UploadPath != null)
                {
                    using var image = Image.Load(model.UploadPath.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    string guid = Guid.NewGuid().ToString();
                    image.Save($"wwwrote/images/user/ {guid} jpg");
                    user.ImagePath = $"/images/user/{guid}.jpg";
                }

                if (model.UserName != null)
                {
                    await _userManager.SetUserNameAsync(user, model.UserName);
                }


                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    await _userManager.UpdateAsync(user);
                }

                if (model.Email != null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }


            }

        }

        public async Task<GetAppUserVM> UserDetails(string id)
        {
            var user = await _unitOfWork.UserRepository.GetFilteredFirstOrDefault(

                 selector: x => new GetAppUserVM
                 {
                     Id = x.Id,
                     UserName = x.UserName,
                     Email = x.Email,
                     Location = x.Location,
                     ImagePath = x.ImagePath,
                     Biyography = x.Biyography,
                     User_Score = x.User_Score,
                     Follower_Count = x.Follower_Count,
                     Following_Count = x.Following_Count,
                 },

             expression: x => x.Id == id && x.Status != Status.Passive);

            var model = _mapper.Map<GetAppUserVM>(user);

            return model;
        }
    }
}
