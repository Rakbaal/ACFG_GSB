using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACFG_LaboGSB.Classes
{
    class Avis
    {
        public int AVI_ID { get; set; }
        public DateTime AVI_DATE { get; set; }
        public string AVI_COMMENTAIRE { get; set; }
        public Medicament medicament { get; set; }
        public Praticien praticien { get; set; }
    }
}
