using ACFG_LaboGSB.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACFG_LaboGSB.SQL
{
    class Requetes
    {
        #region PS - Login

        public static int PS_LOGIN_VALIDATION(string login, string mdpHasher)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = "exec PS_LOGIN_VALIDATION '" + login + "', '" + mdpHasher + "'";
            Visiteur visiteur = null;

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader["VIS_ID"] != DBNull.Value)
                    {
                        visiteur = new Visiteur();
                        visiteur.VIS_ID = Convert.ToInt32(mySqlDataReader["VIS_ID"]);
                    }
                }
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la procédure stockée de validation du login avec le mot de passe : " + erreur.Message);
                throw;
            }

            finally
            {
                conn.Close();
            }

            if (visiteur != null)
            {
                return visiteur.VIS_ID;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region PS - Médicaments

        #endregion
    }
}
