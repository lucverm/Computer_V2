using System.Collections.Generic;

namespace Computers.Domains
{
    /// <summary>
    /// Représente un noeud non-terminal de l'arbre.
    /// Contrairement aux noeuds terminaux, sans enfants, les noeuds non-terminaux peuvent avoir des enfants.
    /// </summary>
    public sealed class NonTerminalNode : AbstractNode
    {
        private readonly List<ITreeNode> _children = new List<ITreeNode>();

        public override bool HasChildren => _children.Count > 0;

        public override void Add(ITreeNode child)
        {
            _children.Add(child);
        }

        public override IEnumerator<ITreeNode> GetEnumerator() => _children.GetEnumerator();
    }
}
