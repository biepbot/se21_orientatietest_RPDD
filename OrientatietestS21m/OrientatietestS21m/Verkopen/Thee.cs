using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Thee : Verkoop
    {
        private static BTWTarief BTWtarief = BTWTarief.Laag;
        private decimal prijs = 0.50M;

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

        public Thee(int aantal)
            : base(aantal)
        {

        }

        public override string ToString()
        {
            return "Thee: " + base.ToString();
        }
    }
}
