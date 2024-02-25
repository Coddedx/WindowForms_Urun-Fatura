using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje1
{
    public partial class frmAnaForm : Form
    {
        public frmAnaForm()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        private void ürünlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUrunler urunler = new frmUrunler();
                urunler.WindowState = FormWindowState.Maximized;
                urunler.MdiParent = frmUrunler.ActiveForm;
                urunler.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            
        }

        private void faturaEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFaturaEkle fatura = new frmFaturaEkle();
            fatura.WindowState = FormWindowState.Maximized;
            fatura.MdiParent = frmUrunler.ActiveForm;
            fatura.Show();
        }

        private void raporToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRapor rapor = new frmRapor();
            rapor.WindowState = FormWindowState.Maximized;
            rapor.MdiParent = frmUrunler.ActiveForm;
            rapor.Show();
        }
    }
}
