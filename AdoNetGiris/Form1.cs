using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetGiris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Server=LAPTOP-F5PIAQHS\BOTANIKSQL;Initial Catalog=Northwind;Integrated Security=true");
        private void button1_Click(object sender, EventArgs e)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Categories", baglan);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Products", baglan);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        void Kategorigetir()
        {


            SqlDataAdapter adapter = new SqlDataAdapter("select * from Categories", baglan);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;


        }
        private void btnKategoriEkle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into categories(CategoryName,Description) values(@p1,@p2)", baglan);
                baglan.Open();
                cmd.Parameters.AddWithValue("@p1", txtKatAdi.Text);
                cmd.Parameters.AddWithValue("@p2", txtAciklama.Text);
                cmd.ExecuteNonQuery();
                baglan.Close();
                Kategorigetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                baglan.Close();


            }
            /*
            #region INFO
            cmd.ExecuteNonQuery();//GERİ DEĞER DÖNMEYEN SELECT HARİCİ İŞLEMLER İÇİN
            cmd.ExecuteReader();//SELECT İŞLEMİNİ SATIR SATIR ALMAK İÇİN
            cmd.ExecuteScalar();//TEK SATIR TEK SÜTUN SORGULAR İÇİNDİR .1.Cİ SATIR 1.Cİ SÜTUNU DÖNDÜRÜR.
            #endregion*/

        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var id = dataGridView1.CurrentRow.Cells[0].Value;
                SqlCommand komut = new SqlCommand("Update Categories set CategoryName=@p1 where CategoryId=@p2", baglan);
                komut.Parameters.AddWithValue("@p1", txtKatAdi.Text);
                komut.Parameters.AddWithValue("@p2", id);
                baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();
                Kategorigetir();
            }

            catch
            {


            }
            finally { baglan.Close(); }
        }

        private void btnKategoriSil_Click(object sender, EventArgs e)
        {

            try {
            //DataBound item araştır.
            var id = dataGridView1.CurrentRow.Cells[0].Value;
            SqlCommand cmd = new SqlCommand("delete from Categories where CategoryId=@p1", baglan);
            cmd.Parameters.AddWithValue("@p1", id);
            baglan.Open();
            cmd.ExecuteNonQuery();
            baglan.Close();
            Kategorigetir();
            }
            catch { }   
            finally { baglan.Close(); }


        }
    }
}
