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

		[HttpPost]
		public async Task<IActionResult> Edit(string id, EditViewModel model)
		{
            if (model.Id != id)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByIdAsync(model.Id);

                if (user is not null)
                {
                    user.Email = model.Email;
                    user.FullName = model.FullName;

					var result = await _userManager.UpdateAsync(user);

					if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
					{
						await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
					}

					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}

					foreach (IdentityError err in result.Errors)
					{
						ModelState.AddModelError("", err.Description);
					}
				}

                
            }

            return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user is not null)
                await _userManager.DeleteAsync(user);

            return View("Index");
        }
	}
}
