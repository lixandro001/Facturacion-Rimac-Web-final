using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.BL;
using Suizalab.FacturacionAuto.Seguridad;

   
namespace Suizalab.FacturacionAuto.Web.Controllers
{
    public class OrdenServicioController : Controller
    {
        private List<eListadoTicketSited> lstTicketSited;
        private ObtenerRutaTicketSited objetoSitedRuta;

        public eUsuarioCia UsuarioSistema => Suizalab.FacturacionAuto.Seguridad.Seguridad.ObtenerUsuarioIntranet();

        // GET: Campania/Index
        //public eUsuarioCia UsuarioSistema
        //{
        //    get { return Seguridad.Seguridad.ObtenerUsuarioIntranet(); }
        //}

        // GET: OrdenServicio

        public ActionResult Index()
        {
            Suizalab.FacturacionAuto.Seguridad.Seguridad.ValidarSesionUsuarioIntranet();
            if (UsuarioSistema != null)
            {
                base.ViewBag.User = UsuarioSistema.NombreCorto;
                base.ViewBag.Permiso = true;
                return View();
            }
            return RedirectToRoute(new
            {
                controller = "Login",
                action = "Index"
            });
        }

        public ActionResult AdjuntarArchivo()
        {
            Suizalab.FacturacionAuto.Seguridad.Seguridad.ValidarSesionUsuarioIntranet();
            if (UsuarioSistema != null)
            {
                base.ViewBag.User = UsuarioSistema.NombreCorto;
                base.ViewBag.Permiso = true;
                return View();
            }
            return RedirectToRoute(new
            {
                controller = "Login",
                action = "Index"
            });
        }

        //<summary>
        // Permite obtener la lista de las ordenes de servicio
        //</summary>
        [HttpPost]
        public JsonResult GetListaOrdenes()
        {
            try
            {
                string jsonStr = JsonConvert.SerializeObject(cnOrdenServicio.List_OS_sin_firma());
                return Json(new
                {
                    Success = true,
                    data = jsonStr
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Msj = ex.Message
                });
            }
        }


        [HttpPost]
        public JsonResult GetListaSited(string fechaInicio, string fechaFin)
        {
            try
            {
                lstTicketSited = cnOrdenServicio.ListaTicketSited(fechaInicio, fechaFin);
                string lstJson = JsonConvert.SerializeObject(lstTicketSited);
                return Json(new
                {
                    Success = true,
                    data = lstJson
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Msj = ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult ObtenerSitedRuta(ObtenerRutaTicketSited ObjRequest)
        {
            try
            {
                objetoSitedRuta = cnOrdenServicio.ObtenerSitedRuta(ObjRequest);
                string RutaPdf = objetoSitedRuta.mensaje;
                byte[] buffer = null;
                string PathfileName = string.Empty;
                PathfileName = RutaPdf;
                using (FileStream fs = new FileStream(PathfileName, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    int longitud = (int)fs.Length;
                }
                byte[] contenido = buffer;
                return File(RutaPdf, "application/pdf");
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Msj = ex.Message
                });
            }
        }


        [HttpPost]
        public ActionResult SaveImage(eFileUpload ObjRequest)
        {
            eObjetoFileUploa objeto = new eObjetoFileUploa();
            try
            {
                if (ObjRequest.FormFile != null && ObjRequest.FormFile.ContentLength > 0)
                {
                    string NumeroTicketSerializado = ObjRequest.Ticket;
                    string FechaDia = DateTime.Now.ToString("dd_MM_yyyy");
                    string extension = "pdf";
                    string newNameFile = NumeroTicketSerializado + "." + extension;
                    string ruta = "\\\\192.168.0.15\\files$\\ArchivosPruebaFacturacionRimac\\";
                    ObjRequest.FormFile.SaveAs(ruta + newNameFile);
                    objeto.numosc = Convert.ToString(ObjRequest.numosc);
                    objeto.perosc = Convert.ToString(ObjRequest.perosc);
                    objeto.anoosc = Convert.ToString(ObjRequest.anoosc);
                    objeto.numsuc = Convert.ToString(ObjRequest.numsuc);
                    objeto.emposc = Convert.ToString(ObjRequest.emposc);
                    objeto.usumod = Convert.ToString(base.Session["idUser"]);
                    objeto.vlruta = ruta + newNameFile;
                    objeto = cnOrdenServicio.RegistrarSited(objeto);

                }
                string JsonStr = JsonConvert.SerializeObject(ObjRequest.resultado);
                return Json(new
                {
                    Success = true,
                    data = JsonStr,
                    Msj = objeto.mensaje
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Msj = ex.Message
                });
            }
        }


        [HttpPost]
        public ActionResult EliminarArchivoSited(eObjetoFileUploa ObjRequest)
        {
            eObjetoFileUploa objeto = new eObjetoFileUploa();
            try
            {     
                    objeto.numosc = Convert.ToString(ObjRequest.numosc);
                    objeto.perosc = Convert.ToString(ObjRequest.perosc);
                    objeto.anoosc = Convert.ToString(ObjRequest.anoosc);
                    objeto.numsuc = Convert.ToString(ObjRequest.numsuc);
                    objeto.emposc = Convert.ToString(ObjRequest.emposc);
                    objeto.usumod = Convert.ToString(base.Session["idUser"]);
                    objeto.vlruta = "";
                    //objeto = cnOrdenServicio.EliminarArchivoSited(objeto);
                
                string JsonStr = JsonConvert.SerializeObject(ObjRequest.resultado);
                return Json(new
                {
                    Success = true,
                    data = JsonStr,
                    Msj = objeto.mensaje
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Msj = ex.Message
                });
            }
        }



        public RedirectToRouteResult CerrarSesion()
        {
            Suizalab.FacturacionAuto.Seguridad.Seguridad.CerrarSesion();
            return RedirectToRoute(new
            {
                controller = "Login",
                action = "Index"
            });
        }
         
    }
}