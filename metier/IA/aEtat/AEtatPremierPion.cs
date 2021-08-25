namespace Puissance4Upgrade.metier.IA.aEtat
{
    /// <summary>
    /// Etat permettant de poser le premier pion
    /// </summary>
    class AEtatPremierPion : AEtat
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="automate">automate de l'état</param>
        public AEtatPremierPion(Automate automate) : base(automate)
        {
        }

        public override Case Action()
        {
            Case c = Automate.Jeu.GetCase(new Coordonnee(3, 0));

            if(c.Etat != Etat.VIDE)
            {
                c = Automate.Jeu.GetCase(new Coordonnee(4, 0));
            }

            return c;
        }

        public override AEtat Transition()
        {
            return new AEtatCheckCase(Automate);
        }
    }
}
