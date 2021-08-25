using Puissance4Upgrade.metier.IA.aEtat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Puissance4Upgrade.metier.IA
{
    /// <summary>
    /// Classe représentant l'automate du Puissance4
    /// </summary>
    class Automate
    {
        private readonly Jeu jeu;                           //Object Jeu permettant l'acces au case
        private AEtat etat;                                 //Représente l'etat de l'automate
        private const Etat couleur = Etat.JAUNE;            //Constante représentant la couleur de l'automate
        private const Etat couleurEnnemi = Etat.ROUGE;      //Constante représentant la couleur de l'adversaire de l'automate
        private List<Case> casesDisponibles;                //Listes de toutes les cases disponibles que peut jouer l'automate

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="jeu">Le jeu permettant l'acces aux cases</param>
        public Automate(Jeu jeu)
        {
            casesDisponibles = new List<Case>();
            this.jeu = jeu;
            etat = new AEtatInitial(this);
        }

        /// <summary>
        /// Retourne la case que joue le bot
        /// </summary>
        /// <returns>la case que joue le bot</returns>
        public Case GetCaseDuBot()
        {
            Case c;

            do
            {
                c = etat.Action();
                etat = etat.Transition();
            } while (c == null);

            return c;
        }

        /// <summary>
        /// Methode permettant de rechercher parmis les cases disponibles des lignes de cases d'une couleur donnée
        /// </summary>
        /// <param name="tailleSegment">taille de la ligne rechercher</param>
        /// <param name="etatRechercher">couleur de la ligne rechercher</param>
        /// <returns>Une case si il en a trouvé une selon les critères demandés ou null sinon</returns>
        public Case HasNbCase(int tailleSegment, Etat etatRechercher)
        {
            if (!casesDisponibles.Any())
            {
                casesDisponibles = jeu.GetCaseDisponible();
            }
            foreach (Case @case in casesDisponibles)
            {
                foreach(Direction d in Enum.GetValues(typeof(Direction)))
                {
                    bool find = VerifyNBCase(@case, d, 0, tailleSegment - 1, etatRechercher);
                    if (find)
                    {
                        return @case;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Methode permettant de rechercher autour d'une case donnée une ligne de case de couleur et de taille donnés.
        /// C'est une méthode récursive.
        /// </summary>
        /// <param name="case">La case de base</param>
        /// <param name="d">La direction souhaité</param>
        /// <param name="cpt">Le nombre de case déja observé</param>
        /// <param name="tailleSegment">taille de la ligne souhaité</param>
        /// <param name="etatRechercher">couleur de la ligne recherché</param>
        /// <returns>true si la case donné en parametre correspond aux critères, false sinon</returns>
        private bool VerifyNBCase(Case @case, Direction d, int cpt, int tailleSegment, Etat etatRechercher)
        {
            Case voisin = jeu.GetVoisin(@case, d);
            bool find = false;
            if (cpt >= tailleSegment) // Si le compteur est supérieur ou égale à 3 cela voudra dire que l'on a bien trouvé un segment valide
            {                   //Dans ce cas on sort.
                find = true;
            }
            else if (voisin == null || voisin.Etat != etatRechercher)//Cas si le voisin est null ou si les etats ne sont pas les mêmes
            {                                                   //Dans ce cas on sort
                find = false;
            }
            else if (voisin.Etat == etatRechercher)//Cas ou l'etat de la case et celui de la voisine sont les mêmes
            {
                cpt++;
                find = VerifyNBCase(voisin, d, cpt, tailleSegment, etatRechercher);
            }

            return find;
        }
    
        /// <summary>
        /// Methode permettant de vider la liste des cases disponibles
        /// </summary>
        public void ClearCaseDispo()
        {
            casesDisponibles.Clear();
        }

        public Jeu Jeu => jeu;                              //Accesseur du jeu
        public static Etat Couleur => couleur;              //Accesseur static de la couleur
        public static Etat CouleurEnnemi => couleurEnnemi;  //Accesseur static de la couleur

        internal AEtat AEtat { get => etat; set => etat = value; }
    }
}
