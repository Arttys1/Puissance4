namespace Puissance4Upgrade.metier.IA.aEtat
{
    /// <summary>
    /// Classe représentant les différentes états de l'automate
    /// </summary>
    abstract class AEtat
    {
        private readonly Automate automate;         //Automate de l'etat

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="automate">automate de l'etat</param>
        protected AEtat(Automate automate)
        {
            this.automate = automate;
        }

        /// <summary>
        /// Methode permettant la transition vers le futur etat
        /// </summary>
        /// <returns>Le futur etat de l'automate</returns>
        public abstract AEtat Transition();

        /// <summary>
        /// Methode permettant de retourner la case jouer par l'automate
        /// </summary>
        /// <returns>la case jouer par l'automate</returns>
        public abstract Case Action();

        protected Automate Automate => automate;        //Accesseur de l'automate
    }
}
