using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Fingerprint_NET_Furion
{
    public partial class Form1 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            string email = Email.Text.Trim(); 
            string password = Password.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Email dan password harus diisi!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT password FROM users WHERE email = @Email";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        string storedHash = cmd.ExecuteScalar()?.ToString();

                        if (string.IsNullOrEmpty(storedHash))
                        {
                            MessageBox.Show("Email tidak ditemukan!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; 
                        }

                        if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                        {
                            MessageBox.Show("Login berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dashboardMenu dashboard = new dashboardMenu();
                            dashboard.Show();
                            this.Hide(); 
                        }
                        else
                        {
                            MessageBox.Show("Email atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}