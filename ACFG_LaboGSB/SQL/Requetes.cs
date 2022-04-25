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

        public static List<Medicament> PS_LISTE_MEDICAMENT()
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = "exec PS_SELECT_ALL_MEDICAMENT";
            List<Medicament> listMedicament = new List<Medicament>();
            Medicament medicament = null;

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader["MED_ID"] != DBNull.Value)
                    {
                        medicament = new Medicament();
                        medicament.MED_ID = Convert.ToInt32(mySqlDataReader["MED_ID"]);
                        medicament.MED_NOM_COMMERCIAL = Convert.ToString(mySqlDataReader["MED_NOM_COMMERCIAL"]);
                        medicament.MED_NOM_DCI = Convert.ToString(mySqlDataReader["MED_NOM_DCI"]);
                        medicament.MED_DOSAGE = Convert.ToString(mySqlDataReader["MED_DOSAGE"]);
                        medicament.MED_TYPE = Convert.ToString(mySqlDataReader["MED_TYPE"]);
                        listMedicament.Add(medicament);
                    }
                }
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête SELECT ListeMedicament " + erreur.Message);
                throw;
            }

            finally
            {
                conn.Close();
            }
            return listMedicament;
        }

        public static Medicament PS_MEDICAMENT_DESCRIPTION(int id)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = "exec PS_SELECT_MEDICAMENT_DESCRIPTION '" + id + "'";
            Medicament medicament = null;

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader["MED_ID"] != DBNull.Value)
                    {
                        medicament = new Medicament();
                        medicament.MED_ID = Convert.ToInt32(mySqlDataReader["MED_ID"]);
                        medicament.MED_NOM_COMMERCIAL = Convert.ToString(mySqlDataReader["MED_NOM_COMMERCIAL"]);
                        medicament.MED_NOM_DCI = Convert.ToString(mySqlDataReader["MED_NOM_DCI"]);
                        medicament.MED_DOSAGE = Convert.ToString(mySqlDataReader["MED_DOSAGE"]);
                        medicament.MED_TYPE = Convert.ToString(mySqlDataReader["MED_TYPE"]);
                        medicament.MED_DESCRIPTION = Convert.ToString(mySqlDataReader["MED_DESCRIPTION"]);
                    }
                }
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête SELECT ListeMedicament " + erreur.Message);
                throw;
            }

            finally
            {
                conn.Close();
            }
            return medicament;
        }

        public static void PS_DELETE_MEDICAMENT(Medicament medicament)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = "exec PS_DELETE_MEDICAMENT '" + medicament.MED_ID + "'";

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                mySqlDataReader.Read();

            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête DELETE Medicament " + erreur.Message);
                throw;
            }

            finally
            {
                conn.Close();
            }
        }

        public static void PS_UPDATE_MEDICAMENT(Medicament medicament)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            var description = medicament.MED_DESCRIPTION.Replace("'", "''");

            string Requete = $"exec PS_UPDATE_MEDICAMENT " +
                $"{medicament.MED_ID}, " +
                $"'{medicament.MED_NOM_COMMERCIAL}', " +
                $"'{medicament.MED_NOM_DCI}', " +
                $"'{medicament.MED_DOSAGE}', " +
                $"'{description}', " +
                $"'{medicament.MED_TYPE}'";

            try
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                mySqlDataReader.Read();
            }
            catch (Exception erreur)
            {
                Console.WriteLine("Erreur lors de la requête UPDATE Medicament " + erreur.Message);
                throw;
            }
        }

        public static void PS_CREATE_MEDICAMENT(Medicament medicament)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string description = medicament.MED_DESCRIPTION;
            description = description.Replace("'", "''");
            string Requete = "exec PS_CREATE_MEDICAMENT '" + medicament.MED_NOM_COMMERCIAL + "' , '" + medicament.MED_NOM_DCI + "' , '" + medicament.MED_DOSAGE + "' , '" + description + "' , '" + medicament.MED_TYPE + "'";

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                mySqlDataReader.Read();
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête CREATE Medicament " + erreur.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region PS - Praticien

        public static List<Praticien> PS_LISTE_PRATICIENS()
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = "exec PS_SELECT_ALL_PRATICIEN";
            List<Praticien> listPraticien = new List<Praticien>();
            Praticien praticien = null;

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader(); 
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader["PRA_ID"] != DBNull.Value)
                    {
                        praticien = new Praticien();
                        praticien.PRA_ID = Convert.ToInt32(mySqlDataReader["PRA_ID"]);
                        praticien.PRA_NOM = Convert.ToString(mySqlDataReader["PRA_NOM"]);
                        praticien.PRA_PRENOM = Convert.ToString(mySqlDataReader["PRA_PRENOM"]);
                        praticien.PRA_PROFESSION = Convert.ToString(mySqlDataReader["PRA_PROFESSION"]);
                        listPraticien.Add(praticien);
                    }
                }
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête SELECT ListePraticiens " + erreur.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return listPraticien;
        }

        public static void PS_CREATE_PRATICIEN(Praticien praticien)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");

            string Requete = "exec PS_CREATE_PRATICIEN '" + praticien.PRA_NOM + "' , '" + praticien.PRA_PRENOM + "' , '" + praticien.PRA_PROFESSION + "'";

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                mySqlDataReader.Read();
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête CREATE praticien " + erreur.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void PS_UPDATE_PRATICIEN(Praticien praticien)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            var profession = praticien.PRA_PROFESSION.Replace("'", "''");

            string Requete = $"exec PS_UPDATE_PRATICIEN " +
                $"{praticien.PRA_ID}, " +
                $"'{praticien.PRA_NOM}', " +
                $"'{praticien.PRA_PRENOM}', " +
                $"'{profession}'";

            try
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                mySqlDataReader.Read();
            }
            catch (Exception erreur)
            {
                Console.WriteLine("Erreur lors de la requête UPDATE Praticien " + erreur.Message);
                throw;
            }
        }

        public static void PS_DELETE_PRATICIEN(Praticien practicien)

        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = "exec PS_DELETE_PRATICIEN '" + practicien.PRA_ID + "'";


            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                mySqlDataReader.Read();
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête DELETE Praticien " + erreur.Message);
                throw;
            }
        }

        public static Praticien PS_SELECT_UNPRATICIEN(int id)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = $"exec PS_SELECT_UNPRATICIEN {id}";
            Praticien praticien = null;

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader["PRA_ID"] != DBNull.Value)
                    {
                        praticien = new Praticien();
                        praticien.PRA_ID = Convert.ToInt32(mySqlDataReader["PRA_ID"]);
                        praticien.PRA_NOM = Convert.ToString(mySqlDataReader["PRA_NOM"]);
                        praticien.PRA_PRENOM = Convert.ToString(mySqlDataReader["PRA_PRENOM"]);
                        praticien.PRA_PROFESSION = Convert.ToString(mySqlDataReader["PRA_PROFESSION"]);
                    }
                }
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête SELECT ListePraticiens " + erreur.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return praticien;
        }

        #endregion

        #region PS - Avis

        public static List<Avis> PS_SELECT_AVIS_MEDICAMENT(int med_id)
        {
            SqlCommand myCommand = null;
            SqlDataReader mySqlDataReader = null;
            SqlConnection conn = BDD.openBDDApplication("ACFG_LaboGSB");
            string Requete = "exec PS_SELECT_AVIS_MEDICAMENT '" + med_id + "'";
            List<Avis> listAvis = new List<Avis>();
            Avis avis = null;

            try // On essaye d'executer la requête
            {
                myCommand = new SqlCommand(Requete, conn);
                mySqlDataReader = myCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    if (mySqlDataReader["AVI_ID"] != DBNull.Value)
                    {
                        avis = new Avis();
                        avis.AVI_DATE = Convert.ToDateTime(mySqlDataReader["AVI_DATE"]);
                        avis.AVI_COMMENTAIRE = Convert.ToString(mySqlDataReader["AVI_COMMENTAIRE"]);
                        avis.praticien = Requetes.PS_SELECT_UNPRATICIEN(Convert.ToInt32(mySqlDataReader["PRA_ID"]));
                        listAvis.Add(avis);
                    }
                }
            }
            catch (Exception erreur) // En cas d'erreur un message s'affiche sur la console
            {
                Console.WriteLine("Erreur lors de la requête SELECT ListeAvis " + erreur.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return listAvis;
        }

        #endregion
    }
}
        