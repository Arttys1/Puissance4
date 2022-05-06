using System;
using System.Collections.Generic;

namespace Puissance4Upgrade
{
    /// <summary>
    /// Classe représentant la grille de jeu. Elle seule à accès au case
    /// </summary>
    [Serializable()]
    public class Grille
    {
        private Dictionary<Coordonnee, Case> cases;    //Représente les cases, on lui donne une coordonnée

        //et elle nous rend la case correspondante      
        /// <summary>
        /// Constructeur
        /// </summary>
        public Grille()
        {
            this.cases = new Dictionary<Coordonnee, Case>();
            CreationCase(); 
        }

        /// <summary>
        /// Methode permettant de créer les cases
        /// </summary>
        private void CreationCase()
        {
            Case @case;
            for(int x = 0; x < 7; x++)
            {
                for(int y = 0; y < 6; y++)
                {
                    Coordonnee coordonnee = new Coordonnee(x, y);
                    @case = new Case(coordonnee); //Creation des cases et ajout à la liste
                    cases.Add(coordonnee, @case);
                }
            }
        }

        /// <summary>
        /// Methode permettant de renvoyer la case selon une coordonnee
        /// </summary>
        /// <param name="coordonnee">Coordonnee de la case voulue</param>
        /// <returns>La case ayant la coordonnee donnée</returns>
        public Case GetCase(Coordonnee coordonnee)
        {
            // int i = coordonnee.X * 10 + coordonnee.Y;   //Le dictionary ne prend en clé que les entiers donc je créer un entier selon les coordonnées
            this.cases.TryGetValue(coordonnee, out Case @case);
            return @case;
        }

        /// <summary>
        /// Methode renvoyant la colonne désirée
        /// </summary>
        /// <param name="nbColonne">La position de la colonne dans l'axe X</param>
        /// <returns>Une liste de case avec toute la colonne</returns>
        public List<Case> GetColonne(int nbColonne)
        {
            List<Case> colonne = new List<Case>();

            for(int i = 0; i < 6; i++)
            {
                colonne.Add(GetCase(new Coordonnee(nbColonne, i)));
            }

            return colonne;
        }

        /// <summary>
        /// Methode renvoyant toutes les cases jouables dans un tour.
        /// Utiliser pour la prise de désicion de l'automate
        /// </summary>
        /// <returns>toutes les cases jouables dans un tour</returns>
        public List<Case> GetCaseDisponible()
        {
            List<Case> caseDisponible = new List<Case>();

            for (int i = 0; i < 7; i++)
            {
                Case c = GetCase(new Coordonnee(i, 0));
                while (c != null && c.Etat != Etat.VIDE) 
                {
                    c = GetVoisin(c, Direction.Haut);
                }
                if (c != null)
                {
                    caseDisponible.Add(c);
                }
            }

            return caseDisponible;
        }


        /// <summary>
        /// Methode permettant de renvoyer le voisin d'une case selon un direction
        /// </summary>
        /// <param name="case">La case souhaité</param>
        /// <param name="direction">La direction souhaité</param>
        /// <returns>le voisin d'une case selon un direction</returns>
        public Case GetVoisin(Case @case, Direction direction)
        {
            int x = @case.Coordonnee.X;
            int y = @case.Coordonnee.Y;
            Case voisin = null;

            switch (direction)
            {
                case Direction.Gauche:
                    voisin = GetCase(new Coordonnee(x - 1, y));
                    break;
                case Direction.Gauche_Haut:
                    voisin = GetCase(new Coordonnee(x - 1, y + 1));
                    break;
                case Direction.Haut:
                    voisin = GetCase(new Coordonnee(x, y + 1));
                    break;
                case Direction.Droite_Haut:
                    voisin = GetCase(new Coordonnee(x + 1, y + 1));
                    break;
                case Direction.Droite:
                    voisin = GetCase(new Coordonnee(x + 1, y));
                    break;
                case Direction.Droite_Bas:
                    voisin = GetCase(new Coordonnee(x + 1, y - 1));
                    break;
                case Direction.Bas:
                    voisin = GetCase(new Coordonnee(x, y - 1));
                    break;
                case Direction.Gauche_bas:
                    voisin = GetCase(new Coordonnee(x - 1, y - 1));
                    break;
            }

            return voisin;
        }

        public Dictionary<Coordonnee, Case> Cases { get => cases;
            set
            {
                if (cases == null)
                {
                    throw new Exception("Les cases de la grille ne peuvent être null");
                }
                cases = value;
            }
            }

        public Grille Clone()
        {
            Grille clone = new Grille();
            clone.Cases = new Dictionary<Coordonnee, Case>(this.cases);
            return clone;
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
            while (!end && (int)d < 4)
            {
                end = VerifySegment(@case, d, 0, 0);
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
            Case voisin = GetVoisin(@case, d);
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
                d += 4;
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
    }
}
