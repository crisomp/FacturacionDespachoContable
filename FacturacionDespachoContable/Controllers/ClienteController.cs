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
    public class ClienteController : Controller
    {
        private despachoContableEntities db = new despachoContableEntities();

        // GET: Cliente
        public async Task<ActionResult> Index()
        {
            var cliente = db.cliente.Include(c => c.departamento).Include(c => c.estado).Include(c => c.municipio);
            return View(await cliente.ToListAsync());
        }

        // GET: Cliente/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            ViewBag.idDepartamento = new SelectList(db.departamento, "idDepartamento", "nombre");
            ViewBag.idEstado = new SelectList(db.estado, "idEstado", "nombre");
            ViewBag.idMunicipio = new SelectList(db.municipio, "idMunicipio", "nombre");
            return View();
        }

        // POST: Cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idCliente,nombreCompleto,nit,registro,giro,direccion,idEstado,idDepartamento,idMunicipio")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.cliente.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idDepartamento = new SelectList(db.departamento, "idDepartamento", "nombre", cliente.idDepartamento);
            ViewBag.idEstado = new SelectList(db.estado, "idEstado", "nombre", cliente.idEstado);
            ViewBag.idMunicipio = new SelectList(db.municipio, "idMunicipio", "nombre", cliente.idMunicipio);
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDepartamento = new SelectList(db.departamento, "idDepartamento", "nombre", cliente.idDepartamento);
            ViewBag.idEstado = new SelectList(db.estado, "idEstado", "nombre", cliente.idEstado);
            ViewBag.idMunicipio = new SelectList(db.municipio, "idMunicipio", "nombre", cliente.idMunicipio);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCliente,nombreCompleto,nit,registro,giro,direccion,idEstado,idDepartamento,idMunicipio")] cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idDepartamento = new SelectList(db.departamento, "idDepartamento", "nombre", cliente.idDepartamento);
            ViewBag.idEstado = new SelectList(db.estado, "idEstado", "nombre", cliente.idEstado);
            ViewBag.idMunicipio = new SelectList(db.municipio, "idMunicipio", "nombre", cliente.idMunicipio);
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cliente cliente = await db.cliente.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            cliente cliente = await db.cliente.FindAsync(id);
            db.cliente.Remove(cliente);
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

        public async Task<ActionResult> ObtenerClientes()
        {
            IEnumerable<cliente> clientes = await db.cliente.ToArrayAsync();
            return View(clientes);
        }
    }
}
