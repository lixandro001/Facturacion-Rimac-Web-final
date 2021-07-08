using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.DA
{
    public class dalLogin : Conexion
    {
        public static Sistema GetParametrosSistema()
        {
            Sistema oEntidad = null;
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand cmd = new OracleCommand(GetPackage() + "SP_DATOS_SISTEMA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    oEntidad = new Sistema();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();
                    while (dataReader.Read())
                    {
                        oEntidad.numemp = dataReader["NUMEMP"].ToString();
                        oEntidad.razemp = dataReader["RAZEMP"].ToString();
                        oEntidad.diremp = dataReader["DIREMP"].ToString();
                        oEntidad.numsuc = dataReader["NUMSUC"].ToString();
                        oEntidad.codPerfilAll = dataReader["PERM_ALL"].ToString();
                        oEntidad.codPerfilCamp = dataReader["PERM_CAMP"].ToString();
                        oEntidad.codPerfilFact = dataReader["PERM_FACT"].ToString();
                        oEntidad.codPerfilOS = dataReader["PERM_OS"].ToString();
                        oEntidad.igv = Convert.ToInt16(dataReader["IGV"].ToString());
                        oEntidad.tipocambio = Convert.ToDecimal(dataReader["TIPOCAMBIO"].ToString());
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion(" LoginDA (GetParametrosSistema): " + ex.Message.ToString());
            }
            return oEntidad;
        }

        public static eUsuarioCia GetSessionIntranet(eUsuarioCia objUsuarioCia)
        {
            eUsuarioCia oEntidad = null;                       

            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand cmd = new OracleCommand(GetPackage() + "SP_LOGIN_SISTEMA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // InParameter
                    cmd.Parameters.Add("NombreUsuario", OracleDbType.Varchar2).Value = objUsuarioCia.NombreUsuario;
                    cmd.Parameters.Add("Contrasena", OracleDbType.Varchar2).Value = objUsuarioCia.Contrasena;

                    // OutParameter
                    cmd.Parameters.Add("Resultado", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    oEntidad = new eUsuarioCia();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    while (dataReader.Read())
                    {
                        oEntidad.CargarEntidad(dataReader);
                    }

                    oEntidad.mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    oEntidad.resultado = Convert.ToInt16(cmd.Parameters["Resultado"].Value.ToString());

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion(" LoginDA (GetParametrosSistema): " + ex.Message.ToString());
            }
            return oEntidad;
        }

    }
}
