using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suizalab.FacturacionAuto.Utilitarios;

namespace Suizalab.FacturacionAuto.BE
{
    [Serializable]
    public class eOrdenServicioDet: Base
    {
        #region Variables
        public string numoscab { get; set; }
        public string peroscab { get; set; }
        public string anooscab { get; set; }
        public string numsuc { get; set; }
        public string numser { get; set; }
        public string numgser { get; set; }
        public string desser { get; set; }
        public decimal presol { get; set; }
        public decimal predol { get; set; }
        public int cantdet { get; set; }
        public decimal subtotsol { get; set; }
        public decimal subtotdol { get; set; }
        public string numfox { get; set; }
        public string flagpaq { get; set; }
        public decimal totsol { get; set; }
        public decimal oscsubtotsol { get; set; }
        public decimal descmon { get; set; }
        public decimal desctot { get; set; }
        public decimal valventa { get; set; }

        #endregion
    }
}
