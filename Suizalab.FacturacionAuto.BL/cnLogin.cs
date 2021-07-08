using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suizalab.FacturacionAuto.BE;
using Suizalab.FacturacionAuto.DA;

namespace Suizalab.FacturacionAuto.BL
{
    public class cnLogin
    {
        public static Sistema GetParametrosSistema()
        {
            return dalLogin.GetParametrosSistema();
        }

        public static eUsuarioCia GetSessionIntranet(eUsuarioCia oEntidad)
        {
            return dalLogin.GetSessionIntranet(oEntidad);
        }
    }
}

