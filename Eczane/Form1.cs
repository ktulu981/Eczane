using Eczane.Core.Models;
using Eczane.Core.Repositories;
using Eczane.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eczane
{
    public partial class Form1 : Form
    {
       
      
        private readonly IILAC_AMBALAJRepository _ilacAmbalajRepo;

        private List<long> favourites = new List<long>();
        private List<long> navigation = new List<long>();
        int index = 0;
        bool isNavigationActive = false;
        public Form1(IILAC_AMBALAJRepository ilacAmbalajRepo)
        {
          
            InitializeComponent();
            _ilacAmbalajRepo = ilacAmbalajRepo;
            txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchTextHandler);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search() {
            index = 0;
            navigation = new List<long>();
            isNavigationActive = false;
            btnPrw.Enabled = false;
            btnNext.Enabled = false;
            grdIlacListe.ClearSelection();
            grdIlacListe.DataSource = _ilacAmbalajRepo.Search(txtSearch.Text);
            grdIlacListe.Columns[0].Visible = false;

            if (grdIlacListe.Rows.Count > 0)
                grdIlacListe.Rows[0].Selected = true;
            else
                ClearDetail();



            grdIlacListe.Select();
        }
        

        private void OnSearchTextHandler(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }

        }

        private void Detail(long id)
        {
            PopulateObjects(_ilacAmbalajRepo.Detay(id));
        }

        private void grdIlacListe_SelectionChanged(object sender, EventArgs e)
        {
            if (grdIlacListe.SelectedCells.Count > 0)
            {
                var id = Convert.ToInt64(grdIlacListe.Rows[grdIlacListe.SelectedCells[0].RowIndex].Cells[0].Value);
                Detail(id);
                if (!isNavigationActive)
                {
                   
                    if (!navigation.Any() || navigation.Last() != id)
                    {
                        navigation.Add(Convert.ToInt64(grdIlacListe.Rows[grdIlacListe.SelectedCells[0].RowIndex].Cells[0].Value));
                        index = navigation.Count - 1;
                    }
                    if (index > 0)
                        btnPrw.Enabled = true;
                        
                }
            }
            isNavigationActive = false;
        }

       private void PopulateObjects(IlacDetay detay)
        {
            ClearDetail();
            lblId.Text = detay.Id.ToString();
            lblAdi.Text = detay.Adi;
            lblATC.Text = detay.ATCKodu;
            lblFirma.Text = detay.Firma;
            lblOlcu.Text = detay.Olcu;
            lblAmbalaj.Text = detay.Ambalaj;
            lblBarkod.Text = detay.Barkod;
            lblFiyat.Text = detay.Fiyat?.ToString("C2");
            lblFiyatTarih.Text = "("+detay.Tarih+")";
            lblKamuFiyat.Text = detay.KamuFiyat?.ToString("C2");
            lblKamuOdenen.Text = detay.KamuOdenen?.ToString();
            lblKamuOdenenFark.Text = detay.KamuOdenenFark;
            lblOrijin.Text = detay.JenericOrijinal;
            lblDepocu.Text = detay.Depocu?.ToString("C2");
            lblImalat.Text = detay.Imalatci?.ToString("C2");
            lblRecete.Text = detay.Recete;
            lblSGK.Text = detay.SgkKodu;
            lblKub.Text = detay.KUB;


            if (detay.Resim != null)
            {
                using (var stream = new MemoryStream(detay.Resim))
                {
                    stream.Position = 0L;

                    pbIlacResim.Image = Image.FromStream(stream);
                }
            }

            if (!string.IsNullOrEmpty(detay.Barkod))
            {
                Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                pbBarcode.Image = barcode.Draw(detay.Barkod, 50); 
            }

            if (detay.EtkinMaddeler.Any())
            {

                grdEtkinMaddeler.DataSource = detay.EtkinMaddeler;
            }


            if(favourites.Any(x => x == detay.Id))
            {
                btnFav.BackColor = Color.Red;
            }
            
        }


     private void  ClearDetail()
        {
            lblId.Text = "";
            lblAdi.Text = "";
            lblATC.Text = "";
            lblFirma.Text = "";
            lblOlcu.Text = "";
            lblAmbalaj.Text = "";
            lblBarkod.Text = "";
            lblFiyat.Text = "";
            lblFiyatTarih.Text = "";
            lblKamuFiyat.Text = "";
            lblKamuOdenen.Text = "";
            lblKamuOdenenFark.Text = "";
            lblOrijin.Text = "";
            lblDepocu.Text = "";
            lblImalat.Text = "";
            lblRecete.Text = "";
            lblSGK.Text = "";
            lblKub.Text = "";

          
            btnFav.BackColor = SystemColors.Control;
            pbIlacResim.Image = null;
            pbBarcode.Image = null;
            grdEtkinMaddeler.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblBarkod.Text);
            MessageBox.Show("Barkod kopyalandı");
        }

        private void btnKub_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblKub.Text))
            {
                Form urunBilgiForm = new Form();
                urunBilgiForm.Width = 600;
                urunBilgiForm.Height = 800;
                urunBilgiForm.Text = "Kısa Ürün Bilgisi";
                WebBrowser Tarayici = new WebBrowser();
                Tarayici.Dock = DockStyle.Fill;
                Tarayici.DocumentText = lblKub.Text;
                urunBilgiForm.Controls.Add(Tarayici);
                urunBilgiForm.Show(); 
            }
        }

        private void btnFav_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblId.Text))
            {
                var id = Convert.ToInt64(lblId.Text);

                if (favourites.Any(x => x == id))
                {
                    favourites.Remove(id);
                    btnFav.BackColor = SystemColors.Control;
                }
                else
                {
                    favourites.Add(id);
                    btnFav.BackColor = Color.Red;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
          

            if (index < navigation.Count -1)
            {
                index++;
                grdIlacListe.ClearSelection();
                isNavigationActive = true;
                var liste = ((List<Ilac>)grdIlacListe.DataSource).ToList();

                var data = liste.FirstOrDefault(x => x.Id == navigation[index]);

                grdIlacListe.Rows[liste.IndexOf(data)].Selected = true;
                btnPrw.Enabled = true;
            }
            else
            {
                index = navigation.Count - 1;
                btnNext.Enabled = false;
            }
        }

        private void btnPrw_Click(object sender, EventArgs e)
        {
        

            if (index > 0)
            {
                index--;
                grdIlacListe.ClearSelection();
                isNavigationActive = true;
                var liste = ((List<Ilac>)grdIlacListe.DataSource).ToList();

                var data = liste.FirstOrDefault(x => x.Id == navigation[index]);

                grdIlacListe.Rows[liste.IndexOf(data)].Selected = true;
                btnNext.Enabled = true;

            }
            else
            { index = 0;
                btnPrw.Enabled = false;
            }
        }

      

      
    }
}
