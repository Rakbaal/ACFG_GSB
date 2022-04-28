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
        Praticien praticienChoisi = new Praticien();

        public AjoutAvis(Medicament medicament)
        {
            InitializeComponent();
            medicamentChoisi = medicament;
            lblDisplay.Content = "Praticien :";
            ActualiserCbBoxFromMedicament();
        }

        public AjoutAvis(Praticien praticien)
        {
            InitializeComponent();
            praticienChoisi = praticien;
            lblDisplay.Content = "Médicament :";
            ActualiserCbBoxFromPraticien();
        }

        private void ActualiserCbBoxFromPraticien()
        {
            List<Medicament> listeMedicaments = Requetes.PS_LISTE_MEDICAMENT();
            if (listeMedicaments != null)
            {
                cbBoxDisplay.ItemsSource = listeMedicaments;
                cbBoxDisplay.DisplayMemberPath = "MED_NOM_COMMERCIAL";
            }
        }

        private void ActualiserCbBoxFromMedicament()
        {
            List<Praticien> listePraticiens = Requetes.PS_LISTE_PRATICIENS();
            if (listePraticiens != null)
            {
                cbBoxDisplay.ItemsSource = listePraticiens;
                cbBoxDisplay.DisplayMemberPath = "PRA_NOMCOMPLET";
            }
        }
        

    private void Btn_AjoutAvis_Click(object sender, RoutedEventArgs e)
        {
            Avis NouveauAvis = new Avis();
            NouveauAvis.AVI_DATE = DateTime.Parse(DP_AVI_DATE.Text);
            NouveauAvis.AVI_COMMENTAIRE = TBX_Commentaire.Text;

            if (medicamentChoisi.MED_NOM_COMMERCIAL != null)
            {
                NouveauAvis.medicament = medicamentChoisi;
                NouveauAvis.praticien = (Praticien)cbBoxDisplay.SelectedValue;
            } else
            {
                NouveauAvis.medicament = (Medicament)cbBoxDisplay.SelectedValue;
                NouveauAvis.praticien = praticienChoisi;
            }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
