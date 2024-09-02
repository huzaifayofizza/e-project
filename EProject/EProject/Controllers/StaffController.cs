using EProject.Data;
using EProject.Models;
using Microsoft.AspNetCore.Mvc;



namespace EProject.Controllers
{
    public class StaffController : Controller
    {

        private readonly E_ProjectContext db;
        public StaffController(E_ProjectContext db)
        {
            this.db = db;
        }
        public IActionResult IndexStaff()
        {
            return View();
        }

		public IActionResult exihibition()
		{
            return View(db.Exhibitions.ToList());
        }

        public IActionResult Deleteexihibition(int? id)
        {
            var data = db.Exhibitions.FirstOrDefault(x => x.ExhibitionId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Exihibition Deleted Successfully";
            return RedirectToAction(nameof(exihibition));
        }

        [HttpGet]
		public IActionResult exihibitionForm()
		{
			return View();
		}
        [HttpPost]
        public IActionResult exihibitionForm(Exhibition exihi, IFormFile file)
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
                exihi.ExhibitionImage = dbpath;
                db.Add(exihi);
                db.SaveChanges();
                TempData["Message"] = "Exihibition Inserted Successfully";
                return RedirectToAction(nameof(exihibition));
            }
            return View();
        }

        public IActionResult Competition()
		{
            return View(db.Competitions.ToList());
        }

		public IActionResult Posting()
		{
            return View(db.Postings.ToList());
        }

	}
}
