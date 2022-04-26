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
using System.Windows.Threading;

namespace ACFG_LaboGSB
{
    /// <summary>
    /// Logique d'interaction pour AjoutAvis.xaml
    /// </summary>
    public partial class AjoutAvis : Window
    {
        Medicament medicamentChoisi = new Medicament();
        public AjoutAvis(Medicament medicament)
        {
            InitializeComponent();
            medicamentChoisi = medicament;
        }

        private void Btn_AjoutAvis_Click(object sender, RoutedEventArgs e)
        {
            Avis NouveauAvis = new Avis();
            NouveauAvis.AVI_DATE = DateTime.Parse(this.DP_AVI_Date.Text);
            NouveauAvis.AVI_COMMENTAIRE = this.TBX_Commentaire.Text;
            NouveauAvis.medicament = medicamentChoisi;

            Requetes.PS_CREATE_AVIS(NouveauAvis);

            this.DP_AVI_Date.Text = "";
            this.TBX_Commentaire.Text = "";
        }
    }
}
