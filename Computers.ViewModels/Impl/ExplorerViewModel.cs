using Computers.Domains;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Computers.ViewModels.Impl
{
    public class ExplorerViewModel : ObservableViewModel, IExplorerViewModel
    {
        private IRunTroughNodes _sequence;
        private ITreeNode _node;

        public ExplorerViewModel(IRunTroughNodes sequence, INotifyNodeChanged observable)
        {
            observable.NodeChanged += (o, node) => Update(node);
            
            _sequence = sequence;
            GoBackCommand = new BasicRelayCommand(_sequence.GoBack);
            Update(_sequence.Current);
        }

        public string Name => _node[NodeKeys.Name];

        public ICommand GoBackCommand { get; }

        private void Update(ITreeNode newNode)
        {
            _node = newNode;
            UpdateChildren();
            RaisePropertiesChanged(nameof(Name), nameof(Children));
        }

        private void UpdateChildren()
        {
            if (_node.HasChildren)
            {
                Children.Clear();
                foreach (var child in _node)
                {
                    Children.Add(NodeViewModel.Of(() => _sequence.GoTo(child), child));
                }
            }
        }

        public ObservableCollection<INodeViewModel> Children { get; } = new ObservableCollection<INodeViewModel>();
    }

    internal class NodeViewModel : INodeViewModel
    {
        internal static NodeViewModel Of(Action onAction, ITreeNode model) => new NodeViewModel
        {
            Model = model,
            Select = new BasicRelayCommand(onAction)
        };

        public string ImagePath => Model[NodeKeys.ImageUrl];

        public string Name => Model[NodeKeys.Name];

        public string Desc
        {
            get
            {
                try
                {
                    return Model[NodeKeys.Description];
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        private ITreeNode Model { get; set; }
        public ICommand Select { get; private set; }
    }
}
