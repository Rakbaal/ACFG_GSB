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
        Medicament medicamentChoisi = new Medicament();

        public DescriptionMedicament(Medicament medicament)
        {
            InitializeComponent();
            Btn_Valider.Visibility = Visibility.Hidden;
            Tbx_Description_NomCommercial.Visibility = Visibility.Hidden;
            Tbx_Description_NomDCI.Visibility = Visibility.Hidden;
            Tbx_Description_Type.Visibility = Visibility.Hidden;
            Tbx_Description_Dosage.Visibility = Visibility.Hidden;
            Tbx_Decription_Description.Visibility = Visibility.Hidden;
            Lbl_Description_NomCommerciale.Content = medicament.MED_NOM_COMMERCIAL;
            Lbl_Description_NomDCI.Content = medicament.MED_NOM_DCI;
            Lbl_Description_Dosage.Content = medicament.MED_DOSAGE;
            Lbl_Description_Description.Content = medicament.MED_DESCRIPTION;
            Lbl_Description_Type.Content = medicament.MED_TYPE;
            medicamentChoisi = medicament;

            List<Avis> listeAvis = Requetes.PS_SELECT_AVIS_MEDICAMENT(medicament.MED_ID);
            if (listeAvis != null)
            {
                this.DataGridAvis.ItemsSource = listeAvis;
            }
        }

        private void Btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            Btn_Modifier.Visibility = Visibility.Hidden;
            Btn_Valider.Visibility = Visibility.Visible;
            Tbx_Description_NomCommercial.Text = (string)Lbl_Description_NomCommerciale.Content;
            Tbx_Description_NomCommercial.Visibility = Visibility.Visible;
            Tbx_Description_NomDCI.Text = (string)Lbl_Description_NomDCI.Content;
            Tbx_Description_NomDCI.Visibility = Visibility.Visible;
            Tbx_Description_Type.Text = (string)Lbl_Description_Type.Content;
            Tbx_Description_Type.Visibility = Visibility.Visible;
            Tbx_Description_Dosage.Text = (string)Lbl_Description_Dosage.Content;
            Tbx_Description_Dosage.Visibility = Visibility.Visible;
            Tbx_Decription_Description.Text = (string)Lbl_Description_Description.Content;
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
            
            Requetes.PS_UPDATE_MEDICAMENT(medicamentChoisi);
            this.Close();
        }

    }
}
