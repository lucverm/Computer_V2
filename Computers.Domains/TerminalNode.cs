using System;
using System.Collections.Generic;

namespace Computers.Domains
{
    /// <summary>
    /// Contrairement aux noeuds non-terminaux, qui peuvent avoir un nombre quelconque d'enfants.
    /// Les noeuds terminaux n'ont pas d'enfants. La méthode Add lève dès lors une exception lorsqu'elle est appelée sur un noeud terminal.
    /// </summary>
    public sealed class TerminalNode : AbstractNode
    {
        private static readonly IReadOnlyCollection<ITreeNode> EmptyList = new List<ITreeNode>();

        public override bool HasChildren => false;

        public override void Add(ITreeNode child) =>
            throw new InvalidOperationException($"Appel de TerminalNode.Add sur le noeud {this[NodeKeys.Name]}.");

        public override IEnumerator<ITreeNode> GetEnumerator() => EmptyList.GetEnumerator();

    }
}
