using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OA_SMS
{
    public partial class apiSet : Form
    {
        public apiSet()
        {
            InitializeComponent();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.password = txtPass.Text;
            Properties.Settings.Default.username = txtUsername.Text;
            Properties.Settings.Default.key = txtApi.Text;
            Properties.Settings.Default.sender = txtSender.Text;
            Properties.Settings.Default.hash = txtHash.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Ayarlar Kaydedildi. Uygulama Kapatılıyor.");
            Application.Exit();
        }
    }
}
