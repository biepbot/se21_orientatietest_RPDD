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
            //De eerste selectie in deze combobox is de feestzaal, indien deze is geselecteerd, voeg een feestzaal toe
            if (cbNieuweVerhuring.SelectedIndex == 0)
            {
                adm.VoegToe(new Feestzaal(dtpNieuweVerhuringTijdstip.Value, Convert.ToInt32(nudNieuweVerhuringUren.Value)));
            }
            //De tweede selectie in deze combobox is een herberg, indien deze is geselecteerd, voeg een herberg toe
            if (cbNieuweVerhuring.SelectedIndex == 1)
            {
                adm.VoegToe(new Herberg(dtpNieuweVerhuringTijdstip.Value, Convert.ToInt32(nudNieuweVerhuringUren.Value)));
            }
            //De derde selectie in deze combobox is de binnenzaal, indien deze is geselecteerd, voeg een binnenzaal toe
            if (cbNieuweVerhuring.SelectedIndex == 2)
            {
                adm.VoegToe(new Binnenzaal(dtpNieuweVerhuringTijdstip.Value, Convert.ToInt32(nudNieuweVerhuringUren.Value)));
            }
            //Update de listbox
            if (cbNieuweVerhuring.SelectedIndex != -1)
            {
                lbVerhuringen.Items.Clear();

                foreach (Verhuur v in adm.ListVerhuren)
                {
                    lbVerhuringen.Items.Add(v);
                }
            }
        }

        private void btnNieuweVerkoopToevoegen_Click(object sender, EventArgs e)
        {
            //De eerste selectie in deze combobox is een sterke drank, indien deze is geselecteerd, voeg sterke drank toe
            if (cbNieuweVerkoop.SelectedIndex == 0)
            {
                adm.VoegToe(new Sterkedrank(Convert.ToInt32(nudNieuweVerkoopAantal.Value)));
            }
            //De tweede selectie in deze combobox is de softdrink, indien deze is geselecteerd, voeg een soft drink toe
            if (cbNieuweVerkoop.SelectedIndex == 1)
            {
                adm.VoegToe(new SoftDrank(Convert.ToInt32(nudNieuweVerkoopAantal.Value)));
            }
            //De derde selectie in deze combobox is thee, indien deze is geselecteerd, voeg thee toe
            if (cbNieuweVerkoop.SelectedIndex == 2)
            {
                adm.VoegToe(new Thee(Convert.ToInt32(nudNieuweVerkoopAantal.Value)));
            }
            //Update de listbox
            if (cbNieuweVerkoop.SelectedIndex != -1)
            {
                lbVerkopen.Items.Clear();

                foreach (Verkoop v in adm.ListVerkopen)
                {
                    lbVerkopen.Items.Add(v);
                }
            }
        }

        private void btnOverzichtDatumbereik_Click(object sender, EventArgs e)
        {
            //Maak een lijstje aan van de verhuren, en laat deze zien
            string message = string.Empty;
            foreach (Verhuur i in adm.Overzicht(dtpOverzichtVan.Value, dtpOverzichtTot.Value))
            {
                message += i.ToString() + Environment.NewLine;
            }
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
                Enum.TryParse(cbOverzichtBTW.Text, out btw);
                adm.Exporteer(Opslaan.FileName, btw);
            }
        }
    }
}
