using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.Core.Models
{
   public class IlacDetay
    {
        public long Id { get; set; }
        public string Ambalaj { get; set; }
        public byte[] Resim { get; set; }
        public string Barkod { get; set; }
        public double? Depocu { get; set; }
        public double? Fiyat { get; set; }
        public string Tarih { get; set; }
        public double? Imalatci { get; set; }
        public double? KamuOdenen { get; set; }
        public double? KamuFiyat { get; set; }
        public string JenericOrijinal { get; set; }
        public string Adi { get; set; }
        public string Firma { get; set; }
        public string ATCKodu { get; set; }
        public string Olcu { get; set; }
        public string KUB { get; set; }

        public string SgkKodu { get; set; }

        public string Recete { get; set; }

        public List<EtkinMadde> EtkinMaddeler { get; set; } = new List<EtkinMadde>();
        public string KamuOdenenFark { get {

                if (KamuFiyat - KamuOdenen > 0)
                    return (KamuFiyat - KamuOdenen)?.ToString("C2") + " Fiyat Farkı";
                else
                    return "Fark Yok";
            } }
    }
}
