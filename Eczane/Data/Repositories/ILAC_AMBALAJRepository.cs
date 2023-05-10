using Eczane.Core.Entities;
using Eczane.Core.Models;
using Eczane.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.Data.Repositories
{
  public  class ILAC_AMBALAJRepository:Repository<ILAC_AMBALAJ>,IILAC_AMBALAJRepository
    {
        private readonly DataContext _context;
        public ILAC_AMBALAJRepository(DataContext context):base(context)
        {
            _context = context;
        }

        public IlacDetay Detay(long id)
        {
            return (from ambalaj in _context.ILAC_AMBALAJ
                    join form in _context.ILAC_FORM on ambalaj.ILAC_FORM_ID equals form.ID
                    join ilac in _context.ILACLAR on form.ILAC_ID equals ilac.ID
                    where ambalaj.ID == id
                    select new IlacDetay
                    {
                        Id = ambalaj.ID,
                        Ambalaj = ambalaj.AMBALAJ,
                        Resim = ambalaj.AMBALAJRESIM,
                        Barkod = ambalaj.BARKOD,
                        Depocu = ambalaj.DEPOCU,
                        Fiyat = ambalaj.FIYAT,
                        Adi = ilac.ILAC_ADI,
                        Firma = ilac.FIRMA,
                        ATCKodu = ilac.ATCKODU,
                        Olcu = form.OLCU,
                        KUB = form.KUB,
                        KamuFiyat = ambalaj.KAMUFIYATI,
                        KamuOdenen=ambalaj.KAMUODENEN,
                        JenericOrijinal=ambalaj.JENERIKORIJINAL,
                        Tarih=ambalaj.FIYATTARIH,
                        Imalatci=ambalaj.IMALATCI,
                        SgkKodu=form.SGKETKINKODU,
                        Recete=ilac.RECETE,
                        EtkinMaddeler=
                        (from maddeler in _context.ILAC_ETKIN_MADDELER
                         join etkinmadde in _context.ETKIN_MADDELER on maddeler.ETKIN_MADDE equals etkinmadde.ID
                         where maddeler.ILAC_FORM_ID == form.ID
                         select new EtkinMadde
                         {
                           
                             Adi=etkinmadde.ETKINMADDE,
                           MiktarBirim=maddeler.MIKTAR.ToString()+" "+ maddeler.BIRIM 
                         }).ToList()


                    }).FirstOrDefault();
        }

        public ICollection<Ilac> Search(string keyword)
        {

            var query = (from ambalaj in _context.ILAC_AMBALAJ
                         join form in _context.ILAC_FORM on ambalaj.ILAC_FORM_ID equals form.ID
                         join ilac in _context.ILACLAR on form.ILAC_ID equals ilac.ID
                         select new Ilac
                         {
                             Id = ambalaj.ID,
                             Adi = ilac.ILAC_ADI+" "+ form.OLCU + " " + ambalaj.AMBALAJ + " " + ambalaj.BARKOD,
                             
                         });


            if (!string.IsNullOrEmpty(keyword))
                return query.Where(x => x.Adi.Contains(keyword) || x.Adi.Contains(keyword.ToUpper())).ToList();
            else
             return  query.ToList();
        }
    }
}
