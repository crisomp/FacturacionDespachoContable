using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FacturacionDespachoContable.Models;
using System.Data.Entity;
using FacturacionDespachoContable.Utils;

namespace FacturacionDespachoContable.Controllers
{
    public class CreditoFiscalController : Controller
    {
        private despachoContableEntities db = new despachoContableEntities();
        private static List<creditoFiscal> archivo;

        // GET: CreditoFiscal
        public ActionResult Index()
        {
            return View();
        }

        


        [HttpPost]
        public JsonResult AgregarServicio(int idServicio, int cantidad)
        {
            detalleCreditoFiscal detalle = new detalleCreditoFiscal();
            servicio servicio = db.servicio.Find(idServicio);
            detalle.idServicio = servicio.idServicio;
            detalle.cantidad = cantidad;
            detalle.precio = servicio.precio;
            detalle.valor = cantidad * servicio.precio;
            detalle.servicio = new servicio();
            detalle.servicio.nombre = servicio.nombre;

            return Json(detalle);

        }

        [HttpPost]
        public JsonResult AgregarCliente(int idCliente)
        {

            cliente clienteDB = db.cliente.Find(idCliente);

            cliente cliente = new cliente();
            cliente.nombreCompleto = clienteDB.nombreCompleto;
            cliente.municipio = new municipio();
            cliente.municipio.nombre = clienteDB.municipio.nombre;
            cliente.registro = clienteDB.registro;

            return Json(cliente);
        }

        [HttpPost]
        public JsonResult Guardar(List<detalleCreditoFiscal> detalle, int idCliente, decimal total)
        {
            creditoFiscal cf = new creditoFiscal();

            cf.fecha = DateTime.Today;
            cf.idCliente = idCliente;
            cf.cliente = db.cliente.Find(idCliente);
            cf.total = total;
            cf.numeroDocumento = 1;
            cf.iva = total - (total / 1.13m);

            int idCF = GuardarCredito(cf);
            foreach (var item in detalle)
            {
                item.idCreditoFiscal = idCF;
                item.servicio = db.servicio.Find(item.idServicio);
            }

            GuardarDetalle(detalle);

            EnviarImpresionCreditoFiscal.ImprimirFactura(cf, detalle);

            return Json(1);
        }
       

        [HttpPost]
        public JsonResult GuardarFactura(List<detalleFactura> detalle, int idCliente, decimal total)
        {
            factura f = new factura
            {
                fecha = DateTime.Today,
                idCliente = idCliente,
                cliente = db.cliente.Find(idCliente),
                iva = total - (total / 1.13m),
                total = total,
                numeroDocumento = 1,
                detalleFactura = detalle
            };

            db.factura.Add(f);
            db.SaveChanges();
            
            foreach (detalleFactura item in f.detalleFactura)
            {
                item.servicio = db.servicio.Find(item.idServicio);
            }

            EnviarImpresionCreditoFiscal.ImprimirFactura(f);
            return Json(1);
        }


        public int GuardarCredito(creditoFiscal cf)
        {
            db.creditoFiscal.Add(cf);
            db.SaveChanges();
            return cf.idCreditoFiscal;
        }

        public void GuardarDetalle (List<detalleCreditoFiscal> detalle)
        {
            db.detalleCreditoFiscal.AddRange(detalle);
            db.SaveChanges();
        }

        public ActionResult imprimir()
        {
            return View(archivo);
        }

        public ActionResult Archivo(DateTime? fecha)
        {
            if (fecha == null)
            {
                fecha = DateTime.Today;
            }
            
            List<creditoFiscal> lista = db.creditoFiscal.Where(x => x.fecha == fecha).ToList();
            archivo = lista;
            return View(lista);
        }

        public ActionResult Detalles(int idCreditoFiscal)
        {
            creditoFiscal cf = db.creditoFiscal.Include(x => x.detalleCreditoFiscal).Where(x => x.idCreditoFiscal == idCreditoFiscal).FirstOrDefault();
            return View(cf);
        }

        
    }
}