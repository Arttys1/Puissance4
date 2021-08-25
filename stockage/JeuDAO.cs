using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Puissance4Upgrade.stockage
{
    /// <summary>
    /// Classe gérant le stockage du jeu. C'est un singleton
    /// </summary>
    public class JeuDAO
    {
        private static JeuDAO instance = null; // instance du singleton

        /// <summary>
        /// Constructeur privée
        /// </summary>
        private JeuDAO() { }

        /// <summary>
        /// Methode permettant de renvoyer l'instance de l'objet
        /// </summary>
        /// <returns>L'instance de l'objet</returns>
        public static JeuDAO Get()
        {
            if(instance == null)
            {
                instance = new JeuDAO();
            }

            return instance;
        }

        /// <summary>
        /// Methode permettant de sauvegarder
        /// </summary>
        /// <param name="jeu">l'objet Jeu que l'on souhaite sauvegarder</param>
        /// <returns>true si il n'y a pas eu de problème, false sinon</returns>
        public bool Sauvegarder(Jeu jeu, DAOType type)
        {
            bool res = true;
            try
            {
                Stream stream = File.Open(GetPathFile(type), FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, jeu);
                stream.Close();
            }
            catch
            {
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Methode permettant de charger une partie
        /// </summary>
        /// <returns>Retourne le Jeu ainsi charger</returns>
        public Jeu Charger(DAOType type)
        {
            Stream stream = File.Open(GetPathFile(type), FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Jeu jeu = (Jeu)formatter.Deserialize(stream);
            stream.Close();

            return jeu;
        }

        private string GetPathFile(DAOType type)
        {
            String pathFile;

            switch (type)
            {
                case DAOType.JVJ: pathFile = "./sauvegarde.txt"; break;
                case DAOType.BOT: pathFile = "./sauvegardeBot.txt"; break;
                default: throw new Exception("DAOType unvalid");
            }

            return pathFile;
        }
    }
}
