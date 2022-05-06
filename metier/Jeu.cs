using Puissance4Upgrade.stockage;
using System;
using System.Collections.Generic;

namespace Puissance4Upgrade
{
    /// <summary>
    /// Classe représentant le Jeu.
    /// Elle s'occupe de gérer le lien entre l'ihm et la couche métier
    /// </summary>
    [Serializable()]
    public class Jeu
    {
        private Etat etat;                      // représente l'etat de la case qui va être placé, permet l'alternance Jaune/Rouge.
                                                //Ne doit jamais être dans l'etat vide.

        private readonly Grille grille;         //Représente la grille de jeu,
                                                //un jeu en possede une, et elle ne peut être modifié
        /// <summary>
        /// Constructeur
        /// </summary>
        public Jeu()
        {
            this.grille = new Grille();
            this.Etat = Etat.ROUGE; //Le premier pion est, par défaut, rouge
        }

        /// <summary>
        /// Renvoie une case selon une coordonnee
        /// </summary>
        /// <param name="coordonnee">Coordonne de la case</param>
        /// <returns>La case selon la coordonnee donnée en parametre</returns>
        public Case GetCase(Coordonnee coordonnee) => grille.GetCase(coordonnee);

        /// <summary>
        /// Methode permettant de placer un pion.
        /// Elle actualise les données de la couche métier.
        /// </summary>
        /// <param name="nbColonne">La position en X de la colonne</param>
        /// <returns>La case qui vient d'être modifié</returns>
        public Case MettreUnPion(int nbColonne)
        {
            List<Case> colonne = grille.GetColonne(nbColonne); // On récupere une colonne
            Case c = null;

            foreach(Case @case in colonne)
            {
                if((@case.Etat == Etat.VIDE))   //dès qu'une case est vide on lui met le pion
                {
                    c = @case;
                    @case.Etat = GetEtat();
                    break;
                }
            }
            
            return c;
        }

        /// <summary>
        /// Methode permettant de renvoyer l'etat de la case qui sera placer.
        /// Elle switch aussi l'etat pour la prochaine
        /// </summary>
        /// <returns>l'etat de la case qui sera placer</returns>
        public Etat GetEtat()
        {
            Etat e;
            if(this.etat == Etat.ROUGE)
            {
                e = Etat.ROUGE;
                this.etat = Etat.JAUNE;
            }
            else
            {
                e = Etat.JAUNE;
                this.etat = Etat.ROUGE;
            }
            return e;
        }

        

        
        public Etat Etat { get => etat; set => etat = value; }      //Accesseur et mutateur de l'etat du jeu

        /// <summary>
        /// Methode pour sauvegarder l'objet Jeu
        /// </summary>
        /// <returns>true si aucun probleme n'a eu lieu, false sinon</returns>
        public bool Sauvegarde(DAOType type) => JeuDAO.Get().Sauvegarder(this, type);

        /// <summary>
        /// Methode renvoyant toutes les cases Jouables dans un tour.
        /// Utiliser pour la prise de décision de l'automate
        /// </summary>
        /// <returns></returns>
        public List<Case> GetCaseDisponible() => grille.GetCaseDisponible();

        /// <summary>
        /// Méthode renvoyant le voisin d'une case donnée dans une certaine direction
        /// </summary>
        /// <param name="c">La case souhaité</param>
        /// <param name="direction">La direction du voisin</param>
        /// <returns>Le voisin de la case</returns>
        public Case GetVoisin(Case c, Direction direction) => grille.GetVoisin(c, direction);

        /// <summary>
        /// Méthode permettant de vérifier si une case donnée fait partie d'un alignement de 4
        /// </summary>
        /// <param name="case">La case que l'on souhaite vérifier</param>
        /// <returns>true si la case donnée fait partie d'un alignement de 4, false sinon</returns>
        public bool VerifyWin(Case @case)
        {
            return grille.VerifyWin(@case);
        }
    }
}
