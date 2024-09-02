using EProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace EProject.Controllers
{
    public class ManagerController : Controller
    {

        private readonly E_ProjectContext db;
        public ManagerController(E_ProjectContext db)
        {
            this.db = db;
        }
        public IActionResult IndexManager()
        {
            return View();
        }

        public IActionResult exihibition()
        {
            return View();
        }

        public IActionResult Competition()
        {
            return View(db.Competitions.ToList());
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
    }
}
