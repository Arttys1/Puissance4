using System;

namespace Puissance4Upgrade
{
    /// <summary>
    /// Classe représentant une case du jeu
    /// </summary>
    [Serializable()]
    public class Case
    {
        private readonly Coordonnee coordonnee;         //Les coordonnees de la case        
        private Etat etat;                              //L'etat de la case(Jaune,Rouge ou vide). Vide par défaut.

        /// <summary>
        /// Constructeur prenant une coordonnée en parametre
        /// </summary>
        /// <param name="coordonnee">coordonnee de la case</param>
        public Case(Coordonnee coordonnee)
        {
            this.coordonnee = coordonnee;
            this.Etat = Etat.VIDE;
        }
       
        public Coordonnee Coordonnee { get => coordonnee; }              //Accesseur des coordonnees        
        public Etat Etat { get => etat; set => etat = value; }          //Accesseur et mutateur de l'etat de la case
    }
}
