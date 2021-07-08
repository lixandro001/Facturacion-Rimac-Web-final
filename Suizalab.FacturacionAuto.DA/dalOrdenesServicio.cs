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
    public class dalOrdenesServicio: Conexion
    {
        /// <summary>
        /// Obtiene los datos del orden de servicio facturada, para crear el informe agrupado
        /// </summary>
        public List<eOrdenServicioCab> Lista_Ordenes_X_Compania(string numcia, string finoscabIni, string finoscabEnd)
        {
            eOrdenServicioCab objOrdenServicio = null;
            List<eOrdenServicioCab> ListOrdenes = null;
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand cmd = new OracleCommand(GetPackage() + "SP_LIST_ORDENES_X_CIA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxNUMCIA", OracleDbType.Varchar2).Value = numcia;
                    cmd.Parameters.Add("xxFECHAINI", OracleDbType.Varchar2).Value = finoscabIni;
                    cmd.Parameters.Add("xxFECHAFIN", OracleDbType.Varchar2).Value = finoscabEnd;

                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    ListOrdenes = new List<eOrdenServicioCab>();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();
                    while (dataReader.Read())
                    {
                        objOrdenServicio = new eOrdenServicioCab();
                        objOrdenServicio.CargarOrdenesServicio(dataReader);
                        ListOrdenes.Add(objOrdenServicio);
                    }

                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion("dalOrdenesServicio (Lista_Ordenes_X_Compania): " + ex.Message.ToString());
            }
            return ListOrdenes;
        }

        /// <summary>
        /// Agrega el nuevo servicio al ticket
        /// </summary>
        public eOrdenServicioDet RegistrarRegateo(eOrdenServicioDet objOS_det)
        {            
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand ocmd = new OracleCommand(GetPackage() + "SP_ADD_SERVICIO_REGATEO", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // InParameter
                    ocmd.Parameters.Add("xxNUMOSCAB", OracleDbType.Varchar2).Value = objOS_det.numoscab;
                    ocmd.Parameters.Add("xxPEROSCAB", OracleDbType.Varchar2).Value = objOS_det.peroscab;
                    ocmd.Parameters.Add("xxANOOSCAB", OracleDbType.Varchar2).Value = objOS_det.anooscab;
                    ocmd.Parameters.Add("xxNUMSUC", OracleDbType.Varchar2).Value = objOS_det.numsuc;
                    ocmd.Parameters.Add("xxPRESOL", OracleDbType.Double).Value = objOS_det.presol;
                    ocmd.Parameters.Add("xxPREDOL", OracleDbType.Double).Value = objOS_det.predol;
                    ocmd.Parameters.Add("xxVALUNIT", OracleDbType.Double).Value = objOS_det.valventa;
                    ocmd.Parameters.Add("xxUSUMOD", OracleDbType.Varchar2).Value = "ADMIN";

                    // OutParameter
                    ocmd.Parameters.Add("Resultado", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocmd.Parameters.Add("Mensaje", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                    ocmd.ExecuteNonQuery();

                    objOS_det.mensaje = ocmd.Parameters["Mensaje"].Value.ToString();
                    objOS_det.resultado = Convert.ToInt16(ocmd.Parameters["Resultado"].Value.ToString());

                    ocmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                objOS_det.mensaje = ex.Message;
                objOS_det.resultado = -1;
            }

            return objOS_det;
        }

        /// <summary>
        /// Agrega el nuevo servicio al ticket
        /// </summary>
        public eOrdenServicioDet ActualizarOrdenCab(eOrdenServicioDet objOS_det)
        {
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {
                    OracleCommand ocmd = new OracleCommand(GetPackage() + "SP_UPDATE_MONTO_OSC", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // InParameter
                    ocmd.Parameters.Add("xxNUMOSCAB", OracleDbType.Varchar2).Value = objOS_det.numoscab;
                    ocmd.Parameters.Add("xxPEROSCAB", OracleDbType.Varchar2).Value = objOS_det.peroscab;
                    ocmd.Parameters.Add("xxANOOSCAB", OracleDbType.Varchar2).Value = objOS_det.anooscab;
                    ocmd.Parameters.Add("xxNUMSUC", OracleDbType.Varchar2).Value = objOS_det.numsuc;
                    ocmd.Parameters.Add("xxUSUMOD", OracleDbType.Varchar2).Value = "ADMIN";

                    // OutParameter
                    ocmd.Parameters.Add("Resultado", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocmd.Parameters.Add("Mensaje", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;

                    ocmd.ExecuteNonQuery();

                    objOS_det.mensaje = ocmd.Parameters["Mensaje"].Value.ToString();
                    objOS_det.resultado = Convert.ToInt16(ocmd.Parameters["Resultado"].Value.ToString());

                    ocmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                objOS_det.mensaje = ex.Message;
                objOS_det.resultado = -1;
            }

            return objOS_det;
        }

        public eObjetoFileUploa RegistrarSited(eObjetoFileUploa objRequest)
        {
            try
            {
                using (OracleConnection connection = Conexion.ObtenerConexion())
                {
                    OracleCommand ocmd = new OracleCommand("USP_FACTRIMAC_INSERTA_RUTA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    ocmd.Parameters.Add("xxnumosc", OracleDbType.Varchar2).Value = objRequest.numosc;
                    ocmd.Parameters.Add("xxperosc", OracleDbType.Varchar2).Value = objRequest.perosc;
                    ocmd.Parameters.Add("xxanoosc", OracleDbType.Varchar2).Value = objRequest.anoosc;
                    ocmd.Parameters.Add("xxsucosc", OracleDbType.Varchar2).Value = objRequest.numsuc;
                    ocmd.Parameters.Add("xxemposc", OracleDbType.Varchar2).Value = objRequest.emposc;
                    ocmd.Parameters.Add("xxusumod", OracleDbType.Varchar2).Value = objRequest.usumod;
                    ocmd.Parameters.Add("xxvlruta", OracleDbType.Varchar2).Value = objRequest.vlruta;
                    ocmd.Parameters.Add("xxvalor1", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocmd.Parameters.Add("xxvalor2", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                    ocmd.ExecuteNonQuery();
                    objRequest.mensaje = ocmd.Parameters["xxvalor2"].Value.ToString();
                    objRequest.resultado = Convert.ToInt16(ocmd.Parameters["xxvalor1"].Value.ToString());
                    ocmd.Dispose();
                    return objRequest;
                }
                 
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                objRequest.mensaje = ex.Message;
                objRequest.resultado = -1;
                return objRequest;
            }
        }

        /// <summary>
        /// Obtiene la lista de servicios del ticket
        /// </summary>
        public List<eOrdenServicioDet> Lista_Servicio(eOrdenServicioDet obj)
        {
            eOrdenServicioDet objServicio = null;
            List<eOrdenServicioDet> ListServicio = null;
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {

                    OracleCommand cmd = new OracleCommand(GetPackage() + "SP_LISTAR_SERVICIO", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    //InParameter
                    cmd.Parameters.Add("xxNUMOSCAB", OracleDbType.Varchar2).Value = obj.numoscab;
                    cmd.Parameters.Add("xxPEROSCAB", OracleDbType.Varchar2).Value = obj.peroscab;
                    cmd.Parameters.Add("xxANOOSCAB", OracleDbType.Varchar2).Value = obj.anooscab;
                    cmd.Parameters.Add("xxNUMSUC", OracleDbType.Varchar2).Value = obj.numsuc;

                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    ListServicio = new List<eOrdenServicioDet>();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    while (dataReader.Read())
                    {
                        objServicio = new eOrdenServicioDet();
                        objServicio.numser = dataReader["NUMSER"].ToString();
                        objServicio.numgser = dataReader["NUMGSER"].ToString();
                        objServicio.desser = dataReader["DESSER"].ToString();
                        objServicio.presol = Convert.ToDecimal(dataReader["PRESOL"].ToString());
                        objServicio.predol = Convert.ToDecimal(dataReader["PREDOL"].ToString());
                        objServicio.numfox = dataReader["NUMFOX"].ToString();
                        objServicio.cantdet = Convert.ToInt32(dataReader["CANTDET"].ToString());
                        objServicio.subtotsol = Convert.ToDecimal(dataReader["SUBTOTSOL"].ToString());
                        objServicio.subtotdol = Convert.ToDecimal(dataReader["SUBTOTDOL"].ToString());
                        objServicio.numoscab = dataReader["NUMOSCAB"].ToString();
                        objServicio.peroscab = dataReader["PEROSCAB"].ToString();
                        objServicio.anooscab = dataReader["ANOOSCAB"].ToString();
                        objServicio.numsuc = dataReader["NUMSUC"].ToString();
                        objServicio.totsol = Convert.ToDecimal(dataReader["TOTSOL"].ToString());
                        objServicio.oscsubtotsol = Convert.ToDecimal(dataReader["SUBTOTSOL"].ToString());
                        objServicio.flagpaq = dataReader["FLAGPAQ"].ToString();
                        objServicio.descmon = Convert.ToDecimal(dataReader["DESCMON"].ToString());
                        objServicio.desctot = Convert.ToDecimal(dataReader["DESCTOT"].ToString());
                        objServicio.valventa = Convert.ToDecimal(dataReader["VALORVENTA"].ToString());
                        ListServicio.Add(objServicio);

                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion("dalOrdenesServicio(Lista_Servicio): " + ex.Message.ToString());
            }
            return ListServicio;
        } /// <summary>
          /// Obtiene la lista de servicios del ticket
          /// </summary>
        public List<eOrdenServicioCab> List_OS_sin_firma()
        {            
            List<eOrdenServicioCab> ListServicio = null;
            eOrdenServicioCab objServicio = null;
            try
            {
                using (OracleConnection connection = ObtenerConexion())
                {

                    OracleCommand cmd = new OracleCommand(GetPackage() + "SP_GET_OS_SIN_FIRMA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // OutParameter
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);

                    cmd.ExecuteNonQuery();

                    ListServicio = new List<eOrdenServicioCab>();

                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();

                    while (dataReader.Read())
                    {
                        objServicio = new eOrdenServicioCab();
                        objServicio.Ticket = dataReader["TICKET"].ToString();
                        objServicio.Finoscab = Convert.ToDateTime(dataReader["FECHA"].ToString());
                        objServicio.Paciente = dataReader["PACIENTE"].ToString();
                        objServicio.Numsuc = dataReader["SUCURSAL"].ToString();
                        objServicio.usumod = dataReader["OSC_USUMOD"].ToString();
                        ListServicio.Add(objServicio);

                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion("dalOrdenesServicio(List_OS_sin_firma): " + ex.Message.ToString());
            }
            return ListServicio;
        }

        public List<eListadoTicketSited> ListaTicketSited(string fechaInicio, string fechaFin)
        {
            eListadoTicketSited objServicio = null;
            List<eListadoTicketSited> ListServicio = null;
            try
            {
                using (OracleConnection connection = Conexion.ObtenerConexion())
                {
                    OracleCommand cmd = new OracleCommand("USP_FACTRIMAC_LIST_TICKETS", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("xxvfcini", OracleDbType.Varchar2).Value = fechaInicio;
                    cmd.Parameters.Add("xxvfcfin", OracleDbType.Varchar2).Value = fechaFin;
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.ExecuteNonQuery();
                    ListServicio = new List<eListadoTicketSited>();
                    OracleDataReader dataReader = ((OracleRefCursor)cmd.Parameters["RC1"].Value).GetDataReader();
                    while (dataReader.Read())
                    {
                        objServicio = new eListadoTicketSited();
                        objServicio.ticket = dataReader["ticket"].ToString();
                        objServicio.fecha = dataReader["fecha"].ToString();
                        objServicio.idcia = Convert.ToInt32(dataReader["idcia"].ToString());
                        objServicio.compania = dataReader["compania"].ToString();
                        objServicio.paciente = dataReader["paciente"].ToString();
                        objServicio.dni = dataReader["dni"].ToString();
                        objServicio.sexo = dataReader["sexo"].ToString();
                        objServicio.nautorizacion = dataReader["nautorizacion"].ToString();
                        objServicio.nafiliado = dataReader["nafiliado"].ToString();
                        objServicio.ruta = dataReader["ruta"].ToString();
                        objServicio.estado = dataReader["estado"].ToString();
                        objServicio.numosc = dataReader["numosc"].ToString();
                        objServicio.perosc = dataReader["perosc"].ToString();
                        objServicio.anoosc = dataReader["anoosc"].ToString();
                        objServicio.numsuc = dataReader["numsuc"].ToString();
                        objServicio.numemp = dataReader["numemp"].ToString();
                        ListServicio.Add(objServicio);
                    }
                    cmd.Dispose();
                    return ListServicio;
                }
                
            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                Utilitario.LogServiceFacturacion("dalOrdenesServicio(ListaTicketSited): " + ex.Message.ToString());
                return ListServicio;
            }
        }

        public ObtenerRutaTicketSited ObtenerSitedRuta(ObtenerRutaTicketSited ObjRequest)
        {
            try
            {
                using (OracleConnection connection = Conexion.ObtenerConexion())
                {
                    OracleCommand ocmd = new OracleCommand("USP_FACTRIMAC_OBTENER_RUTA", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    ocmd.Parameters.Add("xxnumosc", OracleDbType.Varchar2).Value = ObjRequest.numosc;
                    ocmd.Parameters.Add("xxperosc", OracleDbType.Varchar2).Value = ObjRequest.perosc;
                    ocmd.Parameters.Add("xxanoosc", OracleDbType.Varchar2).Value = ObjRequest.anoosc;
                    ocmd.Parameters.Add("xxsucosc", OracleDbType.Varchar2).Value = ObjRequest.sucosc;
                    ocmd.Parameters.Add("xxemposc", OracleDbType.Varchar2).Value = ObjRequest.emposc;
                    ocmd.Parameters.Add("xxvalor1", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    ocmd.Parameters.Add("xxvalor2", OracleDbType.Varchar2, 1000).Direction = ParameterDirection.Output;
                    ocmd.ExecuteNonQuery();
                    ObjRequest.resultado = Convert.ToInt16(ocmd.Parameters["xxvalor1"].Value.ToString());
                    ObjRequest.mensaje = ocmd.Parameters["xxvalor2"].Value.ToString();
                    ocmd.Dispose();
                    return ObjRequest;
                }
                

            }
            catch (Exception ex)
            {
                new Exception(ex.Message, ex);
                ObjRequest.mensaje = ex.Message;
                ObjRequest.resultado = -1;
                return ObjRequest;
            }
        }

    }
}
