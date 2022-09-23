using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OA_SMS
{
    public partial class anaFrm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        ilkayarFrm ilkayar = new ilkayarFrm(); 
        string check = Properties.Settings.Default.username.ToString();
        public anaFrm()
        {
            InitializeComponent();

            if (check == "null")
            {
                ilkayar.ShowDialog();
            }
            label19.Text = Properties.Settings.Default.username.ToString();
            label18.Text = Properties.Settings.Default.password.ToString();
            label17.Text = Properties.Settings.Default.key.ToString();
            label16.Text = Properties.Settings.Default.hash.ToString();
            label15.Text = Properties.Settings.Default.sender.ToString();
        }

        //xml //
        private string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                var res = "";
                byte[] bytes = Encoding.UTF8.GetBytes(xmlData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PostAddress);

                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "text/xml";
                request.Timeout = 300000000;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                // This sample only checks whether we get an "OK" HTTP status code back.
                // If you must process the XML-based response, you need to read that from
                // the response stream.
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format(
                        "POST failed. Received HTTP {0}",
                        response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader rdr = new StreamReader(responseStream))
                    {
                        res = rdr.ReadToEnd();
                    }
                    return res;
                }
            }
            catch
            {

                return "-1";
            }

        }
        //XML SONU //

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtMesaj_TextChanged(object sender, EventArgs e)
        {
            lblTxtCount.Text = txtMesaj.TextLength.ToString();
            int kalan = 154 - Int32.Parse(lblTxtCount.Text);
            lblKalan.Text = kalan.ToString();
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            if (check == "null")
            {
                ilkayar.ShowDialog();
            }
            else {
                DialogResult d;
                d = MessageBox.Show("Mesajı Göndermek istediğiniz Emin Misiniz ?", "Son Kontrol", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    if (textBoxPhoneNumber.Text.Length < 10)
                    {
                        MessageBox.Show("Telefon numarasını 10 hane olarak başında sıfır olmadan yazınız.", "Numara Eksik", MessageBoxButtons.OK);
                    }
                    else
                    {
                        textBoxPhoneNumber.ReadOnly = true;
                        txtMesaj.ReadOnly = true;
                        LBLDURUM.Text = textBoxPhoneNumber.Text.ToString() + " Numarasına Gönderiliyor.";
                        try
                        {

                            LBLDURUM.Text = textBoxPhoneNumber.Text.ToString() + "'a Gönderiliyor...";
                            String testXml = "<request>";
                            testXml += "<authentication>";
                            testXml += "<username>" + Properties.Settings.Default.username.ToString() + "</username>";
                            testXml += "<password>" + Properties.Settings.Default.password.ToString() + "</password>";
                            testXml += "<key>" + Properties.Settings.Default.key.ToString() + "</key>";
                            testXml += "<hash>" + Properties.Settings.Default.hash.ToString() + "</hash>";
                            testXml += "</authentication>";
                            testXml += "<order>";
                            testXml += "<sender>" + Properties.Settings.Default.sender.ToString() + "</sender>";
                            testXml += "<sendDateTime></sendDateTime>";
                            testXml += "<message>";
                            testXml += "<text>" + txtMesaj.Text.ToString() + "</text>";
                            testXml += "<receipents>";
                            testXml += "<number>" + textBoxPhoneNumber.Text + "</number>";
                            testXml += "</receipents>";
                            testXml += "</message>";
                            testXml += "</order>";
                            testXml += "</request>";

                            this.XMLPOST("http://api.iletimerkezi.com/v1/send-sms", testXml);

                            LBLDURUM.Text = "Gönderildi.";
                            panel2.BackColor = Color.Green;
                            textBoxPhoneNumber.Text = "";
                            txtMesaj.Text = "";
                            textBoxPhoneNumber.ReadOnly = false;
                            txtMesaj.ReadOnly = false;
                        }
                        catch (Exception q)
                        {
                            MessageBox.Show(q.Message);

                        }





                    }
                }
                else
                {
                    textBoxPhoneNumber.Text = "";
                    txtMesaj.Text = "";
                }
            }












        }

        private void textBoxPhoneNumber_Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.MistyRose;
            LBLDURUM.Text = "Durum Bekleniyor...";
        }
        int a = 0;
        private void btnL_Click(object sender, EventArgs e)
        {
            anaFrm frm = new anaFrm();

            if (frm.Width == 427)
            {
                Size = new Size(728, 386);


            }


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            anaFrm frm = new anaFrm();

            if (frm.Width.ToString() != "0")
            {
                Size = new Size(427, 386);
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            apiSet api = new apiSet();
            api.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://youtube.com");
        }
    }
}
