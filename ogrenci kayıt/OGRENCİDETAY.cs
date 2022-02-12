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
    public partial class OGRENCİDETAY : Form
    {
        public OGRENCİDETAY()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public string numara;
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-3TDJHFS\\MSSQLSERVER123;Initial Catalog=notkayıt;Integrated Security=True");
        private void OGRENCİDETAY_Load(object sender, EventArgs e)
        {
            lblnum.Text = numara;

            baglantı.Open();
            SqlCommand komut = new SqlCommand("select *From ogrenciler where OGRNUMARA=@P1",baglantı);
            komut.Parameters.AddWithValue("@P1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblad.Text = dr[2].ToString() + " " + dr[3].ToString();
                lblsınav1.Text = dr[4].ToString();
                lblsınav2.Text = dr[5].ToString();
                lblsınav3.Text = dr[6].ToString();
                lblortalama.Text = dr[7].ToString();

                lbldurum.Text = dr[8].ToString();
                if (lbldurum.Text=="True")
                {
                    lbldurum.Text = "Geçtiniz";
                }
                else if (lbldurum.Text == "False")
                {
                    lbldurum.Text = "Kaldınız";
                }
            }
            baglantı.Close();
        }

        
    }
}
