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
    /// Logique d'interaction pour Ajout.xaml
    /// </summary>
    public partial class AjoutPraticien : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public AjoutPraticien()
        {
            InitializeComponent();
            this.ComboBoxProfession.SelectedIndex = 0;
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

        private void Btn_Ajout_Click(object sender, RoutedEventArgs e)
        {
            // On implémente les données saisis dans une classe vide
            Praticien NouveauPraticien = new Praticien();
            
            if (TextboxNomPraticien.Text != "")
            {
                NouveauPraticien.PRA_NOM = this.TextboxNomPraticien.Text;
            }
            else
            {
                MessageBox.Show("Erreur", "Veuillez saisir un Nom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TextboxPrenomPraticien.Text != "")
            {
                NouveauPraticien.PRA_PRENOM = this.TextboxPrenomPraticien.Text;
            }
            else
            {
                MessageBox.Show("Erreur", "Veuillez saisir un Prénom", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ComboBoxProfession.Text != "")
            {
                NouveauPraticien.PRA_PROFESSION = this.ComboBoxProfession.Text;
            }
            else
            {
                MessageBox.Show("Erreur", "Veuillez saisir une Profession", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // On appelle la procédure pour ajouter le médicament
            Requetes.PS_CREATE_PRATICIEN(NouveauPraticien);

            // On vide tous les champs à saisir
            this.TextboxNomPraticien.Text = "";
            this.TextboxPrenomPraticien.Text = "";
            this.ComboBoxProfession.Text = "";

            // On affiche le message de validation d'ajout
            this.LabelValidation.Visibility = Visibility.Visible;

            // Début du timer pour le message de validation d'ajout
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
