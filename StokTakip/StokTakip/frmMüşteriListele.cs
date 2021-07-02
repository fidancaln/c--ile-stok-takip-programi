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

namespace StokTakip
{
    public partial class frmMüşteriListele : Form
    {
        public frmMüşteriListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-8HJ2GO0;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet daset = new DataSet();
       
        private void frmMüşteriListele_Load(object sender, EventArgs e)
        {
            kayıt_göster();
        }
        private void kayıt_göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri", baglanti);
            adtr.Fill(daset,"müşteri");
            dataGridView1.DataSource = daset.Tables["müşteri"];
            baglanti.Close();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            txttc.Text=dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtadsoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
          txttelefon.Text= dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtadres.Text= dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
           txtemail.Text= dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update müşteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc",baglanti);
            komut.Parameters.AddWithValue("@tc", txttc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtadsoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txttelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtadres.Text);
            komut.Parameters.AddWithValue("@email", txtemail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["müşteri"].Clear();
            kayıt_göster();

            MessageBox.Show("Müşteri kaydı güncellendi.");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from müşteri where tc='"+dataGridView1.CurrentRow.Cells["tc"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["müşteri"].Clear();
            kayıt_göster();
            MessageBox.Show("Kayıt silindi.");
        }

        private void txttcara_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
          SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri where tc like '%"+txttcara.Text+"%'", baglanti);
          adtr.Fill(tablo);
          dataGridView1.DataSource = tablo;
          baglanti.Close();

        }

        
          
    }
    }


