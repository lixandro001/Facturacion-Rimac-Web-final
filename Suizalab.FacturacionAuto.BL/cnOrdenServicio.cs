using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suizalab.FacturacionAuto.DA;
using Suizalab.FacturacionAuto.BE;

namespace Suizalab.FacturacionAuto.BL
{
    public class cnOrdenServicio
    {
		public static List<eOrdenServicioCab> Facturar_List(string numcia, string finoscabIni, string finoscabEnd)
		{
			return new dalOrdenesServicio().Lista_Ordenes_X_Compania(numcia, finoscabIni, finoscabEnd);
		}

		public static List<eListadoTicketSited> ListaTicketSited(string fechaInicio, string fechaFin)
		{
			return new dalOrdenesServicio().ListaTicketSited(fechaInicio, fechaFin);
		}

		public static ObtenerRutaTicketSited ObtenerSitedRuta(ObtenerRutaTicketSited ObjRequest)
		{
			return new dalOrdenesServicio().ObtenerSitedRuta(ObjRequest);
		}

		public static eOrdenServicioDet RegistrarRegateo(eOrdenServicioDet ObjOSC)
		{
			return new dalOrdenesServicio().RegistrarRegateo(ObjOSC);
		}

		public static eObjetoFileUploa RegistrarSited(eObjetoFileUploa objRequest)
		{
			return new dalOrdenesServicio().RegistrarSited(objRequest);
		}

		public static eOrdenServicioDet ActualizarOrdenCab(eOrdenServicioDet ObjOSC)
		{
			return new dalOrdenesServicio().ActualizarOrdenCab(ObjOSC);
		}

		public static List<eOrdenServicioDet> Lista_Servicio(eOrdenServicioDet obj)
		{
			return new dalOrdenesServicio().Lista_Servicio(obj);
		}

		public static List<eOrdenServicioCab> List_OS_sin_firma()
		{
			return new dalOrdenesServicio().List_OS_sin_firma();
		}
	}
}
