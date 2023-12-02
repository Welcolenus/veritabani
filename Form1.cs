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

namespace Veritabanı
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'database1DataSet.OGRENCI' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            // this.oGRENCITableAdapter.Fill(this.database1DataSet.OGRENCI);

            liste();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO OGRENCI(ADI,SOYADI,TELEFONU) VALUES(@AD,@SOYAD,@TELEFON)", conn);

            komut.Parameters.AddWithValue("@AD", SqlDbType.NVarChar).Value = Ad.Text;
            komut.Parameters.AddWithValue("@SOYAD", SqlDbType.NVarChar).Value = Soyad.Text;
            komut.Parameters.AddWithValue("@TELEFON", SqlDbType.NVarChar).Value = Telefon.Text;
            komut.ExecuteNonQuery();
            conn.Close();
            liste();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM OGRENCI WHERE ID=@ID", conn);

            komut.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            liste();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            SqlCommand komut = new SqlCommand("UPDATE OGRENCI SET ADI=@AD, SOYADI=@SOYAD, TELEFONU=@TELEFON WHERE ID=@ID", conn);

            
            komut.Parameters.AddWithValue("@AD", SqlDbType.NVarChar).Value = Ad.Text;
            komut.Parameters.AddWithValue("@SOYAD", SqlDbType.NVarChar).Value = Soyad.Text;
            komut.Parameters.AddWithValue("@TELEFON", SqlDbType.NVarChar).Value = Telefon.Text;
            komut.Parameters.AddWithValue("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);

            komut.ExecuteNonQuery();
            conn.Close();
            liste();
        }

        private void Ara_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM OGRENCI WHERE ADI LIKE '%'+@AD+'%'", conn);
            cmd.Parameters.AddWithValue("@AD", SqlDbType.NVarChar).Value = Ad.Text;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "OGRENCI");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "OGRENCI";
        }
        public void liste()
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM OGRENCI", conn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "OGRENCI");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "OGRENCI";
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Ad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Soyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Telefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

        }

        private void ShowAll_Click(object sender, EventArgs e)
        {
            liste();
        }
    }
}
