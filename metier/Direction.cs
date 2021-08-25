namespace Puissance4Upgrade
{
    /// <summary>
    /// Enumération représentant les directions possibles des voisins d'une case.
    /// Elle est utilisé pour verifier une victoire ou pour la prise de décision de l'automate
    /// </summary>
    public enum Direction
    {
        Gauche = 0,
        Gauche_Haut = 1,
        Haut = 2,
        Droite_Haut = 3,
        Droite = 4,
        Droite_Bas = 5,
        Bas = 6,
        Gauche_bas = 7,
    }
}
