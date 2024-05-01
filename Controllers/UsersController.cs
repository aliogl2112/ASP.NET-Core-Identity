﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> _userManager; //kullanıcı işlemlerini yapacağımız usermanageri controllera dahil ettik
        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager=userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users); //tüm user bilgilerini (kullanıcı listesini) alıyoruz
        }
    }
}
