using System;
using System.Windows.Forms;

namespace Fingerprint_NET_Furion
{
    public partial class dashboardMenu : Form
    {
        public dashboardMenu()
        {
            InitializeComponent();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Kembali ke form login
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close(); // Tutup dashboard
            }
        }
    }
}
