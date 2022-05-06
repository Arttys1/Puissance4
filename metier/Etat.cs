namespace Puissance4Upgrade
{
    /// <summary>
    /// Enumeration représentant l'etat d'une case
    /// </summary>
    public enum Etat
    {
        VIDE,
        ROUGE,
        JAUNE
    }

    public static class EtatInverse
    {
        /// <summary>
        /// Méthode renvoyant l'inverse d'un etat.
        /// ROUGE -> JAUNE.
        /// JAUNE -> ROUGE.
        /// VIDE -> VIDE.
        /// </summary>
        /// <param name="etat">L'etat à inversé</param>
        /// <returns>L'inverse de l'état</returns>
        public static Etat Inverse(this Etat etat)
        {
            if (etat == Etat.VIDE)
                return Etat.VIDE;

            if (etat == Etat.ROUGE)
                return Etat.JAUNE;

            return Etat.ROUGE;
        }
    }
}
