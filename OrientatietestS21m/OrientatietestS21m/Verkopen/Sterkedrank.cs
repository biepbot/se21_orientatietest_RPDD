using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Sterkedrank : Verkoop
    {
        private static BTWTarief BTWtarief = BTWTarief.Hoog;
        private static decimal prijs = 3.00M;

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

        public Sterkedrank(int aantal)
            : base(aantal)
        {

        }

        public override string ToString()
        {
            return "Sterke drank: " + base.ToString();
        }
    }
}
