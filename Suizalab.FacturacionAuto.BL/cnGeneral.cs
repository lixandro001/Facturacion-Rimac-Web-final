using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suizalab.FacturacionAuto.DA;
using Suizalab.FacturacionAuto.BE;

namespace Suizalab.FacturacionAuto.BL
{
    public class cnGeneral
    {
        public ePlantillaCorreo GetPlantillaCorreo(ePlantillaCorreo obj)
        {
            return new dalPlantillaCorreo().GetPlantillaCorreo(obj);
        }
    }
}
