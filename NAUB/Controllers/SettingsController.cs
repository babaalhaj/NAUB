using System;
using System.Linq;
using System.Web.Mvc;
using NAUB.Models;

namespace NAUB.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController()
        {
            _context=new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //
        // GET: /Settings/
        public ActionResult Index()
        {
            var setting = _context.Settings.SingleOrDefault(s => s.Id == 1);

            return View(setting);
        }
      
        //
        // GET: /Settings/Edit/5
        public ActionResult Edit(int id=1)
        {
            var setting = _context.Settings.Single(s => s.Id == id);
            return View(setting);
        }

        //
        // POST: /Settings/Edit/5
        [HttpPost]
        public ActionResult Edit()
        {
            if (!ModelState.IsValid)
                return View();
            var settingsInDb = _context.Settings.Single(s => s.Id == 1);
            try
            {
                // update logic
                TryUpdateModel(settingsInDb);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        
    }
}
