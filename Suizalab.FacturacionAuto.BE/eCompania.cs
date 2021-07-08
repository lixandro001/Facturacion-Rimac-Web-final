using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.BE
{
    public class eCompania : Base
    {
        #region Variables
        private string numcia;
        private string razonsocial;
        private string nomcia;
        private string codigocia;
        private string numemp;
        private string numtdven;
        private string nummon;
        private string ruccia;
        private string dircia;
        private int diaspago;
        private int igv;
        private int estado;

        #endregion

        #region Propiedades
        public string Numcia
        {
            get { return numcia; }
            set { numcia = value; }
        }
        public string Razonsocial
        {
            get { return razonsocial; }
            set { razonsocial = value; }
        }
        public string Nomcia
        {
            get { return nomcia; }
            set { nomcia = value; }
        }
        public string Codigocia
        {
            get { return codigocia; }
            set { codigocia = value; }
        }
        public string Numemp
        {
            get { return numemp; }
            set { numemp = value; }
        }
        public string Numtdven
        {
            get { return numtdven; }
            set { numtdven = value; }
        }
        public string Nummon
        {
            get { return nummon; }
            set { nummon = value; }
        }
        public string Ruccia
        {
            get { return ruccia; }
            set { ruccia = value; }
        }
        public string Dircia
        {
            get { return dircia; }
            set { dircia = value; }
        }
        public int Diaspago
        {
            get { return diaspago; }
            set { diaspago = value; }
        }
        public int IGV
        {
            get { return igv; }
            set { igv = value; }
        }
        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Carga los datos del DataReader en la Entidad
        /// </summary>
        /// <param name="objDataReader">Datos a cargar en la Entidad</param>

        public void Cargar(System.Data.IDataReader objDataReader)
        {
            CargarVariable(objDataReader, "C_NUMCIA", out numcia);
            CargarVariable(objDataReader, "C_NUMEMP", out numemp);
            CargarVariable(objDataReader, "C_RAZCIA", out razonsocial);
            CargarVariable(objDataReader, "C_RUCCIA", out ruccia);
            CargarVariable(objDataReader, "C_NCOCIA", out nomcia);
            CargarVariable(objDataReader, "C_DIRCIA", out dircia);
            CargarVariable(objDataReader, "C_SEEKCIA", out codigocia);
            CargarVariable(objDataReader, "C_NUMMON", out nummon);
            CargarVariable(objDataReader, "CM_NUMTDVEN", out numtdven);
            CargarVariable(objDataReader, "CM_NUMDIAS", out diaspago);
            CargarVariable(objDataReader, "IGV", out igv);
            CargarVariable(objDataReader, "PS_ESTADO", out estado);
        }

        #endregion

    }
}
