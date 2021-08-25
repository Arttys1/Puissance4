namespace Puissance4Upgrade.metier.IA.aEtat
{
    /// <summary>
    /// Etat recherchant une ligne de 1 Cases de la même couleur que l'adversaire
    /// </summary>
    class AEtat1CaseEnnemi : AEtat
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="automate">automate de l'état</param>
        public AEtat1CaseEnnemi(Automate automate) : base(automate)
        {
        }

        public override Case Action()
        {
            return Automate.HasNbCase(1, Automate.CouleurEnnemi);
        }

        public override AEtat Transition()
        {
            return new AEtatCheckCase(Automate);
        }
    }
}
