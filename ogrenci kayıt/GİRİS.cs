using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ogrenci_kayıt
{
    public partial class FormOgrenci : Form
    {
        public FormOgrenci()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ogretmen frm = new ogretmen();
            if (maskedTextBox2.Text.ToString()=="1234")
            {
                frm.Show();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OGRENCİDETAY frm = new OGRENCİDETAY();
            frm.numara = maskedTextBox1.Text;
            frm.Show();
        }
    }
}
