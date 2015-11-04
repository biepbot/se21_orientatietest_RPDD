using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    class Sterkedrank : Verkoop
    {
        private BTWTarief BTWtarief;
        private decimal prijs;

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
            return base.ToString();
        }
    }
}
