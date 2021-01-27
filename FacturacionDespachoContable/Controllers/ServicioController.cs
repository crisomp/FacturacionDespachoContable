using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FacturacionDespachoContable.Models;

namespace FacturacionDespachoContable.Controllers
{
    public class ServicioController : Controller
    {
        private despachoContableEntities db = new despachoContableEntities();

        // GET: Servicio
        public async Task<ActionResult> Index()
        {
            return View(await db.servicio.ToListAsync());
        }

        // GET: Servicio/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servicio servicio = await db.servicio.FindAsync(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // GET: Servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idServicio,nombre,precio")] servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.servicio.Add(servicio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(servicio);
        }

        // GET: Servicio/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servicio servicio = await db.servicio.FindAsync(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: Servicio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idServicio,nombre,precio")] servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(servicio);
        }

        // GET: Servicio/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            servicio servicio = await db.servicio.FindAsync(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            servicio servicio = await db.servicio.FindAsync(id);
            db.servicio.Remove(servicio);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public async Task<ActionResult> ObtenerServicios()
        {
            List<servicio> servicios = await db.servicio.ToListAsync();
            return View(servicios);
        }

        
    }
}
