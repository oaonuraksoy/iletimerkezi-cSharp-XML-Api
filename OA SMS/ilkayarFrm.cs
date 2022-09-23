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
    public partial class ilkayarFrm : Form
    {
        public ilkayarFrm()
        {
            InitializeComponent();
            label1.Text = Properties.Settings.Default.username.ToString();
            label2.Text = Properties.Settings.Default.password.ToString();
            label3.Text = Properties.Settings.Default.key.ToString();
            label4.Text = Properties.Settings.Default.hash.ToString();
            label5.Text = Properties.Settings.Default.sender.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            apiSet apiset = new apiSet();
            apiset.ShowDialog();
        }
    }
}
