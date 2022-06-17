using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ti.Models;
using ti.Models.DbModels;

namespace ti.Controllers
{
    /// <summary>
    /// kontroler dla budynku
    /// </summary>
    public class BudynkiController : Controller
    {
        /// <summary>
        /// Zwraca widok z listą budynków
        /// </summary>
        /// /// <returns></returns>
        public ActionResult Index()
        {
            using (DatabaseContext db1 = new DatabaseContext())
            {
                return View(db1.Budynki.ToList());
            }
        }


        /// <summary> Zwraca widok ze szczegółami budynku</summary>
        /// <param name="id">identyfikator</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            Budynek budynek;
            using (DatabaseContext db1 = new DatabaseContext())
            {
                if (id == null)
                {
                    //zapytanie jest nieprawidłowe
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //znajduje dany budynek po id
                budynek = db1.Budynki.Find(id);
                if (budynek == null)
                {
                    return HttpNotFound();
                }
            }
            return View(budynek);
        }


        /// <summary>
        /// zwraca widok do utworzenia nowego budynku
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Pobiera dane wpisane przez użytkownika i gdy nie wystąpią żadne błędy zapisuje nowy budynek
        /// do bzy
        /// </summary>
        /// <param name="budynek"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudynekId,Nazwa")] Budynek budynek)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db1 = new DatabaseContext())
                {
                    foreach (Budynek b in db1.Budynki)
                    {
                        if (b.Nazwa == budynek.Nazwa)
                        {
                            return View("Error");
                        }
                    }
                    if (budynek.Nazwa == null)
                    {
                        return View("Error1");
                    }
                    db1.Budynki.Add(budynek);
                    db1.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(budynek);
        }


        /// <summary>
        /// zwraca widok do edycji budynku
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            Budynek budynek;
            using (DatabaseContext db1 = new DatabaseContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                budynek = db1.Budynki.Find(id);
                if (budynek == null)
                {
                    return HttpNotFound();
                }
            }
            return View(budynek);
        }


        /// <summary>
        /// zapisuje zmiany wprowadzone przez użytkownika
        /// </summary>
        /// <param name="budynek"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudynekId,Nazwa")] Budynek budynek)
        {
            if (ModelState.IsValid)
            {
                using(DatabaseContext db1 = new DatabaseContext())
                {
                    foreach (Budynek b in db1.Budynki)
                    {
                        if (b.Nazwa == budynek.Nazwa)
                        {
                            return View("Error");
                        }
                    }
                    if (budynek.Nazwa == null)
                    {
                        return View("Error1");
                    }
                }
                using(DatabaseContext db1 = new DatabaseContext())
                {
                    db1.Entry(budynek).State = EntityState.Modified;
                    db1.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(budynek);
        }


        /// <summary>
        /// zwraca widok z pytaniem o potwierdzenie usuwania
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            Budynek budynek;
            using(DatabaseContext db1 = new DatabaseContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                budynek = db1.Budynki.Find(id);
                if (budynek == null)
                {
                    return HttpNotFound();
                }
            }
            return View(budynek);
        }


        /// <summary>
        /// Po potwierdzeniu usuwania usuwa dany budynek z bazy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (DatabaseContext db1 = new DatabaseContext())
            {
                Budynek budynek = db1.Budynki.Include(x => x.Sale).FirstOrDefault(x => x.BudynekId == id);
                db1.Budynki.Remove(budynek);
                db1.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
