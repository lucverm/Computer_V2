using Computers.Domains;
using Computers.ViewModels.Impl;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Computers.ViewModels;

namespace Computers.Tests.ViewModels
{
    [TestFixture]
    public class ExplorerViewModelTests
    {
        ITreeNode _root;
        Stack<ITreeNode> _runs;

        Mock<INotifyNodeChanged> _observableMock;
        ExplorerViewModel _viewModel;

        [SetUp]
        public void BeforeEach()
        {
            _root = BuildRoot();
            _runs = new Stack<ITreeNode>();
            _runs.Push(_root); 
            Mock<IRunTroughNodes> nodesRunMock = new Mock<IRunTroughNodes>();
            nodesRunMock.Setup(nodesRun => nodesRun.Current).Returns(() => _runs.Peek());
            nodesRunMock.Setup(nodesRun => nodesRun.GoTo(It.IsAny<ITreeNode>())).Callback(new Action<ITreeNode>((n) => _runs.Push(n)));
            nodesRunMock.Setup(nodesRun => nodesRun.GoBack()).Callback(() => _runs.Pop());
            //Pour vérifier l'inscription, il est nécessaire de configurer l'event du mock à l'aide de SetupAdd.
            _observableMock = new Mock<INotifyNodeChanged>();
            _observableMock.SetupAdd(m => m.NodeChanged += (sender, node) => { });

            _viewModel = new ExplorerViewModel(nodesRunMock.Object, _observableMock.Object);
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
            Assert.That(_viewModel.Name, Is.EqualTo("Computers Components"));
            Assert.That(_viewModel.Children, Has.Count.EqualTo(2));
        }

        [Test]
        public void UpdatesOnNodeChangedNotification()
        {
            //Simule la levée d'un évenement NodeChanged où sender == observableMock.Object et node == root.First()
            _observableMock.Raise(obs => obs.NodeChanged += null, _observableMock.Object, _root.First());

            Assert.That(_viewModel.Name, Is.EqualTo("Processors"));
            Assert.That(_viewModel.Children, Has.Count.EqualTo(3));
        }


        [Test]
        public void DoesNotUpdateChildrenContentOnNoChildren()
        {
            //Simule la levée d'un évenement NodeChanged où sender == observableMock.Object et node == root.First()
            _observableMock.Raise(obs => obs.NodeChanged += null, _observableMock.Object, _root.Last());

            Assert.That(_viewModel.Name, Is.EqualTo("Rams"));
            Assert.That(_viewModel.Children, Has.Count.EqualTo(2));
        }

        [Test]
        public void PresentsChildrenBasicContent()
        {
            foreach (INodeViewModel childVm in _viewModel.Children)
            {
                Assert.That(childVm.Name, Is.EqualTo("Processors").Or.EqualTo("Rams"));
                Assert.That(childVm.Desc, Is.EqualTo("Amd and Intel processors").Or.EqualTo("DDR 3,4 and 4 rams"));
                Assert.That(childVm.ImagePath, Is.EqualTo("https://cdn3.iconfinder.com/data/icons/electronic-3/500/cpu-512.png").Or.EqualTo("https://cdn1.iconfinder.com/data/icons/hardware-2/24/Ram-512.png"));
            }
        }

        [Test]
        public void ChildrenDescHandlesKeyNotFoundCase()
        {
            //Given : I display first child
            GoToFirstChild();

            //Expect : desc query on first child should not throw any exception
            Assert.That(() => _viewModel.Children.First().Desc, Throws.Nothing);
        }

        [Test]
        public void BrowsesTroughNodes()
        {
            //Given : I display processor nodes
            GoToFirstChild();
            
            //When : I Go back
            GoBack();
            //Then : I Should get root name
            Assert.That(_viewModel.Name, Is.EqualTo("Computers Components"));
        }

        private void GoBack()
        {
            _viewModel.GoBackCommand.Execute(null);
            _observableMock.Raise(obs => obs.NodeChanged += null, _observableMock.Object, _runs.Peek());
        }

        private void GoToFirstChild()
        {
            _viewModel.Children.First().Select.Execute(null);
            _observableMock.Raise(obs => obs.NodeChanged += null, _observableMock.Object, _runs.Peek());
        }
    }
}
