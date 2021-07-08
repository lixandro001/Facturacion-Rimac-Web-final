using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.BE
{
    public class eUsuarioCia: Base
    {
        #region Variables Privadas
        private int codPerfil = -1;
        private string nombreUsuario = string.Empty;
        private string contrasena = string.Empty;
        private string nombres = string.Empty;
        private string nombresCorto = string.Empty;
        private string apellidoPaterno = string.Empty;
        private string apellidoMaterno = string.Empty;
        private string permiso = string.Empty;
        #endregion

        #region Propiedades

        /// <summary>
        /// Código del Perfil del Usuario Compañía
        /// </summary>
        public int CodPerfil
        {
            get { return codPerfil; }
            set { codPerfil = value; }
        }

        /// <summary>
        /// Usuario de la Compañía
        /// </summary>
        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }
        /// <summary>
        /// Contraseña plana del Usuario Compañía
        /// </summary>
        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        /// <summary>
        /// Nombres del Usuario Compañía
        /// </summary>
        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        /// <summary>
        /// Nombre corto del Usuario Compañía
        /// </summary>
        public string NombreCorto
        {
            get { return nombresCorto; }
            set { nombresCorto = value; }
        }


        /// <summary>
        /// Apellido Paterno del Usuario Compañía
        /// </summary>
        public string ApellidoPaterno
        {
            get { return apellidoPaterno; }
            set { apellidoPaterno = value; }
        }

        /// <summary>
        /// Apellido Materno del Usuario Compañía
        /// </summary>
        public string ApellidoMaterno
        {
            get { return apellidoMaterno; }
            set { apellidoMaterno = value; }
        }

        /// <summary>
        /// Permiso del usuario en el sistema
        /// </summary>
        public string Permiso
        {
            get { return permiso; }
            set { permiso = value; }
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Carga los datos del DataRow en la Entidad
        /// </summary>
        /// <param name="dr">Datos a cargar en la Entidad</param>
        public void CargarEntidad(IDataReader dr)
        {
            CargarVariable(dr, "UC_CODPERFIL", out codPerfil);
            CargarVariable(dr, "UC_NOMBUSUARIO", out nombreUsuario); 
            CargarVariable(dr, "UC_CONTRASENA", out contrasena); 
            CargarVariable(dr, "UC_NOMBCORTO", out nombresCorto);
            CargarVariable(dr, "PU_NUMPER", out permiso);
        }

        #endregion
    }
}
