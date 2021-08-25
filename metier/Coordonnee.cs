using System;

namespace Puissance4Upgrade
{
    /// <summary>
    /// Classe représentant les coordonnées d'un case.
    /// La case 0,0 est en bas à gauche
    /// </summary>
    [Serializable()]
    public class Coordonnee
    {
        private readonly int x;             //x représente les colonnes
        private readonly int y;             //y représente les lignes

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="x">La position dans l'axe X</param>
        /// <param name="y">La position dans l'axe Y</param>
        public Coordonnee(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get => x; }          //Accesseur de x
        public int Y { get => y; }          //Accesseur de y

        /// <summary>
        /// Permet de comparer deux Coordonnées.
        /// Elles sont égaux si elles ont les mêmes X et Y
        /// </summary>
        /// <param name="obj">objet a comparé</param>
        /// <returns>true si elles ont les mêmes X et Y, false sinon</returns>
        public override bool Equals(object obj)
        {
            return obj is Coordonnee coordonnee &&
                   x == coordonnee.x &&
                   y == coordonnee.y;
        }

        /// <summary>
        /// Retourne le hashCode de x et y.
        /// permet de comparer deux coordonnées
        /// </summary>
        /// <returns>le hashCode de x et y</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }
}
