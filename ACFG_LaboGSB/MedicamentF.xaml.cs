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
            //Affichage de tous les médicaments 
            List<Medicament> listeMedicaments = Requetes.PS_LISTE_MEDICAMENT();

            if (listeMedicaments != null)
            {
                this.DataGridMedicaments.ItemsSource = listeMedicaments;
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
                MessageBoxResult result = MessageBox.Show(messageErreur, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Hand);

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

        #endregion

        #region DataGrid

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        #endregion

        private void Window_Activated(object sender, EventArgs e)
        {
            ActualiserDataGrid();
        }
    }
}
