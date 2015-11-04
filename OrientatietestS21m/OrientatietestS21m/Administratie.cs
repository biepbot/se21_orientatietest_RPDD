using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    class Administratie
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
    }
}
