namespace Puissance4Upgrade.metier.IA.aEtat
{
    /// <summary>
    /// Etat Initial de l'automate
    /// </summary>
    class AEtatInitial : AEtat
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="automate">automate de l'état</param>
        public AEtatInitial(Automate automate) : base(automate) { }

        public override Case Action()
        {
            return null;
        }

        public override AEtat Transition()
        {
            return new AEtatPremierPion(Automate);
        }
    }
}
