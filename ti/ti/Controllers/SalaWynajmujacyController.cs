
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ti.Models;
using ti.Models.DbModels;

namespace ti.Controllers
{
    /// <summary>
    /// Kontroler dla rezerwacji
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class SalaWynajmujacyController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: SalaWynajmujacies
        /// <summary>
        /// Zwraca widok z listą rezerwacji
        /// </summary>
        /// /// <returns></returns>
        public ActionResult Index()
        {
            var salaWynajmujacy = db.SalaWynajmujacy.Include(s => s.Sala).Include(s => s.Wynajmujacy);
            return View(salaWynajmujacy.ToList());
        }


        /// <summary>
        /// Zwraca widok ze szczegółąmi rezerwacji
        /// </summary>
        /// <param name="idSali"></param>
        /// <param name="idWynajmujacego"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: SalaWynajmujacies/Details/5
        public ActionResult Details(int? idSali, int? idWynajmujacego, int? id)
        {
            if (idSali == null || idWynajmujacego == null ||id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaWynajmujacy salaWynajmujacy = db.SalaWynajmujacy.Include(s => s.Sala).Include(s => s.Wynajmujacy).FirstOrDefault(x => x.SalaId == idSali && x.WynajmujacyId == idWynajmujacego && x.SalaWynajmujacyId == id);
            if (idSali == null || idWynajmujacego == null || id==null)
            {
                return HttpNotFound();
            }
            return View(salaWynajmujacy);
        }


        /// <summary>
        /// Zwraca widok do utworzenia rezerwacji
        /// </summary>
        /// <returns></returns>
        // GET: SalaWynajmujacies/Create
        public ActionResult Create()
        {
            ViewBag.SalaId = new SelectList(db.Sale, "SalaId", "Numer");
            ViewBag.WynajmujacyId = new SelectList(db.Wynajmujacy, "WynajmujacyId", "Imie");
            return View();
        }


        /// <summary>
        /// Sprawdza rezerwacje
        /// </summary>
        /// <param name="salaWynajmujacy">The sala wynajmujacy.</param>
        /// <returns></returns>
        private bool SprawdzRezerwacje(SalaWynajmujacy salaWynajmujacy)
        {
            if (salaWynajmujacy.DataPoczatkowa.Date < DateTime.Now.Date)
            {
                return false;

            }
            else if (salaWynajmujacy.GodzinaKoncowa <= salaWynajmujacy.GodzinaPoczatkowa)
            {
                return false;
            }
            foreach (SalaWynajmujacy rez in db.SalaWynajmujacy)
            {
                if (rez.SalaId == salaWynajmujacy.SalaId)
                {

                    if (rez.DataPoczatkowa.Date == salaWynajmujacy.DataPoczatkowa.Date)
                    {
                        if ((salaWynajmujacy.GodzinaPoczatkowa >= rez.GodzinaPoczatkowa && salaWynajmujacy.GodzinaPoczatkowa < rez.GodzinaKoncowa))
                        {

                            return false;
                        }
                        else if (salaWynajmujacy.GodzinaKoncowa > rez.GodzinaPoczatkowa && salaWynajmujacy.GodzinaKoncowa <= rez.GodzinaKoncowa)
                        {

                            return false;
                        }
                        else if (salaWynajmujacy.GodzinaPoczatkowa < rez.GodzinaPoczatkowa && salaWynajmujacy.GodzinaKoncowa > rez.GodzinaKoncowa)
                        {

                            return false;
                        }
                        
                    }
                    return true;

                }
                return true;
            }
            return true;
        }


        // POST: SalaWynajmujacies/Create        
        /// <summary>
        /// Dodaje do bazy utworzona rezerwację
        /// </summary>
        /// <param name="salaWynajmujacy">The sala wynajmujacy.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaWynajmujacyId,SalaId,WynajmujacyId,DataPoczatkowa,GodzinaPoczatkowa,GodzinaKoncowa")] SalaWynajmujacy salaWynajmujacy)
        {
            if (ModelState.IsValid)
            {
                    if (!SprawdzRezerwacje(salaWynajmujacy))
                    {
                        return View("Error2");
                    }
                    db.SalaWynajmujacy.Add(salaWynajmujacy);
                    db.SaveChanges();
               }
                return RedirectToAction("Index");
            

            ViewBag.SalaId = new SelectList(db.Sale, "SalaId", "Numer", salaWynajmujacy.SalaId);
            ViewBag.WynajmujacyId = new SelectList(db.Wynajmujacy, "WynajmujacyId", "Imie", salaWynajmujacy.WynajmujacyId);
            return View(salaWynajmujacy);
        }



        // GET: SalaWynajmujacies/Edit/5        
        /// <summary>
        /// Zwraca widok do edycji rezerwacji
        /// </summary>
        /// <param name="idSali">The identifier sali.</param>
        /// <param name="idWynajmujacego">The identifier wynajmujacego.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int? idSali, int? idWynajmujacego, int? id)
        {
            if (idSali == null || idWynajmujacego == null || id ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaWynajmujacy salaWynajmujacy = db.SalaWynajmujacy.FirstOrDefault(x => x.SalaId == idSali && x.WynajmujacyId == idWynajmujacego && x.SalaWynajmujacyId == id);
            if (idSali == null || idWynajmujacego == null || id==null)
            {
                return HttpNotFound();
            }
            ViewBag.SalaId = new SelectList(db.Sale, "SalaId", "Numer", idSali);
            ViewBag.WynajmujacyId = new SelectList(db.Wynajmujacy, "WynajmujacyId", "Imie", idWynajmujacego);
            return View(salaWynajmujacy);
        }

        // POST: SalaWynajmujacies/Edit/5        
        /// <summary>
        /// Edytuje rezerwacje w bazie
        /// </summary>
        /// <param name="salaWynajmujacy">The sala wynajmujacy.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaId,WynajmujacyId,DataPoczatkowa,GodzinaPoczatkowa,GodzinaKoncowa")] SalaWynajmujacy salaWynajmujacy)
        {
            if (ModelState.IsValid)
            {
                if (SprawdzRezerwacje(salaWynajmujacy))
                {
                    return View("Error2");
                }
                SalaWynajmujacy r1 = db.SalaWynajmujacy.FirstOrDefault(x => x.SalaId == salaWynajmujacy.SalaId && x.WynajmujacyId == salaWynajmujacy.WynajmujacyId && x.SalaWynajmujacyId == salaWynajmujacy.SalaWynajmujacyId);
                db.SalaWynajmujacy.Remove(r1);
                db.SalaWynajmujacy.Add(salaWynajmujacy);
                //db.Entry(salaWynajmujacy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.SalaId = new SelectList(db.Sale, "SalaId", "Numer", salaWynajmujacy.SalaId);
            ViewBag.WynajmujacyId = new SelectList(db.Wynajmujacy, "WynajmujacyId", "Imie", salaWynajmujacy.WynajmujacyId);
            return View(salaWynajmujacy);
        }



        // GET: SalaWynajmujacies/Delete/5        
        /// <summary>
        /// Zwraca widok z prosba o potwierdzenie usuniecia rezerwacji
        /// </summary>
        /// <param name="idSali">The identifier sali.</param>
        /// <param name="idWynajmujacego">The identifier wynajmujacego.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int? idSali, int? idWynajmujacego, int? id)
        {
            if (idSali == null || idWynajmujacego == null || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalaWynajmujacy salaWynajmujacy = db.SalaWynajmujacy.Include(s => s.Sala).Include(s => s.Wynajmujacy).FirstOrDefault(x => x.SalaId == idSali && x.WynajmujacyId == idWynajmujacego && x.SalaWynajmujacyId==id);
            if (idSali == null || idWynajmujacego == null || id == null)
            {
                return HttpNotFound();
            }
            return View(salaWynajmujacy);
        }


        // POST: SalaWynajmujacies/Delete/5        
        /// <summary>
        /// Usuwa rezerwacje z bazy
        /// </summary>
        /// <param name="idSali">The identifier sali.</param>
        /// <param name="idWynajmujacego">The identifier wynajmujacego.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idSali, int idWynajmujacego, int id)
        {
            SalaWynajmujacy salaWynajmujacy = db.SalaWynajmujacy.FirstOrDefault(x => x.SalaId == idSali && x.WynajmujacyId == idWynajmujacego && x.SalaWynajmujacyId == id);
            db.SalaWynajmujacy.Remove(salaWynajmujacy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Zwalnia zasoby niezarządzane, a opcjonalnie także zarządzane
        /// </summary>
        /// <param name="disposing">Wartość true, aby zwolnić zasoby zarządzane i niezarządzane, a wartość false, aby zwolnić tylko zasoby niezarządzane.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
