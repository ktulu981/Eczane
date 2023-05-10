using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.Core.Entities
{
    public  class ILAC_AMBALAJ : Entity
    {
       
        public string BARKOD { get; set; }

        [ForeignKey("ILAC_FORM")]
        public long ILAC_FORM_ID { get; set; }

        public ILAC_FORM ILAC_FORM { get; set; }

        public string AMBALAJ { get; set; }

        public string FIYATTARIH { get; set; }

        public double? FIYAT { get; set; }

        public double? DEPOCU { get; set; }

        public double? IMALATCI { get; set; }

        public double? KAMUFIYATI { get; set; }

        public double? KAMUODENEN { get; set; }

        public string JENERIKORIJINAL { get; set; }

        public string ESDEGERGRUP { get; set; }

        public string DURUM { get; set; }

        public byte[] AMBALAJRESIM { get; set; }
    }
}
