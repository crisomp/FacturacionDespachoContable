using FacturacionDespachoContable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;



namespace FacturacionDespachoContable.Controllers
{
    public class FacturaController : Controller
    {
        private despachoContableEntities db = new despachoContableEntities();

        private static List<factura> lista;
        // GET: Factura
        public ActionResult Index(DateTime? fecha)
        {
            if (fecha == null)
                fecha = DateTime.Today;

            List<factura> facturas = db.factura.Where(x => x.fecha == fecha).ToList();
            lista = facturas;
            return View(facturas);
        }

        public ActionResult imprimir()
        {
            return View(lista);
        }

        public ActionResult Detalles(int idFactura)
        {

            factura f = db.factura.Include(x => x.detalleFactura).Where(x => x.idFactura == idFactura).FirstOrDefault();
            return View(f);
        }
    }
}