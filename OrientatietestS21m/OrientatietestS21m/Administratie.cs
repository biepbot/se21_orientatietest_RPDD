using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Administratie
    {
        public List<IInkomsten> ListAankopen;

        public Administratie()
        {
            ListAankopen = new List<IInkomsten>();
        }

        public void VoegToe(Verhuur verhuur)
        {
            ListAankopen.Add(verhuur);
        }

        public void VoegToe(Verkoop verkoop)
        {
            ListAankopen.Add(verkoop);
        }

        /// <summary>
        /// Van een lijst van Verhuren/verkopen: vind alles tussen die twee tijdstippen en sorteer deze op tijd
        /// </summary>
        /// <param name="van">De tijd vanaf</param>
        /// <param name="tot">De tijd tot</param>
        /// <returns>Een lijst van alle iinkomsten</returns>
        public List<IInkomsten> Overzicht(DateTime van, DateTime tot, bool isverkoop)
        {
            return !isverkoop ?
                (ListAankopen.FindAll(v => v is Verhuur && v.Tijdstip > van && v.Tijdstip.AddHours(((Verhuur)v).UrenVerhuurd) < tot)).OrderByDescending(v => v.Tijdstip).ToList() :
                (ListAankopen.FindAll(v => v is Verkoop && v.Tijdstip > van && v.Tijdstip < tot)).OrderByDescending(v => v.Tijdstip).ToList();
        }

        /// <summary>
        /// Retourneert een lijst van alle aankopen, gesorteerd op tijd
        /// </summary>
        /// <returns></returns>
        public List<IInkomsten> AllOverzicht()
        {
            return ListAankopen.OrderByDescending(v => v.Tijdstip).ToList();
        }

        /// <summary>
        /// Retourneert een lijst van of alle verhuren, of alle verkopen
        /// </summary>
        /// <param name="isverkoop"></param>
        /// <returns></returns>
        public List<IInkomsten> SpecifiekOverzicht(bool isverkoop)
        {
            return isverkoop ? ListAankopen.FindAll(aa => aa is Verkoop) : ListAankopen.FindAll(aa => aa is Verhuur);
        }

        /// <summary>
        /// Retourneert een overzicht van de geselecteerde btw tarieven. Indien ongespecifieerd, retourneer alles.
        /// </summary>
        /// <param name="tarief"></param>
        /// <returns></returns>
        public List<IInkomsten> Overzicht(BTWTarief tarief)
        {
            return tarief != BTWTarief.Ongespecificeerd ?
                ListAankopen.FindAll(i => i.BTWTarief == tarief).OrderByDescending(i => i.Tijdstip).ToList() :
                ListAankopen;
        }

        /// <summary>
        /// Exporteert een file
        /// </summary>
        /// <param name="path">Het pad van de file</param>
        /// <param name="tarief">Welke tarieven worden geexporteerd</param>
        /// <param name="error">De error mits het fout gaat</param>
        /// <returns>Of het is foutgegaan of niet</returns>
        public bool Exporteer(string path, BTWTarief tarief, out string error)
        {
            error = string.Empty;
            try
            {
                decimal hoog = 0M;
                decimal laag = 0M;
                decimal totaal = 0M;
                List<IInkomsten> tarievenlijst = Overzicht(tarief);
                StreamWriter sw = new StreamWriter(path);
                foreach (IInkomsten i in tarievenlijst)
                {
                    sw.WriteLine(String.Format("{0} - {1}", i.ToString(), i.BTWTarief));
                    if (i.BTWTarief == BTWTarief.Laag)
                    {
                        laag += i.Bedrag;
                    }
                    if (i.BTWTarief == BTWTarief.Hoog)
                    {
                        hoog += i.Bedrag;
                    }
                    totaal += i.Bedrag;
                }
                sw.WriteLine();
                sw.WriteLine("Totaal Laag: EUR " + laag);
                sw.WriteLine("Totaal Hoog: EUR " + hoog);
                sw.WriteLine("Totaal: EUR " + totaal);
                sw.Close();
                return false;
            }
            catch (Exception e)
            {
                error = "Oeps, de volgende error heeft zich voortgedaan: \n" + e.Message;
                return true;
            }
        }
    }
}
