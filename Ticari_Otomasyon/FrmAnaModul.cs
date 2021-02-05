using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        FrmUrunler fr;
        FrmMusteriler frmMusteriler;
        FrmFirmalar frmFirmalar;
        FrmPersonel frmPersonel;
        FrmRehber frmRehber;
        FrmGiderler frmGiderler;
        FrmBankalar frmBankalar;
        FrmFaturalar frmFaturalar;
        FrmNotlar frmNotlar;
        FrmHareketler frmHareketler;
        FrmRaporlar frmRaporlar;
        FrmStoklar frmStoklar;
        FrmAyarlar frmAyarlar;
        FrmKasa frmKasa;
        FrmAnaSayfa frmAnaSayfa;

        private void btnProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr == null || fr.IsDisposed)
            {
                fr = new FrmUrunler();
                fr.MdiParent = this;
                fr.Show();
            }
           

        }

        private void btnCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmMusteriler==null || frmMusteriler.IsDisposed)
            {
                frmMusteriler = new FrmMusteriler();
                frmMusteriler.MdiParent = this;
                frmMusteriler.Show();
            }
        }

        private void btnCompanies_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(frmFirmalar==null || frmFirmalar.IsDisposed)
            {
                frmFirmalar = new FrmFirmalar();
                frmFirmalar.MdiParent = this;
                frmFirmalar.Show();
            }
        }

        private void btnStaff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmPersonel == null || frmPersonel.IsDisposed)
            {
                frmPersonel = new FrmPersonel();
                frmPersonel.MdiParent = this;
                frmPersonel.Show();
            }
        }

        private void btnGuide_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmRehber == null || frmRehber.IsDisposed)
            {
                frmRehber = new FrmRehber();
                frmRehber.MdiParent = this;
                frmRehber.Show();
            }
           
        }

        private void btnExpenses_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmGiderler==null||frmGiderler.IsDisposed)
            {
                frmGiderler = new FrmGiderler();
                frmGiderler.MdiParent = this;
                frmGiderler.Show();
            }
           
        }

        private void btnBanks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmBankalar==null || frmBankalar.IsDisposed)
            {
                frmBankalar = new FrmBankalar();
                frmBankalar.MdiParent = this;
                frmBankalar.Show();
            }
        }

        private void btnBills_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmFaturalar == null || frmFaturalar.IsDisposed)
            {
                frmFaturalar = new FrmFaturalar();
                frmFaturalar.MdiParent = this;
                frmFaturalar.Show();
            }
        }

        private void btnNotes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmNotlar==null||frmNotlar.IsDisposed)
            {
                frmNotlar = new FrmNotlar();
                frmNotlar.MdiParent = this;
                frmNotlar.Show();
            }
            

        }

        private void btnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmHareketler == null || frmHareketler.IsDisposed)
            {
                frmHareketler = new FrmHareketler();
                frmHareketler.MdiParent = this;
                frmHareketler.Show();
            }
        }

        private void btnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmRaporlar==null||frmRaporlar.IsDisposed)
            {
                frmRaporlar = new FrmRaporlar();
                frmRaporlar.MdiParent = this;
                frmRaporlar.Show();
            }
        }

        private void btnStock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmStoklar==null||frmStoklar.IsDisposed)
            {
                frmStoklar = new FrmStoklar();
                frmStoklar.MdiParent = this;
                frmStoklar.Show();
            }
            

        }

        private void btnSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmAyarlar == null || frmAyarlar.IsDisposed)
            {
                frmAyarlar = new FrmAyarlar();
                
                frmAyarlar.Show();
            }
        }

        private void btnSafe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmKasa == null || frmKasa.IsDisposed)
            {
                frmKasa = new FrmKasa();
                frmKasa.ad = kullanici;
                frmKasa.MdiParent = this;
                frmKasa.Show();

            }
        }
        public string kullanici;
        

        private void btnHome_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frmAnaSayfa == null || frmAnaSayfa.IsDisposed)
            {
                frmAnaSayfa = new FrmAnaSayfa();
                frmAnaSayfa.MdiParent = this;
                frmAnaSayfa.Show();
            }
        }

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            if (frmAnaSayfa == null || frmAnaSayfa.IsDisposed)
            {
                frmAnaSayfa = new FrmAnaSayfa();
                frmAnaSayfa.MdiParent = this;
                frmAnaSayfa.Show();
            }
        }
    }
}
