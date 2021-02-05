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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Notlar", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void Temizle()
        {
            txtId.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtbaslik.Text = "";
            rchdetay.Text = "";
            txtolusturan.Text = "";
            txthitap.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Notlar (NotTarih,notsaat,notbaslik,notdetay,notolusturan,nothitap) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtbaslik.Text);
            komut.Parameters.AddWithValue("@p4", rchdetay.Text);
            komut.Parameters.AddWithValue("@p5", txtolusturan.Text);
            komut.Parameters.AddWithValue("@p6", txthitap.Text);

            var secenek = MessageBox.Show("Yeni Mesaj Eklensin Mi?", "Yeni Mesaj", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Mesaj Eklendi", "Mesaj Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtId.Text = dr[0].ToString();
            mskTarih.Text = dr[1].ToString();
            mskSaat.Text = dr[2].ToString();
            txtbaslik.Text = dr[3].ToString();
            rchdetay.Text = dr[4].ToString();
            txtolusturan.Text = dr[5].ToString();
            txthitap.Text = dr[6].ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from Tbl_Notlar where NotID=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtId.Text);

            var secenek = MessageBox.Show("Mesaj Silinsin Mi?", "Mesaj Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Mesaj Silindi", "Mesaj Silme", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            Listele();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Notlar set nottarih=@p1,notsaat=@p2,notbaslik=@p3,notdetay=@p4,notolusturan=@p5,nothitap=@p6 where NotId=@p7",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", txtbaslik.Text);
            komut.Parameters.AddWithValue("@p4", rchdetay.Text);
            komut.Parameters.AddWithValue("@p5", txtolusturan.Text);
            komut.Parameters.AddWithValue("@p6", txthitap.Text);
            komut.Parameters.AddWithValue("@p7",txtId.Text);

            var secenek = MessageBox.Show("Mesaj Bilgileri Güncellensin Mi?", "Mesaj Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Mesaj Güncellendi", "Mesaj Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay frm = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr!=null)
            {
                frm.msj = dr[4].ToString();
            }
            frm.Show();
        }
    }
}
