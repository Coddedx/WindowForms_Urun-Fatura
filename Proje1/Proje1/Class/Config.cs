using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Proje1.Class
{
    public class Config
    {
        private static XmlDocument _dosya;
        static Config()

        { _dosya = new XmlDocument(); }

        public static bool DosyaOku()
        {
            _dosya.Load("c:\\piksel_baglanti.xml");
            bool x = false;

            XmlNode alan = SeciliAlan(1);
            if (alan != null)
            {
                ClearConfig();
                Connect.Ayar.ServerName = alan["server"].Attributes["Name"].Value;
                Connect.Ayar.Database = alan["server"].Attributes["Database"].Value;
                Connect.Ayar.SqlUser = alan["server"].Attributes["Uid"].Value;
                Connect.Ayar.SqlPass = alan["server"].Attributes["Password"].Value;
                x = true;
            }
            else
            {
                x = false;
            }
            return x;
        }
        private static void ClearConfig()
        {
            Connect.Ayar.ServerName = string.Empty;
            Connect.Ayar.Database = string.Empty;
            Connect.Ayar.SqlUser = string.Empty;
            Connect.Ayar.SqlPass = string.Empty;


        }
        private static XmlNode SeciliAlan(int id)
        {
            XmlNode secim = _dosya.SelectSingleNode("/ayar/app[@Id=" + id.ToString() + "]");
            return secim;
        }
    }
}

