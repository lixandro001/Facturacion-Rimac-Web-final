using Suizalab.FacturacionAuto.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suizalab.FacturacionAuto.BE
{
	[Serializable]
	public class eListadoTicketSited : Base
	{
		public string ticket { get; set; }

		public string fecha { get; set; }

		public int idcia { get; set; }

		public string compania { get; set; }

		public string paciente { get; set; }

		public string dni { get; set; }

		public string sexo { get; set; }

		public string nautorizacion { get; set; }

		public string nafiliado { get; set; }

		public string ruta { get; set; }

		public string estado { get; set; }

		public string numosc { get; set; }

		public string perosc { get; set; }

		public string anoosc { get; set; }

		public string numsuc { get; set; }

		public string numemp { get; set; }
	}

}
