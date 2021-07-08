using System;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.BE
{
	[Serializable]
	public class ObtenerRutaTicketSited : Base
	{
		public string codeTicket { get; set; }

		public string numosc { get; set; }

		public string perosc { get; set; }

		public string anoosc { get; set; }

		public string sucosc { get; set; }

		public string emposc { get; set; }
	}
}
