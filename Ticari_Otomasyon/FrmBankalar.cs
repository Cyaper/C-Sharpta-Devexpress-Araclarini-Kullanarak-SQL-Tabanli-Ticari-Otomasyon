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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(" Execute Banka_Firma", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            this.gridView1.OptionsView.ShowAutoFilterRow = true;//Veri tabanına arama kısmı ekler

           
        }

        void Temizle()
        {
            txtAd.Text = "";
            lookUpEdit1.Text = "";
            txtHesapno.Text = "";
            txtHesaptur.Text = "";
            txtiban.Text = "";
            txtId.Text = "";
            txtSube.Text = "";
            txtYetkili.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            mskTarih.Text = "";
            mskTel1.Text = "";
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

        void FirmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,Ad from Tbl_Firmalar",bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.NullText = "Lütfen Firma Seçiniz";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "Ad";
            lookUpEdit1.Properties.DataSource = dt;
        }


        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            Listele();
            SehirListesi();
            FirmaListesi();
            Temizle();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Bankalar (BankaAdi,IL,Ilce,Sube,Iban,HesapNo,Yetkili,Telefon,Tarih,HesapTuru,FirmaID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",cmbil.Text);
            komut.Parameters.AddWithValue("@p3",cmbilce.Text);
            komut.Parameters.AddWithValue("@p4",txtSube.Text);
            komut.Parameters.AddWithValue("@p5",txtiban.Text);
            komut.Parameters.AddWithValue("@p6",txtHesapno.Text);
            komut.Parameters.AddWithValue("@p7",txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8",mskTel1.Text);
            komut.Parameters.AddWithValue("@p9",mskTarih.Text);
            komut.Parameters.AddWithValue("@p10",txtHesaptur.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);

            var secenek = MessageBox.Show("Yeni Banka Eklensin Mi?","Banka Ekleme",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Banka Başarıyla Eklendi","Yeni Banka",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            Listele();
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
            txtId.Text = dr[0].ToString();
            txtAd.Text = dr[1].ToString();
            cmbil.Text = dr[2].ToString();
            cmbilce.Text = dr[3].ToString();
            txtSube.Text = dr[4].ToString();
            txtiban.Text = dr[5].ToString();
            txtHesapno.Text = dr[6].ToString();
            txtYetkili.Text = dr[7].ToString();
            mskTel1.Text = dr[8].ToString();
            mskTarih.Text = dr[9].ToString();
            txtHesaptur.Text = dr[10].ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Bankalar where ID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtId.Text);

            var secenek = MessageBox.Show("Banka Sistemden Silinsin Mi?", "Banka Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Banka Başarıyla Sistemden Silindi", "Banka Silme", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            bgl.baglanti().Close();
            Listele();
            Temizle();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Bankalar set BankaAdi=@p1,IL=@p2,Ilce=@p3,Subr=@p4,Iban=@p5,HesapNo=@p6,Yetkili=@p7,Telefon=@p8,Tarih=@p9,HesapTuru=@p10,FirmaID=@p11 where ID=@p12",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtiban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", mskTel1.Text);
            komut.Parameters.AddWithValue("@p9", mskTarih.Text);
            komut.Parameters.AddWithValue("@p10", txtHesaptur.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);

            var secenek = MessageBox.Show("Banka Bilgileri Güncellensin Mi?", "Banka Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Banka Başarıyla Güncellendi", "Banka Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            Listele();
            Temizle();
        }

       
    }
}
