using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suizalab.FacturacionAuto.DA;
using Suizalab.FacturacionAuto.BE;

namespace Suizalab.FacturacionAuto.BL
{
    public class cnCompania
    {
        public static List<eCompania> Compania_List()
        {
            return new dalCompania().ListaCompania();
        }
        public static eCompania ActualizarCompania(eCompania objCompania)
        {
            return new dalCompania().ActualizarCompania(objCompania);
        }
    }
}
