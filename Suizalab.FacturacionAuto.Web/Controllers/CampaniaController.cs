using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.BL;
using Suizalab.FacturacionAuto.Seguridad;

namespace Suizalab.FacturacionAuto.Web.Controllers
{
    public class CampaniaController : Controller
    {
        private List<eOrdenServicioCab> lstOrdenesServicio = null;
        private List<eCompania> lstCompanias = null;

        // GET: Campania/Index
        public eUsuarioCia UsuarioSistema
        {
            get { return Seguridad.Seguridad.ObtenerUsuarioIntranet(); }
        }
        // GET parametro del sistema
        public BE.Sistema DatosSistema
        {
            get { return Seguridad.Seguridad.ObtenerValoresSistema(); }
        }
        public ActionResult Index()
        {
            Seguridad.Seguridad.ValidarSesionUsuarioIntranet();
            if (UsuarioSistema != null)
            {
                ViewBag.User = UsuarioSistema.NombreCorto;
                ViewBag.Permiso = true;
                return View();
            }
            else
                return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        // GET: Campania/Configurar
        public ActionResult Configurar()
        {
            
            Seguridad.Seguridad.ValidarSesionUsuarioIntranet();
            if (UsuarioSistema != null)
            {
                ViewBag.User = UsuarioSistema.NombreCorto;
                ViewBag.Permiso = true;
                return View(UsuarioSistema);
            }
            else
                return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        [HttpPost]
        public JsonResult ListaOrdenes(string numcia, string finoscab)
        {
            try
            {
                string[] fechas = finoscab.Split('_');
                string finoscabIni = fechas[0];
                string finoscabEnd = fechas[1];

                lstOrdenesServicio = cnOrdenServicio.Facturar_List(numcia, finoscabIni, finoscabEnd);

                var lstJson = JsonConvert.SerializeObject(lstOrdenesServicio);
                return Json(new { Success = true, data = lstJson });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ListaCompanias()
        {
            string Msj = string.Empty;
            try
            {
                lstCompanias = cnCompania.Compania_List();

                var lstJson = JsonConvert.SerializeObject(lstCompanias);
                return Json(new { Success = true, data = lstJson });

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }
        // POST
        [HttpPost]
        public JsonResult ListaOrdenesDet(string ticket)
        {
            List<eOrdenServicioDet> LstOrdenesServDet = new List<eOrdenServicioDet>();
            try
            {
                string anio = ticket.Substring(0, 5);
                string mes = ticket.Substring(4, 2);
                string dia = ticket.Substring(6, 2);

                eOrdenServicioDet objServicio = new eOrdenServicioDet();
                objServicio.numoscab = ticket.Substring(0, 5);
                objServicio.peroscab = ticket.Substring(5, 2);
                objServicio.anooscab = ticket.Substring(7, 2);
                objServicio.numsuc = ticket.Substring(9, 2);

                LstOrdenesServDet = cnOrdenServicio.Lista_Servicio(objServicio);

                var lstJson = JsonConvert.SerializeObject(LstOrdenesServDet);

                return Json(new { Success = true, data = lstJson });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        // POST
        [HttpPost]
        public JsonResult RegistrarRegateo(string numcia, string finoscab,string valventa)
        {
            double montoServicio = 0;
            eOrdenServicioDet objOS_det = null;
            double igv = 0;
            int numservicios = 0;
            try
            {
                montoServicio = Convert.ToDouble(valventa);
                if (montoServicio > 0)
                {
                    string[] fechas = finoscab.Split('_');
                    string finoscabIni = fechas[0];
                    string finoscabEnd = fechas[1];

                    Sistema oSistema = Seguridad.Seguridad.ObtenerValoresSistema();
                    lstOrdenesServicio = cnOrdenServicio.Facturar_List(numcia, finoscabIni, finoscabEnd);

                    foreach (eOrdenServicioCab objOrdenServicioCab in lstOrdenesServicio)
                    {
                        objOS_det = new eOrdenServicioDet();
                        objOS_det.numoscab = objOrdenServicioCab.Numoscab;
                        objOS_det.peroscab = objOrdenServicioCab.Peroscab;
                        objOS_det.anooscab = objOrdenServicioCab.Anooscab;
                        objOS_det.numsuc = objOrdenServicioCab.Numsuc;
                        
                        objOS_det.presol = decimal.Round(Convert.ToDecimal((1.18)*montoServicio), 2);
                        objOS_det.predol = decimal.Round(Convert.ToDecimal(objOS_det.presol / oSistema.tipocambio), 2);
                        objOS_det.valventa = decimal.Round(Convert.ToDecimal(montoServicio), 2);
                        objOS_det = cnOrdenServicio.RegistrarRegateo(objOS_det);
                        if (objOS_det.resultado == 1)
                        {
                            objOS_det.resultado = -1;
                            objOS_det = cnOrdenServicio.ActualizarOrdenCab(objOS_det);
                            if (objOS_det.resultado == 1)
                            {
                                eCompania objCompania = new eCompania();
                                objCompania.Numcia = numcia;
                                objCompania.Estado = 1;
                                objCompania = cnCompania.ActualizarCompania(objCompania);
                                if (objCompania.resultado != -1)
                                {
                                    Utilitarios.Utilitario.LogServiceFacturacion(objCompania.mensaje);
                                }
                                numservicios++;
                            }
                        }
                    }

                    if (objOS_det.resultado == 1)
                        return Json(new { Success = true, Message = "Se agregó un nuevo servicio con el monto de S/." + valventa + " en " + numservicios + " Tickets." });
                    else
                    {
                        Utilitarios.Utilitario.LogServiceFacturacion(objOS_det.mensaje);
                        return Json(new { Success = false, Message = objOS_det.mensaje });                        
                    }
                }
                else
                {
                    return Json(new { Success = false, Message = "El monto debe ser mayor que 0." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = "Ingrese un monto correcto." });
            }
        }

        [HttpPost]
        public JsonResult UpdateCompania(string numcia, string estado)
        {            
            try
            {
                
                eCompania objCompania = new eCompania();
                objCompania.Numcia = numcia;
                if (estado == "Activar")
                    objCompania.Estado = 1;
                else
                    objCompania.Estado = 0;

                objCompania = cnCompania.ActualizarCompania(objCompania);
                if (objCompania.resultado == 1)
                {
                    return Json(new { Success = true, Message = objCompania.mensaje });
                }
                else
                {
                    Utilitarios.Utilitario.LogServiceFacturacion(objCompania.mensaje);
                    return Json(new { Success = false, Message = objCompania.mensaje });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        public RedirectToRouteResult CerrarSesion()
        {
            Seguridad.Seguridad.CerrarSesion();

            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }
}
