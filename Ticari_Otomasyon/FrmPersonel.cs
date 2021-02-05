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
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Personeller",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void SehirListesi()
        {
            SqlCommand komut = new SqlCommand("select Sehir from Tbl_Iller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void Temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mskTel1.Text = "";
            mskTC.Text = "";
            txtMail.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            rchAdres.Text = "";
            txtGorev.Text = "";
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
           
            listele();
            SehirListesi();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Personeller (Ad,Soyad,Telefon,TC,Mail,IL,Ilce,Adres,Gorev) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",mskTel1.Text);
            komut.Parameters.AddWithValue("@p4",mskTC.Text);
            komut.Parameters.AddWithValue("@p5",txtMail.Text);
            komut.Parameters.AddWithValue("@p6",cmbil.Text);
            komut.Parameters.AddWithValue("@p7",cmbilce.Text);
            komut.Parameters.AddWithValue("@p8",rchAdres.Text);
            komut.Parameters.AddWithValue("@p9",txtGorev.Text);

            var secenek = MessageBox.Show("Yeni Personel Eklensin Mi?", "Personel Ekleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show(txtAd.Text +" "+txtSoyad.Text+ " İsimli Personel Başarıyla Eklendi", "Yeni Personel", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            bgl.baglanti().Close();
            listele();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Temizle();
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr[0].ToString();
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTel1.Text = dr[3].ToString();
                mskTC.Text = dr[4].ToString();
                txtMail.Text = dr[5].ToString();
                cmbil.Text = dr[6].ToString();
                cmbilce.Text = dr[7].ToString();
                rchAdres.Text = dr[8].ToString();
                txtGorev.Text = dr[9].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Personeller where ID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtId.Text);

            var secenek = MessageBox.Show(txtAd.Text + " İsimli Personel Silinsin Mi?", "Personel Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show(txtAd.Text+" "+txtSoyad.Text + " İsimli Personel Başarıyla Silindi", "Personel Silme", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            }
            bgl.baglanti().Close();
            listele();
            Temizle();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Personeller set Ad=@p1,Soyad=@p2,Telefon=@p3,TC=@p4,Mail=@p5,IL=@p6,Ilce=@p7,Adres=@p8,Gorev=@p9 where ID=@p10", bgl.baglanti()) ;

            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTel1.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.Parameters.AddWithValue("@p6", cmbil.Text);
            komut.Parameters.AddWithValue("@p7", cmbilce.Text);
            komut.Parameters.AddWithValue("@p8", rchAdres.Text);
            komut.Parameters.AddWithValue("@p9", txtGorev.Text);
            komut.Parameters.AddWithValue("@p10", txtId.Text);

            var secenek = MessageBox.Show(txtAd.Text+" "+txtSoyad.Text + " İsimli Personel Güncellensin  Mi?", "Personel Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " İsimli Personel Başarıyla Güncellendi", "Personel Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            bgl.baglanti().Close();
            listele();
            Temizle();

        }

       
    }
}
