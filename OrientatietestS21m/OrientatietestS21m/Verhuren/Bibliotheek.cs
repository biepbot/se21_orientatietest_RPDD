﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrientatietestS21m
{
    public class Bibliotheek : Verhuur
    {
        private static BTWTarief BTWtarief = BTWTarief.Hoog;
        private static decimal prijsPerUUr = 50.00M;

        override public BTWTarief BTWTarief
        {
            get
            {
                return BTWtarief;
            }
        }
        override public decimal PrijsPerUUr
        {
            get
            {
                return prijsPerUUr;
            }
        }

        public Bibliotheek(DateTime tijdstip, int urenVerhuurd)
            : base(tijdstip, urenVerhuurd)
        {

        }

        public override string ToString()
        {
            return "Bibliotheek: " + base.ToString();
        }
    }
}
