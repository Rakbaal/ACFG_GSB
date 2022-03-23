using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACFG_LaboGSB.Classes
{
    class BDD
    {
        public BDD()
        {
        }

        public static SqlConnection openBDDApplication(string v)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
            "Data Source={Environment.MachineName.ToString()};" +
            "Initial Catalog = ACFG_LaboGSB;" +
            "User id=sa;" +
            "Password=info";
            try
            {
                //Connexion à la base de donnée
                conn = new SqlConnection(conn.ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception eMsg1)
            {
                //En cas d’erreur on affiche le message d’erreur
                Console.WriteLine("Erreur durant l’execution de la requete: openBDDApplication" + eMsg1.Message);
                return null;
            }
        }
    }
}
