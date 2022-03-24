using ACFG_LaboGSB.Classes;
using ACFG_LaboGSB.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACFG_LaboGSB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            
        }

        #region Bouton

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.LabelErreur.Visibility = Visibility.Hidden;

            //Algo pour hasher le mot de passe
            string mdppropre = this.TextboxMdp.Password;
            string mdpHasher = "";
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //De String à une liste de Bit
                byte[] sourceBytes = Encoding.UTF8.GetBytes(mdppropre);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                mdpHasher = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }

            string login = this.TextboxIdentifiant.Text;
            int resultatProc = 0;
            resultatProc = Requetes.PS_LOGIN_VALIDATION(login, mdpHasher);

            if (resultatProc != 0)
            {
                MedicamentF MedicamentF = new MedicamentF();
                MedicamentF.ShowDialog();
            }
            else
            {
                this.LabelErreur.Visibility = Visibility.Visible;
            }

            
        }

        #endregion

        #region TextBox


        private void TextboxIdentifiant_MouseLeave(object sender, MouseEventArgs e)
        {
            /*
            if (this.TextboxIdentifiant.IsFocused == true)
            {

            }
            else
            {
                if (this.TextboxIdentifiant.Text == "")
                {
                    this.TextboxIdentifiant.Text = "Identifiant";
                }
            }
            */
        }

        private void TextboxIdentifiant_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            if (this.TextboxIdentifiant.IsFocused == true)
            {
                if (this.TextboxIdentifiant.Text == "Identifiant")
                {
                    this.TextboxIdentifiant.Text = "";
                }
            }
            */
        }

        private void TextboxMdp_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        
        private void TextboxMdp_MouseLeave(object sender, MouseEventArgs e)
        {
            /*
            if (this.TextboxMdp.IsFocused == true)
            {

            }
            else
            {
                if (this.TextboxMdp.Text == "")
                {
                    this.TextboxMdp.Text = "Mot de passe";
                }
            }
            */
        }
        
        private void TextboxMdp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.LabelErreur.Visibility = Visibility.Hidden;

                //Algo pour hasher le mot de passe
                string mdppropre = this.TextboxMdp.Password;
                string mdpHasher = "";
                using (SHA512 sha512Hash = SHA512.Create())
                {
                    //De String à une liste de Bit
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(mdppropre);
                    byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                    mdpHasher = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                }

                string login = this.TextboxIdentifiant.Text;
                int resultatProc = 0;
                resultatProc = Requetes.PS_LOGIN_VALIDATION(login, mdpHasher);

                if (resultatProc != 0)
                {
                    
                    MedicamentF MedicamentF = new MedicamentF();
                    MedicamentF.ShowDialog();
                    
                }
                else
                {
                    this.LabelErreur.Visibility = Visibility.Visible;
                    this.TextboxIdentifiant.Text = "Identifiant";
                    this.TextboxMdp.Password = "Mot de passe";
                }
            }
        }

        #endregion
        
        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void TextboxIdentifiant_GotFocus(object sender, RoutedEventArgs e)
        {
        /*
            if (this.TextboxMdp.IsFocused == false)
            {
                if (this.TextboxMdp.Text == "")
                {
                    this.TextboxMdp.Text = "Mot de passe";
                }
            }

            if (this.TextboxIdentifiant.IsFocused == true)
            {
                if (this.TextboxIdentifiant.Text == "Identifiant")
                {
                    this.TextboxIdentifiant.Text = "";
                }
            }
        */
        }

        private void TextboxMdp_GotFocus(object sender, RoutedEventArgs e)
        {
        /*
            if (this.TextboxIdentifiant.IsFocused == false)
            {
                if (this.TextboxIdentifiant.Text == "")
                {
                    this.TextboxIdentifiant.Text = "Identifiant";
                }
            }

            if (this.TextboxMdp.IsFocused == true)
            {
                if (this.TextboxMdp.Text == "Mot de passe")
                {
                    this.TextboxMdp.Text = "";
                }
            }
        */
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
