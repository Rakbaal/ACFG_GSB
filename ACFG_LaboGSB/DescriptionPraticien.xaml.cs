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
        Praticien praticienConcerner = new Praticien();

        public DescriptionPraticien(/*Praticien praticien*/)
        {
            InitializeComponent();
            this.Btn_Valider.Visibility = Visibility.Hidden;
            this.Tbx_Description_Prenom.Visibility = Visibility.Hidden;
            this.Tbx_Description_Nom.Visibility = Visibility.Hidden;
            /*this.Lbl_Description_Prenom.Content = praticien.PRA_PRENOM;
            this.Lbl_Description_Nom.Content = praticien.PRA_NOM;
            this.ComboBoxProfession.Text = praticien.PRA_PROFESSION;
            praticienConcerner = praticien;*/
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
            this.ComboBoxProfession.IsEnabled = true;
        }

        private void Btn_Valider_Click(object sender, RoutedEventArgs e)
        {
            this.Btn_Modifier.Visibility = Visibility.Visible;
            this.Btn_Valider.Visibility = Visibility.Hidden;
            this.Lbl_Description_Prenom.Content = Tbx_Description_Prenom.Text;
            this.Lbl_Description_Prenom.Visibility = Visibility.Visible;  
            this.Tbx_Description_Prenom.Visibility = Visibility.Hidden;
            this.Lbl_Description_Nom.Content = Tbx_Description_Nom.Text;
            this.Lbl_Description_Nom.Visibility = Visibility.Visible;
            this.Tbx_Description_Nom.Visibility = Visibility.Hidden;
            this.ComboBoxProfession.IsEnabled = false;

            Praticien praticienModifier = new Praticien();
            praticienModifier.PRA_ID = praticienConcerner.PRA_ID;
            praticienModifier.PRA_NOM = this.Tbx_Description_Nom.Text;
            praticienModifier.PRA_PRENOM = this.Tbx_Description_Prenom.Text;
            praticienModifier.PRA_PROFESSION = this.ComboBoxProfession.Text;

            Requetes.PS_UPDATE_PRATICIEN(praticienModifier);
        }

        private void Btn_Modifier_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }
    }
}
