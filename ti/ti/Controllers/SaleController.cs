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
    /// Kontroler dla sali
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class SaleController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: Salas
        /// <summary>
        /// Zwraca widok z listą sal.
        /// </summary>
        /// /// <returns></returns>
        public ActionResult Index()
        {
            using (DatabaseContext db1 = new DatabaseContext())
            {
                var sale = db1.Sale.Include(s => s.Budynek);
                return View(sale.ToList());
            }
        }


        // GET: Salas/Details/5
        /// <summary> Zwraca widok ze szczegółami sali</summary>
        /// <param name="id">identyfikator</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            Sala sala;
            if (id == null)
            {
                //zapytanie jest nieprawidłowe
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //znajduje dana sale po id
            sala = db.Sale.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }


        // GET: Salas/Create
        /// <summary>
        /// zwraca widok do utworzenia nowej sali
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.BudynekId = new SelectList(db.Budynki, "BudynekId", "Nazwa");
            return View();
        }


        // POST: Salas/Create
        /// <summary>
        /// Pobiera dane wpisane przez użytkownika i gdy nie wystąpią żadne błędy zapisuje nowa sale
        /// do bazy
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaId,BudynekId,Numer,IloscMiejsca,TypSali")] Sala sala)
        {
            sala.Numer = $"{sala.BudynekId}.{sala.Numer}";
            if (ModelState.IsValid)
            {
                    foreach (Sala s in db.Sale)
                    {
                        if (s.Numer == sala.Numer)
                        {
                            return View("Error");
                        }
                    }
                    if (sala.Numer == null)
                    {
                        return View("Error1");
                    }
                    db.Sale.Add(sala);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            ViewBag.BudynekId = new SelectList(db.Budynki, "BudynekId", "Nazwa", sala.BudynekId);
            return View(sala);
        }


        // GET: Salas/Edit/5
        /// <summary>
        /// zwraca widok do edycji sal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Sale.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudynekId = new SelectList(db.Budynki, "BudynekId", "Nazwa", sala.BudynekId);
            return View(sala);
        }


        // POST: Salas/Edit/5
        /// <summary>
        /// zapisuje zmiany wprowadzone przez użytkownika
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaId,BudynekId,Numer,IloscMiejsca,TypSali")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db1 = new DatabaseContext())
                {
                    if (sala.Numer == null)
                    {
                        return View("Error1");
                    }
                    foreach (Sala s in db1.Sale)
                    {
                        if (s.Numer == sala.Numer )
                        {
                            return View("Error");
                        }
                    }
                }
                using (DatabaseContext db1 = new DatabaseContext())
                {
                    db1.Entry(sala).State = EntityState.Modified;
                    db1.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.BudynekId = new SelectList(db.Budynki, "BudynekId", "Nazwa", sala.BudynekId);
            return View(sala);
        }


        // GET: Salas/Delete/5
        /// <summary>
        /// zwraca widok z pytaniem o potwierdzenie usuwania
        /// </summary>
        /// <param name="id"></param>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Sale.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }


        // POST: Salas/Delete/5
        /// <summary>
        /// Po potwierdzeniu usuwania usuwa dana sale z bazy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sala sala = db.Sale.Include(x => x.SalaWynajmujacyList).FirstOrDefault(x => x.SalaId == id);
            db.Sale.Remove(sala);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Zwalnia zasoby niezarządzane, a opcjonalnie także zarządzane.
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
