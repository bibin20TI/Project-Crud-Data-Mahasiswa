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
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;



namespace Crud
{
    public partial class Form1 : Form
    {
        private readonly string username;
        SqlConnection koneksi = new SqlConnection("Data Source=DESKTOP-50H6RER;Initial Catalog=youtube;Integrated Security=True");
        public Form1(string username)
        {
            InitializeComponent();
            this.username = username;
            
        }
        string imageLocation = "";
        string jenis_kelamin;
        SqlCommand cmd;

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string nim = dataGridView1.SelectedRows[0].Cells["nim"].Value.ToString();

                try
                {
                    koneksi.Open();
                    SqlCommand cmd = koneksi.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM mahasiswa WHERE nim = @nim";
                    cmd.Parameters.AddWithValue("@nim",nim);
                    cmd.ExecuteNonQuery();
                    koneksi.Close();
                    tamplikanData();
                    MessageBox.Show("DATA BERHASIL DIHAPUS");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ADA KESALAHAN DATA " + ex.Message);
                }

                finally
                {
                    if (koneksi.State == ConnectionState.Open)
                    {
                        koneksi.Close();
                    }
                }



            }
            else
            {
                MessageBox.Show("DATA GAGAL DI HAPUS");
            }


            
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // dengan cara seperti itu akan menjadi pelajaran yang berharga dengan apa yng akan 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            jenis_kelamin = "pria";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            jenis_kelamin = "wanita";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(nimBox.Text.Length < 3)
            {
                MessageBox.Show("NIM TIDAK BOLEH KURANG DARI 3 DIGIT ", "VALIDASI GAGAL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                resetForm();
                nimBox.Focus();
            }
            if (!int.TryParse(nimBox.Text,out _))
            {
                MessageBox.Show("NIM HARUS BILANGAN NUMERIk ", "VALIDASI GAGAL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nimBox.Clear();
                nimBox.Focus();
            }
            if(string.IsNullOrWhiteSpace(nimBox.Text)||
               string.IsNullOrWhiteSpace(textBox2.Text)|| 
               string.IsNullOrWhiteSpace(textBox3.Text)|| 
               string.IsNullOrWhiteSpace(textBox4.Text)|| 
               string.IsNullOrWhiteSpace(textBox5.Text)|| 
               string.IsNullOrWhiteSpace(jenis_kelamin) || 
               string.IsNullOrWhiteSpace(textBox6.Text)|| 
               string.IsNullOrEmpty(imageLocation)  
                )
            {

                MessageBox.Show("FIELD HARUS TERISI SEMUA TERMASUK GAMBAR ", "VALIDASI GAGAL ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                resetForm();
                return;
            }
                


            byte[] images = null;
            FileStream stream = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);


            images = brs.ReadBytes((int)stream.Length);
            koneksi.Open();
            SqlCommand checkNim = new SqlCommand("SELECT COUNT(*)FROM mahasiswa WHERE nim = @nim", koneksi);
            checkNim.Parameters.AddWithValue("@nim", nimBox.Text);
            int count = (int)checkNim.ExecuteScalar();
            if(count > 0)
            {
                MessageBox.Show("NIM SUDAH DI PAKAI ", "VALIDASI GAGAL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nimBox.Clear();
                nimBox.Focus();
                koneksi.Close();
                return;
               // ini merupakan orang yang akan mendapatkan orang dengan harga yang murah dan akan mendapatkan orang 
            }
            SqlCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO mahasiswa(nim,nama,kelas,jurusan,tempat_lahir,jenis_kelamin,tanggal_lahir,alamat,gambar)VALUES(@nim,@nama,@kelas,@jurusan,@tempat_lahir,@jenis_kelamin,@tanggal_lahir,@alamat,@images)";
            cmd.Parameters.Add("@nim", SqlDbType.Int).Value = nimBox.Text;
            cmd.Parameters.Add("@nama", SqlDbType.VarChar).Value = textBox2.Text;
            cmd.Parameters.Add("@kelas", SqlDbType.VarChar).Value = textBox3.Text;
            cmd.Parameters.Add("@jurusan", SqlDbType.VarChar).Value = textBox4.Text;
            cmd.Parameters.Add("@tempat_lahir", SqlDbType.VarChar).Value = textBox5.Text;
            cmd.Parameters.Add("@jenis_kelamin", SqlDbType.VarChar).Value = jenis_kelamin;
            cmd.Parameters.Add("@tanggal_lahir", SqlDbType.Date).Value = dateTimePicker1.Value.Date;
            cmd.Parameters.Add("@alamat", SqlDbType.VarChar).Value = textBox6.Text;  
            cmd.Parameters.Add("@images", SqlDbType.VarBinary).Value = images;
            cmd.ExecuteNonQuery();
            koneksi.Close();
            tamplikanData();
            MessageBox.Show("Data berhasil masuk ", "VALIDASI BERHASIL", MessageBoxButtons.OK);
           
          

            
        }

      
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|all files(*.*)|*.*";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imageLocation;
   
            }
        }
        public void tamplikanData()
        {
            koneksi.Open();
            SqlCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT*FROM mahasiswa";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            koneksi.Close();


        }

        private void cariData(string keywords)
        {// saya butuh jam terbang lebih tinggi untuk meningkatkan skill per
            try
            {

                koneksi.Open();

                SqlCommand cmd = koneksi.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM mahasiswa WHERE nama LIKE @cari OR nim LIKE @cari";
                cmd.Parameters.AddWithValue("@cari", kolomPencarian.Text + "%");

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                koneksi.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("PENCARIAN DATA GAGAL " + e.Message);
                koneksi.Close();
            }
        }
     
        private void resetForm()
        {
            nimBox.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

            radioButton1.Checked = false;
            radioButton2.Checked = false;

            dateTimePicker1.Value = DateTime.Now;
            pictureBox1 = null;
            imageLocation = null;
        }
       
        private void button7_Click(object sender, EventArgs e)
        {
            tamplikanData();        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            byte[] images = null;
            FileStream stream = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            images = brs.ReadBytes((int)stream.Length);
            koneksi.Open();
            SqlCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE mahasiswa SET nama=@nama,kelas=@kelas,jurusan=@jurusan,tempat_lahir=@tempat_lahir,jenis_kelamin=@jenis_kelamin,tanggal_lahir=@tanggal_lahir,alamat=@alamat,gambar=@images WHERE nim=@nim";
            cmd.Parameters.AddWithValue("@nim", nimBox.Text);
            cmd.Parameters.AddWithValue("@nama", textBox2.Text);
            cmd.Parameters.AddWithValue("@kelas", textBox3.Text);
            cmd.Parameters.AddWithValue("@jurusan", textBox4.Text);
            cmd.Parameters.AddWithValue("@tempat_lahir", textBox5.Text);
            cmd.Parameters.AddWithValue("@jenis_kelamin", jenis_kelamin);
            cmd.Parameters.AddWithValue("@tanggal_lahir", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@alamat", textBox6.Text);
            cmd.Parameters.AddWithValue("@images", images);
            cmd.ExecuteNonQuery();
            koneksi.Close();
            MessageBox.Show("Data berhasil diperbarui ");
            tamplikanData();




        }

       
        private void button6_Click(object sender, EventArgs e)
        {
            koneksi.Open();

            SqlCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM mahasiswa WHERE nama LIKE @cari OR nim LIKE @cari";
            cmd.Parameters.AddWithValue("@cari", kolomPencarian.Text + "%");

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            koneksi.Close();

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void kolomPencarian_TextChanged(object sender, EventArgs e)
        {
            cariData(kolomPencarian.Text);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormChangePassword formGantiPassword = new FormChangePassword(username);
            formGantiPassword.ShowDialog();

        }
    }
}
