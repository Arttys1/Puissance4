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
        private readonly Dictionary<Coordonnee, Case> cases;    //Représente les cases, on lui donne une coordonnée
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
    }
}
