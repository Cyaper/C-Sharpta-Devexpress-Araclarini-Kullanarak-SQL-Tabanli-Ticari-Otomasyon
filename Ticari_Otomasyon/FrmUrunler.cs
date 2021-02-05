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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Urunler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtId.Text="";
            txtAd.Text="";
            txtModel.Text="";
            txtMarka.Text="";
            txtMarka.Text="";
            txtAlis.Text="";
            txtSatis.Text="";
            mskYil.Text="";
            rchDetay.Text="";
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Ürün ekleme

            SqlCommand komut = new SqlCommand("insert into Tbl_Urunler (UrunAd,Marka,Model,Yil,Adet,AlisFiyat,SatisFiyat,Detay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtMarka.Text);
            komut.Parameters.AddWithValue("@p3",txtModel.Text);
            komut.Parameters.AddWithValue("@p4",mskYil.Text);
            komut.Parameters.AddWithValue("@p5",int.Parse(NudAdet.Text));
            komut.Parameters.AddWithValue("@p6",decimal.Parse(txtAlis.Text));
            komut.Parameters.AddWithValue("@p7",decimal.Parse(txtSatis.Text));
            komut.Parameters.AddWithValue("@p8",rchDetay.Text);

            var secenek = MessageBox.Show("Ürün Kaydedilsin Mi?","Ürün",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Ürün Başarıyla Sisteme Kaydedildi","Yeni Ürün",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            listele();
            temizle();
                

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Urunler where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1",txtId.Text);

            var secenek = MessageBox.Show("Ürün Silinsin Mi?", "Ürün", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            if (secenek == DialogResult.Yes)
            {
                komutsil.ExecuteNonQuery();
                MessageBox.Show("Ürün Başarıyla Silindi", "Ürün Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            listele();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtId.Text = dr[0].ToString();
                txtAd.Text = dr[1].ToString();
                txtMarka.Text = dr[2].ToString();
                txtModel.Text = dr[3].ToString();
                mskYil.Text = dr[4].ToString();
                NudAdet.Value = decimal.Parse(dr[5].ToString());
                txtAlis.Text = dr[6].ToString();
                txtSatis.Text = dr[7].ToString();
                rchDetay.Text = dr[8].ToString();
            }
           

            

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutupdate = new SqlCommand("update Tbl_Urunler set UrunAd=@p1,Marka=@p2,Model=@p3,Yil=@p4,Adet=@p5,AlisFiyat=@p6,SatisFiyat=@p7,Detay=@p8 where ID=@p9",bgl.baglanti());
            komutupdate.Parameters.AddWithValue("@p1", txtAd.Text);
            komutupdate.Parameters.AddWithValue("@p2", txtMarka.Text);
            komutupdate.Parameters.AddWithValue("@p3", txtModel.Text);
            komutupdate.Parameters.AddWithValue("@p4", mskYil.Text);
            komutupdate.Parameters.AddWithValue("@p5", int.Parse(NudAdet.Text));
            komutupdate.Parameters.AddWithValue("@p6", decimal.Parse(txtAlis.Text));
            komutupdate.Parameters.AddWithValue("@p7", decimal.Parse(txtSatis.Text));
            komutupdate.Parameters.AddWithValue("@p8", rchDetay.Text);
            komutupdate.Parameters.AddWithValue("@p9",txtId.Text);

            var secenek = MessageBox.Show("Ürün Bilgileri Güncellensin Mi?", "Ürün", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (secenek == DialogResult.Yes)
            {
                komutupdate.ExecuteNonQuery();
                MessageBox.Show("Ürün Başarıyla Güncellendi", "Ürün Bilgi Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            listele();
            temizle();
        }

      
    }
}
