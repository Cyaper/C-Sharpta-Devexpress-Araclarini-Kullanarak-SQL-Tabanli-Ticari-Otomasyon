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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Musteriler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }

        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("select Sehir from Tbl_Iller",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select Ilce from Tbl_Ilceler where Sehir=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbil.SelectedIndex+1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Musteriler (Ad,Soyad,Telefon,Telefon2,TC,Mail,IL,Ilce,Adres,VergiDaire) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskTel1.Text);
            komut.Parameters.AddWithValue("@p4",mskTel2.Text);
            komut.Parameters.AddWithValue("@p5",mskTC.Text);
            komut.Parameters.AddWithValue("@p6",txtMail.Text);
            komut.Parameters.AddWithValue("@p7",cmbil.Text);
            komut.Parameters.AddWithValue("@p8",cmbilce.Text);
            komut.Parameters.AddWithValue("@p9",rchAdres.Text);
            komut.Parameters.AddWithValue("@p10",txtVergi.Text);

            var secenek = MessageBox.Show("Yeni Müşteri Eklensin Mi?","Müşeteri Ekleme",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Müşteri Sisteme Kaydedildi","Yeni Müşteri",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            listele();
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr[0].ToString() ;
                txtAd.Text = dr[1].ToString() ;
                txtSoyad.Text = dr[2].ToString() ;
                mskTel1.Text = dr[3].ToString() ;
                mskTel2.Text = dr[4].ToString() ;
                mskTC.Text = dr[5].ToString() ;
                txtMail.Text = dr[6].ToString() ;
                cmbil.Text = dr[7].ToString() ;
                cmbilce.Text = dr[8].ToString() ;
                rchAdres.Text = dr[9].ToString() ;
                txtVergi.Text = dr[10].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Musteriler where ID=@p1 ",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtId.Text);


            var secenek = MessageBox.Show(txtAd.Text+" "+txtSoyad.Text+" isimli Müşteri Silinsin Mi?", "Müşeteri Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Müşteri Başarıyla Silindi", "Müşteri Silme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
            listele();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTel1.Text = "";
            mskTel2.Text = "";
            txtMail.Text = "";
            mskTC.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            rchAdres.Text = "";
            txtVergi.Text = "";
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Musteriler set Ad=@p1,Soyad=@p2,Telefon=@p3,Telefon2=@p4,TC=@p5,Mail=@p6,IL=@p7,Ilce=@p8,Adres=@p9,VergiDaire=@p10 where ID=@p11",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskTel1.Text);
            komut.Parameters.AddWithValue("@p4",mskTel2.Text);
            komut.Parameters.AddWithValue("@p5",mskTC.Text);
            komut.Parameters.AddWithValue("@p6",txtMail.Text);
            komut.Parameters.AddWithValue("@p7",cmbil.Text);
            komut.Parameters.AddWithValue("@p8",cmbilce.Text);
            komut.Parameters.AddWithValue("@p9",rchAdres.Text);
            komut.Parameters.AddWithValue("@p10",txtVergi.Text);
            komut.Parameters.AddWithValue("@p11",txtId.Text);

            var secenek = MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " isimli Müşteri Bilgileri Güncellensin Mi?", "Müşteri Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Müşteri Bilgileri Başarıyla Güncellendi", "Müşteri Bilgi Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            listele();
        }

       
    }
}
