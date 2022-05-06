using System;
using System.Collections.Generic;
using System.Text;

namespace Puissance4Upgrade.metier.IA
{
    public class Tree : INode
    {
        private Node root;
        private Etat couleur;

        public Tree()
        {
            couleur = Etat.ROUGE;
            Grille grilleTest = new Grille();
            for (int i = 0; i < 3; i++)
            {
                Coordonnee coordonnee = new Coordonnee(i, 0);
                Case caseRouge = new Case(coordonnee);
                caseRouge.Etat = Etat.ROUGE;
                grilleTest.Cases[coordonnee] = caseRouge;
            }

            root = new Node(grilleTest, 0, Etat.ROUGE);
        }

        public Tree(Grille grille, Etat couleur)
        {
            this.root = new Node(grille, 0, couleur);
            this.couleur = couleur;
        }

        public int Value => root.Value;

        public void Build(int maxDepth)
        {
            root.Build(maxDepth);
        }

        public int Count()
        {
            return root.Count() + 1;
        }

        public int GetDepth()
        {
            throw new NotImplementedException();
        }

        public Case Mouvement()
        {
            throw new NotImplementedException();
        }

        public void Notation(Etat couleur)
        {
            root.Notation(couleur);
        }

    }
}
