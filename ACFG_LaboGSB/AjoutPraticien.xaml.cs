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
            List<Profession> listProfession = Requetes.PS_LISTE_PROFESSION();
            ComboBoxProfession.ItemsSource = listProfession;
            ComboBoxProfession.SelectedIndex = 0;
        }


        void timerTick(object sender, EventArgs e)
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
            Praticien nouveauPraticien = new Praticien();

            List<string> errorList = new List<string>();

            if (TextboxNomPraticien.Text == "")
            {
                errorList.Add("Un nom doit être saisi.");
            }

            if (TextboxPrenomPraticien.Text == "")
            {
                errorList.Add("Un prénom doit être saisi.");
            }

            if (errorList.Count == 0)
            {
                nouveauPraticien.PRA_NOM = TextboxNomPraticien.Text;
                nouveauPraticien.PRA_PRENOM = TextboxPrenomPraticien.Text;
                nouveauPraticien.PRA_PROFESSION = (Profession)ComboBoxProfession.SelectedValue;

                // On appelle la procédure pour ajouter le praticien
                Requetes.PS_CREATE_PRATICIEN(nouveauPraticien);

                // On vide tous les champs à saisir
                TextboxNomPraticien.Text = "";
                TextboxPrenomPraticien.Text = "";
                ComboBoxProfession.Text = "";

                // On affiche le message de validation d'ajout
                LabelValidation.Visibility = Visibility.Visible;

                // Début du timer pour le message de validation d'ajout
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += timerTick;
                timer.Start();
            }
            else
            {
                MessageBox.Show($"{String.Join("\n", errorList)}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }



            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
