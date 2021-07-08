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
    public class dalPlantillaCorreo : Conexion
    {
        /// <summary>
        /// Obtiene la plantilla de Correo de Aptitud Ocupacional
        /// </summary>
        public ePlantillaCorreo GetPlantillaCorreo(ePlantillaCorreo objPlantilla)
        {
            ePlantillaCorreo oPlantilla = null;

            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand cmd = new OracleCommand("SIGESER.SP_SRO_PLANTILLA_CORREO_GET", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("CodPlantilla", OracleDbType.Varchar2).Value = objPlantilla.Codigo;

                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    oPlantilla = new ePlantillaCorreo();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    while (dataReader.Read())
                    {
                        oPlantilla.CargarEntidad(dataReader);
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
            }

            return oPlantilla;
        }
    }
}
