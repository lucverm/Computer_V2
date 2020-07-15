using System.Collections.Generic;

namespace Computers.Domains
{
    /// <summary>
    /// Représente un noeud dans l'arbre.
    /// Ce noeud associe des données scalaires (Description, Image, Nom, etc.) accessible par un indexeur.
    /// </summary>
    public interface ITreeNode : IEnumerable<ITreeNode>
    {
        /// <summary>
        /// Retourne la données associées à cette clé.
        /// Lève une KeyNotFoundException si la clé ne correspond à aucune valeur.
        /// Lève une ArgumentException si la clé est null.
        /// </summary>
        /// <param name="key">Un string défini</param>
        /// <returns>La valeur correspondante sous la forme de string</returns>
        string this[string key] { get; }

        /// <summary>
        /// Enumére toutes les clés des données associées à ce noeud.
        /// </summary>
        IEnumerable<string> Keys { get; }

        /// <summary>
        /// Retourne vrai si ce noeud a des noeuds enfants.
        /// </summary>
        bool HasChildren { get; } 

        /// <summary>
        /// Ajoute le noeud child aux enfants de ce noeud.
        /// Lève une Exception lorsque l'opération d'Ajout échoue
        /// </summary>
        /// <param name="child"></param>
        void Add(ITreeNode child);

    }

}
