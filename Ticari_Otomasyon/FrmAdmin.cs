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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string ad;

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Admin where KullaniciAd=@p1 and Sifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
               
                FrmAnaModul frm = new FrmAnaModul();
                frm.kullanici = txtAd.Text;
                frm.Show();
                this.Hide();
               
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
    }
}
