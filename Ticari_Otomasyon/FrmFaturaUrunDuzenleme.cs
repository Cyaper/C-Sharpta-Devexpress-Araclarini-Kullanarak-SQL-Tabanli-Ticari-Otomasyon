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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void Aktar()
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_FaturaDetay where FaturaUrunID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtUrunID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtUrunAd.Text = dr[1].ToString();
                txtMiktar.Text = dr[2].ToString();
                txtFiyat.Text = dr[3].ToString();
                txtTutar.Text = dr[4].ToString();
            }
            bgl.baglanti().Close();
        }

        public string id;
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtUrunID.Text = id;
            Aktar();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_FaturaDetay set UrunAd=@p1,Miktar=@p2,Fiyat=@p3,Tutar=@p4 where FaturaUrunID=@p5",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtUrunAd.Text);
            komut.Parameters.AddWithValue("@p2",txtMiktar.Text);
            komut.Parameters.AddWithValue("@p3",decimal.Parse(txtFiyat.Text));
            komut.Parameters.AddWithValue("@p4",decimal.Parse(txtTutar.Text));
            komut.Parameters.AddWithValue("@p5",txtUrunID.Text);

            var secenek = MessageBox.Show("Değişiklikler Kaydedilsin Mi?","Fatura Güncelleme",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Değişiklikler Kaydedildi","Fatura Güncelleme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_FaturaDetay where FaturaUrunID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtUrunID.Text);

            var secenek = MessageBox.Show("Fatura Silinsin Mi?", "Fatura Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Fatura Silindi!", "Fatura Silme", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            bgl.baglanti().Close();

        }
    }
}
