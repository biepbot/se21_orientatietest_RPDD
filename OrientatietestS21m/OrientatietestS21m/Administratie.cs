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

        //
        public List<IInkomsten> Overzicht(DateTime van, DateTime tot)
        {
            List<IInkomsten> listSort = new List<IInkomsten>();
            foreach (Verhuur v in ListAankopen)
            {
                if (v.Tijdstip > van && v.Tijdstip.AddHours(v.UrenVerhuurd) < tot)
                {
                    listSort.Add(v);
                }
            }
            listSort = listSort.OrderBy(v => v.Tijdstip).ToList();
            listSort.Reverse();
            return listSort;
        }

        public List<IInkomsten> Overzicht(DateTime van, DateTime tot, bool isverkoop)
        {
            List<IInkomsten> listSort = new List<IInkomsten>();
            List<IInkomsten> listFilter = isverkoop ? ListAankopen.FindAll(aa => aa is Verkoop) : ListAankopen.FindAll(aa => aa is Verhuur);
            foreach (IInkomsten v in listFilter)
            {
                DateTime totcheck = isverkoop ? v.Tijdstip : v.Tijdstip.AddHours(((Verhuur)v).UrenVerhuurd);
                if (v.Tijdstip > van && totcheck < tot)
                {
                    listSort.Add(v);
                }
            }
            listSort = listSort.OrderBy(v => v.Tijdstip).ToList();
            listSort.Reverse();
            return listSort;
        }

        public List<IInkomsten> AllOverzicht()
        {
            List<IInkomsten> listSort = new List<IInkomsten>();
            listSort = ListAankopen.OrderBy(v => v.Tijdstip).ToList();
            listSort.Reverse();
            return listSort;
        }

        public List<IInkomsten> SpecifiekOverzicht(bool isverkoop)
        {
            return isverkoop ? ListAankopen.FindAll(aa => aa is Verkoop) : ListAankopen.FindAll(aa => aa is Verhuur);
        }

        public List<IInkomsten> Overzicht(BTWTarief tarief)
        {
            List<IInkomsten> listSortOud = ListAankopen;
            List<IInkomsten> listSort = new List<IInkomsten>();
            if (tarief != BTWTarief.Ongespecificeerd)
            {
                foreach (IInkomsten v in listSortOud)
                {
                    if (v.BTWTarief == tarief)
                    {
                        listSort.Add(v);
                    }
                }
            }
            else
            {
                listSort = listSortOud;
            }
            listSort = listSort.OrderBy(v => v.Tijdstip).ToList();
            listSort.Reverse();
            return listSort;
        }

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
