namespace Computers.Domains
{
    /// <summary>
    /// Définit un parcours de noeuds.
    /// </summary>
    public interface IRunTroughNodes
    {
        /// <summary>
        /// Ajoute le noeud Node au parcours et modifie éventuellement le noeud courant.
        /// </summary>
        /// <param name="node"></param>
        void GoTo(ITreeNode node);
        /// <summary>
        /// Retourne au noeud précédent s'il en reste encore un.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Retourne le noeud courant. Au début du parcours, ce noeud correspond au point de départ du parcours.
        /// </summary>
        ITreeNode Current {get;}
    }
}
