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

namespace ogrenci_kayıt
{
    public partial class ogretmen : Form
    {
        public ogretmen()
        {
            InitializeComponent();
        }


        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-3TDJHFS\\MSSQLSERVER123;Initial Catalog=notkayıt;Integrated Security=True");
        private void ogretmen_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'notkayıtDataSet.ogrenciler' table. You can move, or remove it, as needed.
           double ort = 0;
            SqlCommand komut = new SqlCommand("select COUNT( *) from ogrenciler where DURUM='True' ", baglantı);
            SqlCommand komut2 = new SqlCommand("select COUNT( *) from ogrenciler where DURUM='False' ", baglantı);
            SqlCommand komut3 = new SqlCommand("select AVG(ORTALAMA) from ogrenciler ", baglantı);
            baglantı.Open();
           
            label7.Text = komut.ExecuteScalar().ToString(); 
           
            label8.Text= komut2.ExecuteScalar().ToString();
            label10.Text = komut3.ExecuteScalar().ToString();
            ort = Convert.ToDouble(label10.Text);
            ort = Math.Round(ort, 1);
            label10.Text = ort.ToString();
            baglantı.Close();

            this.ogrencilerTableAdapter.Fill(this.notkayıtDataSet.ogrenciler);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sayac = dataGridView1.Rows.Count -1;
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into ogrenciler (OGRID,OGRNUMARA,OGRAD,OGRSOYAD) values(@p,@p1,@p2,@p3)", baglantı);
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textname.Text);
            komut.Parameters.AddWithValue("@p3", textsur.Text);
            if (dataGridView1.RowCount != 40)
            {
                sayac++;
                komut.Parameters.AddWithValue("@p",sayac);


            }
            //komut.Parameters.AddWithValue("@p",11);
            komut.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("öğrenci sisteme eklendi");
            this.ogrencilerTableAdapter.Fill(this.notkayıtDataSet.ogrenciler);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnsil.Visible = true;
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            maskedTextBox1.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textname.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
          textsur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            texts1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            texts2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            texts3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            lblortalama.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            //double ort = Convert.ToDouble(lblortalama.Text);
            // ort = Math.Round(ort, 2);
            // lblortalama.Text = ort.ToString();


            this.ogrencilerTableAdapter.Fill(this.notkayıtDataSet.ogrenciler);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;
            s1 = Convert.ToDouble(texts1.Text);
            s2 = Convert.ToDouble(texts2.Text);
            s3 = Convert.ToDouble(texts3.Text);
            ortalama = (s1 + s2 + s3) / 3;
            lblortalama.Text = ortalama.ToString();

            if (ortalama>=50)
            {
                durum = "true";

            }
            else
            {
                durum = "false";
            }

            baglantı.Open();
            SqlCommand komut = new SqlCommand("update ogrenciler set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@S4,DURUM=@S5 WHERE OGRNUMARA=@S6", baglantı);
            komut.Parameters.AddWithValue("@P1", texts1.Text);
            komut.Parameters.AddWithValue("@P2", texts2.Text);
            komut.Parameters.AddWithValue("@P3", texts3.Text);
            komut.Parameters.AddWithValue("@S4", Convert.ToDecimal(lblortalama.Text));
            komut.Parameters.AddWithValue("@S5", durum);
            komut.Parameters.AddWithValue("@S6", maskedTextBox1.Text);
            komut.ExecuteNonQuery();
            baglantı.Close();

            MessageBox.Show("öğrenci güncellendi");

            ////////////////////////////////////////
             double ort = 0;
            SqlCommand komutt = new SqlCommand("select COUNT( *) from ogrenciler where DURUM='True' ", baglantı);
            SqlCommand komut2 = new SqlCommand("select COUNT( *) from ogrenciler where DURUM='False' ", baglantı);
            SqlCommand komut3 = new SqlCommand("select AVG(ORTALAMA) from ogrenciler ", baglantı);
            baglantı.Open();

            label7.Text = komutt.ExecuteScalar().ToString();

            label8.Text = komut2.ExecuteScalar().ToString();
            label10.Text = komut3.ExecuteScalar().ToString();
            ort = Convert.ToDouble(label10.Text);
            ort = Math.Round(ort, 1);
            label10.Text = ort.ToString();
            baglantı.Close();
//////////////////////////////
            this.ogrencilerTableAdapter.Fill(this.notkayıtDataSet.ogrenciler);

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            DialogResult durum = MessageBox.Show( " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
            if (durum==DialogResult.Yes)
            {
                SqlCommand silKomutu = new SqlCommand("delete from ogrenciler where OGRNUMARA=@p1", baglantı);
                silKomutu.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
                silKomutu.ExecuteNonQuery();
                MessageBox.Show("Silme işlemi başarılı");
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi");
            }
    
            baglantı.Close();
            this.ogrencilerTableAdapter.Fill(this.notkayıtDataSet.ogrenciler);
        }
    }
}
