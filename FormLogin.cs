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
    public partial class FormLogin : Form
    {
        SqlConnection koneksi = new SqlConnection("Data Source=DESKTOP-50H6RER;Initial Catalog=youtube;Integrated Security=True");
        
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string username = textBox1.Text;
            string password = textBox2.Text;

            string hashPass = HashHelper.hashPassword(password);
          


            try
            {
                koneksi.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*)FROM users WHERE username = @username AND password = @password", koneksi);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password",hashPass);

                int result = (int)cmd.ExecuteScalar();
                if(result > 0)
                {
                    MessageBox.Show("LOGIN BERHASIL ", "VALIDASI BERHASIL ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form1 formMain = new Form1(username);
                    formMain.Show();

                }
                else
                {
                    MessageBox.Show("LOGIN GAGAL ", "VALIDASI GAGAL ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("ADA KESALAHAN SYSTEM " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        


           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Masukkan username...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Hindari bunyi 'ding'
                textBox2.Focus();          // Pindah ke textbox berikutnya
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.KeyDown += textBox1_KeyDown;
            // panggil method keyDown
            

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Masukkan username...";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Masukkan password...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Masukkan password...";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {    //admin123
            //240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
        }
    }
}
    

