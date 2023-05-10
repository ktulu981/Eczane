using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.Core.Entities
{
  public  class ILAC_ETKIN_MADDELER : Entity
    {

        [ForeignKey("ETKIN_MADDELER")]
        public long ETKIN_MADDE { get; set; }
        public ETKIN_MADDELER ETKIN_MADDELER { get; set; }
        [ForeignKey("ILAC_FORM")]
        public long ILAC_FORM_ID { get; set; }
        public ILAC_FORM ILAC_FORM { get; set; }
        public double MIKTAR { get; set; }
        public string BIRIM { get; set; }
    }
}
