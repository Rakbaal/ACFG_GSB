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
    /// Logique d'interaction pour DescriptionMedicament.xaml
    /// </summary>
    public partial class DescriptionMedicament : Window
    {
        public DescriptionMedicament()
        {
            InitializeComponent();
            Btn_Valider.Visibility = Visibility.Hidden;
            Tbx_Description_NomCommercial.Visibility = Visibility.Hidden;
            Tbx_Description_NomDCI.Visibility = Visibility.Hidden;
            Tbx_Description_Type.Visibility = Visibility.Hidden;
            Tbx_Description_Dosage.Visibility = Visibility.Hidden;
            Tbx_Decription_Description.Visibility = Visibility.Hidden;
        }

        private void Btn_Modifier_Click(object sender, RoutedEventArgs e)
        {
            Btn_Modifier.Visibility = Visibility.Hidden;
            Btn_Valider.Visibility = Visibility.Visible;
            Tbx_Description_NomCommercial.Visibility = Visibility.Visible;
            Tbx_Description_NomDCI.Visibility = Visibility.Visible;
            Tbx_Description_Type.Visibility = Visibility.Visible;
            Tbx_Description_Dosage.Visibility = Visibility.Visible;
            Tbx_Decription_Description.Visibility = Visibility.Visible;
            Lbl_Description_NomCommerciale.Visibility = Visibility.Hidden;
            Lbl_Description_NomDCI.Visibility = Visibility.Hidden;
            Lbl_Description_Type.Visibility = Visibility.Hidden;
            Lbl_Description_Dosage.Visibility = Visibility.Hidden;
            Lbl_Description_Description.Visibility = Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
        }
    }
}
