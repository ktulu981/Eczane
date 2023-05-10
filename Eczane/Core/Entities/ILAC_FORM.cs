using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.Core.Entities
{
  public  class ILAC_FORM:Entity
    {
        public string OLCU { get; set; }
        [ForeignKey("ILACLAR")]
        public long ILAC_ID { get; set; }
        public ILACLAR ILACLAR { get; set; }
        public string SGKETKINKODU { get; set; }
        public string KUB { get; set; }
        public List<ILAC_ETKIN_MADDELER> ILAC_ETKIN_MADDELER { get; set; } = new List<ILAC_ETKIN_MADDELER>();
        public List<ILAC_AMBALAJ> ILAC_AMBALAJ { get; set; } = new List<ILAC_AMBALAJ>();
    }
}
