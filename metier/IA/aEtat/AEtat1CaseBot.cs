using System;
using System.Collections.Generic;
using System.Text;

namespace Puissance4Upgrade.metier.IA.aEtat
{
    /// <summary>
    /// Etat recherchant une ligne de 1 Cases de la même couleur que le bot
    /// </summary>
    class AEtat1CaseBot : AEtat
    {
        private bool aPoser;            //booléen représentant si l'état à poser un pion ou non

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="automate">automate de l'état</param>
        public AEtat1CaseBot(Automate automate) : base(automate)
        {
            aPoser = false;
        }

        public override Case Action()
        {
            Case c = Automate.HasNbCase(1, Automate.Couleur);
            if (c != null)
            {
                aPoser = true;
            }
            return c;
        }

        public override AEtat Transition()
        {
            AEtat etat;

            if (aPoser)
            {
                etat = new AEtatCheckCase(Automate);
            }
            else
            {
                etat = new AEtat1CaseEnnemi(Automate);
            }

            return etat;
        }
    }
}
