using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;

        private void FrmMail_Load(object sender, EventArgs e)
        {
            txtMail.Text = mail;
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage msj = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                istemci.Credentials = new System.Net.NetworkCredential("mail", "şifre");
                istemci.Port = 587;
                istemci.Host = "smtp.gmail.com";
                istemci.EnableSsl = true;
                msj.To.Add(txtMail.Text);
                msj.From = new MailAddress("mail");
                msj.Subject = txtKonu.Text;
                msj.Body = rchMesaj.Text;
                istemci.Send(msj);
                MessageBox.Show("Mail Başarılı Bir Şekilde Gönderildi","Mail Gönderme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Mail Gönderilirken Bir Hata İle karşılaşıldı", "İşlem Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
