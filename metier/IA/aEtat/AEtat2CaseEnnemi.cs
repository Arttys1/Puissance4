using System;
using System.Collections.Generic;
using System.Text;

namespace Puissance4Upgrade.metier.IA.aEtat
{
    /// <summary>
    /// Etat recherchant une ligne de 2 Cases de la même couleur que l'adversaire
    /// </summary>
    class AEtat2CaseEnnemi : AEtat
    {
        private bool aPoser;            //booléen représentant si l'état à poser un pion ou non

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="automate">automate de l'état</param>
        public AEtat2CaseEnnemi(Automate automate) : base(automate)
        {
            aPoser = false;
        }

        public override Case Action()
        {
            Case c = Automate.HasNbCase(2, Automate.CouleurEnnemi);
            if(c != null)
            {
                aPoser = true;
            }
            return c;
        }

        public override AEtat Transition()
        {
            AEtat etat;

            if(aPoser)
            {
                etat = new AEtatCheckCase(Automate);
            }
            else
            {
                etat = new AEtat2CaseBot(Automate);
            }

            return etat;
        }
    }
}
