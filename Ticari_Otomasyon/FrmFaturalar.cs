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

namespace Ticari_Otomasyon
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_FaturaBilgi", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;


        }

        void Temizle()
        {
            txtId.Text = "";
            txtSeri.Text = "";
            txtSira.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtVergi.Text = "";
            txtAlici.Text = "";
            txtTeslimEden.Text = "";
            txtTesllimAlan.Text = "";
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtFaturaID.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_FaturaBilgi (seri,Sirano,tarih,saat,vergidaire,alici,teslimeden,teslimalan) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtSeri.Text);
                komut.Parameters.AddWithValue("@p2", txtSira.Text);
                komut.Parameters.AddWithValue("@p3", mskTarih.Text);
                komut.Parameters.AddWithValue("@p4", mskSaat.Text);
                komut.Parameters.AddWithValue("@p5", txtVergi.Text);
                komut.Parameters.AddWithValue("@p6", txtAlici.Text);
                komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", txtTesllimAlan.Text);

                var secenek = MessageBox.Show("Yeni Fatura Bilgisi Eklensin Mi?", "Fatura Bilgi Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (secenek == DialogResult.Yes)
                {
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Yeni Fatura Bilgisi Kaydedildi", "Yeni Fatura Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bgl.baglanti().Close();
                Listele();

            }

            if (txtFaturaID.Text != "")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtFiyat.Text);
                miktar = Convert.ToDouble(txtMiktar.Text);
                tutar = miktar * fiyat;
                txtTutar.Text = tutar.ToString();

                SqlCommand komut = new SqlCommand("insert into Tbl_FaturaDetay (UrunAd,Miktar,Fiyat,Tutar,FaturaID) values  (@p1,@p2,@p3,@p4,@p5)",bgl.baglanti());
                komut.Parameters.AddWithValue("@p1",txtUrunAd.Text);
                komut.Parameters.AddWithValue("@p2",txtMiktar.Text);
                komut.Parameters.AddWithValue("@p3",txtFiyat.Text);
                komut.Parameters.AddWithValue("@p4",txtTutar.Text);
                komut.Parameters.AddWithValue("@p5",txtFaturaID.Text);

                var secenek = MessageBox.Show("Yeni Fatura Detayı Eklensin Mi?", "Fatura Detay Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (secenek == DialogResult.Yes)
                {
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Yeni Fatura Detayı Kaydedildi", "Yeni Fatura Detay", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bgl.baglanti().Close();
                Listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr[0].ToString();
            txtSeri.Text = dr[1].ToString();
            txtSira.Text = dr[2].ToString();
            mskTarih.Text = dr[3].ToString();
            mskSaat.Text = dr[4].ToString();
            txtVergi.Text = dr[5].ToString();
            txtAlici.Text = dr[6].ToString();
            txtTeslimEden.Text = dr[7].ToString();
            txtTesllimAlan.Text = dr[8].ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From Tbl_FaturaBilgi where FaturaBilgiID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtId.Text);

            var secenek = MessageBox.Show("Fatura Silinsin Mi?", "Fatura Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Fatura Başarıyla Silindi", "Fatura Silme", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            bgl.baglanti().Close();
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_FaturaBilgi set Seri=@p1,SiraNo=@p2,Tarih=@p3,Saat=@p4,VergiDaire=@p5,Alici=@p6,TeslimEden=@p7,TeslimAlan=@p8 where FaturaBilgiID=@p9",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtSeri.Text);
            komut.Parameters.AddWithValue("@p2", txtSira.Text);
            komut.Parameters.AddWithValue("@p3", mskTarih.Text);
            komut.Parameters.AddWithValue("@p4", mskSaat.Text);
            komut.Parameters.AddWithValue("@p5", txtVergi.Text);
            komut.Parameters.AddWithValue("@p6", txtAlici.Text);
            komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            komut.Parameters.AddWithValue("@p8", txtTesllimAlan.Text);
            komut.Parameters.AddWithValue("@p9", txtId.Text);

            var secenek = MessageBox.Show("Fatura Bilgileri Güncellensin Mi?", "Fatura Detay Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Fatura Bilgileri Güncellendi", "Fatura Detay Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay frmFaturaUrunDetay = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                frmFaturaUrunDetay.id=dr[0].ToString();

            }
            frmFaturaUrunDetay.Show();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select urunad,satisfiyat from Tbl_Urunler where Id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtUrunID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtUrunAd.Text = dr[0].ToString();
                txtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
