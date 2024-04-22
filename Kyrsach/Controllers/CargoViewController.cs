using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kyrsach.Controllers
{
    public class CargoViewController : Controller
    {
        // GET: CargoViewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CargoViewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CargoViewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargoViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CargoViewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CargoViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CargoViewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CargoViewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
