using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;

using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.BL;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.Seguridad
{
    public class Seguridad
    {

        #region Propiedades

        private static eUsuarioCia objUser = null;
        //private static Sistema objSystem = null;

        public static eUsuarioCia ObjUsuario
        {
            get { return Seguridad.objUser; }
            set { Seguridad.objUser = value; }
        }

        // public static Sistema DatosSistema
        // {
        //     get { return Seguridad.objSystem; }
        //      set { Seguridad.objSystem = value; }
        //  }
        #endregion


        /// <summary>
        /// Valida si la informacion de Usuario esta activa, sino redirecciona a la pagina de Timeout
        /// </summary>
        public static void ValidarSesionUsuarioIntranet()
        {
            if (HttpContext.Current.Session["Usuario"] == null)
            {
                HttpContext.Current.Response.Redirect("~/Timeout");
            }
        }


        /// <summary>
        /// Cierra la Sesion Actual y se redirige al Inicio de la Aplicacion
        /// </summary>
        public static void CerrarSesion()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect("~/Login");

        }

        /// <summary>
        /// Devuelve el Usuario actualmente autenticado en el sistema
        /// </summary>
        public static eUsuarioCia ObtenerUsuarioIntranet()
        {
            return HttpContext.Current.Session["Usuario"] as eUsuarioCia;
        }

        /// <summary>
        /// Devuelve los valores con los cuales trabajará el sistema
        /// </summary>
        public static Sistema ObtenerValoresSistema()
        {
            return HttpContext.Current.Session["Sistema"] as Sistema;
        }

        public static bool AutenticarUsuarioIntranet(eUsuarioCia ObjUser, out string strRutaDefecto, out string strMensaje)
        {
            bool blnRpta = false;
            strRutaDefecto = string.Empty;
            strMensaje = string.Empty;

            try
            {
                ObjUser = cnLogin.GetSessionIntranet(ObjUser);
                strMensaje = ObjUser.mensaje;

                if (ObjUser.resultado == 1)
                {
                    Sistema objSistema = new Sistema();
                    objSistema = cnLogin.GetParametrosSistema();
                    HttpContext.Current.Session["Sistema"] = objSistema;

                    HttpContext.Current.Session["Usuario"] = ObjUser;
                    strRutaDefecto = "Login";
                    blnRpta = true;

                    if (ObjUser.Permiso == objSistema.codPerfilCamp)
                    {
                        strRutaDefecto = "Campania";
                    }
                    else if(ObjUser.Permiso == objSistema.codPerfilOS)
                    {
                        strRutaDefecto = "OrdenServicio";
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
            }

            return blnRpta;
        }

        public static bool ValidarPermiso(eUsuarioCia usuario, string PermisoSistema)
        {
            bool _estado = false;            
             if (usuario.Permiso == PermisoSistema) _estado = true;            
            return _estado;
        }

        /// <summary>
        /// Encriptación Suiza Lab
        /// </summary>
        public static string Encriptar(string sPassword)
        {
            string sEncriptado = string.Empty;
            int iContador = sPassword.Length;

            int[] aux = new int[] { 3, 24, 8, 10, 34, 17, 20, 21, 21, 3, 24, 8, 10, 34, 17, 20 };

            for (int i = 0; i < iContador; i++)
            {
                sEncriptado = (sEncriptado + Convert.ToChar(Encoding.ASCII.GetBytes(sPassword.Substring(i, 1))[0] + aux[i]).ToString());
            }

            return sEncriptado;
        }

        /// <summary>
        /// Desencriptación Suiza Lab
        /// </summary>
        public static string Desencriptar(string sPassword)
        {
            string sDesencriptado = string.Empty;
            int iContador = sPassword.Length;

            int[] aux = new int[] { 3, 24, 8, 10, 34, 17, 20, 21, 21, 3, 24, 8, 10, 34, 17, 20 };

            for (int i = 0; i < iContador; i++)
            {
                sDesencriptado = (sDesencriptado + Convert.ToChar(Encoding.ASCII.GetBytes(sPassword.Substring(i, 1))[0] - aux[i]).ToString());
            }

            return sDesencriptado;
        }
    }
}
