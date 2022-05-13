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
        DispatcherTimer timer = new DispatcherTimer();
        bool fromMedicament;

        public AjoutAvis(Medicament medicament, bool fromMed)
        {
            InitializeComponent();
            fromMedicament = fromMed;
            medicamentChoisi = medicament;
            lblDisplay.Content = "Praticien :";
            ActualiserCbBox();
            cbBoxDisplay.SelectedIndex = 0;
            DP_AVI_DATE.DisplayDateEnd = DateTime.Now;
            DP_AVI_DATE.SelectedDate = DateTime.Now;
        }

        public AjoutAvis(Praticien praticien, bool fromMed)
        {
            InitializeComponent();
            fromMedicament = fromMed;
            praticienChoisi = praticien;
            lblDisplay.Content = "Médicament :";
            ActualiserCbBox();
            this.DP_AVI_DATE.DisplayDateEnd = DateTime.Now;
            this.DP_AVI_DATE.SelectedDate = DateTime.Now;
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            // Algo permettant d'afficher un label pendant 3 secondes avant de disparaître
            this.LabelTimer.Content = DateTime.Now.ToString("ss");
            var labelStockage = this.LabelTimer.Content;

            while (this.LabelTimer.Content == labelStockage)
            {
                var labelNouveauStockage = DateTime.Now.ToString("ss");

                if (labelNouveauStockage != (String)labelStockage)
                {
                    this.LabelValidation.Visibility = Visibility.Hidden;
                    timer.Stop();
                    break;
                }
            }
        }

        private void ActualiserCbBox()
        {
            if (fromMedicament)
            {
                List<Praticien> listePraticiens = Requetes.PS_LISTE_PRATICIENS();
                if (listePraticiens != null)
                {
                    cbBoxDisplay.ItemsSource = listePraticiens;
                    cbBoxDisplay.DisplayMemberPath = "PRA_NOMCOMPLET";
                }
            } 
            else
            {
                List<Medicament> listeMedicaments = Requetes.PS_LISTE_MEDICAMENT();
                if (listeMedicaments != null)
                {
                    cbBoxDisplay.ItemsSource = listeMedicaments;
                    cbBoxDisplay.DisplayMemberPath = "MED_NOM_COMMERCIAL";
                }
            }

        }

        private void Btn_AjoutAvis_Click(object sender, RoutedEventArgs e)
        {
            Avis NouveauAvis = new Avis();
            NouveauAvis.AVI_DATE = DateTime.Parse(DP_AVI_DATE.Text);
            NouveauAvis.AVI_COMMENTAIRE = TBX_Commentaire.Text;

            if (fromMedicament)
            {
                NouveauAvis.medicament = medicamentChoisi;
                NouveauAvis.praticien = (Praticien)cbBoxDisplay.SelectedValue;
            } else
            {
                NouveauAvis.medicament = (Medicament)cbBoxDisplay.SelectedValue;
                NouveauAvis.praticien = praticienChoisi;
            }

            if (TBX_Commentaire.Text == "")
            {
                MessageBox.Show("Un commentaire doit être saisi.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            try
            {
                Requetes.PS_CREATE_AVIS(NouveauAvis);

                // On affiche le message de validation d'ajout
                LabelValidation.Visibility = Visibility.Visible;

                // Début du timer pour le message de validation d'ajout
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += timerTick;
                timer.Start();
                TBX_Commentaire.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void timerTick(object sender, EventArgs e)
        {
            // Algo permettant d'afficher un label pendant 3 secondes avant de disparaître
            LabelTimer.Content = DateTime.Now.ToString("ss");
            var labelStockage = LabelTimer.Content;

            while (LabelTimer.Content == labelStockage)
            {
                var labelNouveauStockage = DateTime.Now.ToString("ss");

                if (labelNouveauStockage != (String)labelStockage)
                {
                    LabelValidation.Visibility = Visibility.Hidden;
                    timer.Stop();
                    break;
                }
            }
        }
    }
}
