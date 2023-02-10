using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //sql k�t�phanesini ekledim


namespace OrnekKayitUygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //sql ba�lant�s�n� olu�turdum => Initial catalog: veri taban� ad�
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-66BEJ8D\\SQLEXPRESS;Initial Catalog=Kayitlar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
         //verileri g�ster
        private void verilerig�ster()
        {
            listView1.Items.Clear();
            baglan.Open();
            //sql ile c# � birle�tirdim ve veri taban�mdaki tablomu(gelenler) getirdim
            SqlCommand komut = new SqlCommand("Select*From Gelenler", baglan);
            SqlDataReader oku = komut.ExecuteReader();//sql verilerimi okutturdum ve execute reader ile veri taban�mdaki verilerin ak���n� sa�lad�m.


            //her okudu�unu listviewe ekletelim

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["Sgkkod"].ToString();
                ekle.SubItems.Add(oku["Tckn"].ToString());
                ekle.SubItems.Add(oku["Soyad"].ToString());
                ekle.SubItems.Add(oku["Ad"].ToString());
                ekle.SubItems.Add(oku["Babaadi"].ToString());
                ekle.SubItems.Add(oku["Tutar"].ToString());




                listView1.Items.Add(ekle);

            }
            baglan.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            verilerig�ster();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //kaydet i�lemi
        private void button2_Click(object sender, EventArgs e)
        {
            //girilen veriler veri taban�na insert ettim
            baglan.Open();
            SqlCommand komut = new SqlCommand("Insert into Gelenler (Sgkkod, Tckn, Soyad, Ad, Babaadi, Tutar) Values ('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "', '" + textBox3.Text.ToString() + "', '" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "')", baglan);

            komut.ExecuteNonQuery();//ExecuteNonQuery ; kay�t almada kullan�lan , kendisine parametre y�kleyerek geri d�nd�r�r
            baglan.Close();
            verilerig�ster();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();   
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear(); 
        }
        int sgkkod=0;

        // S�L ��LEM�� SGK KODUNA G�RE YAPTIM
        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Delete From Gelenler where sgkkod=(" + sgkkod + ")", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerig�ster();
        }
        //��FT TIKLAMA �LE TEXTBOXLARA VER�LER� YAZDIRDIK
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            sgkkod = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[5].Text;
             
        }
        //G�NCELLEME ��LEM�
        private void button4_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Update Gelenler set sgkkod='"+textBox1.Text.ToString()+"',Tckn='" + textBox2.Text.ToString()+"',Soyad='"+ textBox3.Text.ToString() +"',Ad='"+ textBox4.Text.ToString()+"' ,Babaadi='"+textBox5.Text.ToString()+"',Tutar='"+ textBox6.Text.ToString() + "' where sgkkod = " + sgkkod + "",baglan); 

            komut.ExecuteNonQuery();
            baglan.Close();
            verilerig�ster();
        }
    }
}