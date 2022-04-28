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
using ACFG_LaboGSB.SQL;
using ACFG_LaboGSB.Classes;

namespace ACFG_LaboGSB
{
    /// <summary>  
    /// Logique d'interaction pour DescriptionMedicament.xaml
    /// </summary>
    public partial class DescriptionMedicament : Window
    {
        #region Variables globales

        Medicament medicamentChoisi = new Medicament();

        #endregion

        #region Méthodes

        public DescriptionMedicament(Medicament medicament)
        {
            InitializeComponent();
            Btn_Valider.Visibility = Visibility.Hidden;
            Tbx_Description_NomCommercial.Visibility = Visibility.Hidden;
            Tbx_Description_NomDCI.Visibility = Visibility.Hidden;
            Tbx_Description_Type.Visibility = Visibility.Hidden;
            Tbx_Description_Dosage.Visibility = Visibility.Hidden;
            Tbx_Decription_Description.Visibility = Visibility.Hidden;
            ActualiserInformations(medicament.MED_ID);
            Lbl_Description_NomCommerciale.Text = medicament.MED_NOM_COMMERCIAL;
            Lbl_Description_NomDCI.Text = medicament.MED_NOM_DCI;
            Lbl_Description_Dosage.Text = medicament.MED_DOSAGE;
            Lbl_Description_Description.Text = medicament.MED_DESCRIPTION;
            Lbl_Description_Type.Text = medicament.MED_TYPE;
            medicamentChoisi = medicament;

            List<Avis> listeAvis = Requetes.PS_SELECT_AVIS_MEDICAMENT(medicament.MED_ID);
            if (listeAvis != null)
            {
                this.DataGridAvis.ItemsSource = listeAvis;
            }
        }

        public void ActualiserInformations(int Medicament_Id)
        {
            Medicament medicamentRefresh = Requetes.PS_MEDICAMENT_DESCRIPTION(Medicament_Id);
            Lbl_Description_NomCommerciale.Text = medicamentRefresh.MED_NOM_COMMERCIAL;
            Lbl_Description_NomDCI.Text = medicamentRefresh.MED_NOM_DCI;
            Lbl_Description_Dosage.Text = medicamentRefresh.MED_DOSAGE;
            Lbl_Description_Description.Text = medicamentRefresh.MED_DESCRIPTION;
            Lbl_Description_Type.Text = medicamentRefresh.MED_TYPE;

            medicamentChoisi = medicamentRefresh;
        }

        private void ActualiserDataGrid()
        {
            List<Avis> listeAvis = Requetes.PS_SELECT_AVIS_MEDICAMENT(medicamentChoisi.MED_ID);
            if (listeAvis != null)
            {
                DataGridAvis.ItemsSource = listeAvis;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ActualiserDataGrid();
        }

        #endregion

        #region Boutons

        private void Btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            Btn_Modifier.Visibility = Visibility.Hidden;
            Btn_Valider.Visibility = Visibility.Visible;
            Tbx_Description_NomCommercial.Text = (string)Lbl_Description_NomCommerciale.Text;
            Tbx_Description_NomCommercial.Visibility = Visibility.Visible;
            Tbx_Description_NomDCI.Text = (string)Lbl_Description_NomDCI.Text;
            Tbx_Description_NomDCI.Visibility = Visibility.Visible;
            Tbx_Description_Type.Text = (string)Lbl_Description_Type.Text;
            Tbx_Description_Type.Visibility = Visibility.Visible;
            Tbx_Description_Dosage.Text = (string)Lbl_Description_Dosage.Text;
            Tbx_Description_Dosage.Visibility = Visibility.Visible;
            Tbx_Decription_Description.Text = (string)Lbl_Description_Description.Text;
            Tbx_Decription_Description.Visibility = Visibility.Visible;
            Lbl_Description_NomCommerciale.Visibility = Visibility.Hidden;
            Lbl_Description_NomDCI.Visibility = Visibility.Hidden;
            Lbl_Description_Type.Visibility = Visibility.Hidden;
            Lbl_Description_Dosage.Visibility = Visibility.Hidden;
            Lbl_Description_Description.Visibility = Visibility.Hidden;


        }

        private void Btn_Valider_Click(object sender, RoutedEventArgs e)
        {
            Tbx_Description_NomCommercial.Visibility = Visibility.Hidden;
            Tbx_Description_NomDCI.Visibility = Visibility.Hidden;
            Tbx_Description_Type.Visibility = Visibility.Hidden;
            Tbx_Description_Dosage.Visibility = Visibility.Hidden;
            Tbx_Decription_Description.Visibility = Visibility.Hidden;
            Lbl_Description_NomCommerciale.Visibility = Visibility.Visible;
            Lbl_Description_NomDCI.Visibility = Visibility.Visible;
            Lbl_Description_Type.Visibility = Visibility.Visible;
            Lbl_Description_Dosage.Visibility = Visibility.Visible;
            Lbl_Description_Description.Visibility = Visibility.Visible;
            Btn_Valider.Visibility = Visibility.Hidden;
            Btn_Modifier.Visibility = Visibility.Visible;

            medicamentChoisi.MED_NOM_COMMERCIAL = Tbx_Description_NomCommercial.Text;
            medicamentChoisi.MED_NOM_DCI = Tbx_Description_NomDCI.Text;
            medicamentChoisi.MED_DOSAGE = Tbx_Description_Dosage.Text;
            medicamentChoisi.MED_DESCRIPTION = Tbx_Decription_Description.Text;
            medicamentChoisi.MED_TYPE = Tbx_Description_Type.Text;

            List<string> errorList = new List<string>();

            if (Tbx_Description_NomCommercial.Text == "")
            {
                errorList.Add("Un nom commercial doit être saisi.");
            }

            if (Tbx_Description_NomDCI.Text == "")
            {
                errorList.Add("Un nom DCI doit être saisi.");
            }

            if (Tbx_Description_Dosage.Text == "")
            {
                errorList.Add("Un dosage doit être saisi.");
            }

            if (Tbx_Decription_Description.Text == "")
            {
                errorList.Add("Une description doit être saisie.");
            }

            if (errorList == null)
            {
                Requetes.PS_UPDATE_MEDICAMENT(medicamentChoisi);
                ActualiserInformations(medicamentChoisi.MED_ID);
            }
            else
            {
                MessageBox.Show($"{String.Join("\n", errorList)}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void BtnAjoutAvis_Click(object sender, RoutedEventArgs e)
        {
            AjoutAvis ajoutAvis = new AjoutAvis(medicamentChoisi, true);
            ajoutAvis.ShowDialog();
        }

        private void BtnSupprAvis_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridAvis.SelectedItem != null || DataGridAvis.SelectedCells.Count > 1)
            {
                //On récupère l'avis sélectionné
                Avis avisSuppression = DataGridAvis.SelectedItem as Avis;

                string messageErreur;

                //On demande la confirmation à l'utilisateur
                if (avisSuppression.AVI_COMMENTAIRE.Length < 20)
                {
                    string commentaireExtrait = avisSuppression.AVI_COMMENTAIRE;
                    messageErreur = $"Voulez-vous vraiment supprimer l'avis '{commentaireExtrait}' ?";
                } else {
                    string commentaireExtrait = avisSuppression.AVI_COMMENTAIRE.Substring(0, 20);
                    messageErreur = $"Voulez-vous vraiment supprimer l'avis '{commentaireExtrait}...' ?";
                }
                MessageBoxResult result = MessageBox.Show(messageErreur, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (result == MessageBoxResult.Yes)
                {
                    //On supprime l'avis en appellant la procédure stockée
                    Requetes.PS_DELETE_AVIS(avisSuppression);
                    ActualiserDataGrid();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un avis à supprimer !", "Impossible de supprimer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
