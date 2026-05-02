using Microsoft.AspNetCore.Mvc;

namespace ExpenseEZ.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(string username, string password)
		{
			if (username == "admin" && password == "admin123")
			{
				return RedirectToAction("Dashboard", "Admin");
			}

			if (username == "employee" && password == "employee123")
			{
				return RedirectToAction("Dashboard", "Employee");
			}

			if (username == "manager" && password == "manager123")
			{
				return RedirectToAction("Dashboard", "Manager");
			}

			if (username == "finance" && password == "finance123")
			{
				return RedirectToAction("Dashboard", "Finance");
			}

			ViewBag.Error = "Invalid username or password.";
			return View();
		}
	}
}