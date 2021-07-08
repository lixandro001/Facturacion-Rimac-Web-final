using Suizalab.FacturacionAuto.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Suizalab.FacturacionAuto.BE
{
	[Serializable]
	public class eFileUpload : Base
	{
		public string AttachedFile { get; set; }

		public HttpPostedFileBase FormFile { get; set; }

		public string Ticket { get; set; }

		public string numosc { get; set; }

		public string perosc { get; set; }

		public string anoosc { get; set; }

		public string numsuc { get; set; }

		public string emposc { get; set; }

		public new string usumod { get; set; }

		public string vlruta { get; set; }
	}

}
