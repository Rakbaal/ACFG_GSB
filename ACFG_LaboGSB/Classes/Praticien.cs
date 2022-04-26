﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACFG_LaboGSB.Classes
{
    public class Praticien
    {
        public int PRA_ID { get; set; }
        public string PRA_NOM { get; set; }
        public string PRA_PRENOM { get; set; }
        public string PRA_PROFESSION { get; set; }
        public string PRA_NOMCOMPLET => this.PRA_NOM.ToUpper() + " " + this.PRA_PRENOM;    
    }
}
