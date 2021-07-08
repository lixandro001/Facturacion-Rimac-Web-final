using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.DA
{
    public class dalCompania : Conexion
    {

        public List<eCompania> ListaCompania()
        {
            eCompania objCompania = null;
            List<eCompania> ListCompania = null;

            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {

                    OracleCommand cmd = new OracleCommand(GetPackage() + "SP_LISTAR_COMPANIA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    ListCompania = new List<eCompania>();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    while (dataReader.Read())
                    {
                        objCompania = new eCompania();
                        objCompania.Cargar(dataReader);
                        ListCompania.Add(objCompania);
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion("FACTURACION PREVENTIVO : " + ex.Message.ToString());
            }

            return ListCompania;
        }


        /// <summary>
        /// Actualizar el estado de la compañia (Habilitado o no para ser facturado)
        /// </summary>
        public eCompania ActualizarCompania(eCompania objOSCompania)
        {
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand ocmd = new OracleCommand(GetPackage() + "SP_UPDATE_PARM_CIA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // InParameter
                    ocmd.Parameters.Add("xxNUMCIA", OracleDbType.Varchar2).Value = objOSCompania.Numcia;
                    ocmd.Parameters.Add("xxESTADO", OracleDbType.Varchar2).Value = objOSCompania.Estado;

                    // OutParameter
                    ocmd.Parameters.Add("Resultado", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocmd.Parameters.Add("Mensaje", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                    ocmd.ExecuteNonQuery();

                    objOSCompania.mensaje = ocmd.Parameters["Mensaje"].Value.ToString();
                    objOSCompania.resultado = Convert.ToInt16(ocmd.Parameters["Resultado"].Value.ToString());

                    ocmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                objOSCompania.mensaje = ex.Message;
                objOSCompania.resultado = -1;
            }

            return objOSCompania;
        }

    }
}
