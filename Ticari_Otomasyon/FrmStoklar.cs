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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select UrunAd,Sum(Adet) As 'Miktar' from Tbl_Urunler group by UrunAd",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //charta stok miktarı çekme
            SqlCommand komut = new SqlCommand("Select UrunAd,Sum(Adet) As 'Miktar' from Tbl_Urunler group by UrunAd",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]),Convert.ToDouble(dr[1]));
            }
            bgl.baglanti().Close();

            //charta firma-şehir sayısı çekme
            SqlCommand komut1 = new SqlCommand("Select IL,Count(*) from Tbl_Firmalar group by IL", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr1[0]), Convert.ToDouble(dr1[1]));
            }
            bgl.baglanti().Close();

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.ad = dr["UrunAd"].ToString();
            }
            fr.Show();
        }
    }
}
