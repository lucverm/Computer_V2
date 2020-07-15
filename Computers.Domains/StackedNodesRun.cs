using System;
using System.Collections.Generic;

namespace Computers.Domains
{
    /// <summary>
    /// Implémente le parcours de noeuds sous forme d'historique observable.
    /// L'historique est implémenté à l'aide d'une pile (Stack).
    /// Le retour en arrière est possible tant qu'il reste plus de deux noeuds dans le parcours.
    /// </summary>
    public class StackedNodesRun : IRunTroughNodes, INotifyNodeChanged
    {
        private readonly Stack<ITreeNode> _run = new Stack<ITreeNode>();

        public StackedNodesRun(ITreeNode root)
        {
            _run.Push(root);
        }

        public void GoTo(ITreeNode node) 
        {
            _run.Push(node);
            NodeChanged?.Invoke(this, Current);
        }

        public void GoBack() 
        {
            if(_run.Count > 1) 
            {
                _run.Pop();
                NodeChanged?.Invoke(this, Current);
            }    
        }

        public ITreeNode Current => this._run.Peek();

        public event Action<object, ITreeNode> NodeChanged;
    }
}
