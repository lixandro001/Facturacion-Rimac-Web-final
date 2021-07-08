using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Suizalab.FacturacionAuto.Web.Models;
using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.BL;
using Suizalab.FacturacionAuto.Seguridad;

namespace Suizalab.FacturacionAuto.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Suiza Lab :: Login Laboratorios";

            return View();
        }
        private bool AutenticarUsuario(UsuarioModel Usuario, out string strRutaDefecto, out string strMensaje)
        {
            strRutaDefecto = "";
            strMensaje = "";
            try
            {
                eUsuarioCia objUsuario = new eUsuarioCia();

                objUsuario.NombreUsuario = Usuario.NombreUsuario.ToUpper().TrimEnd();
                objUsuario.Contrasena = Seguridad.Seguridad.Encriptar(Usuario.Contrasenia.ToUpper());
                if (Seguridad.Seguridad.AutenticarUsuarioIntranet(objUsuario, out strRutaDefecto, out strMensaje))
                {                    
                    Session["idUser"] = Usuario.NombreUsuario.ToUpper().TrimEnd();
                    strRutaDefecto = this.Url.Action("Index", strRutaDefecto, new {}, this.Request.Url.Scheme);
                }
                else
                {
                    //strMensaje = (strMensaje != null && strMensaje.Length > 0) ? strMensaje : "Los datos ingresados no son válidos para acceder al sistema.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
            }

            return true;
        }


        [HttpPost]
        public JsonResult IniciarSesion(UsuarioModel Usuario)
        {
            string strRutaDefecto = string.Empty;
            string strMensaje = string.Empty;

            if (AutenticarUsuario(Usuario, out strRutaDefecto, out strMensaje))
                return Json(new { Success = true, Message = strMensaje, ruta = strRutaDefecto });
            else
                return Json(new { Success = false, Message = strMensaje });
        }
    }
}