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
using ACFG_LaboGSB.Classes;

namespace ACFG_LaboGSB
{
    /// <summary>
    /// Logique d'interaction pour Medicament.xaml
    /// </summary>
    public partial class MedicamentF : Window
    {

        public MedicamentF()
        {
            InitializeComponent();
            ActualiserDataGrid();
        }

        #region Méthodes 

        private void ActualiserDataGrid()
        {
            //Sélection de tous les médicaments
            List<Medicament> listeMedicaments = Requetes.PS_LISTE_MEDICAMENT();

            if (listeMedicaments != null)
            {
                this.DataGridMedicaments.ItemsSource = listeMedicaments;
            }

            List<Praticien> listePraticiens = Requetes.PS_LISTE_PRATICIENS();
            if (listePraticiens!= null)
            {
                this.DataGridPraticien.ItemsSource = listePraticiens;
            }
        }

        #endregion

        #region Boutons

        private void ButtonSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGridMedicaments.SelectedItem != null)
            {
                //On récupère le médicament sélectionné
                Medicament medicamentSuppression = this.DataGridMedicaments.SelectedItem as Medicament;

                //On demande la confirmation à l'utilisateur
                string messageErreur = "Voulez-vous vraiment supprimer le médicament " + medicamentSuppression.MED_NOM_COMMERCIAL + " ?";
                MessageBoxResult result = MessageBox.Show(messageErreur, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.Yes)
                {
                    //On supprime le médicament en appellant la procédure stockée
                    Requetes.PS_DELETE_MEDICAMENT(medicamentSuppression);
                    ActualiserDataGrid();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un médicament à supprimer !", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            Ajout ajout = new Ajout();
            ajout.ShowDialog();
            
        }
        private void ButtonSupprimer_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.ButtonSupprimer.Background = Color.FromRgb("255,105,105");
        }
        private void ButtonSupprimerPraticien_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGridPraticien.SelectedItem != null)
            {
                //On récupère le praticien sélectionné
                Praticien praticienSuppression = this.DataGridPraticien.SelectedItem as Praticien;

                //On demande la confirmation à l'utilisateur
                string messageErreur = "Voulez-vous vraiment supprimer le praticien " + praticienSuppression.PRA_NOM + " ?";
                MessageBoxResult result = MessageBox.Show(messageErreur, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Hand);

                if (result == MessageBoxResult.Yes)
                {
                    //On supprime le praticien en appellant la procédure stockée
                    Requetes.PS_DELETE_PRATICIEN(praticienSuppression);
                    ActualiserDataGrid();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un praticien à supprimer !", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void Btn_AjoutPraticien_Click(object sender, RoutedEventArgs e)
        {
            AjoutPraticien ajoutPraticien = new AjoutPraticien();
            ajoutPraticien.ShowDialog();
        }

        #endregion

        #region DataGrid

        private void DataGridMedicaments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // lors d'un double clique sur une ligne ouvre un le formulaire de détail
            this.DataGridMedicaments.SelectionMode = DataGridSelectionMode.Single;
            int index = DataGridMedicaments.SelectedIndex;
            try
            {
                var gridMedicament = (Medicament)DataGridMedicaments.Items[index];
                var medicament = Requetes.PS_MEDICAMENT_DESCRIPTION(gridMedicament.MED_ID);

                DescriptionMedicament descriptionMedicament = new DescriptionMedicament(medicament);
                descriptionMedicament.Show();
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion


        private void Window_Activated(object sender, EventArgs e)
        {
            ActualiserDataGrid();

        }

        private void DataGridPraticien_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // lors d'un double clic sur une ligne, ouvre un formulaire détaillé du praticien
            this.DataGridPraticien.SelectionMode = DataGridSelectionMode.Single;
            int index = DataGridPraticien.SelectedIndex;

            try
            {
                var praticien = (Praticien)DataGridPraticien.Items[index];

                DescriptionPraticien descriptionPraticien = new DescriptionPraticien(praticien);
                descriptionPraticien.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
            
        }
    }
}
