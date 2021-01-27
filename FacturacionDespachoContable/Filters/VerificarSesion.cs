using FacturacionDespachoContable.Controllers;
using FacturacionDespachoContable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionDespachoContable.Filters
{
    public class VerificarSesion : ActionFilterAttribute
    {
        private usuario usuario;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                usuario = (usuario)HttpContext.Current.Session["Usuario"];

                if (usuario == null)
                {
                    if (filterContext.Controller is LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Login/Index");
                    }
                }
            }
            catch (Exception)
            {

                filterContext.HttpContext.Response.Redirect("~/Login/Index");

            }
        }
    }
}