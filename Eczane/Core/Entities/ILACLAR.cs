using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.Core.Entities
{
   public class ILACLAR:Entity
    {
        public string ILAC_ADI { get; set; }

        public string NFC { get; set; }

        public string ATCKODU { get; set; }

        public string RECETE { get; set; }

        public int ORDER { get; set; }

        public string FARMASOTIKFORM { get; set; }

        public int MUSTAHZAR1 { get; set; }

        public long KONTROLETABI { get; set; }

        public string FIRMA { get; set; }

        public List<ILAC_FORM> ILAC_FORM { get; set; } = new List<ILAC_FORM>();
    }
}
