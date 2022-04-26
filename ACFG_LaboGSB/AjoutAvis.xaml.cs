using ACFG_LaboGSB.Classes;
using ACFG_LaboGSB.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ACFG_LaboGSB
{
    /// <summary>
    /// Logique d'interaction pour AjoutAvis.xaml
    /// </summary>
    public partial class AjoutAvis : Window
    {
        public AjoutAvis()
        {
            InitializeComponent();
            ActualiserCbBox();
        }

        private void ActualiserCbBox()
        {
            List<Praticien> listePraticiens = Requetes.PS_LISTE_PRATICIENS();
            if (listePraticiens != null)
            {
                cbBoxPraticien.ItemsSource = listePraticiens;
                cbBoxPraticien.DisplayMemberPath = "PRA_NOMCOMPLET";
            }
        }
    }
}
