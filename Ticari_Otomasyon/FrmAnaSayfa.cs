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
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void AzalanStokListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select urunad,Sum(adet) as 'Adet' from Tbl_Urunler group by UrunAd having sum(adet)<=20", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void Ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select NotId,NotTarih,Notsaat,notbaslik from Tbl_Notlar order by NotID desc",bgl.baglanti());
            da.Fill(dt);
            gridControl4.DataSource = dt;
        }

        void FirmaHareketListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareket", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void Fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Ad,Telefon from Tbl_Firmalar",bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        void Haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            AzalanStokListele();
            Ajanda();
            FirmaHareketListele();
            Fihrist();
            Haberler();

            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");


        }


        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            FrmAjandaDetay fr = new FrmAjandaDetay();

            if (dr != null)
            {
                fr.not = dr[0].ToString();
            }
            fr.Show();
        }

        
    }
}
