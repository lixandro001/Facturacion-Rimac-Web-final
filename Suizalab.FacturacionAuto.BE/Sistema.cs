using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.BE
{
    public class Sistema:Base
    {
        #region Métodos Públicos
            public string numemp { get; set; }
            public string razemp { get; set; }
            public string diremp { get; set; }
            public string numsuc { get; set; }
            public string codPerfilAll { get; set; }
            public string codPerfilCamp { get; set; }
            public string codPerfilFact { get; set; }
            public string codPerfilOS{ get; set; }
            public int igv { get; set; }
            public decimal tipocambio { get; set; }
        #endregion

    }
}
