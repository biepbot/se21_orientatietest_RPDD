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
        public List<Verkoop> ListVerkopen;
        public List<Verhuur> ListVerhuren;

        public Administratie()
        {
            ListVerhuren = new List<Verhuur>();
            ListVerkopen = new List<Verkoop>();
        }

        public void VoegToe(Verhuur verhuur)
        {
            ListVerhuren.Add(verhuur);
        }

        public void VoegToe(Verkoop verkoop)
        {
            ListVerkopen.Add(verkoop);
        }

        //
        public List<IInkomsten> Overzicht(DateTime van, DateTime tot)
        {
            List<IInkomsten> listSort = new List<IInkomsten>();
            foreach (Verhuur v in ListVerhuren)
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

        public List<IInkomsten> AllOverzicht()
        {
            List<IInkomsten> listSort = new List<IInkomsten>();
            listSort.AddRange(ListVerhuren);
            listSort.AddRange(ListVerkopen);
            listSort = listSort.OrderBy(v => v.Tijdstip).ToList();
            listSort.Reverse();
            return listSort;
        }

        public List<IInkomsten> Overzicht(BTWTarief tarief)
        {
            List<IInkomsten> listSortOud = new List<IInkomsten>();
            listSortOud.AddRange(ListVerhuren);
            listSortOud.AddRange(ListVerkopen);
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

        public void Exporteer(string path, BTWTarief tarief)
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
        }
    }
}
