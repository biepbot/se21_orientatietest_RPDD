using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Kleding : Verkoop
    {
        private static BTWTarief BTWtarief = BTWTarief.Laag;
        private decimal prijs = 49.99M;

        override public BTWTarief BTWTarief
        {
            get
            {
                return BTWtarief;
            }
        }
        override public decimal Prijs
        {
            get
            {
                return prijs;
            }
        }

        public Kleding(int aantal)
            : base(aantal)
        {

        }

        public override string ToString()
        {
            return "Kleding: " + base.ToString();
        }
    }
}
