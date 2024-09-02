using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EProject.Controllers
{
	public class AdminController : Controller
	{
        private readonly E_ProjectContext db;

        public AdminController(E_ProjectContext db)
        {
            this.db = db;
        }
		public IActionResult Competition()
		{
			return View();
		}
        public IActionResult Addcompetition(Competition comp, IFormFile file)
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
                comp.CompetitionBanner = dbpath;
                db.Add(comp);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchComp));
            }
            return View();
        }
        public IActionResult FetchComp()
        {
            return View(db.Competitions.ToList());
        }
        public IActionResult DeleteCompetition(int? id)
        {
            var data = db.Competitions.FirstOrDefault(x => x.CompetitionId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchComp));
        }

        public IActionResult EditCompetition(int? id)
        {
            var data = db.Competitions.FirstOrDefault(x => x.CompetitionId == id);
            return View(data);
        }

        public IActionResult EditCompetition2(Competition comp, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbAddress = Path.Combine("assets/img", unique_name);
                comp.CompetitionBanner = dbAddress;
                db.Update(comp);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchComp));
            }
            else
            {
                var existingProduct = db.Competitions.FirstOrDefault(p => p.CompetitionId == comp.CompetitionId);
                if (existingProduct != null)
                {
                    comp.CompetitionBanner = existingProduct.CompetitionBanner;

                    // Detach existing tracked entity
                    db.Entry(existingProduct).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchComp));
                }
            }

            // Update entity state
            db.Update(comp);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchComp));
        }

        public IActionResult StaffRecord()
        {
            return View(db.UserRecords.Where(x => x.UserRoleId == 3).ToList());
        }

        public IActionResult ApproveStaff(int? id)
        {
            try
            {
                // Retrieve the staff record from the database
                var staff = db.UserRecords.FirstOrDefault(s => s.UserId == id);

                if (staff != null)
                {
                    staff.UserStatus = 1;
                    db.Update(staff);
                    db.SaveChanges();
                    return RedirectToAction("IndexAdmin", "Admin");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        public IActionResult DisApproveStaff(int? id)
        {
            try
            {
                // Retrieve the staff record from the database
                var staff = db.UserRecords.FirstOrDefault(s => s.UserId == id);

                if (staff != null)
                {
                    staff.UserStatus = 0;
                    db.Update(staff);
                    db.SaveChanges();
                    return RedirectToAction("IndexAdmin", "Admin");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        public IActionResult IndexAdmin()
		{
			return View();
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
