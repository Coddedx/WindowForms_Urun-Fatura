using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje1.Class
{
    public class baglanti
    {
        public static SqlConnection bag;
        public static bool GirisKontrol = false;
        public static bool KasiyerGirisKontrol = false;
        bool x = Config.DosyaOku();

        static baglanti()
        {
            Config.DosyaOku();

            bag = new SqlConnection("Data source=" + Connect.Ayar.ServerName + ";Connection Timeout=300000000; Initial Catalog="
                + Connect.Ayar.Database + ";User Id=" + Connect.Ayar.SqlUser + ";Password=" + Connect.Ayar.SqlPass + ";Timeout=900");
        }
    }
}
