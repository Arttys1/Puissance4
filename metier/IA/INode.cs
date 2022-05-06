using System;
using System.Collections.Generic;
using System.Text;

namespace Puissance4Upgrade.metier.IA
{
    public interface INode
    {
        abstract int GetDepth();

        abstract void Build(int maxDepth);

        abstract void Notation(Etat couleur);

        abstract int Count();

    }
}
