using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string ad;

        void MusteriHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute hareketler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void FirmaHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareket",bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            MusteriHareket();
            FirmaHareket();

            //kullanıcı adı çekme
            lblAktifKullanici.Text = ad;
            
            

            //Toplam tutar hesaplama
            SqlCommand komut = new SqlCommand("select Sum(Tutar) from Tbl_FaturaDetay",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblKasaToplam.Text = dr[0].ToString()+ " ₺";
            }
            bgl.baglanti().Close();

            //Son ayın giderlerini çekme
            SqlCommand komut2 = new SqlCommand("SELECT TOP 1 (Elektrik+SU+DOGALGAZ+Internet+Ekstra) FROM Tbl_Giderler ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();

            //Personel Maaşları
            SqlCommand komut3 = new SqlCommand("SELECT TOP 1 Maaslar FROM Tbl_Giderler ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaas.Text = dr3[0].ToString() + " ₺";
            }
            bgl.baglanti().Close();

            //Müşteri sayısı çekme
            SqlCommand komut4 = new SqlCommand("select Count(ID) from Tbl_Musteriler", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Firma sayısı çekme
            SqlCommand komut5 = new SqlCommand("select Count(ID) from Tbl_Firmalar", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //şehir firma sayısı çekme
            SqlCommand komut6 = new SqlCommand("select Count(distinct(IL)) from Tbl_Firmalar", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
               lblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //Personel sayısı çekme
            SqlCommand komut7 = new SqlCommand("select count(ID) from Tbl_Personeller", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblPersonelSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam ürün sayısı çekme
            SqlCommand komut8 = new SqlCommand("select sum(adet) from Tbl_Urunler", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
               lblStokSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //Toplam banka sayısı çekme
            SqlCommand komut9 = new SqlCommand("select count(ID) from Tbl_Bankalar", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                lblBankaSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            //1. chart son 4 ay elektrik faturası listeleme
            SqlCommand komut10 = new SqlCommand("select top 4 Ay,Elektrik from Tbl_Giderler order by ID desc",bgl.baglanti());
            SqlDataReader dr10 = komut10.ExecuteReader();
            while (dr10.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0],dr10[1]));
            }
            bgl.baglanti().Close();

            //2.chart kontrol son 4 ay su faturası listeleme
            SqlCommand komut11 = new SqlCommand("select top 4 Ay,Su from Tbl_Giderler order by ID desc", bgl.baglanti());
            SqlDataReader dr11 = komut11.ExecuteReader();
            while (dr11.Read())
            {
                chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
            }
            bgl.baglanti().Close();
        }

      
    }
}
