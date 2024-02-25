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
    public partial class frmFaturaEkle : Form
    {
        
          
        public frmFaturaEkle()
        {
            InitializeComponent();
        }
        int silme = 0;
        DataTable tbl;
        double AlisToplamtutar = 0;
        double SatisToplamtutar = 0;
        int sayac = 1;
       int maxFatNo=0;

    
        private void btnUrunAdi_Click(object sender, EventArgs e)
        {
            frmUrunler urun = new frmUrunler();
            urun.StartPosition = FormStartPosition.CenterScreen;
            urun.MdiParent = frmUrunler.ActiveForm;
            urun.Show();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            maxFatNo++;
            txtfaturano.Text = Convert.ToString(maxFatNo);
            
            tbl.Rows.Add();
            tbl.Rows[tbl.Rows.Count - 1][0] = txtUrunAdi.Text;
            if (cmbFaturaTipi.SelectedIndex == 0)
            {
                tbl.Rows[tbl.Rows.Count - 1][1] = "Satış Faturası";
            }
            else
            {
                tbl.Rows[tbl.Rows.Count - 1][1] = "Alış Faturası";
            }

            if (cmbFaturaTipi.SelectedIndex == 0)
            {
                tbl.Rows[tbl.Rows.Count - 1][2] = txtSatisFiyati.Text ;
            }
            else if(cmbFaturaTipi.SelectedIndex == 1)
            {
                tbl.Rows[tbl.Rows.Count - 1][2] = txtAlisFiyati.Text;
            }
            tbl.Rows[tbl.Rows.Count - 1][3] = txtMiktar.Text;

            if (cmbFaturaTipi.SelectedIndex==0)
            {
                tbl.Rows[tbl.Rows.Count - 1][4] = Convert.ToDouble(txtMiktar.Text) * Convert.ToDouble(txtSatisFiyati.Text);

            }
            else 
            {
                tbl.Rows[tbl.Rows.Count - 1][4] = Convert.ToDouble(txtMiktar.Text) * Convert.ToDouble(txtAlisFiyati.Text);

            }

            tbl.Rows[tbl.Rows.Count - 1][5] = Convert.ToString(maxFatNo);
            tbl.Rows[tbl.Rows.Count - 1][6] = dateTimePicker1.Text;
            grdFaturaDetay.DataSource = tbl;

            TutarToplam();

            grdFaturaDetay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdFaturaDetay.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void TutarToplam()
        {
            SatisToplamtutar = 0;
            AlisToplamtutar = 0;
            for (int i = 0; i < grdFaturaDetay.Rows.Count; i++)
            {
                
                if (tbl.Rows[i][1] == "Satış Faturası")
                {
                    SatisToplamtutar += Convert.ToDouble(grdFaturaDetay.Rows[i].Cells[4].Value);
                    lblSatisToplam.Text = SatisToplamtutar.ToString();
                }
                else if (tbl.Rows[i][1] == "Alış Faturası")
                {
                    AlisToplamtutar += Convert.ToDouble(grdFaturaDetay.Rows[i].Cells[4].Value);
                    lblAlisToplam.Text = AlisToplamtutar.ToString();
                }
            }

        }


        private void frmFaturaEkle_Load(object sender, EventArgs e)
        {
            tbl = new DataTable();

            tbl.Columns.Add("Ürün Adı");
            tbl.Columns.Add("Fatura Tip");
            tbl.Columns.Add("Birim Fiyat");
            tbl.Columns.Add("Miktar");
            tbl.Columns.Add("Tutar");          
            tbl.Columns.Add("Fatura No");          
            tbl.Columns.Add("Tarih");

            grdFaturaDetay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdFaturaDetay.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
           
            MaxFaturaNo();
        }



        private void btnKaydet_Click(object sender, EventArgs e)
        {
            #region fatura üst kısım alış fat
            int sayacA = 0;
            for (int i = 0; i < grdFaturaDetay.Rows.Count; i++)
            {
                if (tbl.Rows[i][1] == "Alış Faturası" && sayacA==0 )
                {
                    sayacA++;
                    SqlCommand cmdUstKaydetA = new SqlCommand("INSERT INTO FATURA_UST (FATURANO,TARIH,FATURATIP) VALUES (@FATURANO,@TARIH,@FATURATIP)", baglanti.bag);
                    cmdUstKaydetA.Parameters.AddWithValue("@FATURANO", "111");  //tblMaxFatNo.Rows[0][0].ToString()
                    cmdUstKaydetA.Parameters.AddWithValue("@TARIH", Convert.ToDateTime(dateTimePicker1.Text));
                    cmdUstKaydetA.Parameters.AddWithValue("@FATURATIP", "Alış Faturası");
                    if (baglanti.bag.State == ConnectionState.Closed)
                    {
                        baglanti.bag.Open();
                    }
                    cmdUstKaydetA.ExecuteNonQuery();
                    if (baglanti.bag.State == ConnectionState.Open)
                    {
                        baglanti.bag.Close();
                    }
                }
            }
            #endregion


            #region fatura üst kısım Satış fat
            int sayacS = 0;
            for (int i = 0; i < grdFaturaDetay.Rows.Count; i++)
            {
                if (tbl.Rows[i][1] == "Satış Faturası" && sayacS==0)
                {
                    sayacS++;
                    SqlCommand cmdUstKaydetS = new SqlCommand("INSERT INTO FATURA_UST (FATURANO,TARIH,FATURATIP) VALUES (@FATURANO,@TARIH,@FATURATIP)", baglanti.bag);
                    cmdUstKaydetS.Parameters.AddWithValue("@FATURANO", "222");  //tblMaxFatNo.Rows[0][0].ToString()
                    cmdUstKaydetS.Parameters.AddWithValue("@TARIH", Convert.ToDateTime(dateTimePicker1.Text));
                    cmdUstKaydetS.Parameters.AddWithValue("@FATURATIP", "Satış Faturası");

                    if (baglanti.bag.State == ConnectionState.Closed)
                    {
                        baglanti.bag.Open();
                    }
                    cmdUstKaydetS.ExecuteNonQuery();
                    if (baglanti.bag.State == ConnectionState.Open)
                    {
                        baglanti.bag.Close();
                    }
                }
            }
            #endregion



            #region fatura ıd  ALIŞ
            SqlDataAdapter adpFatIdA = new SqlDataAdapter("SELECT MAX (ID) FROM FATURA_UST WHERE FATURATIP='Alış Faturası'", baglanti.bag);
            DataTable tblFatIdA = new DataTable();
            adpFatIdA.Fill(tblFatIdA);
            #endregion            
            
            #region fatura ıd  SATIŞ
            SqlDataAdapter adpFatIdS = new SqlDataAdapter("SELECT MAX (ID) FROM FATURA_UST WHERE FATURATIP='Satış Faturası'", baglanti.bag);
            DataTable tblFatIdS = new DataTable();
            adpFatIdS.Fill(tblFatIdS);
            #endregion

            #region fatura detay 
            for (int i = 0; i < grdFaturaDetay.Rows.Count; i++)
            {
                #region ürün ıd bulunuyor
                SqlDataAdapter urunId = new SqlDataAdapter("SELECT ID FROM URUNLER WHERE URUNAD ='" + grdFaturaDetay.Rows[i].Cells[0].Value.ToString() + "'", baglanti.bag);
                DataTable tblUrunId = new DataTable();
                urunId.Fill(tblUrunId);
                #endregion

                //alış faturası için
                if (tbl.Rows[i][1] == "Alış Faturası") 
                {
                    SqlCommand cmdDetayA = new SqlCommand("INSERT INTO FATURA_DETAY (FATID,URUNID,MIKTAR,TUTAR) VALUES (@FATID,@URUNID,@MIKTAR,@TUTAR) ", baglanti.bag);
                    cmdDetayA.Parameters.AddWithValue("@FATID", tblFatIdA.Rows[0][0].ToString());
                    cmdDetayA.Parameters.AddWithValue("@URUNID", tblUrunId.Rows[0][0].ToString());
                    cmdDetayA.Parameters.AddWithValue("@MIKTAR", Convert.ToDouble(grdFaturaDetay.Rows[i].Cells[3].Value));
                    cmdDetayA.Parameters.AddWithValue("@TUTAR", Convert.ToDouble(grdFaturaDetay.Rows[i].Cells[4].Value));

                    if (baglanti.bag.State == ConnectionState.Closed)
                    {
                        baglanti.bag.Open();
                    }
                    cmdDetayA.ExecuteNonQuery();
                    if (baglanti.bag.State == ConnectionState.Open)
                    {
                        baglanti.bag.Close();
                    }
                }

                if (tbl.Rows[i][1] == "Satış Faturası")
                {
                    SqlCommand cmdDetayS = new SqlCommand("INSERT INTO FATURA_DETAY (FATID,URUNID,MIKTAR,TUTAR) VALUES (@FATID,@URUNID,@MIKTAR,@TUTAR) ", baglanti.bag);
                    cmdDetayS.Parameters.AddWithValue("@FATID", tblFatIdS.Rows[0][0].ToString());
                    cmdDetayS.Parameters.AddWithValue("@URUNID", tblUrunId.Rows[0][0].ToString());
                    cmdDetayS.Parameters.AddWithValue("@MIKTAR", Convert.ToDouble(grdFaturaDetay.Rows[i].Cells[3].Value));
                    cmdDetayS.Parameters.AddWithValue("@TUTAR", Convert.ToDouble(grdFaturaDetay.Rows[i].Cells[4].Value));

                    if (baglanti.bag.State == ConnectionState.Closed)
                    {
                        baglanti.bag.Open();
                    }
                    cmdDetayS.ExecuteNonQuery();
                    if (baglanti.bag.State == ConnectionState.Open)
                    {
                        baglanti.bag.Close();
                    }
                }
            }
            #endregion

            MessageBox.Show("FATURA KAYDEDİLMİŞTİR");
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                silme = grdFaturaDetay.CurrentRow.Index;
                tbl.Rows[silme].Delete();
                TutarToplam();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(ToString());
            }
        }

        private void MaxFaturaNo()
        {
            #region max fat no
            baglanti.bag.Open();
            SqlDataAdapter adpMaxFatNo = new SqlDataAdapter("SELECT MAX (FATURANO) FROM FATURA_UST ", baglanti.bag);
            DataTable tblMaxFatNo = new DataTable();
            adpMaxFatNo.Fill(tblMaxFatNo);
            baglanti.bag.Close();
            #endregion

            txtfaturano.Text = Convert.ToString(Convert.ToInt32(tblMaxFatNo.Rows[0][0].ToString()) + sayac);
            maxFatNo = Convert.ToInt32(txtfaturano.Text);
        }
   
    }
}
