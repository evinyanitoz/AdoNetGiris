using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetGiris
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Server=LAPTOP-F5PIAQHS\BOTANIKSQL;Initial Catalog=Northwind;Integrated Security=true");
        private void button2_Click(object sender, EventArgs e)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("select * from Products", baglan);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void UrunGetir()
        {


            SqlDataAdapter adapter = new SqlDataAdapter("select * from Products", baglan);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;



        }
        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert into Products(ProductName,UnitsInStock,UnitPrice) values(@p1,@p2,@p3)", baglan);
                baglan.Open();
                cmd.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
                cmd.Parameters.AddWithValue("@p2", txtStok.Text);
                cmd.Parameters.AddWithValue("@p3", txtPrice.Text);
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("ÜÜRN EKLENDİ");
                UrunGetir();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                baglan.Close();


            }
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            try
            {

                var id = dataGridView1.CurrentRow.Cells[0].Value;
                SqlCommand cmd = new SqlCommand("delete from Products where ProductId=@p1", baglan);
                cmd.Parameters.AddWithValue("@p1", id);
                baglan.Open();
                cmd.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("ÜÜRN SİLİNDİ");
                UrunGetir();
            }
            catch(Exception ex) {

                MessageBox.Show(ex.Message);
            }   
            finally { baglan.Close(); } 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var id = dataGridView1.CurrentRow.Cells[0].Value;
                SqlCommand komut = new SqlCommand("Update Products set ProductName=@p1 where ProductId=@p2", baglan);
                komut.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
                komut.Parameters.AddWithValue("@p2", id);
                baglan.Open();
                MessageBox.Show("ÜRÜN GÜNCELLENDİ");
                komut.ExecuteNonQuery();
                baglan.Close();
                UrunGetir();
            }
            catch {
            }
            finally { baglan.Close(); }
        }
    }
}
