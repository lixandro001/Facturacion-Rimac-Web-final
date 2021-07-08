using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;

using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.DA
{
    public class dalFacturacion : Conexion
    {        
        /// <summary>
        /// Obtiene los datos del orden de servicio facturada, para crear el informe agrupado
        /// </summary>
        public List<eFacturacion> ListaOrdenFacturados(string serFac, string numfac)
        {
            eFacturacion objFacturacion = null;
            List<eFacturacion> ListFacturacion = null;
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand cmd = new OracleCommand(GetPackage() + "SP_GET_ORDENES_FACTURADAS", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxSERFAC", OracleDbType.Varchar2).Value = serFac;
                    cmd.Parameters.Add("xxNUMFAC", OracleDbType.Varchar2).Value = numfac;

                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    ListFacturacion = new List<eFacturacion>();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();
                    while (dataReader.Read())
                    {
                        objFacturacion = new eFacturacion();
                        objFacturacion.serfac = dataReader["SERFAC"].ToString();
                        objFacturacion.numfac = dataReader["NUMFAC"].ToString();
                        objFacturacion.numoscab = dataReader["NUMOSCAB"].ToString();
                        objFacturacion.peroscab = dataReader["PEROSCAB"].ToString();
                        objFacturacion.anooscab = dataReader["ANOOSCAB"].ToString();
                        objFacturacion.numsuc = dataReader["NUMSUC"].ToString();
                        objFacturacion.numtdven = dataReader["NUMTDVEN"].ToString();
                        objFacturacion.fecfac = Convert.ToDateTime(dataReader["FECFAC"].ToString());
                        objFacturacion.totsol = Convert.ToDecimal(dataReader["TOTSOL"].ToString());
                        objFacturacion.ruccia = dataReader["RUCCIA"].ToString();
                        objFacturacion.firma = dataReader["FIRMA"].ToString();
                        objFacturacion.huella = dataReader["HUELLA"].ToString();
                        objFacturacion.adjunto1 = dataReader["SUSTENTO1"].ToString();
                        objFacturacion.adjunto2 = dataReader["SUSTENTO2"].ToString();
                        objFacturacion.ticket = dataReader["TICKET"].ToString();
                        objFacturacion.codticket = objFacturacion.ticket + objFacturacion.numsuc;
                        ListFacturacion.Add(objFacturacion);
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion("FACTURACION PREVENTIVO (Lista_Ordenes): " + ex.Message.ToString());
            }
            return ListFacturacion;
        }
    }
}
