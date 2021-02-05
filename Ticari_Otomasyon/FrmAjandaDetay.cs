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
    public partial class FrmAjandaDetay : Form
    {
        public FrmAjandaDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        public string not;

        private void FrmAjandaDetay_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Notlar where NotId=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",not);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                richTextBox1.Text = dr[4].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
