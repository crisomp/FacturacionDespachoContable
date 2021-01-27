using FacturacionDespachoContable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionDespachoContable.Controllers
{
    public class LoginController : Controller
    {
        private despachoContableEntities db = new despachoContableEntities();
        // GET: Login
        public ActionResult Index()
        {
            Session["Usuario"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(usuario pusuario)
        {
            usuario usuarioDB = db.usuario.Where(x => x.usuario1 == pusuario.usuario1 && x.contrasena == pusuario.contrasena).FirstOrDefault();
            if (usuarioDB !=null)
            {
                Session["Usuario"] = usuarioDB;
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("contrasena", "Usuario o Contraseña invalida");
            return View();
        }
    }
}