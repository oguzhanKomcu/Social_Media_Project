using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.AppUserService;

namespace SMP.Presantation.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IAppUserService _appUserService;
        public ProfileController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var users = await _appUserService.UserDetails(id);
            return View(users);
        }

        public async Task<IActionResult> UserProfile()
        {
             
            var user = await _appUserService.UserDetails(User.GetUserId());
            return View(user);
        }

        

        public IActionResult Register()//LOYOUT NULL OLUCAK DİKAATT^'!!
        {
            if (User.Identity.IsAuthenticated) //user sistem tarafından geldi ..//kullanıcı hali hazırda sisteme authenticate olmuşsa
            {
                return RedirectToAction("Index", "Home"); //direk member home controlera gidicek // list gibi


                //home controllerada authorzia ekledil.
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)//LOYOUT NULL OLUCAK DİKAATT^'!!
        {
            if (ModelState.IsValid == true)
            {
                var result = await _appUserService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                    TempData["Warning"] = $"{item.Description}";
                }
            }

            return View(model);
        }

        //[AllowAnonymous]
        public IActionResult LogIn(string returnUrl = "/") //LOYOUT NULL OLUCAK DİKAATT^'!!
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl; //view data tasıdık hidden inputa alternatif //login olduktan sonra kaldıgımız yerden devam etmesiiçin bunu yazdık
            return View(); //hali haırda böyle bir kullanıcı var mı ? Yoksa view sayfası

        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LoginDTO model, string returnUrl = "/") //returUrl kaldıgımız yere yönlendirir.
        {

            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl); //kendi yazdıgımız methot 
                }

                TempData["Warning"] = "Invalid log in credantial..!!";
            }



            return View(model);
        }
        //view olusturulup yeler kalınca sayfa tarayısından sildik 

        private IActionResult RedirectToLocal(string returnUrl)  // kendimiz yazdıksadece burada çalıaşacak
        {
            //IsLocalUrl() fonksiyonu, parametresine aldığı değerin yerel bir URL olup olmadıgını kontrol eder.Bir Url bizim domain alanımızda ise yani biizm yetki alanımızda ise bize true değilse false dönecektik.


            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public async Task<IActionResult> logOut(string userName)
        {
            await _appUserService.LogOut();
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userName)
        {
            if (userName == User.Identity.Name) //login olmus kullanıcı ile sistemdeki kullanıcıyı yakalıyacak
            {
                var user = await _appUserService.GetById(User.GetUserId()); // bunun için bir ClaimPrincipleExtensions metot yazdım..Snra burada metodu çağırdım GetUserId().. 

                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(user);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfilDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                TempData["Success"] = "Your profile has been edited..!!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been edited..!!";
                return RedirectToAction("Edit");
            }


        }
        
        [HttpGet]
        public async Task<IActionResult> Users()
        {

            await _appUserService.GetUsers();
            return View(await _appUserService.GetUsers());




        }
    }
}
