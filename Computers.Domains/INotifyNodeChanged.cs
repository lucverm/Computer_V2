using System;

namespace Computers.Domains
{
    /// <summary>
    /// Définit le protocole de notification
    /// des changements de noeuds pendant un parcours.
    ///
    /// Par respect d'ISP, nous avons isolé cette interface du reste de ITreeNode, qui décrit plutôt une entité.
    /// </summary>
    public interface INotifyNodeChanged
    {
        event Action<object, ITreeNode> NodeChanged;
    }
}
