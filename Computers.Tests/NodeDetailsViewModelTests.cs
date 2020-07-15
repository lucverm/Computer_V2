using Computers.ViewModels.Impl;
using Computers.Domains;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Computers.Tests.ViewModels
{
    [TestFixture]
    public class NodeDetailsViewModelTests
    {
        ITreeNode _root;

        Mock<INotifyNodeChanged> _observableMock;
        DetailsViewModel _viewModel;

        [SetUp]
        public void BeforeEach()
        {
            _root = BuildRoot();

            //Pour vérifier l'inscription, il est nécessaire de configurer l'event du mock à l'aide de SetupAdd.
            _observableMock = new Mock<INotifyNodeChanged>();
            _observableMock.SetupAdd(m => m.NodeChanged += (sender, node) => { });

            _viewModel = new DetailsViewModel(_root, _observableMock.Object);
        }

        private static ITreeNode BuildRoot() => new NonTerminalNode()
        {
            { "Name", "Computers Components" },
            { "Description", "Types of computer components"},
            { "Image", "https://thumbs.dreamstime.com/b/computer-components-icon-set-processor-motherboard-ram-video-card-cooler-133676845.jpg" },
            new NonTerminalNode
            {
                { "Name", "Processors" },
                { "Description", "Amd and Intel processors" },
                { "Image", "https://cdn3.iconfinder.com/data/icons/electronic-3/500/cpu-512.png" },
                new TerminalNode
                {
                    { "Name", "Intel Core i3-9100F" },
                    { "Image", "http://www.hfinformatique.be/images/produits/19684.png" },
                    { "Fabricant", "Intel"},
                    { "Coeurs", "2"}
                },
                new TerminalNode
                {
                    {"Name", "Intel Core i7-8700k" },
                    {"Image", "http://www.hfinformatique.be/images/produits/17609.jpg" },
                    {"Fabricant", "Intel"},
                    {"Coeurs", "4"}
                },
                new TerminalNode
                {
                    {"Name", "Amd ryzen 3 3600x" },
                    {"Image", "http://www.hfinformatique.be/images/produits/17609.jpg" },
                    {"Fabricant", "AMD"},
                    {"Coeurs", "8"}
                },
            },
            new NonTerminalNode
            {
                {"Name", "Rams" },
                {"Description", "DDR 3,4 and 4 rams" },
                {"Image", "https://cdn1.iconfinder.com/data/icons/hardware-2/24/Ram-512.png" }
            }
        };

        [Test]
        public void SubscribesToNodeChangedEventOnInit()
        {
            _observableMock.VerifyAdd(observable => observable.NodeChanged += It.IsAny<Action<object, ITreeNode>>(), Times.Once);
        }

        [Test]
        public void PresentsCurrentNodesDataOnInit()
        {
            Assert.That(_viewModel.Name, Is.EqualTo(_root[NodeKeys.Name]));
            Assert.That(_viewModel.ImagePath, Is.EqualTo(_root[NodeKeys.ImageUrl]));
        }

        [Test]
        public void UpdatesOnNodeChanged()
        {
            GoToFirstGrandChild();

            Assert.That(_viewModel.Name, Is.EqualTo("Intel Core i3-9100F"));
            Assert.That(_viewModel.ImagePath, Is.EqualTo("http://www.hfinformatique.be/images/produits/19684.png"));
        }


        [Test]
        public void PresentsOptionalProperties()
        {
            GoToFirstGrandChild();

            Assert.That(_viewModel.Infos.Select(i => i.Key), Is.EquivalentTo(new List<string>() {"Fabricant","Coeurs"}));
        }

        private void GoToFirstGrandChild()
        {
            _observableMock.Raise(obs => obs.NodeChanged += null, _observableMock.Object, _root.First().First());
        }
    }
}
