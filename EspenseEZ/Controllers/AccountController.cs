using Microsoft.AspNetCore.Mvc;

namespace ExpenseEZ.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
