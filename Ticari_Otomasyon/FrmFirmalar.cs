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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Firmalar", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }

        void temizle()
        {
            txtId.Text = "";
            txtAd.Text =
            txtYetkiliGorev.Text = "";
            txtYetkili.Text = "";
            mskTC.Text = "";
            txtSektor.Text = "";
            mskTel1.Text = "";
            mskTel2.Text = "";
            mskTel3.Text = "";
            txtMail.Text = "";
            txtFax.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            txtVergi.Text = "";
            rchAdres.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("select Sehir from Tbl_Iller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void CariKodAciklamalar()
        {
            SqlCommand komut = new SqlCommand("select FirmaKod1 from Tbl_Kodlar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                rchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            sehirListesi();
            CariKodAciklamalar();


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["Ad"].ToString();
                txtYetkiliGorev.Text = dr["YetkiliStatu"].ToString();
                txtYetkili.Text = dr["YetkiliAdSoyad"].ToString();
                mskTC.Text = dr["YetkiliTC"].ToString();
                txtSektor.Text = dr["Sektor"].ToString();
                mskTel1.Text = dr["Telefon"].ToString();
                mskTel2.Text = dr["Telefon2"].ToString();
                mskTel3.Text = dr["Telefon3"].ToString();
                txtMail.Text = dr["Mail"].ToString();
                txtFax.Text = dr["Fax"].ToString();
                cmbil.Text = dr["IL"].ToString();
                cmbilce.Text = dr["Ilce"].ToString();
                txtVergi.Text = dr["VergiDaire"].ToString();
                rchAdres.Text = dr["Adres"].ToString();
                txtKod1.Text = dr["OzelKod1"].ToString();
                txtKod2.Text = dr["OzelKod2"].ToString();
                txtKod3.Text = dr["OzelKod3"].ToString();
            }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Firmalar (Ad,YetkiliStatu,YetkiliAdSoyad,YetkiliTC,Sektor,Telefon,Telefon2,Telefon3,Mail,Fax,IL,Ilce,VergiDaire,Adres,OzelKod1,OzelKod2,OzelKod3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", txtSektor.Text);
            komut.Parameters.AddWithValue("@p6", mskTel1.Text);
            komut.Parameters.AddWithValue("@p7", mskTel2.Text);
            komut.Parameters.AddWithValue("@p8", mskTel3.Text);
            komut.Parameters.AddWithValue("@p9", txtMail.Text);
            komut.Parameters.AddWithValue("@p10", txtFax.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergi.Text);
            komut.Parameters.AddWithValue("@p14", rchAdres.Text);
            komut.Parameters.AddWithValue("@p15", txtKod1.Text);
            komut.Parameters.AddWithValue("@p16", txtKod2.Text);
            komut.Parameters.AddWithValue("@p17", txtKod3.Text);

            var secenek = MessageBox.Show("Yeni Firma Eklensin Mi?", "Firme Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show(txtAd.Text + " İsimli Firma Başarıyla Eklendi", "Yeni Firma", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            bgl.baglanti().Close();
            listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select Ilce from Tbl_Ilceler where Sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Firmalar where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtId.Text);

            var secenek = MessageBox.Show(txtAd.Text + " İsimli Firma Silinsin Mi?", "Firme Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show(txtAd.Text + " İsimli Firma Başarıyla Silindi", "Firma Silme", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            }
            bgl.baglanti().Close();
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Firmalar set Ad=@p1,YetkiliStatu=@p2,YetkiliAdSoyad=@p3,YetkiliTC=@p4,Sektor=@p5,Telefon=@p6,Telefon2=@p7,Telefon3=@p8,Mail=@p9,Fax=@p10,IL=@p11,Ilce=@p12,VergiDaire=@p13,Adres=@p14,OzelKod1=@p15,OzelKod2=@p16,OzelKod3=@p17 where ID=@p18", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@p3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", txtSektor.Text);
            komut.Parameters.AddWithValue("@p6", mskTel1.Text);
            komut.Parameters.AddWithValue("@p7", mskTel2.Text);
            komut.Parameters.AddWithValue("@p8", mskTel3.Text);
            komut.Parameters.AddWithValue("@p9", txtMail.Text);
            komut.Parameters.AddWithValue("@p10", txtFax.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", txtVergi.Text);
            komut.Parameters.AddWithValue("@p14", rchAdres.Text);
            komut.Parameters.AddWithValue("@p15", txtKod1.Text);
            komut.Parameters.AddWithValue("@p16", txtKod2.Text);
            komut.Parameters.AddWithValue("@p17", txtKod3.Text);
            komut.Parameters.AddWithValue("@p18", txtId.Text);

            var secenek = MessageBox.Show(txtAd.Text + " İsimli Firma Güncellensin Mi?", "Firme Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show(txtAd.Text + " İsimli Firma Başarıyla Güncellendi", "Firma Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.
                    Information);

            }
            bgl.baglanti().Close();
            listele();
            temizle();
        }

       
    }
}
