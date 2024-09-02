using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace EProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly E_ProjectContext db;
        public HomeController(E_ProjectContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.AllComp = db.Competitions.ToList();
            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult posting()
        {
            var competition = db.Competitions.FirstOrDefault(p => p.CompetitionId == 4);
            if (competition != null)
            {
                ViewBag.SpecificComp = competition;
            }
            else
            {
                ViewBag.ErrorMessage = "Competition with Id 4 not found."; // Error message set kar sakte hain
            }
            return View();
        }


		public IActionResult AddPosting(Posting post1, IFormFile file)
		{
			try
			{
				if (file != null && file.Length > 0)
				{
					var filename = Path.GetFileName(file.FileName);
					string folderPath = Path.Combine("wwwroot/assets/img", filename);
					var dbpath = Path.Combine("assets/img", filename);
					using (var stream = new FileStream(folderPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
					post1.PostImg = dbpath;

					// Convert user id to integer
					int userId;
					if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId))
					{
						post1.PostUserId = userId;
					}
					else
					{
						// Handle error here, such as logging or showing user-friendly message
					}

					db.Add(post1);
					db.SaveChanges();
					TempData["Message"] = "Record Inserted Successfully";
					return RedirectToAction("Index", "Home"); // Redirect to appropriate action and controller
				}
				return View();
			}
			catch (DbUpdateException ex)
			{
				var innerException = ex.InnerException;
				// Access innerException to see the specific cause of the error
				// Log or handle the error as per your application's requirements
				// For now, you can redirect to an error page or display a generic error message
				return RedirectToAction("Error", "Home");
			}
		}




		public IActionResult contact()
        {
            return View();
        }

        public IActionResult exhibition()
        {
			return View(db.Exhibitions.ToList());
		}

        [HttpGet]
        public IActionResult RegisterStaff()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RegisterStaff(UserRecord user)
        {
            if (ModelState.IsValid)
            {
                db.Add(user);
                db.SaveChanges();
                TempData["Message"] = "Staff Registered Successfully..";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult RegisterUser(UserRecord user)
        {
            if (ModelState.IsValid)
            {
                db.Add(user);
                db.SaveChanges();
                TempData["Message"] = "User Registered Successfully..";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
        public IActionResult Login(UserRecord User)
        {
            var data = db.UserRecords.FirstOrDefault(x => x.UserEmail == User.UserEmail && x.UserPassword == User.UserPassword);
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (data != null)
            {

                if (data.UserRoleId == 1 && data.UserStatus == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
               new Claim(ClaimTypes.Name, data.UserName),
               new Claim(ClaimTypes.Email, data.UserEmail),
               new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
               new Claim(ClaimTypes.Role,"Admin")
           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else if (data.UserRoleId == 2 && data.UserStatus == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
               new Claim(ClaimTypes.Name, data.UserName),
               new Claim(ClaimTypes.Email, data.UserEmail),
               new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
               new Claim(ClaimTypes.Role,"Manager")
           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else if (data.UserRoleId == 3 && data.UserStatus == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
               new Claim(ClaimTypes.Name, data.UserName),
               new Claim(ClaimTypes.Email, data.UserEmail),
               new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
               new Claim(ClaimTypes.Role,"Staff")
           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else
                {
                    identity = new ClaimsIdentity(new[]
                 {
              new Claim(ClaimTypes.Name, data.UserName),
              new Claim(ClaimTypes.Email, data.UserEmail),
              new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
              new Claim(ClaimTypes.Role,"User")

           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    TempData["loginMsg"] = "login Successfully";
                    if (data.UserRoleId == 1)
                    {
                        return RedirectToAction("IndexAdmin", "Admin");
                    }

                    else if (data.UserRoleId == 2)
                    {
                        return RedirectToAction("IndexManager", "Manager");
                    }

                    else if (data.UserRoleId == 3)
                    {
                        return RedirectToAction("IndexStaff", "Staff");
                    }

                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
