﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Sportdrank : Verkoop
    {
        private static BTWTarief BTWtarief = BTWTarief.Laag;
        private decimal prijs = 2.50M;

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

        public Sportdrank(int aantal)
            : base(aantal)
        {

        }

        public override string ToString()
        {
            return "Sportdrank: " + base.ToString();
        }
    }
}
