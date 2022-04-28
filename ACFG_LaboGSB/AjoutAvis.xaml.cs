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
        DispatcherTimer timer = new DispatcherTimer();

        public AjoutAvis(Medicament medicament)
        {
            InitializeComponent();
            medicamentChoisi = medicament;
            ActualiserCbBox();
            this.DP_AVI_DATE.DisplayDateEnd = DateTime.Now;
            this.DP_AVI_DATE.SelectedDate = DateTime.Now;
        }

        void timer_Tick(object sender, EventArgs e)
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

                // On affiche le message de validation d'ajout
                this.LabelValidation.Visibility = Visibility.Visible;

                // Début du timer pour le message de validation d'ajout
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += timer_Tick;
                timer.Start();
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
