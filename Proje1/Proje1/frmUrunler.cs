using Proje1.Class;
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

namespace Proje1
{
    public partial class frmUrunler : Form
    {
        public frmUrunler()
        {
            InitializeComponent();
        }

        private void frmUrunler_Load(object sender, EventArgs e)
        {
           
            urunlistele();
        }

        private void urunlistele()
        {
            SqlDataAdapter adpListe = new SqlDataAdapter("SELECT * FROM URUNLER", baglanti.bag);
            DataTable tblListe = new DataTable();
            adpListe.Fill(tblListe);
            this.grdUrunler.DataSource = tblListe;
            this.grdUrunler.Columns[0].Visible = false;
            this.grdUrunler.Columns[0].Width = 300;
            this.grdUrunler.Columns[1].Width = 500;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            SqlCommand komutekle = new SqlCommand("INSERT INTO URUNLER(URUNAD,ALISFIYAT,SATISFIYAT) VALUES (@URUNAD,@ALISFIYAT,@SATISFIYAT)", baglanti.bag);
           
            komutekle.Parameters.AddWithValue("@URUNAD", txtUrunAdi.Text);
            komutekle.Parameters.AddWithValue("@ALISFIYAT", txtAlisFiyati.Text);
            komutekle.Parameters.AddWithValue("@SATISFIYAT", txtSatisFiyati.Text);
            if (baglanti.bag.State == ConnectionState.Closed)
            {

                baglanti.bag.Open();
            }
            komutekle.ExecuteNonQuery();
            if (baglanti.bag.State == ConnectionState.Open)
            {

                baglanti.bag.Close();
            }
            MessageBox.Show("Ürün Eklenmiştir.");
            urunlistele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult mesaj = new DialogResult();
            mesaj = MessageBox.Show("Seçmiş olduğunuz kayıt silinecektir. Devam edilsin mi?",
                "Uyarı", MessageBoxButtons.YesNo);
            if (mesaj == DialogResult.Yes)
            {
                SqlCommand cmdUrunSil = new SqlCommand("DELETE FROM URUNLER WHERE ID='" + grdUrunler.CurrentRow.Cells[0].Value.ToString() + "'", baglanti.bag);
                if (baglanti.bag.State == ConnectionState.Closed)
                {
                    baglanti.bag.Open();
                }
                cmdUrunSil.ExecuteNonQuery();
                if (baglanti.bag.State == ConnectionState.Open)
                {
                    baglanti.bag.Close();
                }
                urunlistele();
            }
        }

        private void grdUrunler_DoubleClick(object sender, EventArgs e)
        {
            this.txtUrunAdi.Text = grdUrunler.CurrentRow.Cells[1].Value.ToString();
            this.txtAlisFiyati.Text = grdUrunler.CurrentRow.Cells[2].Value.ToString();
            this.txtSatisFiyati.Text = grdUrunler.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand urunguncel = new SqlCommand("UPDATE URUNLER SET URUNAD=@URUNAD, ALISFIYAT=@ALISFIYAT ,SATISFIYAT=@SATISFIYAT WHERE ID='" + grdUrunler.CurrentRow.Cells[0].Value.ToString() + "'", baglanti.bag);
            urunguncel.Parameters.AddWithValue("@URUNAD", txtUrunAdi.Text);
            urunguncel.Parameters.AddWithValue("@ALISFIYAT", txtAlisFiyati.Text);
            urunguncel.Parameters.AddWithValue("@SATISFIYAT", txtSatisFiyati.Text);
           
            if (baglanti.bag.State == ConnectionState.Closed)
            {

                baglanti.bag.Open();
            }
            urunguncel.ExecuteNonQuery();
            if (baglanti.bag.State == ConnectionState.Open)
            {

                baglanti.bag.Close();
            }
            MessageBox.Show("Ürün Güncellemesi Yapıldı.");
            urunlistele();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            foreach (frmFaturaEkle fr in Application.OpenForms.OfType<frmFaturaEkle>())
            {

               
                fr.txtUrunAdi.Text = (grdUrunler.CurrentRow.Cells[1].Value.ToString());
                fr.txtAlisFiyati.Text = (grdUrunler.CurrentRow.Cells[2].Value.ToString());
                fr.txtSatisFiyati.Text = (grdUrunler.CurrentRow.Cells[3].Value.ToString());

            }
            this.Close();
        }
    }
}
