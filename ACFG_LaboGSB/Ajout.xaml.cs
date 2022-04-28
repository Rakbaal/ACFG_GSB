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
    public partial class Ajout : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public Ajout()
        {
            InitializeComponent();
            this.ComboBoxType.SelectedIndex = 0;
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
            List<string> errorList = new List<string>();
            
            if (TextboxNomCom.Text == "")
            {
                errorList.Add("Un nom commercial doit être saisi.");
            }

            if (TextboxNomDCI.Text == "")
            {
                errorList.Add("Un nom DCI doit être saisi.");
            }

            if (TextboxDosage.Text == "")
            {
                errorList.Add("Un dosage doit être saisi.");
            }

            if (TextboxDesc.Text == "")
            {
                errorList.Add("Une description doit être saisie.");
            }

            if (errorList == null)
            {
                // On implémente les données saisies dans une classe vide
                Medicament NouveauMedicament = new Medicament();
                NouveauMedicament.MED_NOM_COMMERCIAL = this.TextboxNomCom.Text;
                NouveauMedicament.MED_NOM_DCI = this.TextboxNomDCI.Text;
                NouveauMedicament.MED_DOSAGE = this.TextboxDosage.Text;
                NouveauMedicament.MED_DESCRIPTION = this.TextboxDesc.Text;
                NouveauMedicament.MED_TYPE = this.ComboBoxType.Text;

                // On appelle la procédure pour ajouter le médicament
                Requetes.PS_CREATE_MEDICAMENT(NouveauMedicament);

                // On vide tous les champs à saisir
                this.TextboxNomCom.Text = "";
                this.TextboxNomDCI.Text = "";
                this.TextboxDosage.Text = "";
                this.TextboxDesc.Text = "";
                this.ComboBoxType.SelectedIndex = 0;

                // On affiche le message de validation d'ajout
                this.LabelValidation.Visibility = Visibility.Visible;

                // Début du timer pour le message de validation d'ajout
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += timer_Tick;
                timer.Start();
            } else
            {
                MessageBox.Show($"{String.Join("\n", errorList)}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
