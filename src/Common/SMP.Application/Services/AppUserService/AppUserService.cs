using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Domain.Enums;
using SMP.Domain.Models.Entities;
using SMP.Domain.UoW;
using SMP.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SMP.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        //extansion metotlarına bakk !! 

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly AppSettings _appSettings;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _appSettings = appSettings.Value;
        }


        public async Task<AppUser> Authentication(string userName, string password)
        {




            var user = _context.AppUsers.SingleOrDefault(x => x.UserName == userName);



            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    user.Token = tokenHandler.WriteToken(token);
                }
                catch (Exception e)
                {

                    throw e;
                }


                return user;
            }
            else
            {



                return null;
            }
        }




        public async Task<UpdateProfilDTO> GetById(string id)
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

            var model = _mapper.Map<UpdateProfilDTO>(user);

            return model;

        }

        public async Task<List<GetAppUserVM>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetFilteredList(
               selector: x => new GetAppUserVM
               {
                   Id = x.Id,
                   UserName = x.UserName,
                   Location = x.Location,
                   ImagePath = x.ImagePath,
                   User_Score = x.Post_Scores.Average(y => y.Score).ToString(),
               },
               expression: x => x.Status != Domain.Enums.Status.Passive,
               orderBy: x => x.OrderBy(x => x.UserName));

            return users;
        }

        public async Task<List<GetAppUserVM>> GetUserName(string user)
        {


            var users = await _unitOfWork.UserRepository.GetFilteredList(
               selector: x => new GetAppUserVM
               {
                   Id = x.Id,
                   UserName = x.UserName,
                   Location = x.Location,
                   ImagePath = x.ImagePath,
                   User_Score = x.Post_Scores.Average(y => y.Score).ToString(),
               },
               expression: x => x.Status != Domain.Enums.Status.Passive && x.UserName.Contains(user) || user == null);


            return users;
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

            model.ImagePath = $"/images/user/default-profile-icon-24.jpg";
            
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
                    image.Save($"wwwroot/images/user/{guid}.jpg");
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

        public async Task<GetAppUserVM> UserDetails(string userId, string followerId)
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
                     User_Score =  Math.Round(x.Posts.Average(y => y.Total_Score), 1).ToString(), 
                     Follower_Count = x.Followings.Count(y => y.Status != Status.Passive).ToString(),
                     Following_Count = x.Followers.Count(y=> y.Status != Status.Passive).ToString(),
                     Followers = x.Followings.Where(x=> x.FollowingId == userId && x.Status !=Status.Passive && x.FollowerId == followerId).Select(y => new FollowVM
                     {
                         Id = y.Id,
                         FollowerId = y.FollowerId,
                         FollowingId = y.FollowingId,
                         FollowerUserName = y.Follow.UserName,
                     }).ToList(),
                     UserPosts = x.Posts.Where(x => x.User_Id == userId && x.Status != Status.Passive || x.User_Id == null).OrderByDescending(z => z.CreateDate).Select(y => new GetPostVM
                     {
                         Id = y.Id,
                         Description = y.Description,
                         ImagePath = y.ImagePath,
                         Total_Score = y.Post_Scores.Average(z => z.Score).ToString(),
                         Total_Comment = y.Post_Comments.Count.ToString(),
                         UserName = y.AppUser.UserName,
                         UserImagePath = y.AppUser.ImagePath,
                         CreateDate = y.CreateDate,
                     }).ToList(),
                     SaharingPosts = x.PostSharings.Where(x => x.UserId == userId && x.Status != Status.Passive || x.UserId == null).OrderByDescending(z => z.CreateDate).Select(y => new PostandPostSharingVm
                     {
                         Id = y.Id,
                         Description = y.Post.Description,
                         ImagePath = y.Post.ImagePath,
                         Total_Score =  Math.Round(x.Posts.Average(y => y.Total_Score), 1).ToString(),
                         Total_Comment = y.Post.Post_Comments.Count.ToString(),
                         UserName = y.Post.AppUser.UserName,
                         UserImagePath = y.Post.AppUser.ImagePath,
                         CreateDate = y.CreateDate,

                     }).ToList(),

                 },

             expression: x => x.Id == userId && x.Status != Status.Passive);
            


            return user;
        }


        public async Task<GetAppUserVM> UserProfile(string id)
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
                     User_Score = Math.Round(x.Posts.Average(y => y.Total_Score), 1).ToString(),
                     Follower_Count = x.Followings.Count(y => y.Status != Status.Passive).ToString(),
                     Following_Count = x.Followers.Count(y => y.Status != Status.Passive).ToString(),
 
                     UserPosts = x.Posts.Where(x => x.User_Id == id && x.Status != Status.Passive || x.User_Id == null).OrderByDescending(z => z.CreateDate).Select(y => new GetPostVM
                     {
                         Id = y.Id,
                         Description = y.Description,
                         ImagePath = y.ImagePath,
                         Total_Score = y.Post_Scores.Average(z => z.Score).ToString(),
                         Total_Comment = y.Post_Comments.Count.ToString(),
                         UserName = y.AppUser.UserName,
                         UserImagePath = y.AppUser.ImagePath,
                         CreateDate = y.CreateDate,
                     }).ToList(),
                     SaharingPosts = x.PostSharings.Where(x => x.UserId == id && x.Status != Status.Passive || x.UserId == null).OrderByDescending(z => z.CreateDate).Select(y => new PostandPostSharingVm
                     {
                         Id = y.Id,
                         Description = y.Post.Description,
                         ImagePath = y.Post.ImagePath,
                         Total_Score = Math.Round(x.Posts.Average(y => y.Total_Score), 1).ToString(),
                         Total_Comment = y.Post.Post_Comments.Count.ToString(),
                         UserName = y.Post.AppUser.UserName,
                         UserImagePath = y.Post.AppUser.ImagePath,
                         CreateDate = y.CreateDate,

                     }).ToList(),

                 },

             expression: x => x.Id == id && x.Status != Status.Passive);



            return user;
        }

    }
}
