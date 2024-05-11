using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
	public class RolesController : Controller
	{
		private readonly RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
		{
			return View(_roleManager.Roles.ToList());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Role model)
		{
			if (ModelState.IsValid)
			{
				var role = new Role { Name = model.Name };
				var res = await _roleManager.CreateAsync(role);
				if (res.Succeeded)
				{
					return RedirectToAction("Index");
				}

				foreach(var error in res.Errors)
				{
					ModelState.AddModelError("",error.Description);
				}
			}
			return View(model);
		}
	}
}
