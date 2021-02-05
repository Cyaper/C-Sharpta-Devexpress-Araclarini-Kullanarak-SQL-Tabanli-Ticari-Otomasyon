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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Giderler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }

        void Temizle()
        {
            txtId.Text = "";
            cmbAy.Text = "";
            cmbYil.Text = "";
            txtElektrik.Text = "";
            txtSu.Text = "";
            txtgaz.Text = "";
            txtint.Text = "";
            txtmaas.Text = "";
            txtEkstra.Text = "";
            rchNotlar.Text = "";

        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Giderler (Ay,Yil,Elektrik,Su,Dogalgaz,Internet,Maaslar,Ekstra,Notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbAy.Text);
            komut.Parameters.AddWithValue("@p2",cmbYil.Text);
            komut.Parameters.AddWithValue("@p3",decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtint.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9",rchNotlar.Text);

            komut.ExecuteNonQuery();
            MessageBox.Show("Gider Eklendi","Yeni Gider",MessageBoxButtons.OK,MessageBoxIcon.Information);
            bgl.baglanti().Close();
            Listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr[0].ToString();
            cmbAy.Text = dr[1].ToString();
            cmbYil.Text = dr[2].ToString();
            txtElektrik.Text = dr[3].ToString();
            txtSu.Text = dr[4].ToString();
            txtgaz.Text = dr[5].ToString();
            txtint.Text = dr[6].ToString();
            txtmaas.Text = dr[7].ToString();
            txtEkstra.Text = dr[8].ToString();
            rchNotlar.Text = dr[9].ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Giderler where ID=@p1", bgl.baglanti()) ;
            komut.Parameters.AddWithValue("@p1",txtId.Text);

            var secenek = MessageBox.Show("Gider Silinsin Mi?","Gider Silme",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Gider Silindi","Gider Silme",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
            bgl.baglanti().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Giderler set ay=@p1,yil=@p2,Elektrik=@p3,su=@p4,dogalgaz=@p5,Internet=@p6,Maaslar=@p7,Ekstra=@p8,Notlar=@p9 where ID=@p10",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbAy.Text);
            komut.Parameters.AddWithValue("@p2", cmbYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtint.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", rchNotlar.Text);
            komut.Parameters.AddWithValue("@p10", txtId.Text);


            var secenek = MessageBox.Show("Gider Güncellensin Mi?", "Gider Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Gider Güncellendi", "Gider Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bgl.baglanti().Close();
            Listele();
            Temizle();
        }

       
    }
}
