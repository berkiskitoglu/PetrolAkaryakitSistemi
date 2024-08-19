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

namespace Test_Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=BERK;Initial Catalog=TestBenzin;Integrated Security=True;");

        void Listele()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBLBENZIN where PETROLTUR = 'Kurşunsuz95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblKursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                LblKursunsuz95Litre.Text = dr[4].ToString();
            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from TBLBENZIN where PETROLTUR = 'Kurşunsuz97'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblKursunsuz97.Text = dr2[3].ToString();
                progressBar2.Value = int.Parse(dr2[4].ToString());
                LblKursunsuz97Litre.Text = dr2[4].ToString();

            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from TBLBENZIN where PETROLTUR = 'EuroDizel10'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblEuroDizel10.Text = dr3[3].ToString();
                progressBar3.Value = int.Parse(dr3[4].ToString());
                LblEuroDizelLitre.Text = dr3[4].ToString();


            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from TBLBENZIN where PETROLTUR = 'YeniProDizel'", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblYeniProDizel.Text = dr4[3].ToString();
                progressBar4.Value = int.Parse(dr4[4].ToString());
                LblYeniProDizelLitre.Text = dr4[4].ToString();

            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("select * from TBLBENZIN where PETROLTUR = 'Gaz'", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblGaz.Text = dr5[3].ToString();
                progressBar5.Value = int.Parse(dr5[4].ToString());
                LblGazLitre.Text = dr5[4].ToString();

            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("SELECT * FROM TBLKASA", baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while(dr.Read())
            {
                LblKasa.Text = dr6[0].ToString();
            }
            baglanti.Close();
    }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(LblKursunsuz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value.ToString());
            tutar = kursunsuz95 * litre;
            TxtKursunsuzFiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz97, litre, tutar;
            kursunsuz97 = Convert.ToDouble(LblKursunsuz97.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = kursunsuz97 * litre;
            TxtKursunsuz97.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double eurodizel, litre, tutar;
            eurodizel = Convert.ToDouble(LblEuroDizel10.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = eurodizel * litre;
            TxtEuroDizel.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double prodizel, litre, tutar;
            prodizel = Convert.ToDouble(LblYeniProDizel.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = prodizel * litre;
            TxtYeniProDizel.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(LblGaz.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = gaz * litre;
            TxtGaz.Text = tutar.ToString();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLHAREKET(PLAKA,BENZINTUR,LITRE,FIYAT) values (@p1,@p2,@p3,@p4)",baglanti);
                komut.Parameters.AddWithValue("@p1", TxtPlaka.Text);
                komut.Parameters.AddWithValue("@p2", "Kurşunsuz 95");
                komut.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtKursunsuzFiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı");

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update TBLKASA set MIKTAR = MIKTAR + @p1 ", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(TxtKursunsuzFiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış Yapıldı");

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update TBLBENZIN set STOK = STOK + @p1 where PETROLTUR = 'kursunsuz95'", baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                komut3.ExecuteNonQuery();
                MessageBox.Show("Satış Yapıldı");
                Listele();
            }
        }
    }
}
