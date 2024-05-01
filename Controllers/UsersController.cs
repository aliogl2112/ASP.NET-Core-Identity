using Identity.Models;
using Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<User> _userManager; //kullanıcı işlemlerini yapacağımız usermanageri controllera dahil ettik
        public UsersController(UserManager<User> userManager)
        {
            _userManager=userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_userManager.Users); //tüm user bilgilerini (kullanıcı listesini) alıyoruz
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateViewModel model)
		{
            if (ModelState.IsValid)
            {
                var user = new User
                {

                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };

                IdentityResult result = await _userManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("",err.Description); //hataları modele ekledik ve cshtml üzerinde gösterilecek. "" olan yere hangi state ile ilişkilendirmek istediğimizi yazıyoruz (emaili, password)
                }
            }
			return View(model);
		}

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
                return RedirectToAction("Index");

            var user = await _userManager.FindByIdAsync(id);

            if (user is not null)
            {
				return View(new EditViewModel
                {
                    Id=user.Id,
                    FullName=user.FullName,
                    Email=user.Email
                });
			}

			return RedirectToAction("Index");

		}
	}
}
