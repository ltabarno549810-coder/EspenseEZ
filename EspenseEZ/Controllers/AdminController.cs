using Microsoft.AspNetCore.Mvc;
using ExpenseEZ.Data;
using ExpenseEZ.Models;

namespace ExpenseEZ.Controllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AdminController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Dashboard()
		{
			ViewBag.Users = _context.Users.Where(x => !x.IsArchived).ToList();
			ViewBag.Departments = _context.Departments.Where(x => !x.IsArchived).ToList();
			ViewBag.Categories = _context.Categories.Where(x => !x.IsArchived).ToList();
			ViewBag.Budgets = _context.Budgets.Where(x => !x.IsArchived).ToList();
			ViewBag.Logs = _context.AuditLogs.OrderByDescending(x => x.Date).ToList();

			return View();
		}

		[HttpPost]
		public IActionResult AddUser(User user)
		{
			user.IsArchived = false;
			user.ArchivedAt = null;

			if (string.IsNullOrEmpty(user.Status))
			{
				user.Status = "Active";
			}

			_context.Users.Add(user);
			AddLog("Added user: " + user.Name);
			_context.SaveChanges();

			return RedirectToAction("Dashboard");
		}

		public IActionResult EditUser(int id)
		{
			var user = _context.Users.Find(id);

			if (user == null)
				return RedirectToAction("Dashboard");

			ViewBag.Departments = _context.Departments.Where(x => !x.IsArchived).ToList();

			return View(user);
		}

		[HttpPost]
		public IActionResult EditUser(User user)
		{
			var existingUser = _context.Users.Find(user.Id);

			if (existingUser != null)
			{
				existingUser.Name = user.Name;
				existingUser.Role = user.Role;
				existingUser.Department = user.Department;
				existingUser.Status = string.IsNullOrEmpty(user.Status) ? "Active" : user.Status;

				AddLog("Updated user: " + user.Name);
				_context.SaveChanges();
			}

			return RedirectToAction("Dashboard");
		}

		public IActionResult DeleteUser(int id)
		{
			var user = _context.Users.Find(id);

			if (user != null)
			{
				user.IsArchived = true;
				user.ArchivedAt = DateTime.Now;

				AddLog("Archived user: " + user.Name);
				_context.SaveChanges();
			}

			return RedirectToAction("Dashboard");
		}

		public IActionResult AssignRoles(string search)
		{
			ViewBag.Search = search;

			var users = _context.Users
				.Where(x => x.IsArchived == false)
				.Where(x =>
					string.IsNullOrEmpty(search) ||
					x.Name.Contains(search) ||
					x.Role.Contains(search) ||
					x.Department.Contains(search) ||
					x.Status.Contains(search)
				)
				.OrderBy(x => x.Name)
				.ToList();

			return View(users);
		}

		[HttpPost]
		public IActionResult UpdateRole(int id, string role)
		{
			var user = _context.Users.Find(id);

			if (user != null)
			{
				user.Role = role;

				AddLog("Updated role of " + user.Name + " to " + role);
				_context.SaveChanges();

				TempData["Success"] = "Role updated successfully.";
			}

			return RedirectToAction("AssignRoles");
		}

		[HttpPost]
		public IActionResult AddDepartment(Department department)
		{
			department.IsArchived = false;
			department.ArchivedAt = null;

			_context.Departments.Add(department);
			AddLog("Added department: " + department.Name);
			_context.SaveChanges();

			return RedirectToAction("Dashboard");
		}

		public IActionResult DeleteDepartment(int id)
		{
			var department = _context.Departments.Find(id);

			if (department != null)
			{
				department.IsArchived = true;
				department.ArchivedAt = DateTime.Now;

				AddLog("Archived department: " + department.Name);
				_context.SaveChanges();
			}

			return RedirectToAction("Dashboard");
		}

		[HttpPost]
		public IActionResult AddCategory(Category category)
		{
			category.IsArchived = false;
			category.ArchivedAt = null;

			_context.Categories.Add(category);
			AddLog("Added expense category: " + category.Name);
			_context.SaveChanges();

			return RedirectToAction("Dashboard");
		}

		public IActionResult DeleteCategory(int id)
		{
			var category = _context.Categories.Find(id);

			if (category != null)
			{
				category.IsArchived = true;
				category.ArchivedAt = DateTime.Now;

				AddLog("Archived category: " + category.Name);
				_context.SaveChanges();
			}

			return RedirectToAction("Dashboard");
		}

		[HttpPost]
		public IActionResult SaveBudget(Budget budget)
		{
			budget.IsArchived = false;
			budget.ArchivedAt = null;

			var existingBudget = _context.Budgets
				.FirstOrDefault(x => x.Department == budget.Department && !x.IsArchived);

			if (existingBudget == null)
			{
				_context.Budgets.Add(budget);
			}
			else
			{
				existingBudget.Amount = budget.Amount;
			}

			AddLog("Set budget for " + budget.Department + " to ₱" + budget.Amount);
			_context.SaveChanges();

			return RedirectToAction("Dashboard");
		}

		public IActionResult DeleteBudget(int id)
		{
			var budget = _context.Budgets.Find(id);

			if (budget != null)
			{
				budget.IsArchived = true;
				budget.ArchivedAt = DateTime.Now;

				AddLog("Archived budget for: " + budget.Department);
				_context.SaveChanges();
			}

			return RedirectToAction("Dashboard");
		}

		public IActionResult Archive(string search)
		{
			ViewBag.Search = search;

			ViewBag.Users = _context.Users
				.Where(x => x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Name.Contains(search) ||
					 x.Role.Contains(search) ||
					 x.Department.Contains(search)))
				.ToList();

			ViewBag.Departments = _context.Departments
				.Where(x => x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Name.Contains(search)))
				.ToList();

			ViewBag.Categories = _context.Categories
				.Where(x => x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Name.Contains(search)))
				.ToList();

			ViewBag.Budgets = _context.Budgets
				.Where(x => x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Department.Contains(search)))
				.ToList();

			return View();
		}

		public IActionResult Restore(string type, int id)
		{
			if (type == "User")
			{
				var item = _context.Users.Find(id);
				if (item != null)
				{
					item.IsArchived = false;
					item.ArchivedAt = null;
					AddLog("Restored user: " + item.Name);
				}
			}
			else if (type == "Department")
			{
				var item = _context.Departments.Find(id);
				if (item != null)
				{
					item.IsArchived = false;
					item.ArchivedAt = null;
					AddLog("Restored department: " + item.Name);
				}
			}
			else if (type == "Category")
			{
				var item = _context.Categories.Find(id);
				if (item != null)
				{
					item.IsArchived = false;
					item.ArchivedAt = null;
					AddLog("Restored category: " + item.Name);
				}
			}
			else if (type == "Budget")
			{
				var item = _context.Budgets.Find(id);
				if (item != null)
				{
					item.IsArchived = false;
					item.ArchivedAt = null;
					AddLog("Restored budget: " + item.Department);
				}
			}

			_context.SaveChanges();

			return RedirectToAction("Archive");
		}

		public IActionResult DeletePermanent(string type, int id)
		{
			if (type == "User")
			{
				var item = _context.Users.Find(id);
				if (item != null)
				{
					_context.Users.Remove(item);
					AddLog("Permanently deleted user: " + item.Name);
				}
			}
			else if (type == "Department")
			{
				var item = _context.Departments.Find(id);
				if (item != null)
				{
					_context.Departments.Remove(item);
					AddLog("Permanently deleted department: " + item.Name);
				}
			}
			else if (type == "Category")
			{
				var item = _context.Categories.Find(id);
				if (item != null)
				{
					_context.Categories.Remove(item);
					AddLog("Permanently deleted category: " + item.Name);
				}
			}
			else if (type == "Budget")
			{
				var item = _context.Budgets.Find(id);
				if (item != null)
				{
					_context.Budgets.Remove(item);
					AddLog("Permanently deleted budget: " + item.Department);
				}
			}

			_context.SaveChanges();

			return RedirectToAction("Archive");
		}

		public IActionResult Records(string search)
		{
			ViewBag.Search = search;

			ViewBag.Users = _context.Users
				.Where(x => !x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Name.Contains(search) ||
					 x.Role.Contains(search) ||
					 x.Department.Contains(search)))
				.ToList();

			ViewBag.Departments = _context.Departments
				.Where(x => !x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Name.Contains(search)))
				.ToList();

			ViewBag.Categories = _context.Categories
				.Where(x => !x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Name.Contains(search)))
				.ToList();

			ViewBag.Budgets = _context.Budgets
				.Where(x => !x.IsArchived &&
					(string.IsNullOrEmpty(search) ||
					 x.Department.Contains(search)))
				.ToList();

			return View();
		}

		public IActionResult ClearLogs()
		{
			var logs = _context.AuditLogs.ToList();

			_context.AuditLogs.RemoveRange(logs);
			_context.SaveChanges();

			return RedirectToAction("Dashboard");
		}

		private void AddLog(string action)
		{
			_context.AuditLogs.Add(new AuditLog
			{
				Action = action,
				Date = DateTime.Now
			});
		}
	}
}