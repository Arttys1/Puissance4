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

        /// <summary>
        /// Methode permettant de verifier la victoire.
        /// Renvoie true si une victoire est faite, false sinon
        /// Cette methode va appeller verifySegment 4 fois, qui s'occupe de verifier chaque "Segment".
        /// Un segment est une ligne. Cela englobe 7 cases dont la case qui vient d'être posé.
        /// </summary>
        /// <param name="case">case qui vient d'être posé</param>
        /// <returns>True si la case qui vient d'être posé est gagante, false sinon</returns>
        public bool VerifyWin(Case @case)
        {
            bool end = false;
            Direction d = 0;
            while(!end && (int)d<4)
            {
                end = VerifySegment(@case,d,0,0);
                d++;
            }

            return end;
        }

        /// <summary>
        /// Méthode récursive permettant de vérifier si un segment est gagnant.
        /// Un segment  est une ligne. Cela englobe 7 cases dont la case qui vient d'être posé.
        /// Chaque récursion de la méthode vérifiera si l'état de la case actuelle est la même que l'etat de la case voisine.
        /// Si oui, elle se rappelle pour vérifier la case voisine cette fois.
        /// Si les etats ne sont pas les mêmes ou si la voisine est null alors on repart dans la direction opposé.
        ///
        /// On lui passe une case qui sera la case actuelle, une direction, un compteur pour compter le nombre de case ayant le même etat,
        /// et le nombre d'essai pour s'arreter quand on a fait tous le segment.
        /// Essai augmente a chaque fois que l'on change de direction.
        /// </summary>
        /// <param name="case">case actuelle</param>
        /// <param name="d">direction</param>
        /// <param name="cpt">Compteur de case</param>
        /// <param name="essai">nb d'essai</param>
        /// <returns>true si la case est gagnante, false sinon</returns>
        private bool VerifySegment(Case @case, Direction d, int cpt, int essai)
        {
            Case voisin = grille.GetVoisin(@case, d);
            bool find = false; 
            if (essai >= 2) //Si essaie est supérieur ou égale à 2 cela voudra dire que l'on a fait tous le segment sans trouver de victoire
            {               //Dans ce cas on sort.
                find = false;
            }
            else if (cpt >= 3) // Si le compteur est supérieur ou égale à 3 cela voudra dire que l'on a bien trouvé une victoire
            {                   //Dans ce cas on sort.
                find = true;
            }
            else if (voisin == null || @case.Etat != voisin.Etat)//Cas si le voisin est null ou si les etats ne sont pas les mêmes
            {                                                   //alors on repart dans la direction opposé
                d +=4;
                essai++;
                find = VerifySegment(@case, d, 0, essai);
            }
            else if (@case.Etat == voisin.Etat)//Cas ou l'etat de la case et celui de la voisine sont les mêmes
            {
                cpt++;
                find = VerifySegment(voisin, d, cpt, essai);
            }

            return find;
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
    }
}
