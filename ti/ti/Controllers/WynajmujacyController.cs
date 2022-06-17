using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ti.Models;
using ti.Models.DbModels;

namespace ti.Controllers
{
    public class WynajmujacyController : Controller
    {

        // GET: Wynajmujacies
        /// <summary>
        /// Zwraca widok z listą wynajmujacych
        /// </summary>
        /// /// <returns></returns>
        public ActionResult Index()
        {
            using (DatabaseContext db1 = new DatabaseContext())
            {
                return View(db1.Wynajmujacy.ToList());
            }
        }


        // GET: Wynajmujacies/Details/5
        /// <summary> Zwraca widok ze szczegółami wynajmujacych</summary>
        /// <param name="id">identyfikator</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            Wynajmujacy wynajmujacy;
            using (DatabaseContext db1 = new DatabaseContext())
            {
                if (id == null)
                {   //zapytanie jest nieprawidłowe
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //znajduje danego wynajmujacego po id
                wynajmujacy = db1.Wynajmujacy.Find(id);
                if (wynajmujacy == null)
                {
                    return HttpNotFound();
                }
            }
            return View(wynajmujacy);
        }


        // GET: Wynajmujacies/Create
        /// <summary>
        /// zwraca widok do utworzenia nowego wynajmujacego
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }


        // POST: Wynajmujacies/Create
        /// <summary>
        ///  Pobiera dane wpisane przez użytkownika i gdy nie wystąpią żadne błędy zapisuje nowego wynajmujacego
        /// do bazy
        /// </summary>
        /// <param name="wynajmujacy"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WynajmujacyId,Imie,Nazwisko,Wydzial")] Wynajmujacy wynajmujacy)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db1 = new DatabaseContext())
                {

                    if (wynajmujacy.Imie == null || wynajmujacy.Nazwisko == null)
                    {
                        return View("Error1");
                    }
                    db1.Wynajmujacy.Add(wynajmujacy);
                    db1.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(wynajmujacy);
        }


        // GET: Wynajmujacies/Edit/5
        /// <summary>
        /// wraca widok do edycji wynajmujacego
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            Wynajmujacy wynajmujacy;
            using (DatabaseContext db1 = new DatabaseContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                wynajmujacy = db1.Wynajmujacy.Find(id);
                if (wynajmujacy == null)
                {
                    return HttpNotFound();
                }
            }
            return View(wynajmujacy);
        }


        // POST: Wynajmujacies/Edit/5
        /// <summary>
        /// zapisuje zmiany wprowadzone przez użytkownika
        /// </summary>
        /// <param name="wynajmujacy"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WynajmujacyId,Imie,Nazwisko,Wydzial")] Wynajmujacy wynajmujacy)
        {

            if (ModelState.IsValid)
            {
                using (DatabaseContext db1 = new DatabaseContext())
                {

                    if (wynajmujacy.Imie == null || wynajmujacy.Nazwisko == null)
                    {
                        return View("Error1");
                    }
                }
                using (DatabaseContext db1 = new DatabaseContext())
                {
                    db1.Entry(wynajmujacy).State = EntityState.Modified;
                    db1.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(wynajmujacy);
        }


        // GET: Wynajmujacies/Delete/5
        /// <summary>
        ///  zwraca widok z pytaniem o potwierdzenie usuwania
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            Wynajmujacy wynajmujacy;
            using (DatabaseContext db1 = new DatabaseContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                wynajmujacy = db1.Wynajmujacy.Find(id);
                if (wynajmujacy == null)
                {
                    return HttpNotFound();
                }
            }
            return View(wynajmujacy);
        }


        // POST: Wynajmujacies/Delete/5
        /// <summary>
        /// Po potwierdzeniu usuwania usuwa danego wynajmujacego z bazy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (DatabaseContext db1 = new DatabaseContext())
            {
                Wynajmujacy wynajmujacy = db1.Wynajmujacy.Include(x => x.SalaWynajmujacyList).FirstOrDefault(x => x.WynajmujacyId == id);
                db1.Wynajmujacy.Remove(wynajmujacy);
                db1.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
