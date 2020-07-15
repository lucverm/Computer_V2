using System.Collections;
using System.Collections.Generic;

namespace Computers.Domains
{
    /// <summary>
    /// Factorise le code commun à TerminalNode et NonTerminalNode.
    ///
    /// Les autres projets ne devraient pas dépendre de cette classe, car elle
    /// pourraient être supprimée au besoin.
    /// </summary>
    public abstract class AbstractNode : ITreeNode
    {
        private readonly Dictionary<string, string> _datas = new Dictionary<string, string>();

        public string this[string key] => _datas[key];

        /// <summary>
        /// Ajoute des données scalaires au noeud terminal.
        /// La classe NodeKeys liste les clés les plus utilisées à cette fin.
        /// </summary>
        /// <param name="k">la clé d'accès à la données</param>
        /// <param name="v">la valeur associée à cette clé</param>
        public void Add(string k, string v) => _datas.Add(k, v);
        
        public abstract void Add(ITreeNode child);

        public abstract bool HasChildren { get; }

        public IEnumerable<string> Keys => _datas.Keys;

        public abstract IEnumerator<ITreeNode> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
