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
    /// Logique d'interaction pour Ajout.xaml
    /// </summary>
    public partial class Ajout : Window
    {
        public Ajout()
        {
            InitializeComponent();
            this.ComboBoxType.SelectedIndex = 0;
        }

        private void Btn_Ajout_Click(object sender, RoutedEventArgs e)
        {
            Medicament NouveauMedicament = new Medicament();
            NouveauMedicament.MED_NOM_COMMERCIAL = this.TextboxNomCom.Text;
            NouveauMedicament.MED_NOM_DCI = this.TextboxNomDCI.Text;
            NouveauMedicament.MED_DOSAGE = this.TextboxDosage.Text;
            NouveauMedicament.MED_DESCRIPTION = this.TextboxDesc.Text;
            NouveauMedicament.MED_TYPE = this.ComboBoxType.Text;

            Requetes.PS_CREATE_MEDICAMENT(NouveauMedicament);

            this.TextboxNomCom.Text = "";
            this.TextboxNomDCI.Text = "";
            this.TextboxDosage.Text = "";
            this.TextboxDesc.Text = "";
            this.ComboBoxType.SelectedIndex = 0;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
