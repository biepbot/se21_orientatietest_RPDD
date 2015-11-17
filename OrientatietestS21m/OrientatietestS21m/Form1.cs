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

            //Laad in alle verhuur klassen
            var baseType = typeof(Verhuur);
            var assembly = baseType.Assembly;
            cbNieuweVerhuring.DataSource = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).Select(c => c.Name).ToList();

            //Laad in alle verkoop klassen
            baseType = typeof(Verkoop);
            assembly = baseType.Assembly;
            cbNieuweVerkoop.DataSource = assembly.GetTypes().Where(t => t.IsSubclassOf(baseType)).Select(c => c.Name).ToList();
        }

        private void btnNieuweVerhuringToevoegen_Click(object sender, EventArgs e)
        {
            if (cbNieuweVerhuring.SelectedIndex != -1)
            {
                lbVerhuringen.Items.Add(
                    adm.VoegToe(cbNieuweVerhuring.Text, Convert.ToInt32(nudNieuweVerhuringUren.Value), dtpNieuweVerhuringTijdstip.Value));
            }
        }

        private void btnNieuweVerkoopToevoegen_Click(object sender, EventArgs e)
        {
            if (cbNieuweVerhuring.SelectedIndex != -1)
            {
                lbVerkopen.Items.Add(
                    adm.VoegToe(cbNieuweVerkoop.Text, Convert.ToInt32(nudNieuweVerkoopAantal.Value)));
            }
        }

        private void btnOverzichtDatumbereik_Click(object sender, EventArgs e)
        {
            //Maak een lijstje aan van de verhuren, en laat deze zien
            string message = "Verhuren:" + Environment.NewLine;

            foreach (Verhuur i in adm.Overzicht(dtpOverzichtVan.Value, dtpOverzichtTot.Value, false))
            {
                message += i.ToString() + Environment.NewLine;
            }
            message += "Geen verdere verhuren gevonden." + Environment.NewLine;
            message += "-------------------------------------------------------" + Environment.NewLine;
            //Maak een lijstje aan van de verkopen, en laat deze zien
            message += "Verkopen" + Environment.NewLine;
            foreach (Verkoop i in adm.Overzicht(dtpOverzichtVan.Value, dtpOverzichtTot.Value, true))
            {
                message += i.ToString() + Environment.NewLine;
            }
            message += "Geen verdere verkopen gevonden." + Environment.NewLine;

            MessageBox.Show(message);
        }

        private void btnOverzichtExporteer_Click(object sender, EventArgs e)
        {
            //Open een savefiledialog en laat men kiezen waar ze willen opslaan.
            //In het geval dat er geen BTW wordt gekozen, is deze ongespecificeerd.
            BTWTarief btw = BTWTarief.Ongespecificeerd;
            SaveFileDialog Opslaan = new SaveFileDialog();
            Opslaan.DefaultExt = ".txt";
            Opslaan.Filter = "Text file (*.txt)|*.txt|Alle bestanden|*.*";
            if (Opslaan.ShowDialog() == DialogResult.OK)
            {
                string errormessage = string.Empty;
                Enum.TryParse(cbOverzichtBTW.Text, out btw);
                if (adm.Exporteer(Opslaan.FileName, btw, out errormessage))
                {
                    MessageBox.Show(errormessage);
                }
            }
        }
    }
}
