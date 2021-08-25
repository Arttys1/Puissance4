using System;

namespace Puissance4Upgrade.metier.IA.aEtat
{
    /// <summary>
    /// Etat central de l'automate, A chaque pion posé l'automate reviens à cet état
    /// </summary>
    class AEtatCheckCase : AEtat
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="automate">automate de l'état</param>
        public AEtatCheckCase(Automate automate) : base(automate)
        {
        }

        public override Case Action()
        {
            Automate.ClearCaseDispo();
            return null;
        }

        public override AEtat Transition()
        {
            return new AEtat3CasesBot(Automate);
        }
    }
}
