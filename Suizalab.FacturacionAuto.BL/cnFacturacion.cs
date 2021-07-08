using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suizalab.FacturacionAuto.DA;
using Suizalab.FacturacionAuto.BE;

namespace Suizalab.FacturacionAuto.BL
{
    public class cnFacturacion
    {
        public List<eFacturacion> ListaOrdenFacturados(string serFac, string numfac)
        {
            return new dalFacturacion().ListaOrdenFacturados(serFac, numfac);
        }
    }
}
