using Microsoft.AspNetCore.Mvc;

namespace ExpenseEZ.Controllers
{
	public class EmployeeController : Controller
	{
		public IActionResult Dashboard()
		{
			return View();
		}

		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Edit()
		{
			return View();
		}

		public IActionResult Records()
		{
			return View();
		}
	}
}