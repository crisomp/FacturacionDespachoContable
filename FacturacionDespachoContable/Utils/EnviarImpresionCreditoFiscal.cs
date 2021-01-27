using FacturacionDespachoContable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionDespachoContable.Utils
{
    public class EnviarImpresionCreditoFiscal
    {
        public static void ImprimirFactura(creditoFiscal factura, List<detalleCreditoFiscal> detalle)
        {
            //Creamos una instancia d ela clase CrearTicket
            ImprimirCreditoFiscal ticket = new ImprimirCreditoFiscal();
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.TextoExtremos(factura.cliente.nombreCompleto, factura.fecha.ToShortDateString());
            ticket.TextoIzquierda("");
            ticket.TextoExtremos("", factura.cliente.nit);
            ticket.TextoExtremos(factura.cliente.direccion, factura.cliente.registro);
            ticket.TextoExtremos(factura.cliente.municipio.nombre, factura.cliente.giro);
            ticket.TextoIzquierda(factura.cliente.departamento.nombre);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            int lineas = 30 - detalle.Count;

            foreach (var item in detalle)
            {
                decimal cantidad = item.cantidad;
                decimal precio = Math.Round((item.precio/1.13m), 2);
                decimal subtotal = Math.Round((item.valor/1.13m), 2);


                ticket.AgregaArticulo(item.servicio.nombre, cantidad, precio, subtotal);
            }

            while (lineas != 0)
            {
                lineas--;
                ticket.TextoIzquierda("");
            };
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.AgregarTotales("                         ", (factura.total/1.13M));
            ticket.AgregarTotales("                         ", (factura.iva));

            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda(Convertir.NumeroALetras(factura.total));
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.AgregarTotales("                         ", factura.total);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.ImprimirTicket("Microsoft XPS Document Writer");//Nombre de la impresora ticketera


        }

        public static void ImprimirFactura(factura factura)
        {
            //Creamos una instancia d ela clase CrearTicket
            ImprimirCreditoFiscal ticket = new ImprimirCreditoFiscal();
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.TextoExtremos(factura.cliente.nombreCompleto, factura.fecha.ToShortDateString());
            ticket.TextoIzquierda("");
            ticket.TextoExtremos("", factura.cliente.nit);
            ticket.TextoExtremos(factura.cliente.direccion, factura.cliente.registro);
            ticket.TextoExtremos(factura.cliente.municipio.nombre, factura.cliente.giro);
            ticket.TextoIzquierda(factura.cliente.departamento.nombre);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            int lineas = 30 - factura.detalleFactura.Count;

            foreach (var item in factura.detalleFactura)
            {
                decimal cantidad = item.cantidad;
                decimal precio = Math.Round((item.precio / 1.13m), 2);
                decimal subtotal = Math.Round((item.valor / 1.13m), 2);


                ticket.AgregaArticulo(item.servicio.nombre, cantidad, precio, subtotal);
            }

            while (lineas != 0)
            {
                lineas--;
                ticket.TextoIzquierda("");
            };
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.AgregarTotales("                         ", (factura.total / 1.13M));
            ticket.TextoIzquierda("");

            //ticket.AgregarTotales("                         ", (factura.iva));

            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda(Convertir.NumeroALetras(factura.total));
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.AgregarTotales("                         ", factura.total);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.ImprimirTicket("Microsoft XPS Document Writer");//Nombre de la impresora ticketera


        }

    }
}