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
    public partial class frmRapor : Form
    {
        public frmRapor()
        {
            InitializeComponent();
        }

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adpRapor = new SqlDataAdapter("SELECT UR.URUNAD," +
                "[ALIŞ MİKTARI] = (SELECT SUM(MIKTAR)FROM FATURA_DETAY FDD LEFT OUTER JOIN FATURA_UST FUU ON FDD.FATID=FUU.ID LEFT OUTER JOIN URUNLER URR ON FDD.URUNID=URR.ID WHERE FUU.FATURATIP='Alış Faturası' AND URR.ID=UR.ID GROUP BY URR.URUNAD)," +

                "[SATIŞ MİKTARI] = (SELECT SUM(MIKTAR)FROM FATURA_DETAY FDD LEFT OUTER JOIN FATURA_UST FUU ON FDD.FATID=FUU.ID LEFT OUTER JOIN URUNLER URR ON FDD.URUNID=URR.ID WHERE FUU.FATURATIP='Satış Faturası' AND URR.ID=UR.ID GROUP BY URR.URUNAD)," +
                
                "[STOK MİKTARI]= (SELECT SUM(MIKTAR)FROM FATURA_DETAY FDD LEFT OUTER JOIN FATURA_UST FUU ON FDD.FATID=FUU.ID LEFT OUTER JOIN URUNLER URR ON FDD.URUNID=URR.ID WHERE FUU.FATURATIP='Alış Faturası' AND URR.ID=UR.ID GROUP BY URR.URUNAD) " +
                "-(SELECT SUM(MIKTAR)FROM FATURA_DETAY FDD LEFT OUTER JOIN FATURA_UST FUU ON FDD.FATID=FUU.ID LEFT OUTER JOIN URUNLER URR ON FDD.URUNID=URR.ID WHERE FUU.FATURATIP='Satış Faturası' AND URR.ID=UR.ID GROUP BY URR.URUNAD), " +
                
                "[ALIŞ TUTARI] = (SELECT SUM(FDD.TUTAR)FROM FATURA_DETAY FDD LEFT OUTER JOIN FATURA_UST FUU ON FDD.FATID=FUU.ID LEFT OUTER JOIN URUNLER URR ON FDD.URUNID=URR.ID WHERE FUU.FATURATIP='Alış Faturası' AND FDD.URUNID=UR.ID GROUP BY FDD.URUNID)," +
                
                "[SATIŞ TUTARI] = (SELECT SUM(FDD.TUTAR)FROM FATURA_DETAY FDD LEFT OUTER JOIN FATURA_UST FUU ON FDD.FATID=FUU.ID LEFT OUTER JOIN URUNLER URR ON FDD.URUNID=URR.ID WHERE FUU.FATURATIP='Satış Faturası' AND FDD.URUNID=UR.ID GROUP BY FDD.URUNID) " +
               
                "FROM FATURA_UST FU LEFT OUTER JOIN FATURA_DETAY FD ON FU.ID=FD.FATID " +
                "LEFT OUTER JOIN URUNLER UR ON UR.ID=FD.URUNID " +
                "WHERE FU.TARIH >= '" + Convert.ToDateTime(dtBaslangic.Text).ToString("yyyy-MM-dd HH:mm:ss") + "'AND FU.TARIH<= '"
                + Convert.ToDateTime(dtBitis.Text).ToString("yyyy-MM-dd HH:mm:ss") +
                "'GROUP BY UR.ID,UR.URUNAD,FU.TARIH ", baglanti.bag);
            DataTable tblRapor = new DataTable();
            adpRapor.Fill(tblRapor);
            this.grdRapor.DataSource = tblRapor;           
        }
    }
}
