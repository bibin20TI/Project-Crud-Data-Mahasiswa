using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Crud
{
    public partial class FormChangePassword : Form
    {
        SqlConnection koneksi = new SqlConnection("Data Source=DESKTOP-50H6RER;Initial Catalog=youtube;Integrated Security=True");


        public FormChangePassword(string username)
        {
            InitializeComponent();
            textBox1.Text = username;
            textBox1.ReadOnly = true;

        }
        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
           
        }
        private void FormChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string oldPassword = textBox2.Text;
            string newPassword = textBox3.Text;
            string confirmPassword = textBox4.Text;
            string hashPass = HashHelper.hashPassword(newPassword);
            if(newPassword != confirmPassword)
            {
                MessageBox.Show("konfirmasi password salah ", "validasi gagal  ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                koneksi.Open();
                SqlCommand checkPass = new SqlCommand("SELECT COUNT(*)FROM users WHERE username = @username AND password = @password", koneksi);
                checkPass.Parameters.AddWithValue("@username",username);
                checkPass.Parameters.AddWithValue("@password",oldPassword);

                int count = (int)checkPass.ExecuteScalar();

                if(count == 1)
                {
                    SqlCommand updatePass = new SqlCommand("UPDATE users SET password = @newPassword WHERE username = @username", koneksi);
                    updatePass.Parameters.AddWithValue("@newPassword",hashPass);
                    updatePass.Parameters.AddWithValue("@username", username);
                    updatePass.ExecuteNonQuery();
                    MessageBox.Show("password berhasil di update ", "validasi berhasil  ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FormLogin formlogin = new FormLogin();
                    formlogin.Show();


                }
                else
                {
                   
                    MessageBox.Show("password gagal di update ", "validasi gagal ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("password berhasil di update " + ex.Message);
            }




         }

            

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
