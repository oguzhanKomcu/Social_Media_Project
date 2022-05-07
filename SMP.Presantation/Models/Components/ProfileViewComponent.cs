using Microsoft.AspNetCore.Mvc;
using SMP.Application.Services.AppUserService;
using SMP.Application.Extensions;


namespace SMP.Presantation.Models.Components
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        public ProfileViewComponent(IAppUserService appUserService)
        {
            _appUserService = appUserService;

        }
    

        public IViewComponentResult Invoke()
        {
            var user = _appUserService.GetById("72a55ee0-766f-4857-aa11-e6ca88902a23");
            return View(user);
        }
    }
}
