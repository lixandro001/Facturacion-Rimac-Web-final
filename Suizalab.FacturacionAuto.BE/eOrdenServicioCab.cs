using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.BE
{
    [Serializable]
    public class eOrdenServicioCab : Base
    {

        #region Variables
        private string ticket;
        private string numoscab;
        private string peroscab;
        private string anooscab;
        private string numemp;
        private string numsuc;
        private string tipcambio;
        private DateTime finoscab;
        private string sexo;
        private string paciente;
        private string numcia;
        private decimal totsol;
        private decimal totdol;
        private decimal totdebe;
        private decimal imposcab;
        private string totlet;
        private decimal subtotdol;
        private decimal subtotsol;
        private decimal totalventa;
        private decimal debeventa;
        private decimal subtotalventa;

        private List<eOrdenServicioCab> listOrdenes = null;
        #endregion

        #region Propiedades
        public string Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }
        public string Numoscab
        {
            get { return numoscab; }
            set { numoscab = value; }
        }
        public string Peroscab
        {
            get { return peroscab; }
            set { peroscab = value; }
        }
        public string Anooscab
        {
            get { return anooscab; }
            set { anooscab = value; }
        }
        public string Numemp
        {
            get { return numemp; }
            set { numemp = value; }
        }
        public string Numsuc
        {
            get { return numsuc; }
            set { numsuc = value; }
        }
        public string Tipcambio
        {
            get { return tipcambio; }
            set { tipcambio = value; }
        }
        public DateTime Finoscab
        {
            get { return finoscab; }
            set { finoscab = value; }
        }
        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        public string Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }
        public string Numcia
        {
            get { return numcia; }
            set { numcia = value; }
        }
        public decimal Totsol
        {
            get { return totsol; }
            set { totsol = value; }
        }
        public decimal Totdol
        {
            get { return totdol; }
            set { totdol = value; }
        }
        public decimal Totdebe
        {
            get { return totdebe; }
            set { totdebe = value; }
        }
        public decimal Imposcab
        {
            get { return imposcab; }
            set { imposcab = value; }
        }
        public string Totlet
        {
            get { return totlet; }
            set { totlet = value; }
        }
        public decimal Subtotdol
        {
            get { return subtotdol; }
            set { subtotdol = value; }
        }
        public decimal Subtotsol
        {
            get { return subtotsol; }
            set { subtotsol = value; }
        }
        public decimal Totalventa
        {
            get { return totalventa; }
            set { totalventa = value; }
        }
        public decimal Debeventa
        {
            get { return debeventa; }
            set { debeventa = value; }
        }
        public decimal Subtotalventa
        {
            get { return subtotalventa; }
            set { subtotalventa = value; }
        }

        public List<eOrdenServicioCab> ListOrdenes
        {
            get { return listOrdenes; }
            set { listOrdenes = value; }
        }
        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Carga los datos del DataReader en la Entidad
        /// </summary>
        /// <param name="objDataReader">Datos a cargar en la Entidad</param>

        public void Cargar(System.Data.IDataReader objDataReader)
        {
            CargarVariable(objDataReader, "TICKET", out ticket);
            CargarVariable(objDataReader, "NUMOSCAB", out numoscab);
            CargarVariable(objDataReader, "PEROSCAB", out peroscab);
            CargarVariable(objDataReader, "ANOOSCAB", out anooscab);
            CargarVariable(objDataReader, "NUMEMP", out numemp);
            CargarVariable(objDataReader, "NUMSUC", out numsuc);
            CargarVariable(objDataReader, "TIPCAMBIO", out tipcambio);
            CargarVariable(objDataReader, "FINOSCAB", out finoscab);
            CargarVariable(objDataReader, "PACIENTE", out paciente);
            CargarVariable(objDataReader, "NUMCIA", out numcia);
            CargarVariable(objDataReader, "TOTSOL", out totsol);
            CargarVariable(objDataReader, "TOTDOL", out totdol);
            CargarVariable(objDataReader, "TOTDEBE", out totdebe);
            CargarVariable(objDataReader, "IMPOSCAB", out imposcab);
            CargarVariable(objDataReader, "TOTLET", out totlet);
            CargarVariable(objDataReader, "SUBTOTDOL", out subtotdol);
            CargarVariable(objDataReader, "SUBTOTSOL", out subtotsol);
            CargarVariable(objDataReader, "TOTALVENTA", out totalventa);
            CargarVariable(objDataReader, "DEBEVENTA", out debeventa);
            CargarVariable(objDataReader, "SUBTOTALVENTA", out subtotalventa);
        }

        public void CargarOrdenesServicio(System.Data.IDataReader objDataReader)
        {
            CargarVariable(objDataReader, "TICKET", out ticket);
            CargarVariable(objDataReader, "NUMOSCAB", out numoscab);
            CargarVariable(objDataReader, "PEROSCAB", out peroscab);
            CargarVariable(objDataReader, "ANOOSCAB", out anooscab);
            CargarVariable(objDataReader, "NUMEMP", out numemp);
            CargarVariable(objDataReader, "NUMSUC", out numsuc);
            CargarVariable(objDataReader, "FINOSCAB", out finoscab);
            CargarVariable(objDataReader, "PACIENTE", out paciente);
            CargarVariable(objDataReader, "SEXO", out sexo);
        }

        #endregion
    }
}
