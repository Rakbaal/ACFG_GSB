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

namespace ACFG_LaboGSB
{
    /// <summary>
    /// Logique d'interaction pour DescriptionPraticien.xaml
    /// </summary>
    public partial class DescriptionPraticien : Window
    {
        // Variables Globales
        Praticien praticienChoisi = new Praticien();

        public DescriptionPraticien(Praticien praticien)
        {
            InitializeComponent();
            List<Profession> listProfession = Requetes.PS_LISTE_PROFESSION();
            ComboBoxProfession.ItemsSource = listProfession;
            ComboBoxProfession.SelectedItem = listProfession.First(x => x.PRO_ID == praticien.PRA_PROFESSION.PRO_ID);

            Btn_Valider.Visibility = Visibility.Hidden;
            Tbx_Description_Prenom.Visibility = Visibility.Hidden;
            Tbx_Description_Nom.Visibility = Visibility.Hidden;
            ComboBoxProfession.Visibility = Visibility.Hidden;
            Lbl_Description_Prenom.Content = praticien.PRA_PRENOM;
            Lbl_Description_Nom.Content = praticien.PRA_NOM;
            Lbl_Description_Profession.Content = praticien.PRA_PROFESSION.PRO_LIBELLE;
            praticienChoisi = praticien;

            ActualiserDataGrid();
        }

        private void ActualiserDataGrid()
        {
            List<Avis> listeAvis = Requetes.PS_SELECT_AVIS_PRATICIEN(praticienChoisi.PRA_ID);
            if (listeAvis != null)
            {
                DataGridAvis.ItemsSource = listeAvis;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ActualiserDataGrid();
        }

        private void Btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            this.Btn_Modifier.Visibility = Visibility.Hidden;
            this.Btn_Valider.Visibility = Visibility.Visible;
            this.Tbx_Description_Prenom.Text = (string)Lbl_Description_Prenom.Content;
            this.Tbx_Description_Prenom.Visibility = Visibility.Visible;
            this.Lbl_Description_Prenom.Visibility = Visibility.Hidden;
            this.Tbx_Description_Nom.Text = (string)Lbl_Description_Nom.Content;
            this.Tbx_Description_Nom.Visibility = Visibility.Visible;
            this.Lbl_Description_Nom.Visibility = Visibility.Hidden;
            this.ComboBoxProfession.Visibility = Visibility.Visible;
            this.Lbl_Description_Profession.Visibility = Visibility.Hidden;
            this.ComboBoxProfession.IsEnabled = true;
        }

        private void Btn_Valider_Click(object sender, RoutedEventArgs e)
        {
            Praticien nouveauPraticien = new Praticien();
            nouveauPraticien.PRA_ID = praticienChoisi.PRA_ID;
            nouveauPraticien.PRA_NOM = Tbx_Description_Nom.Text;
            nouveauPraticien.PRA_PRENOM = Tbx_Description_Prenom.Text;
            nouveauPraticien.PRA_PROFESSION = (Profession)ComboBoxProfession.SelectedItem;

            List<string> errorList = new List<string>();

            if (Tbx_Description_Nom.Text == "")
            {
                errorList.Add("Un nom doit être saisi.");
            }

            if (Tbx_Description_Prenom.Text == "")
            {
                errorList.Add("Un prénom doit être saisi.");
            }

            if (ComboBoxProfession.SelectedItem == null)
            {
                errorList.Add("Une profession doit être sélectionnée");
            }

            if (errorList.Count == 0)
            {
                Requetes.PS_UPDATE_PRATICIEN(nouveauPraticien);

                Btn_Modifier.Visibility = Visibility.Visible;
                Btn_Valider.Visibility = Visibility.Hidden;
                Lbl_Description_Prenom.Content = Tbx_Description_Prenom.Text;
                Lbl_Description_Prenom.Visibility = Visibility.Visible;
                Tbx_Description_Prenom.Visibility = Visibility.Hidden;
                Lbl_Description_Nom.Content = Tbx_Description_Nom.Text;
                Lbl_Description_Nom.Visibility = Visibility.Visible;
                Tbx_Description_Nom.Visibility = Visibility.Hidden;
                ComboBoxProfession.Visibility = Visibility.Hidden;
                Lbl_Description_Profession.Content = nouveauPraticien.PRA_PROFESSION.PRO_LIBELLE;
                Lbl_Description_Profession.Visibility = Visibility.Visible;
                ComboBoxProfession.IsEnabled = false;
            }
            else
            {
                MessageBox.Show($"{String.Join("\n", errorList)}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Lbl_Description_Nom.Content = praticienChoisi.PRA_NOM;
                Lbl_Description_Prenom.Content = praticienChoisi.PRA_PRENOM;
            }
            
        }

        private void BtnAjoutAvis_Click(object sender, RoutedEventArgs e)
        {
            AjoutAvis ajoutAvis = new AjoutAvis(praticienChoisi, false);
            ajoutAvis.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
                }
                else
                {
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
    }
}
