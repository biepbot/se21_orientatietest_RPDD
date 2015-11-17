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
        private decimal HoogBTW = 0M;
        private decimal LaagBTW = 0M;

        public Administratie()
        {
            ListAankopen = new List<IInkomsten>();
        }

        /// <summary>
        /// Voegt een verhuur of verkoop item toe aan de IInkomstenlijst
        /// </summary>
        /// <param name="type">Het type van het item (Feestzaal, SoftDrink, etc)</param>
        /// <param name="amount">De hoeveelheid (uren)</param>
        /// <param name="dtpvalue">Het tijdstip</param>
        public string VoegToe(string type, int amount, Nullable<DateTime> dtpvalue = null)
        {
            //Voeg item toe
            if (dtpvalue != null)
            {
                ListAankopen.Add((Verhuur)Activator.CreateInstance(Type.GetType("OrientatietestS21m." + type), dtpvalue, amount));
            }
            else
            {
                ListAankopen.Add((Verkoop)Activator.CreateInstance(Type.GetType("OrientatietestS21m." + type), amount));
            }

            //Voeg bedrag toe
            IInkomsten lastItem = ListAankopen[ListAankopen.Count-1];
            if (lastItem.BTWTarief == BTWTarief.Hoog)
            {
                HoogBTW += lastItem.Bedrag;
            }
            else
            {
                LaagBTW += lastItem.Bedrag;
            }

            //Retourneer laatste item
            return lastItem.ToString();
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
                StreamWriter sw = new StreamWriter(path);
                Overzicht(tarief).ForEach(i => sw.WriteLine(String.Format("{0} - {1}", i.ToString(), i.BTWTarief)));
                sw.WriteLine();
                sw.WriteLine("Totaal Laag: EUR " + LaagBTW);
                sw.WriteLine("Totaal Hoog: EUR " + HoogBTW);
                sw.WriteLine("Totaal: EUR " + Convert.ToDecimal(LaagBTW + HoogBTW));
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
