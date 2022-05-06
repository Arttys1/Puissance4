using System;
using System.Collections.Generic;
using System.Text;

namespace Puissance4Upgrade.metier.IA
{
    public class Node : INode
    {
        private readonly List<Node> childs;
        private readonly Grille grille;
        private int value;
        private readonly int depth;
        private readonly Etat couleur;
        private Case mouvement;

        public Node(Grille grille, int depth, Etat couleur)
        {
            this.grille = grille;
            childs = new List<Node>();
            value = 0;
            this.depth = depth;
            this.couleur = couleur;
            mouvement = null;
        }

        public int Value { get => value; set => this.value = value; }

        public Case Mouvement { get => mouvement; set => mouvement = value; }

        public void Build(int maxDepth)
        {
            if (depth < maxDepth)
            {
                foreach (Case @case in grille.GetCaseDisponible())
                {
                    Grille clone = grille.Clone();
                    Case nouveau = new Case(@case.Coordonnee);
                    nouveau.Etat = couleur.Inverse();
                    clone.Cases[@case.Coordonnee] = nouveau;
                    Node child = new Node(clone, depth + 1, couleur.Inverse())
                    {
                        Value = Evaluate(clone, nouveau),
                        Mouvement = nouveau
                    };
                    childs.Add(child);
                    child.Build(maxDepth);
                }
            }
            else
            {
                Value = Evaluate(grille, Mouvement);
            }
        }

        public void Notation(Etat couleurAChercher)
        {
            if(childs.Count != 0)
            {
                if(couleurAChercher == Etat.ROUGE)
                {
                    int max = int.MinValue;
                    foreach (Node child in childs)
                    {
                        child.Notation(couleurAChercher);
                        if(child.Value > max)
                        {
                            max = child.Value;
                        }
                    }
                    Value = max;
                }
                else if(couleurAChercher == Etat.JAUNE)
                {
                    int min = int.MaxValue;
                    foreach (Node child in childs)
                    {
                        child.Notation(couleurAChercher);
                        if (child.Value < min)
                        {
                            min = child.Value;
                        }
                    }
                    Value = min;
                }

            }
        }


        private int Evaluate(Grille grille, Case @case)
        {
            if (grille.VerifyWin(@case))
            {
                if (@case.Etat == Etat.ROUGE)
                {
                    return int.MaxValue;
                }
                else if (@case.Etat == Etat.JAUNE)
                {
                    return int.MinValue;
                }
            }

            return 0;
        }

        public int Count()
        {
            int count = childs.Count;

            foreach (Node child in childs)
            {
                count += child.Count();
            }

            return count;
        }

        public int GetDepth()
        {
            return depth;
        }


    }
}
