using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suizalab.FacturacionAuto.Utilitarios;


namespace Suizalab.FacturacionAuto.BE
{
    [Serializable]
    public class ePlantillaCorreo : Base
    {
        #region Variables Privadas

        private string codigo = string.Empty;
        private string nombre = string.Empty;
        private string titulo = string.Empty;
        private string descripcion = string.Empty;

        private List<ePlantillaCorreo> listPlantilla = null;

        #endregion

        #region Propiedades

        /// <summary>
        /// Código de la Plantilla
        /// </summary>
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        /// <summary>
        /// Nombre de la Plantilla
        /// </summary>
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        /// <summary>
        /// Titulo de la Plantilla
        /// </summary>
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        /// <summary>
        /// Descripcion de la Plantilla (Código HTML)
        /// </summary>
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public List<ePlantillaCorreo> ListPlantilla
        {
            get { return listPlantilla; }
            set { listPlantilla = value; }
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Carga los datos del DataRow en la Entidad
        /// </summary>
        /// <param name="dr">Datos a cargar en la Entidad</param>
        public void CargarEntidad(System.Data.IDataReader dr)
        {
            CargarVariable(dr, "CODIGO", out codigo);
            CargarVariable(dr, "NOMBRE", out nombre);
            CargarVariable(dr, "TITULO", out titulo);
            CargarVariable(dr, "DESCRIPCION", out descripcion);
        }

        #endregion
    }
}
