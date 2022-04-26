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

        private void Btn_AjoutAvis_Click(object sender, RoutedEventArgs e)
        {
            Avis NouveauAvis = new Avis();
            NouveauAvis.AVI_DATE = DateTime.Parse(DP_AVI_DATE.Text);
            NouveauAvis.AVI_COMMENTAIRE = TBX_Commentaire.Text;
            NouveauAvis.medicament = medicamentChoisi;
            NouveauAvis.praticien = (Praticien)cbBoxPraticien.SelectedValue;

            try
            {
                Requetes.PS_CREATE_AVIS(NouveauAvis);
                MessageBox.Show($"Création de l'avis terminée");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }
    }
}
