using Microsoft.AspNetCore.Mvc;

namespace ExpenseEZ.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Dashboard()
		{
			return View();
		}
	}
}