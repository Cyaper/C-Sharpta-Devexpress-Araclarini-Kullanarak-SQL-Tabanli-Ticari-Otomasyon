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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void MusteriListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Ad,Soyad,Telefon,Telefon2,Mail from Tbl_Musteriler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void FirmaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Ad,YetkiliAdSoyad,Telefon,Telefon2,Telefon3,Mail,Fax from Tbl_Firmalar",bgl.baglanti());

            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            MusteriListele();
            FirmaListele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmMail = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frmMail.mail = dr["Mail"].ToString() ;
                
                
            }
            frmMail.Show();
            
            
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {

            FrmMail frmMail = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                frmMail.mail = dr["Mail"].ToString();


            }
            frmMail.Show();
        }
    }
}
