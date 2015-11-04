using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrientatietestS21m
{
    public partial class Form1 : Form
    {
        private Administratie adm;

        public Form1()
        {
            InitializeComponent();
            adm = new Administratie();
        }

        private void btnNieuweVerhuringToevoegen_Click(object sender, EventArgs e)
        {
            //TODO: check voor dubbele waardes
            if (cbNieuweVerhuring.SelectedIndex == 0)
            {
                adm.VoegToe(new Feestzaal(dtpNieuweVerhuringTijdstip.Value, Convert.ToInt32(nudNieuweVerhuringUren.Value)));
            }
            if (cbNieuweVerhuring.SelectedIndex == 1)
            {
                adm.VoegToe(new Herberg(dtpNieuweVerhuringTijdstip.Value, Convert.ToInt32(nudNieuweVerhuringUren.Value)));
            }
            if (cbNieuweVerhuring.SelectedIndex == 2)
            {
                adm.VoegToe(new Binnenzaal(dtpNieuweVerhuringTijdstip.Value, Convert.ToInt32(nudNieuweVerhuringUren.Value)));
            }
            if (cbNieuweVerhuring.SelectedIndex != -1)
            {
                foreach (Verhuur v in adm.ListVerhuren)
                {
                    if (!lbVerhuringen.Items.Contains(v.ToString()))
                    {
                        lbVerhuringen.Items.Add(v.ToString());
                    }
                }
            }
        }

        private void btnNieuweVerkoopToevoegen_Click(object sender, EventArgs e)
        {
            //TODO check voor dubbele waardes
            if (cbNieuweVerkoop.SelectedIndex == 0)
            {
                adm.VoegToe(new Sterkedrank(Convert.ToInt32(nudNieuweVerkoopAantal.Value)));
            }
            if (cbNieuweVerkoop.SelectedIndex == 1)
            {
                adm.VoegToe(new SoftDrank(Convert.ToInt32(nudNieuweVerkoopAantal.Value)));
            }
            if (cbNieuweVerkoop.SelectedIndex == 2)
            {
                adm.VoegToe(new Thee(Convert.ToInt32(nudNieuweVerkoopAantal.Value)));
            }
            if (cbNieuweVerkoop.SelectedIndex != -1)
            {
                foreach (Verkoop v in adm.ListVerkopen)
                {
                    if (!lbVerkopen.Items.Contains(v.ToString()))
                    {
                        lbVerkopen.Items.Add(v.ToString());
                    }
                }
            }
        }

        private void btnOverzichtDatumbereik_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            foreach (Verhuur i in adm.Overzicht(dtpOverzichtVan.Value, dtpOverzichtTot.Value))
            {
                message += i.ToString() + Environment.NewLine;
            }
            MessageBox.Show(message);
        }

        private void btnOverzichtExporteer_Click(object sender, EventArgs e)
        {

        }
    }
}
