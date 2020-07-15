using Computers.Domains;
using System.ComponentModel;
using System.Windows;

namespace Computers.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator() : this(new DependencyObject())
        {}

        public ViewModelLocator(DependencyObject dep = null)
        {
            var _dependenciesProp = dep ?? new DependencyObject();

            if (DesignerProperties.GetIsInDesignMode(_dependenciesProp))
            {
                Explorer = new Stubs.ExplorerViewModel();
                Details = new Stubs.DetailsViewModel();
            }
            else 
            {
                var tree = new StackedNodesRun(BuildRoot());
                Explorer = new Impl.ExplorerViewModel(tree, tree);
                Details = new Impl.DetailsViewModel(tree.Current, tree);
                Save = new Impl.SaveViewModel(tree.Current);
            }
        }

        private static ITreeNode BuildRoot() => new NonTerminalNode()
        {
            { NodeKeys.Name, "Computers Components" },
            { NodeKeys.Description, "Discover our computer components"},
            { NodeKeys.ImageUrl, "https://thumbs.dreamstime.com/b/computer-components-icon-set-processor-motherboard-ram-video-card-cooler-133676845.jpg" },
            new NonTerminalNode() 
            {
                    { NodeKeys.Name, "Processors" },
                    { NodeKeys.Description, "Processors provided by AMD and Intel" },
                    { NodeKeys.ImageUrl, "https://cdn3.iconfinder.com/data/icons/electronic-3/500/cpu-512.png" },
                    new TerminalNode()
                        {
                            { NodeKeys.Name, "Intel Core i3-9100F" },
                            { NodeKeys.ImageUrl, "http://www.hfinformatique.be/images/produits/19684.png" },
                            { "Fabricant", "Intel"},
                            { "Coeurs", "2"}
                        },
                        new TerminalNode()
                        {
                            {NodeKeys.Name, "Intel Core i7-8700k" },
                            {NodeKeys.ImageUrl, "http://www.hfinformatique.be/images/produits/17609.jpg" },
                            {"Fabricant", "Intel"},
                            {"Coeurs", "4"}
                        }
                },
                new NonTerminalNode()
                {
                    {NodeKeys.Name, "Rams" },
                    {NodeKeys.Description, "DDR 3 and 4 rams" },
                    {NodeKeys.ImageUrl, "https://cdn1.iconfinder.com/data/icons/hardware-2/24/Ram-512.png" }
                },
                new NonTerminalNode()
                {
                    {NodeKeys.Name, "GPU" },
                    {NodeKeys.Description, "Graphical Card provided by Nvidia and Amd" },
                    {NodeKeys.ImageUrl, "https://cdn4.iconfinder.com/data/icons/computer-hardware-and-devices-1/512/gpu-512.png" }
                }
        };

        public IExplorerViewModel Explorer { get; }
        public IDetailsViewModel Details { get; }
        public ISaveViewModel Save { get; }
    }
}
